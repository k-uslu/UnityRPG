using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core{

    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;

        public void StartAction(IAction action){
            if(currentAction == action) return;
            if(currentAction!=null){
                currentAction.Cancel();
                //Debug.Log("Cancelled "+currentAction);
            }          
            currentAction = action;
            //Debug.Log("new Act: "+currentAction);
        }

        public void CancelAll(){
            currentAction.Cancel();
            currentAction=null;
        }
    }
}
