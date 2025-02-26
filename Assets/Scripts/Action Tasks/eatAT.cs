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
        public BBParameter<NavMeshAgent> agent;
        private Blackboard pandaBB; // Reference to the Blackboard

        protected override string OnInit()
        {  // Get the Blackboard from the agent
                pandaBB = agent.value.GetComponent<Blackboard>();

                    // Retrieve "nearestBamboo" from Blackboard
                    closestBamboo = pandaBB.GetVariableValue<GameObject>("nearestBamboo");


            return null;
        }

        protected override void OnExecute()
        {
            if (closestBamboo == null || closestBamboo.value == null)
            {
                Debug.LogWarning("No valid bamboo found in Blackboard! Cannot destroy.");
                EndAction(false);
                return;
            }

            Debug.Log("Destroying bamboo: " + closestBamboo.value.name);
            GameObject.Destroy(closestBamboo.value);
            EndAction(true);
        }
    }
}