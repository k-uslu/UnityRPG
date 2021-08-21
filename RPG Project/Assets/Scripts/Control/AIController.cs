using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using UnityEngine.AI;
using RPG.Movement;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float aggroDistance = 6.0f;
        Fighter fighter;
        GameObject player;
        Vector3 guardLocation;
        [SerializeField] float guardRotation;
        [SerializeField] bool guarding=true;
        [SerializeField] PatrolPath patrolPath;

        int patrolWPCounter=0;
        float suspicionTimer = Mathf.Infinity;
        float suspicionMax = 4;
        float waitTimer = Mathf.Infinity;
        float waitMax = 1.5f;
        float rotType = 0.4f;
        float WPTolerance = 0.8f;

        Vector3 nextPosition;

        private void Start()
        {
            nextPosition = guardLocation;
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            guardLocation = transform.position;
            guardRotation = transform.rotation.y;
            //debug.Log(guardRotation);
        }

        private void Update() {
            if(!gameObject.GetComponent<Health>().isDead()){
                if (gameObject.GetComponent<NavMeshAgent>().velocity == Vector3.zero)suspicionTimer += Time.deltaTime;  
                if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    GetComponent<NavMeshAgent>().speed = 0;
                }
                else
                {
                    GetComponent<NavMeshAgent>().speed = 3f;
                }

                if (inRange(player) && fighter.canAttack(player))
                {
                    fighter.Attack(player);
                    suspicionTimer=0;
                }
                else if(suspicionTimer<suspicionMax)
                {
                    fighter.Cancel();
                    if(gameObject.GetComponent<NavMeshAgent>().velocity == Vector3.zero) transform.Rotate(new Vector3(0f, rotType, 0f));
                    if(suspicionTimer>suspicionMax/2) rotType=-0.4f;                   
                }    
                else
                {
                    rotType=0.4f;
                    fighter.Cancel();
                    if(guarding){
                        GetComponent<Mover>().MoveTo(guardLocation); 
                    }
                    else if(patrolPath!=null){
                        PatrolBehaviour();
                    }                  
                }
            }

            if(gameObject.GetComponent<NavMeshAgent>().velocity == Vector3.zero && Vector3.Distance(guardLocation,transform.position)<1){
                float localRotate = guardRotation - transform.rotation.y;
                if(localRotate>1){
                    localRotate= 1-guardRotation+(-1-transform.rotation.y);
                }
                transform.Rotate(new Vector3(0,localRotate,0),Space.Self);
            }
        }

        private void PatrolBehaviour()
        {           
            if(AtWaypoint()&&waitTimer>waitMax){
                NetxWaypoint();
            }
            nextPosition = GetCurrentWaypoint();
            GetComponent<Mover>().MoveTo(nextPosition);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.transform.GetChild(patrolWPCounter).position;
        }

        private void NetxWaypoint()
        {
            patrolWPCounter = patrolPath.GetNext(patrolWPCounter);
        }

        private bool AtWaypoint()
        {
            if(Vector3.Distance(transform.position,GetCurrentWaypoint())<WPTolerance){
                waitTimer += Time.deltaTime;
                return true;
            }
            waitTimer=0;
            return false;
        }

        bool inRange(GameObject player){

            if(Vector3.Distance(GetComponent<Transform>().position, player.GetComponent<Transform>().position) < aggroDistance) return true;
            else return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(gameObject.transform.position, aggroDistance);
            
        }
    }

    
}
