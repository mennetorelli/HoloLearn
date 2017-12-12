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

        private Transform targetObject;
        private Vector3 targetPosition;
        private Vector3 assistantPosition;

        private float distanceFromTarget;
        private float lerpPosition;

        private bool isColliding;
        private bool isBusy;


        // Use this for initialization
        public override void Start()
        {
            assistantPosition = transform.position;
            lerpPosition = 0f;

            patience = 2;
            isBusy = false;

            gameObject.GetComponent<Animator>().SetTrigger("Stop");
        }

        // Update is called once per frame
        public override void Update()
        {
            Debug.DrawLine(transform.position, targetPosition, Color.blue, 5f);

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
            // Altrimenti deve guardare il target
            else
            {
                Vector3 relativePos = targetObject.position - gameObject.transform.position;
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
                    targetPosition = targetObject.GetComponent<Rigidbody>().ClosestPointOnBounds(transform.position);
                    Vector3 assistantDirection = targetPosition - transform.position;
                    Vector3 targetPoint = transform.position + assistantDirection * 1f;
                    lerpPosition += Time.deltaTime / 5f;
                    transform.position = Vector3.Lerp(assistantPosition, targetPoint, lerpPosition);
                }
            }
        }


        public override void PrepareToWalkToTarget(GameObject draggedObject)
        {
            if (isBusy || 
                gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Walking") ||
                gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Pointing"))
            {
                Debug.Log("sto già facendo cose");
            }
            else
            {
                Debug.Log("preparing to walk to next placement");
                gameObject.GetComponent<Animator>().ResetTrigger("Stop");
                isBusy = true;
                StartCoroutine(WalkToNearestPlacement(draggedObject));
                isBusy = false;
            }
        }


        public override void PrepareToWalkToNextObject()
        {
            if (isBusy || 
                gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Walking") || 
                gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Pointing"))
            {
                Debug.Log("sto già facendo cose");
                gameObject.GetComponent<Animator>().SetTrigger("Stop");
            }
            else
            {
                Debug.Log("dropped: preparing to walk to object");
                gameObject.GetComponent<Animator>().ResetTrigger("Stop");
                isBusy = true;
                StartCoroutine(WalkToNextObject());
                isBusy = false;
            }
        }

        public override void Jump()
        {
            gameObject.GetComponent<Animator>().SetTrigger("Jump");
        }

        public override void ShakeHead()
        {
            gameObject.GetComponent<Animator>().SetTrigger("ShakeHead");
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
                }
            }

            SortByDistance(targets);

            GameObject closestTarget = targets[0];

            targetObject = closestTarget.transform;
            targetPosition = targetObject.GetComponent<Rigidbody>().ClosestPointOnBounds(transform.position);
            assistantPosition = transform.position;
            distanceFromTarget = Vector3.Distance(transform.position, targetPosition);
            lerpPosition = 0;

            Debug.Log("walking to next placement " + targetObject);
            gameObject.GetComponent<Animator>().SetTrigger("Walk");
        }

        private IEnumerator WalkToNextObject()
        {
            yield return new WaitForSeconds(patience);

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

            GameObject closestTarget = targets[0];

            targetObject = closestTarget.transform;
            targetPosition = targetObject.GetComponent<Rigidbody>().ClosestPointOnBounds(transform.position);
            assistantPosition = transform.position;
            distanceFromTarget = Vector3.Distance(transform.position, targetPosition);
            lerpPosition = 0;
            

            Debug.Log("walking to next object " + targetObject);
            gameObject.GetComponent<Animator>().SetTrigger("Walk");
        }


        private void SortByDistance(List<GameObject> targets)
        {
            GameObject temp;
            for (int i = 0; i < targets.Count; i++)
            {
                for (int j = 0; j < targets.Count - 1; j++)
                {
                    if (Vector3.Distance(targets.ElementAt(j).transform.position, transform.position)
                        > Vector3.Distance(targets.ElementAt(j + 1).transform.position, transform.position))
                    {
                        temp = targets[j + 1];
                        targets[j + 1] = targets[j];
                        targets[j] = temp;
                    }
                }
            }
        }

    }
}
