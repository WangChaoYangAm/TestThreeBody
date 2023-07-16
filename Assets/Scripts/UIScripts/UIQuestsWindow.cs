using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIQuestsWindow : MyBaseWindow
{
    [SerializeField]
    private Text _textDes;

    public void UpdateTextPanel(MyQuestBase myQuest)
    {
        _textDes.text = string.Format("������:{0}\n����������{1}\n,������ȣ�{2}/1", myQuest._questName, myQuest._questDes, myQuest.GetCurAmount);
    }


}