using UnityEngine;

public class Goomba : MonoBehavior
{
    public Sprite flatSprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //using dot test from other class to make sure the player is hitting the goomba while moving down
            if(collision.transform.DotTest(transform, Vector2.down))
            {
                Flatten();
            }
        }

        
    }
    private void Flatten(){
        GetComponent<Collider2D>().enabled = false; 
        GetComponent<EntityMovement>().enabled = false; 
        GetComponent<AnimateSprite>().enabled = false; 
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 0.5f);
    }
}