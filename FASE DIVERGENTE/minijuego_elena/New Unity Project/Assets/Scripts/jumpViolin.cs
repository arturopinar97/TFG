using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpViolin : MonoBehaviour
{
public float minRotationSpeed = 80.0f;
    bool jumpingViolin = false;
	
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
		if(jumpingViolin){
			rb.AddForce(new Vector3(0, 3, 0), ForceMode.Impulse);
		}
    }
	 void JumpViolin() {
			jumpingViolin =! jumpingViolin;
}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && isGrounded && !jumpingViolin)
        {
			JumpViolin();
            rb.AddForce(new Vector3(0, 3, 0), ForceMode.Impulse);
            isGrounded = false;
        }
		else if (Input.GetKeyDown(KeyCode.V) && isGrounded && jumpingViolin)
        {
			JumpViolin();
            rb.AddForce(new Vector3(0, 0, 0), ForceMode.Impulse);
            isGrounded = false;
        }
		
    }
}