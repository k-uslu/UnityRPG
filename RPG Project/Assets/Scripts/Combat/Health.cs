using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float hitPoints;
        [SerializeField] float maxHitPoints = 100.0f;

        bool dead;

        public bool isDead(){
            return dead;
        }

        // Start is called before the first frame update
        void Start()
        {
            dead=false;
            hitPoints = maxHitPoints;
        }

        public void takeDamage(float damageTaken){
            if(hitPoints-damageTaken>0){
                hitPoints -= damageTaken;
                Debug.Log("currentHP:"+hitPoints);
            }
            else{                
                if(!dead) Destroy();
                hitPoints=0;
            }
           
        }

        public void Destroy(){
            dead=true;
            GetComponent<Animator>().SetTrigger("die");
        }
    }
}
