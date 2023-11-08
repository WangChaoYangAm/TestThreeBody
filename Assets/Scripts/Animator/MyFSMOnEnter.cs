using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFSMOnEnter : StateMachineBehaviour
{
    public string[] OnEnterMsg;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var signal in OnEnterMsg)
        {
            animator.gameObject.SendMessageUpwards(signal);
        }
    }
}
