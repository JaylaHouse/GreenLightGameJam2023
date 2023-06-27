using UnityEngine;

public class AnimateSprite : MonoBehavior
{
    public Sprite[] sprite;
    public float frameRate =1f/6; //6 frames a second can custimize
    private SpriteRenderer spriteRenderer;
    private int frame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(Animate), frameRate, frameRate);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Animate()
    {
        if(frame >= sprite.Length)
        {
            frame = 0;
        }

        if(frame >= 0 && frame < sptires.Length){
            spriteRenderer.sprite = sprites[frame];
        }
        
    }
}
//script using for changing sprite on a loop to look like an animation