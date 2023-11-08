using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyFSMOnExit : StateMachineBehaviour
{
    public string[] OnExitMsg;
    //private Action OnExitAction;

    //public void AddEvent(Action action)
    //{
    //    OnExitAction += action;
    //}

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //OnExitAction?.Invoke();
        foreach (var signal in OnExitMsg)
        {
            animator.gameObject.SendMessageUpwards(signal);
        }
    }
}
