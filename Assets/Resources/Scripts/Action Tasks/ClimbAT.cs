using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions
{

    public class ClimbAT : ActionTask
    {


        //Variables
        public NavMeshAgent agent;
        public BBParameter<float> pandaEnergy;
        public BBParameter<GameObject> nearestTreeTop;
        public BBParameter<GameObject> origin;
        float climbTimer;
        float energyLoss;

        protected override string OnInit()
        {
            return null;
        }

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute()
        {
            energyLoss = 0;
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {
            Debug.Log(climbTimer);

            if (energyLoss <= 15)
            {
                // Climbs up to ensure that the navmesh is actually on the top of the tree.
                if (climbTimer < 3)
                {
                    agent.SetDestination(nearestTreeTop.value.transform.position);
                }
               //climbs down by checking if the timer is greater than three and less than 6
                else if (climbTimer >= 3 && climbTimer < 6)
                {
                    Vector3 climbDown = agent.transform.position + new Vector3(0, -0.5f, 0);
                    agent.SetDestination(climbDown);
                }
                // if the climbTimer is greater than 6, it will reset to 6 and make the agent climb back up again.
                else if (climbTimer >= 6)
                {
                    climbTimer = 0; // Reset timer after full cycle
                    agent.SetDestination(nearestTreeTop.value.transform.position);
                }

                // energyLoss is incrementing and subtracts from the panda's energy value.
                climbTimer += Time.deltaTime; //the climb timer is incremented.
                pandaEnergy.value -= Time.deltaTime;
                energyLoss += Time.deltaTime; // Keep increasing energy loss
            }



            if (energyLoss >= 15 || pandaEnergy.value<30) //if the energy loss is greater than 15 or the panda's energy value is less than 30, the panda will climb off and end the state.
            {
                agent.SetDestination(origin.value.transform.position);
                EndAction(true);
            }

        }

    }
}