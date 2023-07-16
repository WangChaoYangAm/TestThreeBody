using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AreaTrigger : MonoBehaviour
{
    Collider _collider;
    [SerializeField]
    private string _areaID;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.PLAYER)
        {
            MyGameManager.Instance.UpdateQuestsState(EObjectiveType.EnterArea, _areaID, 1);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == Tags.PLAYER)
        {
            MyGameManager.Instance.UpdateQuestsState(EObjectiveType.ExitArea, _areaID, 1);
        }
    }
}
