using NodeCanvas.Framework;
using NodeCanvas.Tasks.Conditions;
using ParadoxNotion.Design;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Conditions
{

    public class eatCondition : ConditionTask
    {
        public NavMeshAgent agent;
        public float distanceWidth = 4;
        Blackboard pandaBB;
        public BBParameter<float> pandaEnergy;
        public BBParameter<GameObject> nearestBamboo;
        float distance;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            pandaBB = agent.GetComponent<Blackboard>();
            


            pandaEnergy = new BBParameter<float>();
            agent = agent.GetComponent<NavMeshAgent>();
            return null;
        }

     
        protected override bool OnCheck()
        {
            // Find all bamboo objects
            GameObject[] allBamboo = GameObject.FindGameObjectsWithTag("Bamboo");

            if (allBamboo.Length == 0)
            {
                return false; // No bamboo, condition fails
            }

            // Find the closest bamboo
            nearestBamboo.value = null;
            float shortestDistance = Mathf.Infinity;
            Vector3 agentPosition = agent.transform.position;

            foreach (GameObject bamboo in allBamboo)
            {
                float distanceToBamboo = Vector3.Distance(agentPosition, bamboo.transform.position);
                if (distanceToBamboo < shortestDistance)
                {
                    shortestDistance = distanceToBamboo;
                    nearestBamboo.value = bamboo; 
                }
            }


            // Store the closest bamboo's distance
            distance = shortestDistance;

            if (distance < distanceWidth)
            {
                agent.SetDestination(nearestBamboo.value.transform.position);
                if (agent.remainingDistance < 3)
                {
                    Vector3 direction = (nearestBamboo.value.transform.position - agent.transform.position).normalized;
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, lookRotation, Time.deltaTime * 4);
                    return true;
                }
               
            }

            return false;
        }
    }
}
