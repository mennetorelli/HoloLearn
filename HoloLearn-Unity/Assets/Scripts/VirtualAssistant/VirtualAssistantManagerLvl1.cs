﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.VirtualAssistant
{
    class VirtualAssistantManagerLvl1 : VirtualAssistantManager
    {
        private Vector3 targetPosition;

        private float distanceFromTarget;
        private float lerpPosition;


        // Use this for initialization
        public override void Start()
        {
            targetPosition = Camera.main.transform.position;
            lerpPosition = 0f;
        }

        // Update is called once per frame
        public override void Update()
        {
            // Se siamo negli stati Idle o Jump, allora l'assistente guarda verso di te
            if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle") ||
                gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Jump"))
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
                if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                {
                    gameObject.GetComponent<Animator>().SetTrigger("TargetReached");  
                }
                // Altrimenti cammina verso il target
                else
                {
                    lerpPosition += Time.deltaTime / distanceFromTarget * 0.005f;
                    transform.position = Vector3.Lerp(transform.position, targetPosition, lerpPosition);
                }
            }       

        }

        public override void Jump()
        {
            gameObject.GetComponent<Animator>().SetTrigger("Jump");
        }

        public override void Walk(GameObject draggedObject)
        {
            String tag = draggedObject.tag;

            Transform[] placements = GameObject.FindGameObjectWithTag("Placements").GetComponentsInChildren<Transform>();
            List<GameObject> targets = new List<GameObject>();
            foreach (Transform target in placements)
            {
                if (target.gameObject.tag == tag)
                {
                    targets.Add(target.gameObject);
                }
            }
            Debug.Log(targets.Count);

            GameObject closestTarget = targets[0];
            Debug.Log(closestTarget);

            targetPosition = closestTarget.transform.position;

            distanceFromTarget = Vector3.Distance(transform.position, targetPosition);
            lerpPosition = 0;

            gameObject.GetComponent<Animator>().SetTrigger("Walk");

        }
    }
}
