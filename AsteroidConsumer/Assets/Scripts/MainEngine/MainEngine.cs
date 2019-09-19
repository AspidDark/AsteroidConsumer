using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEngine : MonoBehaviour {

    public static MainEngine instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        AwakeInitiation();

    }

    void Start () {
        StartingInitiation();
	}

    private void FixedUpdate()
    {
        
    }

    void Update () {
		
	}





    private void AwakeInitiation()
    {

    }


    private void StartingInitiation()
    {

    }
}
