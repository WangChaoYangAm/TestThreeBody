using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using System;
using System.Reflection;
using DG.Tweening;

public class MyLoadDataManager : MySingle<MyLoadDataManager>
{
    public string PATH_DIALOGUE = Application.streamingAssetsPath + "/NPC_Dialogue_Group/Excels/";
    //private Dictionary<string,string>
    public List<MyDialogueBase> LoadDialogueList(string key)
    {
        List<MyDialogueBase> dialogueList = new List<MyDialogueBase>();
        //TODO 暂时使用直接读取excel的方式
        string path = PATH_DIALOGUE + key + ".xlsx";
        var fields = GetFieldInfos("MyDialogueBase");
        foreach (var field in fields)
        {
            Debug.Log(field.Name);
        }
        DataSet dataset = ExcelRead.ReadExcel(path);
        Dictionary<string, int> dicTitle = new Dictionary<string, int>();
        for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
        {
            if (i == 0)
            {
                for (int j = 0; j < dataset.Tables[0].Columns.Count; j++)
                {
                    //记载excel表头及对应的列的序号
                    int t = j;
                    dicTitle.Add(dataset.Tables[0].Rows[i][j].ToString(), t);
                }
            }
            if (i < 2)
            {
                continue;
            }
            var table = dataset.Tables[0].Rows[i];
            //解析通用配置
            MyDialogueBase @base = new MyDialogueBase()
            {
                _ID = table[dicTitle["ID"]].ToString(),
                _groupId = table[dicTitle["Group_ID"]].ToString(),
                _npcName = table[dicTitle["NPC_Name"]].ToString(),
                _dialogueText = table[dicTitle["Dialogue"]].ToString(),
                _missionState = table[dicTitle["Mission_State"]].ToString(),
                _missionId = table[dicTitle["Mission_ID"]].ToString(),
            };
            //解析按钮选项
            float.TryParse(dataset.Tables[0].Rows[i][dicTitle["Time"]].ToString(), out @base._delayTime);
            List<ConfigDialogue_Single> list = new List<ConfigDialogue_Single>();
            for (int k = 1; k < 5; k++)
            {
                string btnTitle = string.Format("Button_{0}", k);
                if (!dicTitle.ContainsKey(btnTitle)) continue;
                string btnFunc = string.Format("Function_{0}", k);
                string btnPara = string.Format("Parameter_{0}", k);

                btnFunc = table[dicTitle[btnFunc]].ToString();
                list.Add(new ConfigDialogue_Single()
                {
                    _btnDes = table[dicTitle[btnTitle]].ToString(),
                    _function = string.IsNullOrEmpty(btnFunc) ? EDialogueFunc.None : (EDialogueFunc)Enum.Parse(typeof(EDialogueFunc), btnFunc),
                    _para = table[dicTitle[btnPara]].ToString()
                });
            }
            //添加选项后清除不合法的对话选项
            @base._dialogueOptionList = list;
            @base.ClearUnlegalOption();
            dialogueList.Add(@base);
        }
        return dialogueList;
    }
    public FieldInfo[] GetFieldInfos(string key)
    {
        Type type = Type.GetType(key);
        return type.Assembly.CreateInstance(type.FullName).GetType().GetFields();
    }

}
