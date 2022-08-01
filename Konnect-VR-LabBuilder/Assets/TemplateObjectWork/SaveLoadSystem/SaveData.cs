using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public List<PlacedObjectSaveData> placedObjectSaveDatas;
    public List<QuizSaveData> quizObjSaveData;
}
