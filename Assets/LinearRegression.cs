/*******************************************************************************
 Linear Regression  Module
 Date: Jan 22 2022
 Author: Dominic Ndondo
 Description:
 This module provides the capability to plot a curve based on a Simple Linear
 Regression model.
 ******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinearRegressionModel
{
  public class LinearRegression : MonoBehaviour
  {

      //CONSTRUCTOR
      public LinearRegression(){
        data = new List<Point>();
      }

      public float slope     = 0;
      public float intercept = 0;

      //a point on a graph consists of an x and y coordinate
      public struct Point {
        public float x;
        public float y;
      };


      /*array will hold points to be used in the curve plotting*/
      public List<Point> data;
      public float n;

      /*
      *  Function: This function creates a point with x and y coordinates and adds
      *            it to the data ArrayList.
      *  Input : coordinates (float) x and y
      *  Output: None (void function)
      */
      public void CreatePoint(float x, float y){
         Point val = new Point();
         val.x     = x;
         val.y     = y;

         data.Add(val);
      }




      /*
      Helper function to calculate the total sum of values in a list
      Input: a float list
      Output: float total sum
      */
      public float SumOf(List<float> values){
        float sum = 0;

        for (int i = 0; i < values.Count; i++){
          sum = sum + values[i];
        }

        return sum;
      }


      //SETTERS --> For Test Functions
      public float TestCallSumOf(List<float> list){
        return SumOf(list);
      }

      public void TestCallGetCurve(){
        //GetCurve();
      }




  }
}
