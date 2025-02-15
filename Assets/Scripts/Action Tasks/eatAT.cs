using NodeCanvas.Framework;
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
        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            return null;
        }

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute()
        {
            GameObject[] allBamboo = GameObject.FindGameObjectsWithTag("Bamboo");

            GameObject currentBamboo;
            currentBamboo = GameObject.FindGameObjectWithTag("Bamboo");
            GameObject.Destroy(currentBamboo);
            EndAction(true);
            GameObject nearestBamboo = null;
            float shortestDistance = Mathf.Infinity;
            Vector3 agentPosition = agent.value.transform.position;

            foreach (GameObject bamboo in allBamboo)
            {
                float distance = Vector3.Distance(agentPosition, bamboo.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestBamboo = bamboo;
                }
            }

            // Store the closest Bamboo in the Blackboard
            closestBamboo.value = nearestBamboo;
            Debug.Log("Closest Bamboo found at: " + nearestBamboo.transform.position);

            EndAction(true);
        }
    }
}