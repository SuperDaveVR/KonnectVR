using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightingManager : MonoBehaviour
{
    public static HighlightingManager Instance;

    [SerializeField]
    private Color outlineColor = Color.red;

    [SerializeField, Range(0f, 10f)]
    private float outlineWidth = 10f;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
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
}