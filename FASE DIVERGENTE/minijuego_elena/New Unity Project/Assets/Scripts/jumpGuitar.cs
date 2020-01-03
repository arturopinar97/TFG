using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpGuitar : MonoBehaviour
{
public float minRotationSpeed = 80.0f;
    bool jumpingGuitar = false;
	
    public bool isGrounded;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == ("Ground") && isGrounded == false)
        {
            isGrounded = true;
        }
		if(jumpingGuitar){
			rb.AddForce(new Vector3(0, 3, 0), ForceMode.Impulse);
		}
    }
	 void JumpGuitar() {
			jumpingGuitar =! jumpingGuitar;
}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && isGrounded && !jumpingGuitar)
        {
			JumpGuitar();
            rb.AddForce(new Vector3(0, 3, 0), ForceMode.Impulse);
            isGrounded = false;
        }
		else if (Input.GetKeyDown(KeyCode.G) && isGrounded && jumpingGuitar)
        {
			JumpGuitar();
            rb.AddForce(new Vector3(0, 0, 0), ForceMode.Impulse);
            isGrounded = false;
        }
		
    }
}