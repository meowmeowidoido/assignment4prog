using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;


namespace NodeCanvas.Tasks.Actions {

	public class dancingAT : ActionTask {

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwisepublic NavMeshAgent agent= new NavMeshAgent();
		public NavMeshAgent agent;
        Blackboard panda;
        public BBParameter<float> energy;
        float energyReduction;
		public BBParameter<GameObject> target;
	
		public BBParameter <AudioSource> source;
		public BBParameter <AudioClip> clip;
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
				agent.SetDestination(target.value.transform.position);
               

            }
		 if(agent.remainingDistance < 3)
			{
				Vector3 direction = (target.value.transform.position - agent.transform.position).normalized;
				Quaternion lookRotation = Quaternion.LookRotation(direction);
				agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, lookRotation, Time.deltaTime * 4);
				Debug.Log(lookRotation.x + " Y: " + lookRotation.y);

                if (lookRotation.y>=0.5f)
				{
                    source.value = agent.gameObject.GetComponent<AudioSource>();
                    source.value.clip = clip.value;
					source.value.volume = 1; source.value.pitch = 1;
                    if (!source.value.isPlaying)
                    {
                        source.value.PlayOneShot(clip.value,1.0f);
                    }
                   



                    StartCoroutine(rotateOnSpot());


                    if (Input.GetMouseButtonDown(0))
					{
						source.value.Stop();
						EndAction(true);
					}
                }


            }

        }

		IEnumerator rotateOnSpot()
		{
            float rotationAmount = 0f;
            float speed = 90f; // Degrees per second
          
            while (rotationAmount < 400)
            {
             
                float step = speed * Time.deltaTime;
                agent.transform.Rotate(Vector3.up * step);
                rotationAmount += step;
               


            }
            yield return null;
        }

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}