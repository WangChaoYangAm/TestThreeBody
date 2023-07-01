using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using System;
using System.Reflection;

public class MyLoadDataManager : MySingle<MyLoadDataManager>
{
    public string PATH_DIALOGUE = Application.streamingAssetsPath + "/NPC_Dialogue_Group/";
    public List<MyDialogueBase> LoadDialogueList(string key)
    {
        List<MyDialogueBase> dialogueList = new List<MyDialogueBase>();
        //TODO 暂时使用直接读取excel的方式
        string path = PATH_DIALOGUE + key;
        var fields = GetFieldInfos("MyDialogueBase");
        foreach (var field in fields)
        {
            Debug.Log(field.Name);
        }
        //DataSet dataset = ExcelRead.ReadExcel(path);
        //for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
        //{
        //    for (int j = 0; j < dataset.Tables[0].Columns.Count; j++)
        //    {
        //        if(i == 0)
        //        {

        //        }
        //    }
        //}
        return dialogueList;
    }
    public FieldInfo[] GetFieldInfos(string key)
    {
        Type type = Type.GetType("MyDialogueBase");
        return type.Assembly.CreateInstance(type.FullName).GetType().GetFields();
    }
}
