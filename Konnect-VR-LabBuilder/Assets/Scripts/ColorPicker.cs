using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;
using UnityEngine.EventSystems;

[Serializable]
public class ColorEvent : UnityEvent<Color> { }

public class ColorPicker : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI DebugText;
    public ColorEvent OnColorPreview;
    public ColorEvent OnColorSelect;

    public float max = 10f; //Failsafe value initially. Shouldn't ever change but :person_shrugging:

    RectTransform Rect;
    Texture2D ColorTexture;

    private bool tracking = false;

    // Start is called before the first frame update
    void Start()
    {
        Rect = GetComponent<RectTransform>();

        ColorTexture = GetComponent<Image>().mainTexture as Texture2D;
    }

    public void OnPointerClick(PointerEventData data)
    {
        Color color = ColorChecker(data.position);

        OnColorSelect?.Invoke(color);
    }

    public void OnPointerEnter(PointerEventData data)
    {
        Camera camera = data.enterEventCamera;
        Debug.Log(camera.name);
        Color color = ColorChecker(data.position);

        Debug.Log("Raycast Screen Pos: " + data.pointerCurrentRaycast.screenPosition);
        Debug.Log("Raycast World Pos: " + data.pointerCurrentRaycast.worldPosition);
        Debug.Log("Event Pos: " + data.position);

        tracking = true;
        StartCoroutine(PreviewCo(data));
    }

    public void OnPointerExit(PointerEventData data)
    {
        tracking = false;
    }

    private Color ColorChecker(Vector2 eventPos)
    {
        Vector2 delta;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(Rect, eventPos, Camera.main, out delta);

        string debug = "Position=" + eventPos;
        debug += "<br>delta=" + delta;

        float width = Rect.rect.width;
        float height = Rect.rect.height;
        delta += new Vector2(width * .5f, height * .5f);
        debug += "<br>offset delta=" + delta;

        if (width <= height) { max = width; } //If we don't check the bounds of the color picker, we'll get some errors
        else { max = height; }                //We also can't guarantee they'll stay the same size, so we'll check on Update()


        float x = Mathf.Clamp(delta.x / width, 0f, max);
        float y = Mathf.Clamp(delta.y / height, 0f, max);
        debug += "<br>x=" + x + "y=" + y;

        int texX = Mathf.RoundToInt(x * ColorTexture.width);
        int texY = Mathf.RoundToInt(y * ColorTexture.height);
        debug += "<br>texX=" + texX + " texY=" + texY;

        Color color = ColorTexture.GetPixel(texX, texY);

        DebugText.color = color;

        DebugText.text = debug;

        //Debug.Log(debug + " Color: " + color);

        return color;

    }

    private IEnumerator PreviewCo(PointerEventData data)
    {
        while (tracking)
        {
            Debug.Log(data.pointerId);
            Color color = ColorChecker(data.position);
            OnColorPreview?.Invoke(color);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
