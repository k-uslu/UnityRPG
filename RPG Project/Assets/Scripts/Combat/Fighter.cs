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
        public Transform hitTarget;
        [SerializeField] float timeBetweenAttacks = 4.0f;    

        float weaponDamage = 5.0f;   
        float attackTime;
        float attackRange_Debug = 2.0f;
        
        // Update is called once per frame
        void Update()
        {
            attackTime += Time.deltaTime;

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
            GetComponent<Transform>().LookAt(target.position);
            
            if(attackTime >= timeBetweenAttacks){
                GetComponent<Animator>().SetTrigger("attack");
                attackTime = 0;               
                hitTarget = target;
                
            }            
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
            GetComponent<Animator>().SetTrigger("stopAttack");           
        }

        //Animation hit event
        public void Hit(){
            Health healthComponent = hitTarget.GetComponent<Health>();
            healthComponent.takeDamage(weaponDamage);
            if(hitTarget.GetComponent<Health>().isDead()){
                Debug.Log("target died");
                target=null;
                GetComponent<Animator>().SetTrigger("stopAttack");
            }
        }
    }
}

