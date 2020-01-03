using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public float deltaRotation = 30f; // velocidad de movimiento
    public float deltaMovement = 10f;  // velocidad de movimiento
    // IMPORTANTE: public en una variable global nos permite verlo desde el inspector de Unity. 

    public Color initColor; 

    // Start is called before the first frame update
    void Start() {
        GetComponent<Renderer>().material.color = initColor; 
    }

    // Update is called once per frame
    void Update() {
        rotate();
        move();
    }
    void rotate()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0f, -deltaRotation, 0f) * Time.deltaTime); 
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(new Vector3(0f, deltaRotation, 0f) * Time.deltaTime); 
        }
    }

    void move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * deltaMovement * Time.deltaTime);

        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * deltaMovement * Time.deltaTime); 
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * deltaMovement * Time.deltaTime); 
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * deltaMovement * Time.deltaTime); 
        }
    }

}
