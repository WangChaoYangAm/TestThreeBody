using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFSMOnExit : StateMachineBehaviour
{
    public string[] OnExitMsg;
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var signal in OnExitMsg)
        {
            animator.SendMessageUpwards(signal);
        }
    }
}
