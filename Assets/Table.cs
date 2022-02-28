/*
  This script contains a scriptable object filled with
  table value for coordinates on the graph
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Table : ScriptableObject
{

    public float x;
    public float y;


    [SerializeField] public  List<float> table;

    public void Start(){
      table = new List<float>();
    }
}
