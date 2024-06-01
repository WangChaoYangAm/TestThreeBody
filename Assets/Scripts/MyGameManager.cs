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
        //InitWindows();
        MyFacade.SendMsg(MyCommand.INIT_GAME_UI, null);

    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode) && _dicKeyEvent.ContainsKey(keyCode))
                {
                    _dicKeyEvent[keyCode]?.Invoke();
                }
            }
        }
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
