﻿using System;
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
            Vector3 relativePos = Camera.main.transform.position - gameObject.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            rotation.x = 0f;
            rotation.z = 0f;

            gameObject.transform.rotation = rotation;

        }

        public override void Jump()
        {
            gameObject.GetComponent<Animator>().SetTrigger("Jump");
        }
    }
}
