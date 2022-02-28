using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphDrawer : MonoBehaviour
{
/*
    [SerializeField] public LineRenderer line;
    [SerializeField] public Transform point;
    [SerializeField] public GameObject graph;
    private Transform newdot;


    private Vector3 spawnLocation;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();

        spawnLocation = new Vector3(point.position.x + 0.1f, point.position.y, point.position.z);
        newdot = spawnDot(spawnLocation);
        drawLine(point, newdot);
    }

    // Update is called once per frame
    void Update()
    {
      drawLine(point, newdot);
    }


    private void drawLine(Transform startPosition, Transform endPosition){

        Debug.Log(startPosition);
        Debug.Log(endPosition);
        //Instantiate a new line renderer gameObject
        LineRenderer l = gameObject.AddComponent<LineRenderer>();
        //LineRenderer l = Instantiate(line, graph.transform.position, Quaternion.identity);
        l.transform.SetParent(graph.transform, true);

        //create an array of points from the two transforms - start and end
        List<Vector3> pos = new List<Vector3>();
        pos.Add(startPosition.position);
        pos.Add(endPosition.position);

        //l.localScale = new Vector3(0.015f, 0.015f, 0.015f);
        //establish the width of the line
        l.startWidth = 0.01f;
        l.endWidth = 0.01f;
        l.transform.localScale = point.localScale;
        //set positions of the line and draw it
        l.SetPositions(pos.ToArray());
        l.useWorldSpace = true;
    }

    private Transform spawnDot(Vector3 position){
      Transform new_dot = Instantiate(point, new Vector3(position.x, position.y, position.z), Quaternion.identity);
      new_dot.SetParent(graph.transform, true);
      new_dot.localScale = point.localScale;
      return new_dot;
    }
  */

  //This is a test

  [SerializeField] private Transform[] points;
  [SerializeField] private LineController line;

  private void Start() {
    line.SetUpLine(points);
  }
}
