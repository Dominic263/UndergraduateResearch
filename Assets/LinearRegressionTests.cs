/*******************************************************************************
 Linear Regression Tests Module
 Date: Jan 22 2022
 Author: Dominic Ndondo
 Description:
 This module provides the testing capability for functions used to plot a curve
 in the Linear Regression Module
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LinearRegressionModel;

public class LinearRegressionTests: LinearRegression
{
    // Start is called before the first frame update
    void Start()
    {
      CreatePointsTest();

      //PrintPoints test
      PrintPointsTest();

      //SumOf Test
      SumOfTest();

      //Get Curve Tests
      GetCurveTest();

    }

    /*Unit Test Functions for Linear Regression Class*/

    /*
    * Test: Print out points initialized
    * Input: none
    * Output: void function
    */
    private void PrintPointsTest(){
      Debug.Log("Print Out Points Test");
      for( int i = 0; i < data.Count; i++){
        Debug.Log("x " + data[i].x + ",y" + data[i].y);
      }
    }

    /*
    * Test:
    * Input: none
    * Output: void function
    */
    private void CreatePointsTest(){
      Debug.Log("Create Points Test");
      //Create points of motor position against light sensor values
      CreatePoint(43.0f, 99.0f);
      CreatePoint(21.0f, 65.0f);
      CreatePoint(25.0f, 79.0f);
      CreatePoint(42.0f, 75.0f);
      CreatePoint(57.0f, 87.0f);
      CreatePoint(59.0f, 81.0f);
      //CreatePoint(247f, 486.0f);

      //PrintPoints test helper
      PrintPointsTest();
    }


    public List<float> vals = new List<float>();
    /*
    * Test: sums up values in a list
    * Input: none
    * Output: void function
    */
    private void SumOfTest(){
      Debug.Log("Sum Of List Test");


      vals.Add(0.1f);
      vals.Add(2.3f);
      vals.Add(5.9f);
      //changed protection level to private therefore need an accessor function
      //to call  SumOf
      float sum = TestCallSumOf(vals);

      Debug.Log("Sum = " + sum);
    }

    /*
    Test  : Get the slope and the intercept to the curve being created
    Input : none
    Output: none
    */
    private void GetCurveTest(){
      Debug.Log("Get Curve Test");
      //protection level to be changed to public innorder to use function
      TestCallGetCurve();
      Debug.Log("n: " + n + " slope: " + slope + ", intercept: " + intercept);
    }




    // Update is called once per frame
    void Update()
    {

    }
}
