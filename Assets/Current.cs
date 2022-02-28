using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Current : ScriptableObject
{
  
    public float currentMotorPosition;
    public float currentLightIntensity;

    public void OnEnable() => currentMotorPosition = currentLightIntensity = 0f;
}
