using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        // Update is called once per frame
        void Update()
        {
            Debug.DrawRay(GetMouseRay().origin, GetMouseRay().direction * 100);

            if (CombatInteraction())
            {
                //Debug.Log("combatint");
                return;
            }

            else if (MovementInteraction())
            {
                //Debug.Log("movementint");
                return;
            }

            Debug.Log("Nothing to do.");
        }

        private bool CombatInteraction()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (var hit in hits)
            {
                if (GetComponent<Fighter>().canAttack(hit.transform.GetComponent<CombatTarget>()))
                {
                    if (Input.GetMouseButtonDown(0))
                    { 
                            gameObject.GetComponent<Fighter>().Attack(hit.transform.gameObject.GetComponent<CombatTarget>());                                            
                    }
                    return true;
                }

            }
            return false;
        }

        private bool MovementInteraction()
        {
            RaycastHit hit;
            bool HasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (HasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().MoveTo(hit.point);
                }
                return true;
            }
            return false;
        }


        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}