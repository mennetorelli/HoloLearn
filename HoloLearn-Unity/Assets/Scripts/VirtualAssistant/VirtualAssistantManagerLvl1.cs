using HoloToolkit.Unity.InputModule;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.VirtualAssistant
{
    class VirtualAssistantManagerLvl1 : VirtualAssistantManager
    {

        private int patience;

        private Vector3 targetPosition;
        private Vector3 assistantPosition;

        private float distanceFromTarget;
        private float lerpPosition;


        // Use this for initialization
        public override void Start()
        {
            targetPosition = Camera.main.transform.position;
            assistantPosition = transform.position;
            lerpPosition = 0f;

            patience = 3;
        }

        // Update is called once per frame
        public override void Update()
        {
            // Se siamo negli stati Idle o Jump, allora l'assistente guarda verso di te
            if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle") ||
                gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Jumping") ||
                gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Shaking Head No"))
            {
                Vector3 relativePos = Camera.main.transform.position - gameObject.transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePos);
                rotation.x = 0f;
                rotation.z = 0f;

                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 2f);

                gameObject.GetComponent<Animator>().ResetTrigger("TargetReached");
            }
            // Altrimenti deve guardare il target (cioè il placement)
            else
            {
                Vector3 relativePos = targetPosition - gameObject.transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePos);
                rotation.x = 0f;
                rotation.z = 0f;

                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 2f);


                // Se è arrivato a meno di 10 cm dal target, scatta il trigger TargetReached
                if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
                {
                    gameObject.GetComponent<Animator>().SetTrigger("TargetReached");
                }
                // Altrimenti cammina verso il target
                else
                {
                    lerpPosition += Time.deltaTime / (distanceFromTarget * 6f);
                    transform.position = Vector3.Lerp(assistantPosition, targetPosition, lerpPosition);
                }
            }
        }



        public override void Idle()
        {
            if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Walking") ||
                gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Pointing"))
            {
                gameObject.GetComponent<Animator>().SetTrigger("Stop");
            }
            StartCoroutine(WalkToNextObject());
        }

        public override void Jump()
        {
            gameObject.GetComponent<Animator>().SetTrigger("Jump");
        }

        public override void ShakeHead()
        {
            gameObject.GetComponent<Animator>().SetTrigger("ShakeHead");
        }


        public override void TargetChanged(GameObject draggedObject)
        {         
            StartCoroutine(WalkToNearestPlacement(draggedObject));
        }


        private IEnumerator WalkToNearestPlacement(GameObject draggedObject)
        {
            yield return new WaitForSeconds(patience);


            String tag = draggedObject.tag;

            Rigidbody[] placements = GameObject.FindGameObjectWithTag("Placements").GetComponentsInChildren<Rigidbody>();
            List<GameObject> targets = new List<GameObject>();
            foreach (Rigidbody target in placements)
            {
                if (target.gameObject.tag == tag)
                {
                    targets.Add(target.gameObject);
                    Debug.Log(target);
                }
            }
            Debug.Log(targets.Count);

            GameObject closestTarget = targets[0];
            Debug.Log(closestTarget);

            targetPosition = closestTarget.GetComponent<Rigidbody>().ClosestPointOnBounds(transform.position);
            assistantPosition = transform.position;
            distanceFromTarget = Vector3.Distance(transform.position, targetPosition);
            lerpPosition = 0;


            gameObject.GetComponent<Animator>().SetTrigger("Walk");
            gameObject.GetComponent<Animator>().ResetTrigger("Stop");
        }

        private IEnumerator WalkToNextObject()
        {
            yield return new WaitForSeconds(patience * 2);


            Rigidbody[] remainingObjects = GameObject.FindGameObjectWithTag("ObjectsToBePlaced").GetComponentsInChildren<Rigidbody>();
            List<GameObject> targets = new List<GameObject>();
            foreach (Rigidbody target in remainingObjects)
            {
                if (target.gameObject.GetComponent<CustomHandDraggable>().IsDraggingEnabled)
                {
                    targets.Add(target.gameObject);
                    Debug.Log(target);
                }
            }
            Debug.Log(targets.Count);

            GameObject closestTarget = targets[0];
            Debug.Log(closestTarget);

            targetPosition = closestTarget.GetComponent<Rigidbody>().ClosestPointOnBounds(transform.position);
            assistantPosition = transform.position;
            distanceFromTarget = Vector3.Distance(transform.position, targetPosition);
            lerpPosition = 0;


            gameObject.GetComponent<Animator>().SetTrigger("Walk");
            gameObject.GetComponent<Animator>().ResetTrigger("Stop");
        }

    }
}
