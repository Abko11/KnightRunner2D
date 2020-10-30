using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;  //references the CharacterController2D script we have already made for our player

    float horizontalMove = 0f;  //variable for horizontal movement (initialize to 0)
    public float runSpeed = 40f;   //variable to hold run speed (set to public so that we can see/adjust it in the Unity inspector)

    bool jump = false;  //create a variable for jump (initially set flag to false since we don't want to always be jumping)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;  //Input class is for user input such as pressing arrow keys while "Horizontal specifies a value 1 or -1 based on which direction the player moves in (1 for right, -1 for left)
                                                                     //multiply by runSpeed so that it can factor into the player movement based on the input

        if (Input.GetButtonDown("Jump")) //checking to see if user presses jump button
        {
            jump = true;  //if jump key is pressed, jump
        }
    }

    private void FixedUpdate()  //updates a fixed amount of times per second (this is what we want to use to move our character)
    {
        //move our character
        //FixedUpdate is where the input actually gets applied to the character  (Time.fixedDeltaTime is good to use to multiply horizontalMove by)
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);  //the Move() function takes 3 parameters (2nd one is for whether we want to crouch and 3rd one is for whether we want to jump)
        jump = false;  //set jump back to false so we don't keep jumping forever
    }
}
