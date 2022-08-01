using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Netcode;

[System.Serializable]
public class CustomSettings
{
    public float[] hairColor;
    public float[] skinColor;
    public float[] shirtColor;
    public int hairStyle;

    public CustomSettings(Avatar avatar)
    {
        hairColor = new float[3];
        hairColor[0] = avatar.hairMat.color.r;
        hairColor[1] = avatar.hairMat.color.g;
        hairColor[2] = avatar.hairMat.color.b;
        skinColor = new float[3];
        skinColor[0] = avatar.skinMat.color.r;
        skinColor[1] = avatar.skinMat.color.g;
        skinColor[2] = avatar.skinMat.color.b;
        shirtColor = new float[3];
        shirtColor[0] = avatar.shirtMat.color.r;
        shirtColor[1] = avatar.shirtMat.color.g;
        shirtColor[2] = avatar.shirtMat.color.b;

        hairStyle = avatar.hairStyle;
    }

    public CustomSettings(NetworkCustomSettings networkCustomSettings)
    {
        hairColor = new float[3];
        hairColor[0] = networkCustomSettings.rHairColor;
        hairColor[1] = networkCustomSettings.gHairColor;
        hairColor[2] = networkCustomSettings.bHairColor;
        skinColor = new float[3];
        skinColor[0] = networkCustomSettings.rSkinColor;
        skinColor[1] = networkCustomSettings.gSkinColor;
        skinColor[2] = networkCustomSettings.bSkinColor;
        shirtColor = new float[3];
        shirtColor[0] = networkCustomSettings.rShirtColor;
        shirtColor[1] = networkCustomSettings.gShirtColor;
        shirtColor[2] = networkCustomSettings.bShirtColor;

        hairStyle = networkCustomSettings.hairStyle;
    }
}
