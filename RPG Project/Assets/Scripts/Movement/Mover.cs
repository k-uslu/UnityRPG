using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;


namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        Ray lastRay;
        NavMeshAgent navMeshAgent;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {       
            updateAnimator();          
        }

        public void MoveTo(Vector3 dest)
        {
            GetComponent<ActionScheduler>().StartAction(this);  
            navMeshAgent.destination = dest;
            navMeshAgent.isStopped = false;
                    
        }

        public void AttackMove(Vector3 dest)
        {
            navMeshAgent.destination = dest;
            navMeshAgent.isStopped = false;
        
        }

        private void updateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;

            GetComponent<Animator>().SetFloat("forwardSpeed",speed);

        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }
    }
}
