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
        public BBParameter<float> energy;
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

			agent.SetDestination(target.value.transform.position); //set the destination of the stage to the agents destination thing.
		 if(agent.remainingDistance < 3) //if the remaining distance is less than three 
			{ //It will begin to rotate the panda towards the screen
				Vector3 direction = (target.value.transform.position - agent.transform.position).normalized;
				Quaternion lookRotation = Quaternion.LookRotation(direction);
				agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, lookRotation, Time.deltaTime * 4);
				Debug.Log(lookRotation.x + " Y: " + lookRotation.y); //debugging to see the why value of the lookRotation

                if (lookRotation.y>=0.5f) //when the Y value reaches 0.5 or greater
				{
					//it will player the song and get the agents audiosource
                    source.value = agent.gameObject.GetComponent<AudioSource>();
                    source.value.clip = clip.value; //set the sources clip to the actual clip
					source.value.volume = 1; source.value.pitch = 1; //adjust the pitch and volume to 1
                    if (!source.value.isPlaying) //if a song is already playing or sound do not play this
                    {
                        source.value.PlayOneShot(clip.value,1.0f); //plays song
                    }
                   



                    StartCoroutine(SpinDance()); //Starts coroutine


                    if (Input.GetMouseButtonDown(0)) //when getmousedown is pressed
					{
						source.value.Stop();  //stops the song
						EndAction(true);//exits from the action task
					}
                }


            }

        }

		IEnumerator SpinDance()
		{
            float rotationAmount = 0f; 
            float speed = 90f; // speed of rotation
          
            while (rotationAmount < 400) //while the rotation amount is less than 400
            {
             
                float step = speed * Time.deltaTime; //rotate the transform of the agent by vector3.up and multiplied by step
                agent.transform.Rotate(Vector3.up * step); 
                rotationAmount += step; //rotationAmount then takes in the variable of step
               


            }
            yield return null; //exits couroutine
        }

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}