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

        // Use this for initialization
        public override void Start()
        {
            patience = 2;

            StartCoroutine(WalkToNearestObject());
        }

        // Update is called once per frame
        public override void Update()
        {

        }

        
        public override void Jump()
        {
            gameObject.GetComponent<Animator>().SetTrigger("Jump");
        }

        public override void ShakeHead()
        {
            gameObject.GetComponent<Animator>().SetTrigger("ShakeHead");
        }


        public override void ObjectDragged(GameObject draggedObject)
        {
            if (!isBusy)
            {
                Debug.Log("preparing to walk to next placement");
                gameObject.GetComponent<Animator>().ResetTrigger("Stop");
                StartCoroutine(WalkToNearestPlacement(draggedObject));
            }
        }


        public override void ObjectDropped()
        {
            if (isBusy)
            {
                Debug.Log("dropped: preparing to walk to object");
                gameObject.GetComponent<Animator>().SetTrigger("Stop");
            }
            StartCoroutine(WalkToNearestObject());
        }


        private IEnumerator WalkToNearestPlacement(GameObject draggedObject)
        {
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
            targetObject = targets[0].transform;


            yield return new WaitForSeconds(patience);

            Debug.Log("walking to next placement " + targetObject);
            gameObject.GetComponent<Animator>().SetTrigger("Walk");
        }


        private IEnumerator WalkToNearestObject()
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
            targetObject = targets[0].transform;
            

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
