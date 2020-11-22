using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2DFollow : MonoBehaviour
{
    public Transform target;
    public float damping = 1f;
    public float lookAheadFactor = 3f;  //will keep the camera looking 3 units ahead of the player so they can see in front of them better
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;

    float offsetZ;
    Vector3 lastTargetPosition;
    Vector3 currentVelocity;
    Vector3 lookAheadPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        lastTargetPosition = target.position;
        offsetZ = (transform.position - target.position).z;
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        //only update lookAheadPosition if accelerating or changed direction
        float xMoveDelta = (target.position - lastTargetPosition).x;
        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

        if(updateLookAheadTarget)
        {
            lookAheadPosition = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
        }
        else
        {
            lookAheadPosition = Vector3.MoveTowards(lookAheadPosition, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
        }

        Vector3 aheadTargetPosition = target.position + lookAheadPosition + Vector3.forward * offsetZ;
        Vector3 newPosition = Vector3.SmoothDamp(transform.position, aheadTargetPosition, ref currentVelocity, damping);

        transform.position = newPosition;
        lastTargetPosition = target.position;
    }
}
