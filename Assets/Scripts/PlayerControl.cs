using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;

    public float forwardSpeed;
    public float maxSpeed;
    public float laneDistance;
    public float laneChangeSpeed;
    private int desiredLane = 1; // 0 = Left, 1 = Middle, 2 = Right

    public bool isSliding = false;
    public float jumpForce;
    public float gravity = -20f;
    public Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //START GAME
        if(!PlayerManager.isGameStarted)
            return;

        //SPEED    
        if(forwardSpeed < maxSpeed)
            forwardSpeed += 0.1f * Time.deltaTime;


        //ANIMATION
        animator.SetBool("isGameStarted", true);
        animator.SetBool("isGrounded", controller.isGrounded);


        direction.z = forwardSpeed;

        //JUMP
        if (controller.isGrounded)
        {
            direction.y = -1f;
            if (SwipeManager.swipeUp)
            {
                Jump();
            }
        }else
        {
            direction.y += gravity * Time.deltaTime;
        }

        // Right
        if (SwipeManager.swipeRight)
        {
            desiredLane++;

            if (desiredLane > 2)
                desiredLane = 2;
        }

        // Left
        if (SwipeManager.swipeLeft)
        {
            desiredLane--;

            if (desiredLane < 0)
                desiredLane = 0;
        }

        if (SwipeManager.swipeDown && !isSliding)
        {
            StartCoroutine(Slide());
        }

        float targetX = (desiredLane - 1) * laneDistance;

        // Smooth
        float newX = Mathf.Lerp(
            transform.position.x,
            targetX,
            laneChangeSpeed * Time.deltaTime
        );

        float horizontalMovement = newX - transform.position.x;

        controller.Move(
            new Vector3(
            horizontalMovement,
            direction.y * Time.deltaTime,
            direction.z * Time.deltaTime
            )
        );
    }

    private void Jump()
    {
        if (controller.isGrounded)
        {
            direction.y = jumpForce;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);
        yield return new WaitForSeconds(0.25f/ Time.timeScale);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;

        yield return new WaitForSeconds(1.3f);

        animator.SetBool("isSliding", false);

        controller.center = Vector3.zero;
        controller.height = 2;

        isSliding = false;
    }
}

