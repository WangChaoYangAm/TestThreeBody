using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIQuestsWindow : MyBaseWindow,Imsg
{
    [SerializeField]
    private Text _textDes;
    private void Awake()
    {
        Bind();
    }
    public void Bind()
    {
        MyFacade.Register(this._windowName, this);
        MyFacade.Register(MyCommand.INIT_DATA_QUEST, this);
    }

    public void RemoveBind()
    {

    }
    public void UpdateTextPanel(MyQuestBase myQuest)
    {
        _textDes.text = string.Format("任务名:{0}\n任务描述：{1}\n,任务进度：{2}/1", myQuest._questName, myQuest._questDes, myQuest.GetCurAmount);
    }
    public void NormalText()
    {
        _textDes.text = "暂无选中任务";
    }

    public void RecieveMsg(string command, object data)
    {
        switch (command)
        {
            case MyCommand.INIT_DATA_QUEST:
                ////预加载任务界面并填充数据
                //var UIQuests = UIManager.Instance.LoadWindow(EWindowUI.UIQuests) as UIQuestsWindow;
                string questStartId = (string)data;
                MyQuestManager.Instance.LoadData(questStartId);
                MyQuestBase myQuest = MyQuestManager.Instance.GetQuests();
                MyQuestManager.Instance.SetCurQuests(myQuest);
                break;
        }
    }


}
