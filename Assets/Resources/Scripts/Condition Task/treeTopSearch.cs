using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class treeTopSearch : ConditionTask
    {

		  
        //Variables and initialization
        public NavMeshAgent agent;
        public float distanceMax = 5f;
        Blackboard pandaBB;
        public BBParameter<float> pandaEnergy;
        public BBParameter<GameObject> nearestTreeTop;
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
            GameObject[] allTreeTops = GameObject.FindGameObjectsWithTag("BambooTop");

            if (allTreeTops.Length == 0)
            {
                return false; // If there aren't any bamboo the condition skips and returns false.
            }

            // Find the closest bamboo
            float shortestDistance = Mathf.Infinity;
            Vector3 agentPosition = agent.transform.position;

            foreach (GameObject treeTop in allTreeTops) //Foreach loop that looks at the distance of each tree top in all of the arrays and checks the distance of each from the player
            {
                float distanceToTreeTop= Vector3.Distance(agentPosition, treeTop.transform.position);
                if (distanceToTreeTop < shortestDistance)//If the distance to the tree top is less than the shortestDistance it will set the nearestTreeTop.value to the current treeTop
                {
                    shortestDistance = distanceToTreeTop;
                    nearestTreeTop.value = treeTop;
                }
            }


            // Distance stores the closest tree top's distance
            distance = shortestDistance;
            Debug.Log(distance);
            if (distance < distanceMax) //checks whether the distance is shorter than the distanceWidth
            {
                agent.SetDestination(nearestTreeTop.value.transform.position); //Sets the destination of the agent 
                if (agent.remainingDistance < 3) //if the remaining distance of the navmesh agent is less than 3, it will rotate the diection of the panda so that it looks towards the tree top.
                {
                    Vector3 direction = (nearestTreeTop.value.transform.position - agent.transform.position).normalized;
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, lookRotation, Time.deltaTime * 4);
                    return true;
                }

            }

            return false;
        }
    }
}