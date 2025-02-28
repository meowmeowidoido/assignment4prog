using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions
{

    public class ClimbAT : ActionTask
    {

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise

        public NavMeshAgent agent;
        public float distanceWidth = 4;
        Blackboard pandaBB;
        public BBParameter<float> pandaEnergy;
        public BBParameter<GameObject> nearestBambooTop;
        float distance;
        protected override string OnInit()
        {
            return null;
        }

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute()
        {

        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {

            // Find all bamboo objects
            GameObject[] allBambooTop = GameObject.FindGameObjectsWithTag("BambooTop");


            // Find the closest bamboo
            nearestBambooTop.value = null;
            float shortestDistance = Mathf.Infinity;
            Vector3 agentPosition = agent.transform.position;

            foreach (GameObject bambooTop in allBambooTop)
            {
                float distanceToBamboo = Vector3.Distance(agentPosition, bambooTop.transform.position);
                if (distanceToBamboo < shortestDistance)
                {
                    shortestDistance = distanceToBamboo;
                    nearestBambooTop.value = bambooTop;
                }
            }


            // Store the closest bamboo's distance
            distance = shortestDistance;

            if (distance < 0.5)
            {
                agent.SetDestination(nearestBambooTop.value.transform.position);

               
            }
            if(Input.GetMouseButton(0))
            {
                EndAction(true);
            }
          
        }
    }
}