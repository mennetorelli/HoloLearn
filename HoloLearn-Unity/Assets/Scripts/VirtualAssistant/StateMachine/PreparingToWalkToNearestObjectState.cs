using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PreparingToWalkToNearestObjectState : StateMachineBehaviour {

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Rigidbody[] remainingObjects = GameObject.FindGameObjectWithTag("ObjectsToBePlaced").GetComponentsInChildren<Rigidbody>();
        List<GameObject> targets = new List<GameObject>();
        foreach (Rigidbody target in remainingObjects)
        {
            if (target.gameObject.GetComponent<CustomHandDraggable>().IsDraggingEnabled)
            {
                targets.Add(target.gameObject);
            }
        }

        SortByDistance(targets);
        VirtualAssistantManager.Instance.targetObject = targets[0].transform;

        //Debug.Log("walking to next object " + VirtualAssistantManager.Instance.targetObject);
        VirtualAssistantManager.Instance.gameObject.GetComponent<Animator>().SetTrigger("Walk");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 relativePos = Camera.main.transform.position - VirtualAssistantManager.Instance.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        rotation.x = 0f;
        rotation.z = 0f;

        VirtualAssistantManager.Instance.transform.rotation = Quaternion.Lerp(VirtualAssistantManager.Instance.transform.rotation, rotation, Time.deltaTime * 2f);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}


    public void SortByDistance(List<GameObject> targets)
    {
        GameObject temp;
        for (int i = 0; i < targets.Count; i++)
        {
            for (int j = 0; j < targets.Count - 1; j++)
            {
                if (Vector3.Distance(targets.ElementAt(j).transform.position, VirtualAssistantManager.Instance.transform.position)
                    > Vector3.Distance(targets.ElementAt(j + 1).transform.position, VirtualAssistantManager.Instance.transform.position))
                {
                    temp = targets[j + 1];
                    targets[j + 1] = targets[j];
                    targets[j] = temp;
                }
            }
        }
    }

}
