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
        public BBParameter<GameObject> closestBamboo; // Store result in Blackboard
        public BBParameter<GameObject> nonNomSignifier;
        public BBParameter<AudioSource> source;
        public BBParameter<AudioClip> clip;
        private float eatingTime;
        public BBParameter<float> energy;
        protected override string OnInit()
        {
            source.value = agent.gameObject.GetComponent<AudioSource>();
          return base.OnInit();
        }
        protected override void OnExecute()
 
        {

            eatingTime = 0;
            return;
        }

        protected override void OnUpdate()
        {
           
            eatingTime += Time.deltaTime;
            source.value.clip = clip.value;
            source.value.volume = 1; source.value.pitch = 1;
            if (!source.value.isPlaying)
            {
                source.value.PlayOneShot(clip.value, 1.0f);
            }
            if (!nonNomSignifier.value.active)
            {
                nonNomSignifier.value.SetActive(true);
            }


            if (eatingTime > 4)
            {
                GameObject.Destroy(closestBamboo.value);
                nonNomSignifier.value.SetActive(false);
                energy.value += eatingTime;
                source.value.Stop();
                EndAction(true);
            }
        }
    }
}