using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;


namespace NodeCanvas.Tasks.Actions {

	public class dancingAT : ActionTask {

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwisepublic NavMeshAgent agent= new NavMeshAgent();
		public NavMeshAgent agent;
        Blackboard panda;
        public BBParameter<float> energy;
        float energyReduction;
		public BBParameter<GameObject> stageTarget;
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
		
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			if (agent.destination != null)
			{
				agent.SetDestination(stageTarget.value.transform.position);


			}
		 if(agent.remainingDistance < 3)
			{
				Vector3 direction = (stageTarget.value.transform.position - agent.transform.position).normalized;
				Quaternion lookRotation = Quaternion.LookRotation(direction);
				agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, lookRotation, Time.deltaTime * 4);
				EndAction(true);
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