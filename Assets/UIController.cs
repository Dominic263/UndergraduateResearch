using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIController : MonoBehaviour
{
  //USER INTERFACE:
  [SerializeField] public Slider slider;
  [SerializeField] public TMP_Text motorValue;
  [SerializeField] public TMP_Text lightValue;

 // MOTOR VALUES
 /*
  [SerializeField] public TMP_Text x1;
  [SerializeField] public TMP_Text x2;
  [SerializeField] public TMP_Text x3;
  [SerializeField] public TMP_Text x4;
  [SerializeField] public TMP_Text x5;
  [SerializeField] public TMP_Text x6;
  [SerializeField] public TMP_Text x7;
  [SerializeField] public TMP_Text x8;
  [SerializeField] public TMP_Text x9;
  [SerializeField] public TMP_Text x10;
  [SerializeField] public TMP_Text x11;
  [SerializeField] public TMP_Text x12;
*/

  [SerializeField] public List<TMP_Text> motorValues;
  [SerializeField] public List<TMP_Text> lightValues;

  [SerializeField] public TMP_Text dataPoints;
  private int data;
  TMP_Text tempText;
  
  public void Start(){
    //Initialize a data set of motor and light values
    //motorValues = new List<TMP_Text>();
    //lightValues = new List<TMP_Text>();
    data = 0;
  }

  // trigger the value of the slider to change when the value changes
  public void OnSliderChange(){
    motorValue.text = slider.value.ToString() + " deg";
  }

  public void OnPush(){
    // push the current data and light readings to the data set

    motorValues[data].text = "" + slider.value.ToString();


    lightValues[data].text = lightValue.text;

    //increment the data points count
    data++;
    dataPoints.text = data.ToString();
  }
}
