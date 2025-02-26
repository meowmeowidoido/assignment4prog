using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Actions {

	public class sleepTask : ActionTask {

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        public NavMeshAgent agent = new NavMeshAgent();
        public BBParameter<float> energy; 
		public Blackboard panda;
        public GameObject bed;
     
        protected override string OnInit() {
            panda = panda.GetComponent<Blackboard>();
            agent = panda.GetComponent<NavMeshAgent>();

            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
		
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {
		 agent.SetDestination(bed.transform.position);
         float distance =  Vector3.Distance(agent.transform.position, bed.transform.position);
            if (distance < 2)
            {
                if (energy.value < 100)
                {
                    energy.value += 10f * Time.deltaTime;  // Increment at a fixed rate
                }
                else
                {
                    EndAction(true);  // Only end when energy reaches 100
                }
            }
        }

        //Called when the task is disabled.
        protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}