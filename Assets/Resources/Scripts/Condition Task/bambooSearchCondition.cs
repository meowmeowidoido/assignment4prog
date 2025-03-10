using NodeCanvas.Framework;
using NodeCanvas.Tasks.Conditions;
using ParadoxNotion.Design;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Conditions
{

    public class bambooSearchCondition : ConditionTask
    {
        //Variables and initialization
        public NavMeshAgent agent;
        public float distanceMax = 3;
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
                return false; // If there aren't any bamboo the condition skips and returns false.
            }

            // Find the closest bamboo
            float shortestDistance = Mathf.Infinity;
            Vector3 agentPosition = agent.transform.position;

            foreach (GameObject bamboo in allBamboo) //Foreach loop that looks at the distance of each bamboo tree in all of the arrays and checks the distance of each from the player
            {
                float distanceToBamboo = Vector3.Distance(agentPosition, bamboo.transform.position);
                if (distanceToBamboo < shortestDistance)//If the distance to the bamboo is less than the shortestDistance it will set the nearestTreeTop.value to the current treeTop
                {
                    shortestDistance = distanceToBamboo;
                    nearestBamboo.value = bamboo; 
                }
            }


            // Distance stores the closest bamboo's distance
            distance = shortestDistance;

            if (distance < distanceMax) //checks whether the distance is shorter than the distanceWidth
            {
                agent.SetDestination(nearestBamboo.value.transform.position); //Sets the destination of the agent 
                if (agent.remainingDistance < 3) //if the remaining distance of the navmesh agent is less than 3, it will rotate the diection of the panda so that it looks towards the bamboo.
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
