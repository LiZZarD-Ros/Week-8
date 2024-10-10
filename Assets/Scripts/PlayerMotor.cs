using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private const float LANE_DISTANCE = 3.0f;

    // Movment
    private CharacterController controller;
    private float jumpForce = 4.0f;
    private float gravity = 12.0f; 
    private float verticalVelocity;
    private float speed = 7.0f;
    private int desiredLane = 1; // 0 = left, 1 = middle, 2 = right

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Gather the inputs on which lanw
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            MoveLane(false);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            MoveLane(true);
        }

        //Calculate where we should be in future
        Vector3 targetPostion = transform.position.z * Vector3.forward;
        if (desiredLane == 0)
            targetPostion += Vector3.left * LANE_DISTANCE;
        else if (desiredLane == 2)
            targetPostion += Vector3.right * LANE_DISTANCE;




        // Move Delta
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPostion - transform.position).normalized.x * speed;
        moveVector.y = -0.1f;
        moveVector.z = speed;

        // Move the Penguin
        controller.Move(moveVector * Time.deltaTime);

        Debug.Log(desiredLane);

    }

    private void MoveLane(bool goingRight)
    {
        desiredLane += (goingRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);

    }



}
