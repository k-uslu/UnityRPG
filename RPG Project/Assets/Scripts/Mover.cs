using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    Ray lastRay;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)){
            
            MoveToCursor();
        }
        
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool HasHit = Physics.Raycast(ray, out hit);

        if (HasHit)
        {
            GetComponent<NavMeshAgent>().destination = hit.point;
        }

        Debug.DrawRay(ray.origin, ray.direction * 100);

    }
}