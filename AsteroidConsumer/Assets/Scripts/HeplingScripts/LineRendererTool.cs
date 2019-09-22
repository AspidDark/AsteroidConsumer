using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererTool : MonoBehaviour {


    LineRenderer lineRenderer;
    float counter;
    float distance;

    public Transform origin;
    public Transform destination;

    public float lineDrawSpeed;
	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.startWidth = .5f;
        lineRenderer.endWidth = .5f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
