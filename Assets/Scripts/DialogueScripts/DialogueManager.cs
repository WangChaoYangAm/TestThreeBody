using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using DG.Tweening;
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
    private UIDialogueWindow _dialogueWindow;


    public void InitDialogueGroup(string key)
    {
        _dialogueGroupList = MyLoadDataManager.Instance.LoadDialogueList(key);

        //var go = MyLoad.LoadGamepbject("UIPrefabs/" + EWindowUI.UIDialogue.ToString());
        _dialogueWindow = UIManager.Instance.LoadWindow(EWindowUI.UIDialogue.ToString()) as UIDialogueWindow;
        _dialogueWindow.Init(AutoNextDialogue, SwichDialogueList);
        _allowNextDialogue = true;
        //_dialogueWindow.OnClickAction = ;
        if (_dialogueGroupList.Count > 0)
        {
            //_curGroupIndex = 1;
            //_curDiaIndex = 0;
            //_dialogueList = _dialogueGroupList.FindAll(t => _curGroupIndex.ToString() == t._groupId);
            //_curDialogue = _dialogueList[_curDiaIndex];
            //_dialogueWindow.UpdateDialogue(_curDialogue);
            SwitchDialogue(1, 0);
        }
    }

    private void AutoNextDialogue()
    {
        if (!_allowNextDialogue) return;
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(() =>
        {
            _curDiaIndex++;
            if (_dialogueList.Count > _curDiaIndex)//切换当前groupId的下一句
            {
                _curDialogue = _dialogueList[_curDiaIndex];
                _dialogueWindow.UpdateDialogue(_curDialogue);
            }
            else
            {
                //对话结束
                Debug.Log("对话结束");
                _allowNextDialogue = false;
                _dialogueWindow.ClearTextField();
                _curDialogue = null;
            }

        }).SetDelay(_curDialogue == null ? 0 : _curDialogue._delayTime);
    }
    private void SwitchDialogue(int groupIndex,int index)
    {
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
    private void ReInit()
    {
        _curGroupIndex = 0;
    }
}
