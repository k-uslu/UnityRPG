using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Movement;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        Ray lastRay;

        // Update is called once per frame
        void Update()
        {
            // if (Input.GetMouseButton(0)){            
            //     MoveToCursor();
            // }

            updateAnimator();
            
        }

        public void MoveTo(Vector3 dest)
        {
            GetComponent<NavMeshAgent>().destination = dest;
        }

        private void updateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;

            GetComponent<Animator>().SetFloat("forwardSpeed",speed);

        }
    }
}
