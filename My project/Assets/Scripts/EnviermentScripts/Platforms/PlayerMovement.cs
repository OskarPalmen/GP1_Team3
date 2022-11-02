using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody theRG;
    public float jumpForce;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        theRG = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        theRG.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, theRG.velocity.y, Input.GetAxis("Vertical") * moveSpeed); 

    }

    private void FixedUpdate()
    {
        if (!isGrounded)
        {
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            theRG.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }
    // This will not make player do any Air jump
    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

}
