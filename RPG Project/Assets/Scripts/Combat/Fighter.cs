using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        public Transform target;
        float attackRange_Debug = 2.0f;
        

        // Update is called once per frame
        void Update()
        {
            if(target!=null){
                GetComponent<Mover>().MoveTo(target.position);
                if(targetInRange()){
                    
                    GetComponent<Mover>().StopAgent();
                }
            }
        }

        public void Attack(CombatTarget ctarget){
            print("Attack triggered!");
            target = ctarget.transform;
        }

        private bool targetInRange(){
            float dist = Vector3.Distance(target.position,GetComponent<Transform>().position);
            Debug.Log(dist);
            if(dist<attackRange_Debug)return true;
            return false;
        }
    }
}

