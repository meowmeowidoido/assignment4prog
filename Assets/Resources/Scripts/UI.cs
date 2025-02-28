using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    // Start is called before the first frame update

    public Blackboard pandaBlackboard;
    public Slider slider;
    float energyValue;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        energyValue = pandaBlackboard.GetVariable<float>("pandaEnergy").value;
        slider.value = energyValue;
        Debug.Log(energyValue);
    }
}
