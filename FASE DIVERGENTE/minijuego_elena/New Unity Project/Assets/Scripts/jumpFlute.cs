using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpFlute : MonoBehaviour
{
    public float minRotationSpeed = 80.0f;
    bool jumpingFlute = false;
	
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
		if(jumpingFlute){
			rb.AddForce(new Vector3(0, 3, 0), ForceMode.Impulse);
		}
    }
	 void JumpFlute() {
			jumpingFlute =! jumpingFlute;
}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isGrounded && !jumpingFlute)
        {
			JumpFlute();
            rb.AddForce(new Vector3(0, 3, 0), ForceMode.Impulse);
            isGrounded = false;
        }
		else if (Input.GetKeyDown(KeyCode.F) && isGrounded && jumpingFlute)
        {
			JumpFlute();
            rb.AddForce(new Vector3(0, 0, 0), ForceMode.Impulse);
            isGrounded = false;
        }
		
    }
}