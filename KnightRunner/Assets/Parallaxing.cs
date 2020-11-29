using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] backgrounds;  //array of all the back and foregrounds to be parallaxed
    private float[] parallaxScales;  //the proportion of the camera's movement to move the backgrounds by
    public float smoothing = 1f; //how smooth the parallax is going to be (set to 1 by default) (make sure to set this ABOVE zero)

    private Transform cam; //reference to the main camera's transform
    private Vector3 previousCamPosition;   //the position of the camera in the previous frame
    
    //Awake is called before Start() Great for references
    private void Awake()
    {
        //set up the camera reference
        cam = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        //the previous frame had the current frame's camera position when starting out    
        previousCamPosition = cam.position;

        parallaxScales = new float[backgrounds.Length];

        for(int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;  //we are making sure that our parallaxScales array is just as long as our backgrounds array, then we are going through each background and are assigning that background's z-position to the corresponding parallax scale
        }
    }

    // Update is called once per frame
    void Update()
    {
        //for each background
        for(int i = 0; i < backgrounds.Length; i++)
        {
            //the parallax is the opposite of the camera movement because the previous frame multiplied by the scale
            float parallax = (previousCamPosition.x - cam.position.x) * parallaxScales[i];  //the parallax effect should be the difference of what ar camera position was before and what it is currently (how much it has moved) multiplied by how much we want the parallaxing to be (the scale)

            //set a target exposition which is the current position + the parallax
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;  //taking the parallaxing and applying it to an actual position

            //create a target position which is the background's current position with its target x-position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            //fade between current position and the target position using Lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        //set the previousCamPosition to the camera's position at the end of the frame
        previousCamPosition = cam.position;
    }
}
