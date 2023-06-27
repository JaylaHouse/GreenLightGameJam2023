using UnityEngine;

public class EntityMovement : MonoBehavior
{
    private new RigidBody2D rigidbody;

    public float speed = 2f;

    public Vector2 direction = Vector2.Left; //public because somethings start moving based on a certain direction
  
    private Vector2 velocity;

    private void Awake()
    {
        rigidbody = GetComponent<RigidBody2D>();
        enabled = false;
    }

    private void OnBecomeVisable()
    {
        enabled = true;
    }
    private void OnBecomeInvisable()
    {
        enabled = false;
    }

    private void OnEnable(){
        rigidbody.WakeUp();
    }
    private void OnDisable(){
        rigidbody.velocity = Vector2.zero;
        rigidbody.Sleep();
    }

    private void FixedUpdate()
    {
        velocity.x = direction.x * speed;
        velocity.y += Physics2D.grvity.y * Time.fixedDeltaTime; //this is unitys gravity or could create own gravity Vector 2 variable 

        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);

    //use extensions script to see if it hit something to turn around 
        if(rigidbody.Raycast(direction)){
            direction = -direction;
        }

        if(rigidbody.Raycast(Vector2.down)){
            velocity.y = Mathf.Max(velocity.y, 0f);
        }
    }

//need to adjust collission matrix dont let enemys collide with each other 





}