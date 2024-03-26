using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChracterController : MonoBehaviour
{
    // Speed of the character
    [SerializeField] private float speed = 10f;

    // Force applied when jumping
    [SerializeField] private float jumpForce = 5f;

    // Reference to the Rigidbody2D component
    private Rigidbody2D _rigidbody2D;

    // Reference to the Animator component
    private Animator _animator;


    private bool grounded;    // Boolean to check if the character is grounded
    private bool started;    // Boolean to check if the game has started
    private bool jumping;    // Boolean to check if the character is currently jumping


    // Initialization method called before the game starts
    private void Awake()
    {
        // Caching Rigidbody2D and Animator components for efficiency
        _rigidbody2D = GetComponent<Rigidbody2D>(); //caching
        _animator = GetComponent<Animator>();

        grounded = true;         // Setting the character initially grounded

    }

    // Update method called once per frame
    private void Update()
    {
        // Checking for spacebar input
        if (Input.GetKeyDown("space"))
        {
            // If the game has started and the character is grounded, initiate jump
            if (started && grounded)
            {
                _animator.SetTrigger("Jump");
                grounded = false;
                jumping = true;
            }

            // If the game has not started, start the game
            else
            {
                _animator.SetBool("GameStarted", true);
                started = true;
            }
        }

        // Updating the 'Grounded' parameter in the animator controller
        _animator.SetBool("Grounded", grounded);
        
    }

    // FixedUpdate method called at fixed intervals for physics calculations
    private void FixedUpdate()
    {
        // Moving the character horizontally if the game has started
        if (started)
        {
            _rigidbody2D.velocity = new Vector2(speed, _rigidbody2D.velocity.y);
        }

        // Applying jump force if the character is jumping
        if (jumping)
        {
            _rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            jumping = false;
        }
    }

    // Collision detection method for 2D collisions
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Checking if the character collides with the ground
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = true;     // Setting the character grounded
        }

    }
}
