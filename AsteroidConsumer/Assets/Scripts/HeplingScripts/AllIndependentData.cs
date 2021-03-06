﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimB
{
    public class AllIndependentData : MonoBehaviour
    {

        public static AllIndependentData instance;

        //Camera
        public float cameraXWidth;
        public float cameraYHeight;
        // Use this for initialization
        void Start()
        {
            instance = instance ?? this;
            StartingInitiation();
        }
        private void StartingInitiation()
        {
            GetCameraSize();
        }

        #region //Camera
        public void GetCameraSize()
        {
            Camera camera = Camera.main;
            float halfHeight = camera.orthographicSize;
            cameraYHeight = halfHeight;
            cameraXWidth = camera.aspect * halfHeight;

        }
        #endregion

        // Update is called once per frame
        void Update()
        {

        }
    }
}
