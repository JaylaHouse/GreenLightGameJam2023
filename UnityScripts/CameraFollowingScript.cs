using UnityEngine;

public class SideScrolling : MonoBehavior
{
    //move camera to match player position (LikeMario)

    private Tranform player;

    private void Awake()
    {
        //need to make sure there is a tag on the player (would need public custom variables if multiplayer)
        player = GameObject.FindWithTag("Player").transform;
        
    }

    //important for camera so it makes sure player movement is done before camera updates
    private void LateUpdate()
    {
        /*Start Section: this section of code is fine if you want to allow the player to go back and forth 
        Vector3 cameraPosition = transform.position;

        cameraPosition.x = player.position.x;
        transform.position = cameraPosition;
        */ //end section

        //Start Section: this section of code is fine if you only allow character to move forawrd through level and not back
        Vector3 cameraPosition = transform.position;
        
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x); //works by only picks the greater of the value 
        transform.position = cameraPosition;
        //end section

    }
}