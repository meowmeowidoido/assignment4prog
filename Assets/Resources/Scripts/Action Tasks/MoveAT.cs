using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using NodeCanvas.Tasks.Actions;
using Unity.VisualScripting;
using UnityEditor;

namespace NodeCanvas.Tasks.Actions
{

    public class MoveAT : ActionTask
    {
        public NavMeshAgent agent = new NavMeshAgent();
        public BBParameter<float> distance;
        public Blackboard panda;
        public BBParameter<float> energy;
        float energyReduction;
        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            Debug.Log("CLICK with M1, to move panda around, when panda is near green bamboo it will eat for energy, if panda runs low on energy it will go into hut and sleep. Press at the very top of the brown trees TWICE to enter climb state, press right mouse to enter dancing state");
            //initializing and getting components
            panda = panda.GetComponent<Blackboard>();
            agent = panda.GetComponent<NavMeshAgent>();// Get from GameObject
            energyReduction = 10; //setting energy reduction
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

            if (Input.GetMouseButtonDown(0) && energy.value >= 20) //if the energy value is greater than 20 and the mouse is clicked
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //ray thtat uses Camera.main.ScreenPointToRay which returns a ray to the screenpoint to specify it returns it to whenever the mouse is while it was clicked
                RaycastHit hit;
                Debug.Log(energy.value); //displays energy value
                energy.SetValue(energy.value - energyReduction); //reduces energy by 10 each time click.
                if (Physics.Raycast(ray, out hit)) //if the raycast has been clicked or the mouse has been clicked to an rea where the agent can move
                {

                    hit.point.Equals(ray); //the hit point equals wherever the ray is 
                    agent.SetDestination(hit.point); //sets the agents destination to the hit.point whenever the player clicked on the screen.
                    EndAction(true); //end action task
                }


            }
        }
    }
}