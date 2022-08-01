using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighting : MonoBehaviour
{
    [SerializeField]
    private Color outlineColor = Color.red;

    [SerializeField, Range(0f, 10f)]
    private float outlineWidth = 10f;

    [SerializeField]
    private PlacedObjectsHandler handler;

    private int index = 0;

    private void Awake()
    {
        handler = PlacedObjectsHandler.Instance;
    }

    public void AddHighlight(GameObject target)
    {
        Outline outline = target.AddComponent<Outline>();
        outline.OutlineColor = outlineColor;
        outline.OutlineWidth = outlineWidth;
    }

    public void RemoveHighlight(GameObject target)
    {
        Outline outline = target.GetComponent<Outline>();

        if (outline != null)
        {
            Destroy(outline);
        }
    }

    public void NextUsingHandler()
    {
        if (handler != null)
        {
            int pastIndex = index;

            if (!(index == (handler.listSize() - 1)))
                index++;

            SwitchOnChange(pastIndex, index);
        }
    }

    public void PreviousUsingHandler()
    {
        if (handler != null)
        {
            int pastIndex = index;

            if (index != 0)
                index--;

            SwitchOnChange(pastIndex, index);
        }
    }

    private void SwitchOnChange(int pastIndex, int newIndex)
    {
        if (pastIndex != newIndex)
        {
            RemoveHighlight(handler.GetObject(pastIndex));
            AddHighlight(handler.GetObject(newIndex));
        }
    }

    private void OnEnable()
    {
        if (handler != null)
            AddHighlight(handler.GetObject(index));
    }

    private void OnDisable()
    {
        if (handler != null)
            RemoveHighlight(handler.GetObject(index));
    }
}
