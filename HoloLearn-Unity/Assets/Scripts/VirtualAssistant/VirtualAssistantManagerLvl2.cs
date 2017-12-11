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
    class VirtualAssistantManagerLvl2 : VirtualAssistantManager
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
            Vector3 relativePos = Camera.main.transform.position - gameObject.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            rotation.x = 0f;
            rotation.z = 0f;

            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 2f);
        }



        public override void Idle()
        {
            gameObject.GetComponent<Animator>().SetTrigger("Stop");
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
            // Nothing to do
        }


    }
}
