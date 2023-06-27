using UnityEngine;

public class PlayerSpriteRenderer : MonoBehavior
{
    private PlayerSpriteRenderer spriteRenderer;
    private PlayerMovement movement;

    public Sprite idle;
    public Sprite jump;
    public AnimatedSprite running;
    public Sprite slide;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponent.GetComponentInParent<PlayerMovement>();
    }
    
    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    private void LateUpdate()
    {
        run.enabled = movement.running;

        //order does matter we want jump to be first 
        if(movement.jumping)
        {
            spriteRenderer.sprite = jump;
        }/*else if(movement.sliding){
            spriteRenderer.sprite = sliding;
        }else(movement.idle && !movement.running){
            spriteRenderer.sprite = idle;
        }*/
    }
}