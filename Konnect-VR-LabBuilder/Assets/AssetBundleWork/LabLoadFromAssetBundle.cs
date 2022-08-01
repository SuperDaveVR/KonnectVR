using KonnectVR.Interactions;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LabLoadFromAssetBundle : ALoadFromAssetBundle
{
    //Add components necessary to function in lab builder
    public override void AddComponents(GameObject loadedObj)
    {
        loadedObj.AddComponent<GrabRotateTransformToggle>();
        loadedObj.AddComponent<XRGrabInteractable>();
        //loadedObj.AddComponent<ParentTransformOnXRGrab>();

        //ParentTransform Settings
        //var parTransform = loadedObj.GetComponent<ParentTransformOnXRGrab>();
        //parTransform.ParentObject = this.gameObject;

        //Gravity
        var rb = loadedObj.GetComponent<Rigidbody>();
        //if (rb != null)
        //{
        //    rb.useGravity = false;
        //}

        //XRGrab Settings
        var xrGrab = loadedObj.GetComponent<XRGrabInteractable>();
        xrGrab.trackPosition = true;
        xrGrab.trackRotation = true;
        xrGrab.throwOnDetach = true;
        xrGrab.forceGravityOnDetach = rb.useGravity;

        //Parent Object Settings
        var saveData = this.gameObject.GetComponent<SaveablePlacedObject>();
        saveData.RotationScaleDataObject = loadedObj;
        saveData.GraphicDataObject = loadedObj;
        this.GraphicObject = loadedObj;

        var rotAndPosData = this.gameObject.GetComponent<GetSetRotationAndPosition>();
        rotAndPosData.RotationObject = loadedObj;
    }
}
