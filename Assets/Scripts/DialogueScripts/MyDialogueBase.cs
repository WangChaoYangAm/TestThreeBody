using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDataBase;
public class MyDialogueBase 
{
    public string _ID;
    public string _groupId;
    public string _npcName;
    public string _dialogueText;
    public float _delayTime;
    public List<ConfigDialogue_Single> _dialogueOptionList;//对话选项列表

}
