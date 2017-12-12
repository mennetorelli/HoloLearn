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

        // Use this for initialization
        public override void Start()
        {
            patience = 3;
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
            if (!isBusy)
            {
                gameObject.GetComponent<Animator>().SetTrigger("ShakeHead");
            }
        }


        public override void ObjectDragged(GameObject draggedObject)
        {         
            // Nothing to do
        }

        public override void ObjectDropped()
        {
            gameObject.GetComponent<Animator>().SetTrigger("Stop");
        }


    }
}
