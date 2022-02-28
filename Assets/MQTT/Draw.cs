using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    //Access to the graph game object
    public GameObject graph;
    public Transform dot;
    [SerializeField] private GameObject target;

    //Initialize center coordinates for the position of the graph
    Vector3 startPos = new Vector3(440.0f, 160.0f, 2.0f);

    //Initialize the size of the point on the graph
    //Vector3 dotScale = new Vector3(0.015f, 0.015f, 0.015f);

    //Threshold Values To MAP X and Y positions from input data
    float xMin = -0.404f;
    float xMax = 0.482f;

    //This is the potential range in which an input x value might lie
    float xInputMin = 0;
    float xInputMax = 180.0f;

    float yMin = -0.35f;
    float yMax = 0.494f;

    //This is the potential range in which an input y value might lie
    float yInputMin = 0;
    float yInputMax = 180.0f;

    //float constantAxis = 0.84f;


    //Calibrating the center
    float width = 1.0f;
    float height = 1.0f;

    private int count = 0;
    private Transform point1;
    private Transform point2;
    // Start is called before the first frame update
    void Start()
    {

      //Vector3 pos = new Vector3(-0.406f,0.84f, -0.349f);
      //Vector3 pos2 = new Vector3(mapX(150), mapY(150), constantAxis);

      //Transform point2 = spawnDot(pos2);
      //DrawLine(point1, point2);

    }


    // Update is called once per frame
    void Update()
    {

        //To Do
        float x = graph.transform.position.x;
        float y = graph.transform.position.y;
        float z = graph.transform.position.z;


        //Transform point1 = spawnDot(getOrigin(graph));
          if (count == 0){
            Debug.Log("The x: " + dot.position + " + 0.45f  = " + dot.position.x + 0.45f);
            Debug.Log(-.035f);

            point1 = spawnDot(new Vector3(-.035f,dot.position.y,dot.position.z));
            point1.SetParent(graph.transform, true);
            //point1.localScale = dotScale;
            count ++;
          }
          /*
          else if( count == 1){
            point2 = spawnDot(new Vector3(0.216f,0.8392f,-0.072f));
            point2.SetParent(graph.transform, true);
            count ++;
          }
          */
          //count++;

          DrawLine(dot, point1);
        //point1.position.x = x;


    }



    //Get the origin's x value in 3D space
    Vector3 getOrigin(GameObject graph){

      Vector3 origin = new Vector3((graph.transform.position.x - width/2), graph.transform.position.y,(graph.transform.position.z - height/2));
      return origin;
    }
     //map an input value into a corresponding X value works any input
     float mapX(float inputX){
       return (xMax - xMin) * (inputX - xInputMin) / (xInputMax - xInputMin) + xMin;
     }

     //map an input value into a corresponding X value works any input
     float mapY(float inputY){
       return ((yMax - yMin) * (inputY - yInputMin) / (yInputMax - yInputMin) + yMin);
     }

    //Spawns a co-ordinate position on the graph
    private Transform spawnDot(Vector3 position){
      Debug.Log(dot.localScale.x);
      //dot.localScale = dotScale;
      Transform new_dot = Instantiate(dot, new Vector3(position.x, position.y, position.z), Quaternion.identity);
      new_dot.localScale = dot.localScale;
      return new_dot;
    }

    private void DrawLine(Transform startPosition, Transform endPosition){

        //Instantiate a new line renderer gameObject
        LineRenderer l = gameObject.AddComponent<LineRenderer>();
        l.transform.SetParent(target.transform, true);

        //create an array of points from the two transforms - start and end
        List<Vector3> pos = new List<Vector3>();
        pos.Add(startPosition.position);
        pos.Add(endPosition.position);

        //l.localScale = new Vector3(0.015f, 0.015f, 0.015f);
        //establish the width of the line
        l.startWidth = 0.50f;
        l.endWidth = 0.50f;
        //set positions of the line and draw it
        l.SetPositions(pos.ToArray());
        l.useWorldSpace = true;
    }
}
