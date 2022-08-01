using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Avatar : MonoBehaviour
{
    public float[] hairColor;
    public float[] skinColor;
    public float[] shirtColor;
    public int hairStyle;
    public string hairStyleText;
    public TextMeshProUGUI style;

    [SerializeField]
    public Material hairMat;
    public Color hairMatColor;

    [SerializeField]
    public Material skinMat;
    [SerializeField]
    public Material shirtMat;

    [SerializeField]
    public GameObject[] hairStyles = new GameObject[5];

    //[SerializeField]
    //private List<GameObject> UsesHair;

    [SerializeField]
    private List<GameObject> UsesSkin;

    [SerializeField]
    private List<GameObject> UsesShirt;

    [SerializeField]
    private bool isOnline = false;

    void Start()
    {
        CreateMaterialCopies();
        SetMaterialsToCopy();

        updateStyleText();

        if (!isOnline)
            LoadYou();
    }

    private void CreateMaterialCopies()
    {
        Material hairCopy = new Material(hairMat);
        Material skinCopy = new Material(skinMat);
        Material shirtCopy = new Material(shirtMat);

        hairMat = hairCopy;
        skinMat = skinCopy;
        shirtMat = shirtCopy;
    }

    private void SetMaterialsToCopy()
    {
        foreach (GameObject obj in hairStyles)
        {
            MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
                meshRenderer.material = hairMat;
        }

        foreach (GameObject obj in UsesSkin)
        {
            MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
                meshRenderer.material = skinMat;
            else {
                SkinnedMeshRenderer skinnedMeshRenderer = obj.GetComponent<SkinnedMeshRenderer>();
                if (skinnedMeshRenderer != null)
                {
                    skinnedMeshRenderer.material = skinMat;
                }
            }
        }

        foreach (GameObject obj in UsesShirt)
        {
            MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
                meshRenderer.material = shirtMat;
        }
    }

    public void SaveYou()
    {
        AvatarSaveSystem.SaveCustomSettings(this);
    }
    public void LoadYou()
    {
        CustomSettings data = AvatarSaveSystem.LoadCustomSettings();
        ApplyAvatarSettings(data);
    }
    public CustomSettings GetAvatarSettings() //To be used in multiplayer, but is easy to use elsewhere if a CustomSettings is needed
    {
        CustomSettings data = AvatarSaveSystem.LoadCustomSettings();
        return data;
    }
    public void ApplyAvatarSettings(CustomSettings data) //Primarily made for multiplayer, but is easy to use elsewhere if given a CustomSettings
    {
        hairColor = data.hairColor;
        skinColor = data.skinColor;
        shirtColor = data.shirtColor;
        hairStyle = data.hairStyle;
        hairStyleOnLoad();

        hairColorChange();
        skinColorChange();
        shirtColorChange();
    }


    public void hairColorChange()
    {
        Color tempColor = new Color(hairColor[0], hairColor[1], hairColor[2]);

        hairMat.SetColor("_Color", tempColor);
    }
    public void skinColorChange()
    {
        Color tempColor = new Color(skinColor[0], skinColor[1], skinColor[2]);
        skinMat.SetColor("_Color", tempColor);
    }
    public void shirtColorChange()
    {
        Color tempColor = new Color(shirtColor[0], shirtColor[1], shirtColor[2]);
        shirtMat.SetColor("_Color", tempColor);
    }


    public void hairStyleDown()
    {
        hairStyles[hairStyle].SetActive(false);
        hairStyle--;
        if (hairStyle < 0) { hairStyle = (hairStyles.Length - 1); }
        hairStyles[hairStyle].SetActive(true);
        updateStyleText();
    }
    public void hairStyleUp()
    {
        hairStyles[hairStyle].SetActive(false);
        hairStyle++;
        if(hairStyle >= hairStyles.Length) { hairStyle = 0; }
        hairStyles[hairStyle].SetActive(true);
        updateStyleText();
    }
    private void hairStyleOnLoad()
    {
        for(int i=0;i<hairStyles.Length;i++)
        {
            hairStyles[i].SetActive(false);
        }
        hairStyles[hairStyle].SetActive(true);
        updateStyleText();
    }
    private void updateStyleText()
    {
        if (style != null)
        {
            hairStyleText = "Style " + hairStyle;
            style.text = hairStyleText;
        }
    }

    public void SetHairColor (Color color)
    {
        hairColor[0] = color.r;
        hairColor[1] = color.g;
        hairColor[2] = color.b;

        hairMat.color = color;
    }

    public void SetSkinColor(Color color)
    {
        skinColor[0] = color.r;
        skinColor[1] = color.g;
        skinColor[2] = color.b;

        skinMat.color = color;
    }

    public void SetShirtColor(Color color)
    {
        shirtColor[0] = color.r;
        shirtColor[1] = color.g;
        shirtColor[2] = color.b;

        shirtMat.color = color;
    }
}