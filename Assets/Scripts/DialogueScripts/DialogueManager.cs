using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDataBase;
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
    private int _curDiaIndex;
    private UIDialogueWindow _dialogueWindow;


    public void InitDialogueGroup(string key)
    {
        _dialogueGroupList = MyLoadDataManager.Instance.LoadDialogueList(key);
        _curDiaIndex = 0;
        _curDialogue = _dialogueGroupList[_curDiaIndex];
        _dialogueWindow = UIManager.Instance.LoadWindow(EWindowUI.UIDialogue.ToString()) as UIDialogueWindow;
        //_dialogueWindow.OnClickAction = ;
        _dialogueWindow.UpdateDialogue(_curDialogue);
    }

    private void SwichDialogueList(EDialogueFunc function,string para)
    {
        switch (function)
        {
            case EDialogueFunc.Jump:
                string groupId = _dialogueGroupList.Find(t => t._ID == para)._groupId;
                _dialogueList = _dialogueGroupList.FindAll(t => t._groupId == groupId);
                break;
            case EDialogueFunc.Continue:
                _curDiaIndex++;
                break;
        }
    }
}
