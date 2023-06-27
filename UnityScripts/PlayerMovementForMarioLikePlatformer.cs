using UnityEngine;

public class PlayerMovement : MonoBehavior
{

    private new Camera camera; // need to keep player in bounds
    private new RigidBody2D rigidbody;

    private Vector2 velocity;
    private float inputAxis;

    public float movespeed = 8f;

    //jumping variables------------------------------------------
    public float maxJumpHeight = 5f; //saying he can jump 5 units up
    public float maxJumpTime = 1f; //how long does it take player to reach max height
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f); //arrow turns it into a computed property (creating a parabola for jump)
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpHeight / 2f),2);

    public bool grounded {get; private set;}
    public bool jumping {get; private set;}

    //end jump--------------------------------------

    //for changing animation---------------------------
    public bool running => Mathf.Abs(velocity.x) > .25f || Mathf.Abs(inputAxis) > 0.25f;//zero may be 2 low of a number also need to make sure inbetween switching from left to right is still running 
    public bool sliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f);//when turning 
    //end =---------------------------------------------

    private void Awake()
    {
        rigidbody = GetComponent<RigidBody2D>();
        camera = Camera.Main;
    }

    private void Upadate()
    {
        HorizontalMovement();

        //JumpStart---------------------------------------------------
        //need a raycast to detect if mario is on the ground Created a class
        grounded = rigidbody.Raycast(Vector2.down);
        if(grounded){
            GroundedMovement();
        }

        //jump end--------------------------------------------------

    }

    private void ApplyGravity()
    {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump"); //making sure you are falling so can add ultiplyer to gravity so falls faster
        float multiplier = falling ? 2f : 1f; //if falling go 2 times faster else regular gravity applied

        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity /2f); //stopping from falling tooo fast
    }

    private void GroundedMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f); //making sure when stationary or just falling off of a block the falling speed isnt 2 crazy 
        jumping = velocity.y > 0f; //making sure your not on ground because frames might not be registered yet
        
        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
            jumping = true;
        }
    }

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * movespeed, movespeed * Time.deltaTime);

        //making sure if running into wall dont build x velocity
        if(rigidbody.Raycast(Vector2.right * velocity.x))
        {
            velocity.x = 0f;
        }
        //--------------------------------------------------

        //change rotation of sprite depending on where looking
        if (velocity.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;
        }else{
            transform.eulerAngles = Vector3(0f, 180f, 0f);
        }
        //-----------------------------------------------------
    }

    //need this to make sure fake physics is not variable
    private void FixedUpdate()
    {
        /*
        // start section: without screen bounds
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;

        rigidbody.MovePosition(position);
        //end section
        */

        // start section: making player stay in bounds of screen
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);
        Vector rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        //position.x = Mathf.Clamp(position.x, leftEdge.x, rightEdge.x); //this the player sort of goes out of screen for a second
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f); //adding the 0.5f helps make player stay little bit more in camera might need to change actual value
        rigidbody.MovePosition(position);
        //end section



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //make player bounce off of enemy head
        if(collision.GameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if(tranform.DotTest(collisionl.transform, Vector2.down))
            {
                velocity.y = jumpforece / 2f; //could change this variable 
                jumping = true; 
            }
        }

        if(collision.GameObject.layer != LayerMask.NameToLayer("PowerUp")) //becuase we would want to not bonk on stuff like power ups
        {
            //determing if object is above you aka bonking your head (using dot product which tells you how similar are 2 vectors ) This help becuase if you hit something sideways then you dont want to stop but only if your underneath the object
            if(transform.DotTest(collision.transform, Vector2.up))
            {
                velocity.y = 0f;
            }
        }

        
    }


    /* Notes
    -Make sure you define all the layers and have correct object on them 

    - need to edit the horizontal movement in input manager
        - gravity = 1000
        - dead = 0.001
        - sensitivity = 3

    -Add physics material to remove friction so that dont get stuck on different stuff put it on everything basically 
    -Also helps to make collision detection to continuous on player obejct (but does hinder performance not a lot but a little)
    -Also change interpolate mode to interpolate helps smooths physics and rendering (stopps that gittering visual)

    */
}