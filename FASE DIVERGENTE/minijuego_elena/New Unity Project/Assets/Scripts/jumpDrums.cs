using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpDrums : MonoBehaviour
{
  public float minRotationSpeed = 80.0f;
    bool jumpingDrums = false;
	
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
		if(jumpingDrums){
			rb.AddForce(new Vector3(0, 3, 0), ForceMode.Impulse);
		}
    }
	 void JumpDrums() {
			jumpingDrums =! jumpingDrums;
}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && isGrounded && !jumpingDrums)
        {
			JumpDrums();
            rb.AddForce(new Vector3(0, 3, 0), ForceMode.Impulse);
            isGrounded = false;
        }
		else if (Input.GetKeyDown(KeyCode.D) && isGrounded && jumpingDrums)
        {
			JumpDrums();
            rb.AddForce(new Vector3(0, 0, 0), ForceMode.Impulse);
            isGrounded = false;
        }
		
    }
}