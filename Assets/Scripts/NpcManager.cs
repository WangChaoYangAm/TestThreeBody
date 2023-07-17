using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MySingle<NpcManager>
{
    public string LoadNpcDialogueId(string npcId)
    {
        var config = MyLoadDataManager.Instance.LoadNpcDialogueConfig(npcId);
        for (int i = 0; i < config._npc_DialogueConfigs.Count; i++)
        {
            //TODO
            var quest = MyQuestManager.Instance.GetTargetQuests(config._npc_DialogueConfigs[i]._idQuests);
            if (quest._questStatus.ToString() == config._npc_DialogueConfigs[i]._status)
            {
                return config._npc_DialogueConfigs[i]._idDialogue;
            }
        }
        return config._DefaultDialogueId;
    }
}
