using UnityEnguine;
using Systems.Collections;
using Systems.Collections.Generic;

public static class Extensions
{
    private static LayerMask layermask = LayerMask.GetMask("Default");
    public static bool Raycast(this RigidBody2D rigidbody, Vector2 direction)
    {
        if(rigidbody.isKinimatic){
            return false;
        }

        float radius = .25f;  //circle path basically casting a circle to detect whats within it *may need to change size of radius* *radius is half of the radious of capsule collider*
        float distance = .375f; //can also check through capsule cillider 

        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radious, direction.normalized, distance, layermask); //will need to make sure that the layers for the ray cast is correct so ray cast does not collide with itself
        return hit.collider != null && hit.rigidbody != rigidbody; //make sure that its not the rigid body your casting from is hitting you 
    }

    //used for determinign if bonking head 
    public static bool DotTest(this Transform transform,Transform other, Vector2 testDirection)
    {
        Vector2 direction = other.position - transform.position; //other in this case would be the block (is it above next below etc... )
        return Vector2.Dot(direction.normalized, testDirection) > 0.25f; //could be arranges ass necesary stay in .25-.5 range 
    }
}