using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using KonnectVR.Interactions;

public class BuilderLoadFromAssetBundle : ALoadFromAssetBundle
{
    [SerializeField] GameObject rotateObject;

    //Add components necessary to function in lab builder
    public override void AddComponents(GameObject loadedObj)
    {
        loadedObj.AddComponent<GrabRotateTransformToggle>();
        loadedObj.AddComponent<XRGrabInteractable>();
        loadedObj.AddComponent<ParentTransformOnXRGrab>();
        loadedObj.AddComponent<RotateOnXRDrag>();
        loadedObj.GetComponent<RotateOnXRDrag>().enabled = false;

        //ParentTransform Settings
        var parTransform = loadedObj.GetComponent<ParentTransformOnXRGrab>();
        parTransform.ParentObject = this.gameObject;

        //Rotate on Drag Settings
        var rotDrag = loadedObj.GetComponent<RotateOnXRDrag>();
        rotDrag.GraphicObject = loadedObj;

        //XRGrab Settings
        var xrGrab = loadedObj.GetComponent<XRGrabInteractable>();
        xrGrab.trackPosition = false;
        xrGrab.trackRotation = false;
        xrGrab.throwOnDetach = false;
        xrGrab.forceGravityOnDetach = false;

        //Gravity
        var rb = loadedObj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
        }

        //Parent Object Settings
        var saveData = this.gameObject.GetComponent<SaveablePlacedObject>();
        saveData.RotationScaleDataObject = loadedObj;
        saveData.GraphicDataObject = loadedObj;
        this.GraphicObject = loadedObj;

        var rotAndPosData = this.gameObject.GetComponent<GetSetRotationAndPosition>();
        rotAndPosData.RotationObject = loadedObj;

        //Fix Rotation Settings
        foreach (RotateOnXRDrag rotate in rotateObject.GetComponentsInChildren<RotateOnXRDrag>())
        {
            rotate.GraphicObject = GraphicObject;
        }
    }
}
