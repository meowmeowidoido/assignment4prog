using NodeCanvas.Framework;
using NodeCanvas.Tasks.Actions;
using ParadoxNotion.Design;
using UnityEngine;
using Unity.VisualScripting;
using UnityEditor;

namespace NodeCanvas.Tasks.Conditions {

	public class receiveClick : ConditionTask {
	 
	
		public bool clicked;
        public BBParameter<float> pandaEnergy;
        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit(){
			return null;
		}
		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck() {
		
		


			if( Input.GetMouseButton(0)) //Checks whether the mouse was clicked
			{
				Debug.Log("CLICKED!");

                return true; //returns true
            }
            return false;
			
        }

      
	}
}