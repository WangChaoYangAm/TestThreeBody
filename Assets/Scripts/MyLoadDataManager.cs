using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using System;
using System.Reflection;
using System.IO;

public class MyLoadDataManager : MySingle<MyLoadDataManager>
{
    public string PATH_DIALOGUE = Application.streamingAssetsPath + "/NPC_Dialogue_Group/Excels/";
    public string PATH_QUESTS = Application.streamingAssetsPath + "/QuestsFile/Excels/";
    public string PATH_NPC_DIALOGUE_CONFIG = Application.streamingAssetsPath + "/NPC_Dialogue_AllConfigs/Excels/NPC_Dialogue.xlsx";
    private Dictionary<string, List<MyQuestBase>> _dicQuests = new Dictionary<string, List<MyQuestBase>>();
    private Dictionary<string, Npc_DialogueConfig> _dicNpcDialogueConfig = new Dictionary<string, Npc_DialogueConfig>();
    public List<MyDialogueBase> LoadDialogueList(string key)
    {
        List<MyDialogueBase> dialogueList = new List<MyDialogueBase>();
        //TODO 暂时使用直接读取excel的方式
        string path = PATH_DIALOGUE + key + ".xlsx";
        var fields = GetFieldInfos("MyDialogueBase");
        //foreach (var field in fields)
        //{
        //    Debug.Log(field.Name);
        //}
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

    public List<MyQuestBase> LoadQuestList(string key)
    {
        if (_dicQuests.ContainsKey(key)) return _dicQuests[key];
        List<MyQuestBase> listQuests = new List<MyQuestBase>();
        string path = PATH_QUESTS + key + ".xlsx";
        if (!System.IO.File.Exists(path))
        {
            Debug.LogError("解析不到目标文件," + path);
            return null;
        }

        var fields = GetFieldInfos("MyQuestBase");
        //foreach (var field in fields)
        //{
        //    Debug.Log(field.Name);
        //}
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
                    string tmpKey = dataset.Tables[0].Rows[i][j].ToString();
                    if (!string.IsNullOrEmpty(tmpKey))
                    {
                        if (!dicTitle.ContainsKey(tmpKey))
                            dicTitle.Add(tmpKey, t);
                        else
                            Debug.LogError(key + "excel中存在重复键：" + key);
                    }
                }
            }
            if (i < 2)
            {
                continue;
            }
            var table = dataset.Tables[0].Rows[i];
            //解析通用配置
            MyQuestBase @base = new MyQuestBase()
            {
                _questId = table[dicTitle["ID"]].ToString(),
                _questName = table[dicTitle["Name"]].ToString(),
                _questDes = table[dicTitle["Description"]].ToString(),
                _objectiveId = table[dicTitle["Objective_ID"]].ToString(),
                _nextQuestId = table[dicTitle["Next"]].ToString(),
                _questStatus = EQuestStatus.NotAccept,
            };
            //解析任务类型
            @base._objectiveType = (EObjectiveType)Enum.Parse(typeof(EObjectiveType), table[dicTitle["Objective"]].ToString());
            listQuests.Add(@base);
        }
        _dicQuests.Add(key, listQuests);
        return _dicQuests[key];
    }
    /// <summary>
    /// 加载Npc的对话配置表
    /// 数据并非一次加载完成，而是需要哪个加载哪个
    /// </summary>
    /// <param name="npcId"></param>
    /// <returns></returns>
    public Npc_DialogueConfig LoadNpcDialogueConfig(string npcId)
    {
        if (!_dicNpcDialogueConfig.ContainsKey(npcId))
        {
            Npc_DialogueConfig npc_DialogueConfig = new Npc_DialogueConfig();
            string path = PATH_NPC_DIALOGUE_CONFIG;
            //var fields = GetFieldInfos("MyQuestBase");
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
                        //Debug.Log(dataset.Tables[0].Rows[i][j].ToString());
                    }
                }
                if (i < 2)
                {
                    continue;
                }
                var table = dataset.Tables[0].Rows[i];
                if (table[dicTitle["NPCID"]].ToString() != npcId)
                {
                    continue;//如果该行的NPCID并非要加载的目标的id，那就跳过
                }
                //解析通用配置
                Npc_DialogueConfig @base = new Npc_DialogueConfig()
                {
                    _NpcId = table[dicTitle["NPCID"]].ToString(),
                    _DefaultDialogueId = table[dicTitle["Default"]].ToString()
                };
                ////解析任务类型
                //@base._objectiveType = (EObjectiveType)Enum.Parse(typeof(EObjectiveType), table[dicTitle["Objective"]].ToString());
                //listQuests.Add(@base);
                List<Npc_DialogueConfigSingle> list = new List<Npc_DialogueConfigSingle>();
                int prefix = 3;//此处的3对应excel中非对话配置的部分，即有序号上的顺序的部分,若表格发生变动此处也需要变化
                int cols = dataset.Tables[0].Columns.Count - prefix;//计算属于配置部分的列数
                cols /= 3;//所需要获取的列数
                //加载所有的配置
                for (int j = 0; j < cols; j++)
                {
                    string dialogue = table[dicTitle[string.Format("Dialogue_{0}", j + 1)]].ToString();
                    if (string.IsNullOrEmpty(dialogue)) break;//未获取到有效信息，则跳出获取，说明已经结束
                    Npc_DialogueConfigSingle config = new Npc_DialogueConfigSingle()
                    {
                        _idDialogue = dialogue,
                        _idQuests = table[dicTitle[string.Format("Mission_{0}", j + 1)]].ToString(),
                        _status = table[dicTitle[string.Format("State_{0}", j + 1)]].ToString(),
                    };
                    list.Add(config);
                }
                //配置赋值
                @base._npc_DialogueConfigs = list;
                _dicNpcDialogueConfig.Add(npcId, @base);
                break;
            }

        }
        return _dicNpcDialogueConfig.ContainsKey(npcId) ? _dicNpcDialogueConfig[npcId] : null;
    }
    public FieldInfo[] GetFieldInfos(string key)
    {
        Type type = Type.GetType(key);
        return type.Assembly.CreateInstance(type.FullName).GetType().GetFields();
    }

}
