using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpPiano : MonoBehaviour
{
public float minRotationSpeed = 80.0f;
    bool jumpingPiano = false;
	
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
		if(jumpingPiano){
			rb.AddForce(new Vector3(0, 3, 0), ForceMode.Impulse);
		}
    }
	 void JumpPiano() {
			jumpingPiano =! jumpingPiano;
}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && isGrounded && !jumpingPiano)
        {
			JumpPiano();
            rb.AddForce(new Vector3(0, 3, 0), ForceMode.Impulse);
            isGrounded = false;
        }
		else if (Input.GetKeyDown(KeyCode.P) && isGrounded && jumpingPiano)
        {
			JumpPiano();
            rb.AddForce(new Vector3(0, 0, 0), ForceMode.Impulse);
            isGrounded = false;
        }
		
    }
}