/*******************************************************************************
 Graph Module
 Date: Jan 22 2022
 Author: Dominic Ndondo
 Description:
 This module provides the capability to plot a curve based on a Simple Linear
 Regression model.
 ******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LinearRegressionModel;
using UnityEngine.UI;
using TMPro;

public class Graph : LinearRegression
{

  public MQTTObject clientObject;
  //table to be filled with data
   public Table points;
   //public float currentMotorPosition;
   //public float currentLightIntensity;

   public int count;
   [SerializeField] public Transform sphere1;
   [SerializeField] public Transform sphere2;

    public LineRenderer line;

    public Transform pos1;
    public Transform pos2;

    public Transform target;
    private Vector3 final;
    private Vector3 orig;



    private float xMax;
    private float xMin;
    private float yMin;
    private float yMax;

    public float maxMotorPosition;
    public float maxLightIntensity;

    /*
    * Function     : Draws a line connecting two coordinates
    * Input        : none
    * Output       : none (void)
    * Pre-condition: Data has already been entered
    */
    private void Draw()
    {


      line.SetPosition(0, sphere1.position);
      line.SetPosition(1, sphere2.position);
      Debug.Log("Drawing");

    }


    /*
    * Function : calibrate a new location for a coordinate on the graph
    * Input    : float motorPosition and float light
    * Output   : Returns a Vector3 location on graph
    */
   Vector3 GetLocation(float motorPosition, float light)
   {


        Debug.Log("Entering loop with values , motor = " + motorPosition + " and light = " + light);

        float tempDiv = motorPosition/maxMotorPosition;
        Debug.Log("Temporary Div" + tempDiv);
        Debug.Log("Xmax = " + xMax.ToString("F4"));
        Debug.Log("XmIN = " +  xMin.ToString("F4"));
         float xCoordinate = tempDiv*(xMax - xMin) ;

         Debug.Log("X Coodinate" + xCoordinate.ToString("F4"));
         //;
         //float tempSubtract = xMax - xMin;
         //float tempProduct = tempSubtract * tempDiv;
        /*
         Debug.Log("My value for pos/max is " + tempDiv);
         Debug.Log("My value for max - min is " + tempSubtract);
         Debug.Log("My value for product is " + tempProduct);
         Debug.Log("My value for x is " + xCoordinate);
        */

         float yCoordinate = (light/maxLightIntensity)*(yMax - yMin) ;

         Debug.Log("x = " + xCoordinate + "const(y) = " + sphere1.position.y + "z = " + yCoordinate);
         float xvalue = xCoordinate + sphere1.position.x;
         float yvalue = sphere1.position.y;
         float zvalue = yCoordinate + sphere1.position.z;
         return new Vector3( xCoordinate , yvalue, yCoordinate);
  }

    /*
    * This function spawns a point on the Graph
    * Returns a Transform (to give us access to position)
    */
   private Transform SpawnPoint(Vector3 position)
   {
        Debug.Log(position);
        Debug.Log("x position in spawn point is " + position.x.ToString("F4"));
        Transform pos = Instantiate(sphere1, new Vector3(sphere1.position.x + position.x,
                        sphere1.position.y, sphere1.position.z + position.z), Quaternion.identity);
        pos.SetParent(target, true);
        //pos.localScale = sphere1.localScale;

        return pos;
   }


   //using values added to the table. add points on the graph
   public void DrawGraphPoints(){

     //first value is x (motor pos) second value is y light intensity
     for(int i = 0; i < points.table.Count - 2; i += 2) {
       Vector3 newVector = GetLocation(points.table[i],points.table[i+1]);
       Transform spawn = SpawnPoint(newVector);
     }

     //Draw two points from a regression line
     GetCurve();


     //assign new position to sphere1 and sphere2

     sphere1.position = new Vector3(sphere1.position.x + orig.x, sphere1.position.y, sphere1.position.z + orig.z);

     sphere2.position = new Vector3(sphere1.position.x + final.x, sphere1.position.y, sphere1.position.z + final.z);


   }


   /*
   * GetCurve
   * Function: Calculates the slope and intercept values of a graph
   * Input   : none
   * Output  : none (void function)
   */
   public void GetCurve(){
     //create lists of the variables in the simple regression formula
     List<float> x  = new List<float>();
     List<float> y  = new List<float>();
     List<float> x2 = new List<float>();
     List<float> y2 = new List<float>();
     List<float> xy = new List<float>();

     //populate the variable lists with values
     for (int i = 0; i < points.table.Count-1; i += 2){
       x.Add(points.table[i]);
       y.Add(points.table[i+1]);
       x2.Add(x[i] * x[i]);
       y2.Add(y[i] * y[i]);
       xy.Add(y[i] * x[i]);
     }

     //get the n value count and calculate slope
     n          = points.table.Count/2;
     intercept  = (SumOf(y) * SumOf(x2) - SumOf(x) * SumOf(xy))/
                  (n * SumOf(x2) - (SumOf(x) * SumOf(x)));
     slope      = (n * SumOf(xy) - SumOf(x) * SumOf(y))/
                  (n * SumOf(x2) - SumOf(x) * SumOf(x));
     Debug.Log("Done Getting curve.");

     //line is drawn from the origin first then
     float ycoord = slope * maxMotorPosition + intercept;
     final  = GetLocation(maxMotorPosition, ycoord);
     orig = GetLocation(0f, 0f);
   }

    // Start is called before the first frame update
    void Start()
    {
      maxMotorPosition = 200f;
      maxLightIntensity = 200f;
      xMin = -0.03495f;
      xMax = 0.0472f;
      yMin = 0.0135f;
      yMax = 0.0858f;
      count = 0;
      // Spawn points on the graph from the coordinates in the Table
      Vector3 vector = GetLocation(195f, 195f);
      Transform spawn = SpawnPoint(vector);
      DrawGraphPoints();

    }

    // Update is called once per frame
    void Update()
    {
      //Debug.Log("Printing out the values from table");
/*
      for(int i = 0; i < points.table.Count; i++){
        Debug.Log(points.table[i]);
      }
*/
      //points[i] = SpawnPoint(GetLocation(data[i].x, data[i].y));

        Draw();


    }
}
