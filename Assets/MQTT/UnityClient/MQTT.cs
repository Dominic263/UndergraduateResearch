/******************************************************************************
*  This module contains code necessary to instantiate an MQTT Client on the   *
*  Unity end. The M2MQtt client library adapted from Paho helps with this a   *
*  adaptation.                                                                *
*  Last Day Update : December 2 2021                                          *
*  Author          : Ndondo Dominic Tinashe                                   *
******************************************************************************/
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;
using UnityEngine.SceneManagement;


public class MQTT : M2MqttUnityClient
{
    /* Topics to publish from and subscribe to */
    public string [] publish_topics    = {"motorPosition"};
    public string [] subscribe_topics  = {"LightSensor"};
    public Camera ARcam;

    //TO DO : Connect the game objects to the TMP_Text
    public TMP_Text motorVal;
    public TMP_Text sensorVal;

    public Current valueCurrent;
    public Table tb;

    //Control Knob
    [SerializeField] public Transform handle;
    Vector3 mousePos;
    [SerializeField] public Image fill;


    /* Private List of event messages */
    private List<string> eventMessages = new List<string>();

    /*Client Instance is already initialized in the base class*/

    /*
    *  Function: publish() sends a string message from the client to the broker
    *  Input   : input string message and input string topic on broker
    *  Output  : no output (void)
    */
    public void publish(string topic, string message)
    {
        client.Publish(topic, System.Text.Encoding.UTF8.GetBytes(message),
                               MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        Debug.Log(message + " published to topic:" + topic + "\n");

    }

    /* Override function calls base function on connecting to the broker */
    protected override void OnConnecting()
    {
        base.OnConnecting();
        Debug.Log("Connecting to broker on " + brokerAddress + ":"
                                          + brokerPort + "...\n");
    }

    //On push: Add a value to the points table
    public void OnPush(){

        tb.table.Add(valueCurrent.currentMotorPosition);
        tb.table.Add(valueCurrent.currentLightIntensity);
        Debug.Log("Pushing Values " + valueCurrent.currentMotorPosition + " & " + valueCurrent.currentLightIntensity);
    }

    //On Deploy: change the scene and draw the graph.
    public void OnDeploy(){
        //code goes here
         SceneManager.LoadScene("DeployMode");
    }
    /* Override function calls base function once connected to broker */
    protected override void OnConnected()
    {
        base.OnConnected();
        Debug.Log("Connected to broker on " + brokerAddress + "\n");

    }

    /*
    * Function : SubcribeTopics() override function subscribes to topics
    * Input    : list of topics to subscribe to
    * Output   : no output (void)
    */
    protected override void SubscribeTopics()
    {
        //client.Subscribe(new string[] {"motorPosition"}, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        client.Subscribe(new string[] {"LightValue"}, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
    }

    /*
    * Function : UnSubcribeTopics() override function unsubscribes from topics
    * Input    : list of topics to unsubscribe from
    * Output   : no output (void)
    */
    protected override void UnsubscribeTopics()
    {
        for(int i = 0; i < subscribe_topics.Length; i++)
        {
            client.Unsubscribe(new string [] {subscribe_topics[i]});
        }

    }

    /* Message to display when connection fails */
    protected override void OnConnectionFailed(string error_message)
    {
        Debug.Log("Connection Failed! " + error_message + "\n");

    }

    /* Message to display on disconnected */
    protected override void OnDisconnected()
    {
        Debug.Log("Disconnected.\n");

    }

    /* Message displayed on connection lost event */
    protected override void OnConnectionLost()
    {
        Debug.Log("Connection Lost! \n");

    }

    /*
    * Function : DecodeMessage() parses out the message received
    * Input    : Byte of message and the corresponding topic
    * Output   : none
    */
    protected override void DecodeMessage(string topic, byte[] message)
    {
        /* Get string message from the byte array */
        string msg = System.Text.Encoding.UTF8.GetString(message);
        Debug.Log("Received: " + msg + "\n");

        StoreMessage(msg);

        if (topic == "LightValue")
        {
            float value = float.Parse(msg);
            sensorVal.text = value.ToString();
            //sensorVal.text = value.ToString();

            //Update the value of light intensity when a change comes from brokerPort
            valueCurrent.currentLightIntensity = value;
        }
    }

    /* Store the event message */
    private void StoreMessage(string event_msg)
    {
        eventMessages.Add(event_msg);
    }

    /* Process the message received */
    private void ProcessMessage(string msg)
    {
        Debug.Log("Received: " + msg + "\n");
    }

    /* Destroy client */
    private void OnDestroy()
    {
        Disconnect();
    }

    /* START : Called before the first frame */
    protected override void Start()
    {

        ARcam.enabled = true;
        Debug.Log("Ready to Connect. \n");
        base.Start();

    }


    //FUNCTION ; called when a trigger event of drag is initiated
      public void OnHandleDrag(){
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

          fill.fillAmount = degree / 180f;

          motorVal.text = Mathf.Round(degree).ToString();

        }

      }

    /* UPDATE: Called once per frame */
    protected override void Update()
    {

        if(client.IsConnected){

            base.Update(); // call ProcessMqttEvents()
            if (eventMessages.Count > 0)
            {
                foreach (string msg in eventMessages)
                {
                    ProcessMessage(msg);
                }
                eventMessages.Clear();
            }
        }
    }
}
