using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLoadDataManager : MySingle<MyLoadDataManager>
{
    public List<MyDialogueBase> LoadDialogueList(string key)
    {
        List < MyDialogueBase > dialogueList = new List <MyDialogueBase >();
        return dialogueList;
    }
}
