using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private const float LANE_DISTANCE = 3.0f;
    private const float TURN_SPEED = 0.05f;

    //Function
    private bool isRunning = false;

    // Animation
    private Animator anim;
    
    // Movment
    private CharacterController controller;
    private float jumpForce = 7.0f;
    private float gravity = 12.0f; 
    private float verticalVelocity;
   
    private int desiredLane = 1; // 0 = left, 1 = middle, 2 = right

    //Speed modifier
    private float originalSpeed = 7.0f;
    private float speed = 7.0f;
    private float speedIncreaseLastTick;
    private float speedIncreaseDelay = 2.5f;
    private float speedIncreaseAmount = 0.1f;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!isRunning)
        {
            return;
        }

        if (Time.time - speedIncreaseLastTick > speedIncreaseDelay)
        {
            speedIncreaseLastTick = Time.time;
            speed += speedIncreaseAmount;

            //Change modifier score text
            GameManager.Instance.UpdateModifier(speed - originalSpeed);
        }

        //Gather inputs on which lane we should be
        //Move Left
        if (MobileInputs.Instance.SwipeLeft)
            MoveLane(false);

        //Move Right
        if (MobileInputs.Instance.SwipeRight)
            MoveLane(true);

        //Calculate where we should be in future
        Vector3 targetPostion = transform.position.z * Vector3.forward;
        if (desiredLane == 0)
            targetPostion += Vector3.left * LANE_DISTANCE;
        else if (desiredLane == 2)
            targetPostion += Vector3.right * LANE_DISTANCE;




        // Move Delta
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPostion - transform.position).normalized.x * speed;

        bool isGrounded = IsGrounded();
        
        // Calcualte Y
        if (IsGrounded())
        {
            // if grounded{
            verticalVelocity = -0.1f;
            anim.SetBool("Grounded", isGrounded);

            if (MobileInputs.Instance.SwipeUp)
            {
                // Jump
                anim.SetTrigger("Jump");
                verticalVelocity = jumpForce;
            }else if (MobileInputs.Instance.SwipeDown)
            {
                StartSliding();
                Invoke("StopSliding", 1.0f);
            }
        }
        else
        {
            verticalVelocity -= (gravity * Time.deltaTime);

            // FAST FALL

            if (MobileInputs.Instance.SwipeDown)
            {
                verticalVelocity = -jumpForce;

            }
        }
        moveVector.y = verticalVelocity;
        moveVector.z = speed;

        // Move the Penguin
        controller.Move(moveVector * Time.deltaTime);

        //Debug.Log(desiredLane);

        //Rotate the player in the dir they move
        Vector3 dir = controller.velocity;
        if (dir != Vector3.zero)
        {
            dir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, dir, TURN_SPEED);
        }
        


    }

    private void MoveLane(bool goingRight)
    {
        desiredLane += (goingRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);

    }

    private bool IsGrounded()
    {
        Ray groundRay = new Ray(
            new Vector3(
                controller.bounds.center.x,
                (controller.bounds.center.y - controller.bounds.extents.y) + 0.2f,
                controller.bounds.center.z),
            Vector3.down);
        Debug.DrawRay(groundRay.origin, groundRay.direction, Color.cyan, 1.0f);

        return Physics.Raycast(groundRay, 0.2f + 0.1f);
        
    }

    public void StartRunning()
    {
        isRunning = true;
        anim.SetTrigger("Startrunning");
    }

    private void StartSliding()
    {
        anim.SetBool("Sliding", true);
        controller.height /= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y / 2, controller.center.z);
    }

    private void StopSliding()
    {
        anim.SetBool("Sliding", false);
        controller.height *= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y * 2, controller.center.z);
    }

    /*private void Crash()
    {
        anim.SetTrigger("Death");
        isRunning = false;
        GameManager.Instance.OnDeath();
    }*/

    private void Crash()
    {
        anim.SetTrigger("Death");
        isRunning = false;
        //GameManager.Instance.OnDeath();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (hit.gameObject.tag)
        {
            case "Obstacle":
                Crash();
                break;
        }
    }

}
