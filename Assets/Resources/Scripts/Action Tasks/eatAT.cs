using NodeCanvas.Framework;
using NodeCanvas.Tasks.Conditions;
using ParadoxNotion.Design;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions
{
 
    public class eatAT : ActionTask
    {
        //Variables being initialized
        public BBParameter<GameObject> closestBamboo; // Store result in Blackboard
        public BBParameter<GameObject> nonNomSignifier; //Gameobject nom nom signifier
        public BBParameter<AudioSource> source; //audio source
        public BBParameter<AudioClip> clip; //Eating sound
        private float eatingTime; //Timer for how long eating should take 
        public BBParameter<float> energy; //ENergy of panda
        protected override string OnInit()
        {
            source.value = agent.gameObject.GetComponent<AudioSource>();
          return base.OnInit();
        }
        protected override void OnExecute()
 
        {

            eatingTime = 0;//Resets eating time whenever the task is executed
            return;
        }

        protected override void OnUpdate()
        {
           
            eatingTime += Time.deltaTime; //EatingTime increments in onUpdate
            source.value.clip = clip.value;
            source.value.volume = 1; source.value.pitch = 1; 
            if (!source.value.isPlaying)
            {
                source.value.PlayOneShot(clip.value, 1.0f); //plays clip, I used PlayOneShot, and ensured to check there was not another sound the source was playing so it doesn't sound repeated and distorted
            }
            if (!nonNomSignifier.value.active) //if the nomNom signifier is hidden or unactive 
            {
                nonNomSignifier.value.SetActive(true); //activate it 
            }


            if (eatingTime > 4) //once eating time is greater than 4 seconds
            {
                GameObject.Destroy(closestBamboo.value); //destroy the closest bamboo (received from bambooSearchCondition)
                nonNomSignifier.value.SetActive(false); //Set the signifier to false.
                energy.value += eatingTime; //increment energy by the eatingTime.
                source.value.Stop();//Stop the sound
                EndAction(true); //End the action.
            }
        }
    }
}