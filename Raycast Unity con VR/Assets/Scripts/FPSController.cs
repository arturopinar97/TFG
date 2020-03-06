﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public Camera camera;

    public float horizontalSpeed; // Velocidad de rotacion con el raton eje X.
    public float verticalSpeed; // Velocidad de rotacion con el raton eje Y.

    //  variables auxiliares para el movimiento del raton
    float h; 
    float v;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        changeCamera();
        move();
    }
    void changeCamera()
    {
        h = horizontalSpeed * Input.GetAxis("Mouse X");
        v = verticalSpeed * Input.GetAxis("Mouse Y");

        transform.Rotate(0, h, 0);
        camera.transform.Rotate(-v, 0, 0);
    }

    void move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, 0.05f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -0.05f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-0.05f, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(0.05f, 0, 0);
        }
    }

}