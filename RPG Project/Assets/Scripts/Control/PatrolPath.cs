using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] Color wpColor = Color.blue;

        private void OnDrawGizmos() {

            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.color = wpColor;
                Gizmos.DrawSphere(transform.GetChild(i).position, 0.5f);
                if(i<transform.childCount-1){
                    Gizmos.DrawLine(transform.GetChild(i).position,transform.GetChild(i+1).position);
                }
                else{
                    Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(0).position);
                }
                
            }
        }

        public int GetNext(int i){
            if(i==transform.childCount-1){
                return 0;
            }
            else return i+1;
        }
    }
}
