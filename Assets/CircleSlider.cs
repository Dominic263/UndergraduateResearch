using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CircleSlider : MonoBehaviour
{
  [SerializeField] public Transform handle;
  [SerializeField] public Text value;
  Vector3 mousePos;
  [SerializeField] Image fill;

  public Current current;

  //FUNCTION ; called when a trigger event of drag is initiated
    public void OnHandle(){
      Debug.Log("Drag!");

      mousePos = Input.mousePosition;
      Vector2 dir = mousePos - handle.position;

      float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
      angle = (angle <= 180f) ? (180f + angle) : angle;

      if (angle <= 0f || angle >= 180f){
        Quaternion r = Quaternion.AngleAxis(angle , Vector3.forward);
        handle.rotation = r;

        float degree = r.z * 180f;
        Debug.Log(degree);

        //change the current motor position when the slider changes
        current.currentMotorPosition = degree;

        fill.fillAmount = degree / 180f;

        value.text = Mathf.Round(degree) + "";

      }
      Debug.Log("Drag");
    }

}
