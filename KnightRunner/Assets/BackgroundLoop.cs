using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    //this script is for parallax scrolling

    public GameObject[] levels; //create an array of levels for background layers
    private Camera mainCamera;  //create a reference to our main Camera 
    private Vector2 screenBounds; //store the boundaries of the camera

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>(); //define main camera to the object that is holding the script
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        foreach(GameObject obj in levels)
        {
            loadChildObject(obj); //goes through list of sprites and executes loadChildObect function for each sprite in the list
        }
    }

    void loadChildObject(GameObject obj)
    {
        float objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x; //gives us the horizontal value of the boundary box of the sprite
        int childsNeeded = (int)Mathf.Ceil(screenBounds.x * 2 / objectWidth); //to fill the screen with the sprites based on the width of the screen (cast to int and round up using Mathf.Ceil() to make sure there are enough clones to fill the screen)
        GameObject clone = Instantiate(obj) as GameObject;
        for(int i = 0; i < childsNeeded; i++)
        {
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(objectWidth * i, obj.transform.position.y, obj.transform.position.z);  //to space out the clones evenly
            c.name = obj.name + i; //to keep the naming clean for the clones
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
