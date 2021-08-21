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
        [SerializeField] float timeBetweenAttacks = 1.5f;    

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
                GetComponent<Animator>().ResetTrigger("stopAttack");
                GetComponent<Animator>().SetTrigger("attack");
                attackTime = 0;               
                hitTarget = target;
                
            }            
        }

        public void Attack(GameObject ctarget){
            if(GetComponent<ActionScheduler>()!=null)GetComponent<ActionScheduler>().StartAction(this);    
            target = ctarget.transform;           
        }

        private bool targetInRange(){
            float dist = Vector3.Distance(target.position,GetComponent<Transform>().position);
            // Debug.Log(dist);
            if(dist<attackRange_Debug)return true;
            return false;
        }

        public bool canAttack(GameObject target){
            if(target!=null && !target.GetComponent<Health>().isDead()){
                return true;
            }
            else{
                return false;
            }
        }

        public void Cancel()
        {      
            GetComponent<Animator>().SetTrigger("stopAttack");            
            target = null;                      
        }

        //Animation hit event
        public void Hit(){
            if(hitTarget==null) return;

            Health healthComponent = hitTarget.GetComponent<Health>();
            healthComponent.takeDamage(weaponDamage);
            
            if(hitTarget.GetComponent<Health>().isDead()){
                //Debug.Log("target died");                   
                GetComponent<Animator>().SetTrigger("stopAttack");
                target=null;
            }
        }
    }
}

