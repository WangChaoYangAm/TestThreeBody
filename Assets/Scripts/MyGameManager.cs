using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class MyGameManager : MySingle<MyGameManager>
{
    void Start()
    {
        //DialogueManager.Instance.InitDialogueGroup("301102");
        InitWindows();

    }
    void InitWindows()
    {
        //预加载任务界面并填充数据
        var UIQuests = UIManager.Instance.LoadWindow(EWindowUI.UIQuests) as UIQuestsWindow;
        MyQuestManager.Instance.LoadData("1000");
        MyQuestBase myQuest = MyQuestManager.Instance.GetQuests();
        myQuest.Accept();
        MyQuestManager.Instance.SetCurQuests(myQuest);
        //预加载对话界面,暂定无对话，初次加载直接隐藏对话UI
        UIManager.Instance.LoadWindow(EWindowUI.UIDialogue).ShowWindow(false);
    }

    public void UpdateQuestsState(EObjectiveType objectiveType, string objectiveNmae, int objectiveAmount)
    {
        MyQuestManager.Instance.UpdateQuestsObserver(objectiveType, objectiveNmae, objectiveAmount);
    }
}
