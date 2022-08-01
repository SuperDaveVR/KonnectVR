using Unity.Netcode;

[System.Serializable]
public struct NetworkCustomSettings : INetworkSerializable
{
    public float rHairColor;
    public float gHairColor;
    public float bHairColor;

    public float rSkinColor;
    public float gSkinColor;
    public float bSkinColor;

    public float rShirtColor;
    public float gShirtColor;
    public float bShirtColor;

    public int hairStyle;

    public NetworkCustomSettings(CustomSettings customSettings)
    {
        rHairColor = customSettings.hairColor[0];
        gHairColor = customSettings.hairColor[1];
        bHairColor = customSettings.hairColor[2];

        rSkinColor = customSettings.skinColor[0];
        gSkinColor = customSettings.skinColor[1];
        bSkinColor = customSettings.skinColor[2];

        rShirtColor = customSettings.shirtColor[0];
        gShirtColor = customSettings.shirtColor[1];
        bShirtColor = customSettings.shirtColor[2];

        hairStyle = customSettings.hairStyle;
    }

    public void CopyCustomSettings(CustomSettings customSettings) {
        rHairColor = customSettings.hairColor[0];
        gHairColor = customSettings.hairColor[1];
        bHairColor = customSettings.hairColor[2];

        rSkinColor = customSettings.skinColor[0];
        gSkinColor = customSettings.skinColor[1];
        bSkinColor = customSettings.skinColor[2];

        rShirtColor = customSettings.shirtColor[0];
        gShirtColor = customSettings.shirtColor[1];
        bShirtColor = customSettings.shirtColor[2];

        hairStyle = customSettings.hairStyle;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref rHairColor);
        serializer.SerializeValue(ref gHairColor);
        serializer.SerializeValue(ref bHairColor);

        serializer.SerializeValue(ref rSkinColor);
        serializer.SerializeValue(ref gSkinColor);
        serializer.SerializeValue(ref bSkinColor);

        serializer.SerializeValue(ref rShirtColor);
        serializer.SerializeValue(ref gShirtColor);
        serializer.SerializeValue(ref bShirtColor);

        serializer.SerializeValue(ref hairStyle);
    }
}