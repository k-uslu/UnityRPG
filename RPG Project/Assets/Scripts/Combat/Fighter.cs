 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        public Transform target;
        float attackRange_Debug = 2.0f;
        [SerializeField] float timeBetweenAttacks = 2.0f;
        

        // Update is called once per frame
        void Update()
        {
            if(target!=null){               
                if(!targetInRange()){
                    GetComponent<Mover>().AttackMove(target.position);   
                    
                }
                else{
                    GetComponent<Mover>().Cancel();
                    AttackBehaviour();                
                }
            }
        }

        private void AttackBehaviour()
        {
            GetComponent<Animator>().SetTrigger("attack");
        }

        public void Attack(CombatTarget ctarget){
            GetComponent<ActionScheduler>().StartAction(this);
            target = ctarget.transform;           
        }

        private bool targetInRange(){
            float dist = Vector3.Distance(target.position,GetComponent<Transform>().position);
            // Debug.Log(dist);
            if(dist<attackRange_Debug)return true;
            return false;
        }

        public void Cancel()
        {
            target = null;
            //GetComponent<Animator>().SetTrigger("attack");
        }

        //Animation hit event
        public void Hit(){
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }
}

