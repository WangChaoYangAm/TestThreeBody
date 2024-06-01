using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 对话类
[System.Serializable]
public class MyDialogueBase
{
    public string _ID;//其实不需要
    public string _groupId;
    public string _npcName;
    public string _dialogueText;
    public string _missionState;
    public string _missionId;
    public float _delayTime;
    public List<ConfigDialogue_Single> _dialogueOptionList;//对话选项列表

    /// <summary>
    /// 清除无效的对话选项
    /// </summary>
    public void ClearUnlegalOption()
    {
        for (int i = 0; i < _dialogueOptionList.Count; i++)
        {
            _dialogueOptionList.RemoveAll(t => string.IsNullOrEmpty(t._btnDes) && string.IsNullOrEmpty(t._para) && t._function == EDialogueFunc.None);
        }
    }

}

/// <summary>
/// 单条对话
/// </summary>
[System.Serializable]
public class ConfigDialogue_Single
{
    public string _btnDes;
    public EDialogueFunc _function;
    public string _para;

    public bool HaveFunction
    {
        get { return _function != EDialogueFunc.None; }
    }
}
#endregion

#region NPC类
public struct Npc_DialogueConfigSingle
{
    public string _idDialogue;
    public string _idQuests;
    public string _status;
}
public class Npc_DialogueConfig
{
    public string _NpcId;
    public string _DefaultDialogueId;
    public List<Npc_DialogueConfigSingle> _npc_DialogueConfigs;
}
#endregion

public enum EModule
{
    UIManager,
    QuestsManager,
    DialogueManager
}
public enum EWindowUI
{
    UIDialogue,
    UIQuests,
    UIPackage,
    UIPlayerPackage,
}

public class Tags
{
    public const string PLAYER = "Player";
    public const string MYSINGLE_ROOT = "MySingleRoot";
}
public class Layers
{
    public const string ITEM = "Item";
}
public class OpenViewConfig
{
    public EWindowUI _eWindowUI;
    public object[] _datas; 
}
public class MyCommand
{
    /// <summary>
    /// 初始化游戏的UI窗口 设置应该打开的窗口
    /// </summary>
    public const string INIT_GAME_UI = "INIT_GAME_UI";
    /// <summary>
    /// 通知UI进行数据初始化 暂时是不需要传入数据，自行初始化的部分
    /// </summary>
    public const string INIT_GAME_UI_DATA = "INIT_GAME_UI_DATA";
    /// <summary>
    /// 打开窗口
    /// </summary>
    public const string OPEN_VIEW = "OPEN_VIEW";
    /// <summary>
    /// 关闭窗口
    /// </summary>
    public const string HIDE_VIEW = "HIDE_VIEW";
    /// <summary>
    /// 初始化任务面板UI的数据
    /// </summary>
    public const string INIT_DATA_QUEST = "INIT_DATA_QUEST";
    /// <summary>
    /// 添加道具
    /// </summary>
    public const string ADD_ITEM = "ADD_ITEM";
}

