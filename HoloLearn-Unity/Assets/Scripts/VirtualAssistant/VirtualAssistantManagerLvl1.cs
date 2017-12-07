using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.VirtualAssistant
{
    class VirtualAssistantManagerLvl1 : VirtualAssistantManager
    {
        // Use this for initialization
        public override void Start()
        {

        }

        // Update is called once per frame
        public override void Update()
        {
            gameObject.transform.LookAt(Camera.main.transform.position);
        }

        public override void Jump()
        {
            gameObject.GetComponent<Animator>().SetTrigger("Jump");
        }
    }
}
