using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyGameManager : MySingle<MyGameManager>
{
    /// <summary>
    /// 将按钮事件注册进来，方便统一管理，而不是每个脚本都有自己的，不好增删改
    /// </summary>
    private Dictionary<KeyCode, Action> _dicKeyEvent = new Dictionary<KeyCode, Action>();
    void Start()
    {
        //DialogueManager.Instance.InitDialogueGroup("301102");
        InitWindows();

    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode) && _dicKeyEvent.ContainsKey(keyCode))
                {
                    _dicKeyEvent[keyCode]();
                }
            }
        }
    }
    void InitWindows()
    {
        //预加载任务界面并填充数据
        var UIQuests = UIManager.Instance.LoadWindow(EWindowUI.UIQuests) as UIQuestsWindow;
        MyQuestManager.Instance.LoadData("1000");
        MyQuestBase myQuest = MyQuestManager.Instance.GetQuests();
        MyQuestManager.Instance.SetCurQuests(myQuest);
        //预加载对话界面,暂定无对话，初次加载直接隐藏对话UI
        UIManager.Instance.LoadWindow(EWindowUI.UIDialogue).ShowWindow(false);

    }

    public void UpdateQuestsState(EObjectiveType objectiveType, string objectiveNmae, int objectiveAmount)
    {
        MyQuestManager.Instance.UpdateQuestsObserver(objectiveType, objectiveNmae, objectiveAmount);
    }

    public void Regist(KeyCode keyCode, Action action)
    {
        if (!_dicKeyEvent.ContainsKey(keyCode))
        {
            _dicKeyEvent.Add(keyCode, null);
        }
        _dicKeyEvent[keyCode] += action;
    }
}
