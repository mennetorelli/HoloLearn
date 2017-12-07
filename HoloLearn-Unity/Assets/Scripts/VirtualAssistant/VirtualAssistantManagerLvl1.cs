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
        private float lerpPosition;

        // Use this for initialization
        public override void Start()
        {
            targetPosition = Camera.main.transform.position;
        }

        // Update is called once per frame
        public override void Update()
        {
            if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle") ||
                gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                targetPosition = Camera.main.transform.position;
            }


            Vector3 relativePos = targetPosition - gameObject.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            rotation.x = 0f;
            rotation.z = 0f;

            gameObject.transform.rotation = rotation;

            lerpPosition = Time.deltaTime / 5f;
            if (targetPosition != Camera.main.transform.position)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, lerpPosition);
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

            gameObject.GetComponent<Animator>().SetTrigger("Walk");

        }
    }
}
