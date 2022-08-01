using KonnectVR.Interactions;
using KonnectVR.ModuleScripts;
using UnityEngine;

public class ScriptWithReferences : MonoBehaviour
{
    [SerializeField]
    private LogMessageOnStart serializedMessageLogger;

    public Interactable publicInteractable;

    [SerializeField, HideInInspector]
    private MeshRenderer meshRenderer;

    private void OnValidate()
    {
        if (!meshRenderer)
            meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Awake()
    {
        serializedMessageLogger.message = "Haha world, I changed the message being logged!";
    }

    private void Start()
    {
        meshRenderer.enabled = false;
    }
}
