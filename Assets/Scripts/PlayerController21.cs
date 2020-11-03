using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController21 : MonoBehaviour
{
    //Declaration Only - start
    bool isOnGround;
    float speed = 16.0f;
    float zLimit = 20.0f;
    float jumpForce = 10.0f;
    float gravityModifier = 2.0f;

    Rigidbody playerRb;
    Renderer playerRdr;

    public Material[] playerMtrs;

    //Declaration Only - end

    // Start is called before the first frame update
    void Start()
    {
        //Initialization - start
        isOnGround = true;
        Physics.gravity *= gravityModifier;

        playerRb = GetComponent<Rigidbody>();
        playerRdr = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        //move player (GameObject) according to user input
        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);

        //Boundary Limit for Down 
        if (transform.position.z < -zLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zLimit);
            playerRdr.material.color = playerMtrs[0].color;
        }
        //Boundary Limit for Up
        else if (transform.position.z > zLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zLimit);
            playerRdr.material.color = playerMtrs[1].color;
        }
        //Boundary Limit for Left
        if (transform.position.x < -zLimit)
        {
            transform.position = new Vector3(-zLimit, transform.position.y, transform.position.z);
            playerRdr.material.color = playerMtrs[2].color;
        }
        //Boundary Limit for Right
        else if (transform.position.x > zLimit)
        {
            transform.position = new Vector3(zLimit, transform.position.y, transform.position.z);
            playerRdr.material.color = playerMtrs[3].color;
        }
        
        PlayerJump();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Listen for collision with the GamePlane TAG
        if (collision.gameObject.CompareTag("GamePlane"))
        {
            isOnGround = true;

            //RedColor Material
            playerRdr.material.color = playerMtrs[1].color;
        }
    }

    private void PlayerJump()
    {
       if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;

            //BlueColor Material
            playerRdr.material.color = playerMtrs[0].color;
        }
    }
}
