using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class NpcTrigger : MonoBehaviour
{
    Collider _collider;
    Collider _otherCollider;
    [SerializeField]
    private string _npcID;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger=true;
        MyGameManager.Instance.Regist(KeyCode.F, Dialogue);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.PLAYER)
        {
            _otherCollider = other;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == Tags.PLAYER)
        {
            _otherCollider = null;
        }
    }
    private void Dialogue()
    {
        if(_otherCollider != null)
        {
            //MyGameManager.Instance.UpdateQuestsState(EObjectiveType.Dialogue, _npcID, 1);
            var dialogueConfig = NpcManager.Instance.LoadNpcDialogueId(_npcID);
            if(!string.IsNullOrEmpty(dialogueConfig) )
            {
                DialogueManager.Instance.InitDialogueGroup(dialogueConfig);
            }
        }
    }
}
