using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions
{

    public class bambooSpawner : ActionTask
    {

        public BBParameter<GameObject> bambooSpawn;//gameobject spawner for bamboo

        protected override string OnInit()
        {
            return null; // No need for manual initialization
        }

        protected override void OnUpdate()
        {
          
            
                GameObject prefab = Resources.Load<GameObject>("Prefabs/Bamboo"); //goes through Resources folder and finds prefabs titled bamboo.

       
                Vector3 spawnerLocation = new Vector3(Random.Range(-1.70f, -20f), 5.5f, Random.Range(-3.80f, -20.65f));//randomizes spawn location
                bambooSpawn.value = GameObject.Instantiate(prefab, spawnerLocation, Quaternion.identity);//spawns bamboo 
                   
                


                EndAction(true);//Exits state
            }
        }
    }


