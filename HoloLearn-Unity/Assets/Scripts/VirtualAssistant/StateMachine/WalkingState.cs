using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : StateMachineBehaviour {

    private Vector3 startingPosition;
    public Vector3 targetPosition;
    private float lerpPercentage;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        startingPosition = VirtualAssistantManager.Instance.transform.position;
        targetPosition = VirtualAssistantManager.Instance.targetObject.GetComponent<Rigidbody>().ClosestPointOnBounds(VirtualAssistantManager.Instance.transform.position);
        lerpPercentage = 0f;

        VirtualAssistantManager.Instance.isBusy = true;
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.DrawLine(VirtualAssistantManager.Instance.transform.position, targetPosition, Color.blue, 5f);

        Vector3 relativePos = VirtualAssistantManager.Instance.targetObject.position - VirtualAssistantManager.Instance.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        rotation.x = 0f;
        rotation.z = 0f;

        VirtualAssistantManager.Instance.transform.rotation = Quaternion.Lerp(VirtualAssistantManager.Instance.transform.rotation, rotation, Time.deltaTime * 2f);


        targetPosition = VirtualAssistantManager.Instance.targetObject.GetComponent<Rigidbody>().ClosestPointOnBounds(VirtualAssistantManager.Instance.transform.position);
        Vector3 assistantDirection = targetPosition - VirtualAssistantManager.Instance.transform.position;
        Vector3 targetPoint = VirtualAssistantManager.Instance.transform.position + assistantDirection * 1f;
        lerpPercentage += Time.deltaTime / 5f;
        VirtualAssistantManager.Instance.transform.position = Vector3.Lerp(startingPosition, targetPoint, lerpPercentage);

        if (Vector3.Distance(VirtualAssistantManager.Instance.transform.position, targetPosition) < 0.05f)
        {
            animator.SetTrigger("TargetReached");
        }
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
}
