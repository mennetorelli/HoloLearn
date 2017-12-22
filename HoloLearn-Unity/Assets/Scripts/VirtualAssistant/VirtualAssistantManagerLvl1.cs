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
            targetObject = draggedObject.transform;
            gameObject.GetComponent<Animator>().SetTrigger("DraggingStarted");
        }

        public override void ObjectDropped()
        {
            gameObject.GetComponent<Animator>().SetTrigger("DraggingStopped");
        }

        public override void Count()
        {
            StartCoroutine(Count2());
        }

        private IEnumerator Count2()
        {
            VirtualAssistantManager.Instance.gameObject.GetComponent<Animator>().SetTrigger("Walk");
            yield return new WaitForSeconds(5f);
            
        }
    }
}
