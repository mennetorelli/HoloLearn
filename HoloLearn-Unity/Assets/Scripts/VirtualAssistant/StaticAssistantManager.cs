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
    class StaticAssistantManager : VirtualAssistantManager
    {

        // Use this for initialization
        public override void Start()
        {

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
            // Nothing to do
        }

        public override void ObjectDropped()
        {
            // Nothing to do
        }


        public override void Count()
        {
            return;
        }

        public override void SetTriggers()
        {
            // Nothing to do
        }

        public override void CommandReceived()
        {
            // Nothing to do
        }

    }
}
