using NodeCanvas.Framework;
using NodeCanvas.Tasks.Conditions;
using ParadoxNotion.Design;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions
{

    public class eatAT : ActionTask
    {
        public BBParameter<GameObject> closestBamboo; // Store result in Blackboard
        public BBParameter<GameObject> nonNomSignifier;
        private float eatingTime;
        protected override void OnExecute()
 
        {
            eatingTime = 0;
            return;
        }

        protected override void OnUpdate()
        {
            if (closestBamboo.value == null)
            {
                Debug.LogWarning("No valid bamboo found in Blackboard! Cannot destroy.");
                EndAction(false);
                return;
            }
            eatingTime += Time.deltaTime;
            if (!nonNomSignifier.value.active)
            {
                nonNomSignifier.value.SetActive(true);
            }

           
            if (eatingTime > 3)
            {
                GameObject.Destroy(closestBamboo.value);
                nonNomSignifier.value.SetActive(false);
                EndAction(true);
            }
        }
    }
}