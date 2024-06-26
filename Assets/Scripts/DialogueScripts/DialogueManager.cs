using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using DG.Tweening;
using System;

public enum EDialogueFunc
{
    None,
    Continue,
    Jump,//跳转对话
    Skip,
    End,
    Receiveing,
    Complete,
    Submit,
    Abort
}
public class DialogueManager : MySingle<DialogueManager>
{
    private List<MyDialogueBase> _dialogueGroupList;//当前配置中的所有对话
    private List<MyDialogueBase> _dialogueList;//对话组groupID对应的所有id
    private MyDialogueBase _curDialogue;
    private int _curGroupIndex, _curDiaIndex;//指定序号
    private bool _allowNextDialogue;
    private string _objectiveName;
    private UIDialogueWindow _dialogueWindow;

    private Queue<Action> _queueEvents;
    private Action<float, Action> _curDialogueAction;//延迟时间和执行事件

    private void Update()
    {

    }

    /// <summary>
    /// 传入对话组文件名，播放对应的对话
    /// </summary>
    /// <param name="key"></param>
    public void InitDialogueGroup(string key)
    {
        _dialogueGroupList = MyLoadDataManager.Instance.LoadDialogueList(key);
        if (!_dialogueWindow)
        {
            MyFacade.SendMsg(MyCommand.OPEN_VIEW, new OpenViewConfig() { _eWindowUI = EWindowUI.UIDialogue });
            _dialogueWindow = UIManager.Instance.LoadWindow(EWindowUI.UIDialogue) as UIDialogueWindow;
            _dialogueWindow.Init(AutoNextDialogue, SwichDialogueList);

        }
        _allowNextDialogue = true;
        //_dialogueWindow.OnClickAction = ;
        if (_dialogueGroupList.Count > 0)
        {
            SwitchDialogue(1, 0);
        }
    }

    private void AutoNextDialogue()
    {
        if (!_allowNextDialogue) return;
        float delayTime = _curDialogue == null ? 0 : _curDialogue._delayTime;
        MyDelayEventManager.Instance.Register(true, MyDelayActionName.DIALOPGUE, new DelayAction
            (
            delayTime,
            () =>
            {
                DialogueTrigger();
                _curDiaIndex++;
                if (_dialogueList.Count > _curDiaIndex)//切换当前groupId的下一句
                {
                    _curDialogue = _dialogueList[_curDiaIndex];
                    _dialogueWindow.UpdateDialogue(_curDialogue);
                }
                else
                {
                    DialogueTrigger();
                    //对话结束
                    //_dialogueWindow.ShowWindow(false);
                    MyFacade.SendMsg(MyCommand.HIDE_VIEW, new MyResponseData() { _actionName = MyCommand.HIDE_VIEW, _data = EWindowUI.UIDialogue });
                    Debug.Log("对话结束");
                    _allowNextDialogue = false;
                    _dialogueWindow.ClearTextField();
                    _curDialogue = null;
                }
            }
            )

        );
    }
    private void SwitchDialogue(int groupIndex, int index)
    {
        //DOTween.KillAll();//杀掉已加载的对话sequence
        _curGroupIndex = groupIndex;
        _curDiaIndex = index;
        _dialogueList = _dialogueGroupList.FindAll(t => _curGroupIndex.ToString() == t._groupId);
        _curDialogue = _dialogueList[_curDiaIndex];
        _dialogueWindow.UpdateDialogue(_curDialogue);
    }

    private void SwichDialogueList(EDialogueFunc function, string para)
    {
        switch (function)
        {
            case EDialogueFunc.Jump:
                _dialogueList = _dialogueGroupList.FindAll(t => t._groupId == para);
                SwitchDialogue(int.Parse(para), 0);
                break;

        }
    }
    private void DialogueTrigger()
    {
        if (!string.IsNullOrEmpty(_curDialogue._missionId) && !string.IsNullOrEmpty(_curDialogue._missionState))
        {
            EDialogueFunc questStatus = EDialogueFunc.None;
            if (System.Enum.TryParse<EDialogueFunc>(_curDialogue._missionState, out questStatus))
            {
                switch (questStatus)
                {
                    //case "Dialogue":
                    //    MyQuestManager.Instance.UpdateQuestsObserver(EObjectiveType.Dialogue, _curDialogue._missionId, 1);
                    //    break;
                    case EDialogueFunc.Complete:
                        MyQuestManager.Instance.UpdateQuestStatus(EQuestStatus.Complete, _curDialogue._missionId);
                        break;
                }
            }
            else
            {
                Debug.LogError("传入的任务指定状态错误，无法转义为EDialogueFunc，请检查：" + _curDialogue._missionState);
            }
        }
    }
}
