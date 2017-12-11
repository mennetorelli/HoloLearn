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
        private Vector3 assistantDirection;

        private float distanceFromTarget;
        private float lerpPosition;

        private Transform obstacle;

        private bool isColliding;
        private bool isBusy;


        // Use this for initialization
        public override void Start()
        {
            assistantPosition = transform.position;
            lerpPosition = 0f;

            patience = 3;
        }

        // Update is called once per frame
        public override void Update()
        {
            Debug.DrawRay(transform.position, targetPosition, Color.cyan, 3f);
            // Se siamo negli stati Idle o Jump, allora l'assistente guarda verso di te
            if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle") ||
                gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Jumping") ||
                gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Shaking Head No"))
            {
                Vector3 relativePos = Camera.main.transform.position - gameObject.transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePos);
                rotation.x = 0f;
                rotation.z = 0f;

                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 100f);

                gameObject.GetComponent<Animator>().ResetTrigger("TargetReached");
            }
            // Altrimenti deve guardare il target (cioè il placement)
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
                    
                    if (isColliding)
                    {
                        

                        rotation = Quaternion.LookRotation(assistantDirection);
                        rotation.x = 0f;
                        rotation.z = 0f;

                        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 5f);

                        Vector3 targetPoint = transform.position + assistantDirection * 1f;
                        lerpPosition += Time.deltaTime / 5f;
                        transform.position = Vector3.Lerp(assistantPosition, targetPoint, lerpPosition);
                    }
                    else
                    {
                        targetPosition = targetObject.GetComponent<Rigidbody>().ClosestPointOnBounds(transform.position);
                        assistantDirection = targetPosition - transform.position;
                        Debug.DrawRay(transform.position, assistantDirection, Color.blue, 3f);
                        Vector3 targetPoint = transform.position + assistantDirection * 1f;
                        lerpPosition += Time.deltaTime / 5f;
                        transform.position = Vector3.Lerp(assistantPosition, targetPoint, lerpPosition);
                    }
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
                }
            }

            SortByDistance(targets);

            GameObject closestTarget = targets[0];

            targetObject = closestTarget.transform;
            targetPosition = targetObject.GetComponent<Rigidbody>().ClosestPointOnBounds(transform.position);
            assistantPosition = transform.position;
            distanceFromTarget = Vector3.Distance(transform.position, targetPosition);
            lerpPosition = 0;


            gameObject.GetComponent<Animator>().SetTrigger("Walk");
            gameObject.GetComponent<Animator>().ResetTrigger("Stop");
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


            gameObject.GetComponent<Animator>().SetTrigger("Walk");
            gameObject.GetComponent<Animator>().ResetTrigger("Stop");
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


        void OnTriggerEnter(Collider other)
        {
            StartCoroutine(NewCollision(other));
        }


        private IEnumerator NewCollision(Collider other)
        {
            if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Walking") && !isColliding)
            {
                isColliding = true;
                obstacle = other.transform;
                lerpPosition = 0;
                Debug.Log(isColliding);

                Vector3 obstacleDirection = obstacle.transform.position - transform.position;
                Debug.DrawRay(transform.position, obstacleDirection, Color.green, 10f);
                Vector3 up = transform.TransformVector(Vector3.up);
                Debug.DrawRay(transform.position, up, Color.black, 10f);
                assistantDirection = Vector3.Cross(obstacleDirection, up);
                Debug.DrawRay(transform.position, assistantDirection, Color.red, 10f);

                yield return new WaitForSeconds(0.1f);
                isColliding = false;
                assistantPosition = transform.position;

            }

        }

    }
}
