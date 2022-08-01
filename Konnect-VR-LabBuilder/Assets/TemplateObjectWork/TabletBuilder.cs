using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabletBuilder : MonoBehaviour
{
    public TMP_InputField positionX;
    public TMP_InputField positionY;
    public TMP_InputField positionZ;
    public TMP_InputField rotationX;
    public TMP_InputField rotationY;
    public TMP_InputField rotationZ;
    public TMP_InputField scale;

    public TMP_Text name;
    public GameObject builderPanel;

    public PlacedObjectsHandler handler;

    private int index = 0;

    private Vector3 position;
    private Vector3 eulerAngle;
    private Quaternion rotation;
    private Vector3 scaleSize;

    private string undoPosX, undoPosY, undoPosZ, undoRotX, undoRotY, undoRotZ, redoPosX, redoPosY, redoPosZ, redoRotX, redoRotY, redoRotZ, undoScale, redoScale;

    private bool isRedo = false;
    private bool isUndo = false;
    private bool canRedo = false;
    private bool canUndo = false;

    // Start is called before the first frame update
    void Awake()
    {
        if(handler == null)
        {
            handler = GameObject.Find("PlacedObjects").GetComponent<PlacedObjectsHandler>();
        }
        getValues();
    }

    // Update is called once per frame
    void Update()
    {
        objectName();
    }

    public void setValues()
    {


        position.x = float.Parse(positionX.GetComponent<TMP_InputField>().text);
        position.y = float.Parse(positionY.GetComponent<TMP_InputField>().text);
        position.z = float.Parse(positionZ.GetComponent<TMP_InputField>().text);

        eulerAngle.x = float.Parse(rotationX.GetComponent<TMP_InputField>().text);
        eulerAngle.y = float.Parse(rotationY.GetComponent<TMP_InputField>().text);
        eulerAngle.z = float.Parse(rotationZ.GetComponent<TMP_InputField>().text);

        scaleSize.x = float.Parse(scale.GetComponent<TMP_InputField>().text);
        scaleSize.y = float.Parse(scale.GetComponent<TMP_InputField>().text);
        scaleSize.z = float.Parse(scale.GetComponent<TMP_InputField>().text);


        rotation.eulerAngles = eulerAngle;

        handler.SetPosition(index, position);
        handler.SetRotation(index, rotation);
        handler.SetScale(index, scaleSize);

        if (isRedo)
        {
            saveRedo();
            canRedo = false;
            canUndo = true;
        }
        else if (isUndo) { 
        
            canUndo = false;
            saveValues();
        }
        else
        {
            
            canUndo = true;
            canRedo = false;
        }

        
        isRedo = false;
        isUndo = false;
    }

    public void getValues()
    {
        canUndo = true;
        position = handler.GetPosition(index);
        rotation = handler.GetRotation(index);
        scaleSize = handler.GetScale(index);
        eulerAngle = rotation.eulerAngles;
        saveValues();

        positionX.GetComponent<TMP_InputField>().text = position.x.ToString();
        positionY.GetComponent<TMP_InputField>().text = position.y.ToString();
        positionZ.GetComponent<TMP_InputField>().text = position.z.ToString();

        rotationX.GetComponent<TMP_InputField>().text = eulerAngle.x.ToString();
        rotationY.GetComponent<TMP_InputField>().text = eulerAngle.y.ToString();
        rotationZ.GetComponent<TMP_InputField>().text = eulerAngle.z.ToString();

        scale.GetComponent<TMP_InputField>().text = scaleSize.x.ToString();

    }

    public void undoValues()
    {
        if (canUndo)
        {


            isUndo = true;
            saveRedo();
            positionX.GetComponent<TMP_InputField>().text = undoPosX;
            positionY.GetComponent<TMP_InputField>().text = undoPosY;
            positionZ.GetComponent<TMP_InputField>().text = undoPosZ;

            rotationX.GetComponent<TMP_InputField>().text = undoRotX;
            rotationY.GetComponent<TMP_InputField>().text = undoRotY;
            rotationZ.GetComponent<TMP_InputField>().text = undoRotZ;

            scale.GetComponent<TMP_InputField>().text = undoScale;
            canRedo = true;
            setValues();
        }
    }

    public void redoValues()
    {
        if (canRedo)
        {


            isRedo = true;
            positionX.GetComponent<TMP_InputField>().text = redoPosX;
            positionY.GetComponent<TMP_InputField>().text = redoPosY;
            positionZ.GetComponent<TMP_InputField>().text = redoPosZ;

            rotationX.GetComponent<TMP_InputField>().text = redoRotX;
            rotationY.GetComponent<TMP_InputField>().text = redoRotY;
            rotationZ.GetComponent<TMP_InputField>().text = redoRotZ;

            scale.GetComponent<TMP_InputField>().text = redoScale;

            setValues();
        }
    }

    void saveValues()
    {
        undoPosX = positionX.GetComponent<TMP_InputField>().text;
        undoPosY = positionY.GetComponent<TMP_InputField>().text;
        undoPosZ = positionZ.GetComponent<TMP_InputField>().text;
        undoRotX = rotationX.GetComponent<TMP_InputField>().text;
        undoRotY = rotationY.GetComponent<TMP_InputField>().text;
        undoRotZ = rotationZ.GetComponent<TMP_InputField>().text;
        undoScale = scale.GetComponent<TMP_InputField>().text;
    }

    void saveRedo()
    {
        redoPosX = positionX.GetComponent<TMP_InputField>().text;
        redoPosY = positionY.GetComponent<TMP_InputField>().text;
        redoPosZ = positionZ.GetComponent<TMP_InputField>().text;
        redoRotX = rotationX.GetComponent<TMP_InputField>().text;
        redoRotY = rotationY.GetComponent<TMP_InputField>().text;
        redoRotZ = rotationZ.GetComponent<TMP_InputField>().text;
        redoScale = scale.GetComponent<TMP_InputField>().text;
    }

    void objectName()
    {

        if(!handler.CheckObjectExists(index))
        {
            handler.BuildList();
            previousObject();
            

            if(handler.listSize() == 0)
            {
                name.text = "No Objects";
                builderPanel.SetActive(false);
            }
            else
            {
                getValues();
            }
            
        }
        else
        {
            name.text = handler.GetName(index);
            builderPanel.SetActive(true);
        }
    }

    public void nextObject()
    {
        if(!(index == (handler.listSize() - 1)))
        index++;
    }

    public void previousObject()
    {
        if(index != 0)
        index--;
    }
}
