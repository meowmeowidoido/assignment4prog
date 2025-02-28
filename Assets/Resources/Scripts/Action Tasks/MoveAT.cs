using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using NodeCanvas.Tasks.Actions;
using Unity.VisualScripting;
using UnityEditor;

namespace NodeCanvas.Tasks.Actions {
	
	public class MoveAT : ActionTask {
		public NavMeshAgent agent= new NavMeshAgent();
		public BBParameter<float> distance; 
		public Blackboard panda;
        public BBParameter<float> energy;
        float energyReduction;
        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			panda= panda.GetComponent<Blackboard>();
            agent = panda.GetComponent<NavMeshAgent>();// Get from GameObject
            energyReduction = 10;
            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {

          

        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {

            if (Input.GetMouseButtonDown(0) && energy.value >= 20)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Debug.Log(energy.value);
                //	pandaEnergy.value = pandaEnergy.value - 10;
                energy.SetValue(energy.value - energyReduction);
                if (Physics.Raycast(ray, out hit))
                {

                    hit.point.Equals(ray);
                    agent.SetDestination(hit.point);
                    EndAction(true);
                }
               
                //	distance = Vector3.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), agent.transform.position);

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