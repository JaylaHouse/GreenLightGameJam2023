using UnityEngine;
using UnityEnguine.SceneManagement;

public class GameManager : MonoBehavior
{
    public int world{get; private set;}
    public int stage {get; private set;}
    public int lives {get; private set;}
    public static GameManager Instance{get; private set;}

    private void Awake()
    {
        if(Instance != null ){
            DestroyImmediate(gameObject);
        }else {
            Instance = this;
            DontDestoryOnLoad(gameObject);
        }
    }

    private void Destroy()
    {
        if(Instance == this){
            Instance = null;
        }
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        lives = 3;
        LoadLevel(1,1);
    }

    private void LoadLevel(int world, int stage) //for example 1-1, or like 1-2,
    {
        this.world = world;
        this.stage = stage;
        
        SceneManager.LoadScene($"{world}-{stage}"); //this means we need to make sure scene are called "1-1", "1-2" etc..
    }

    public void NextLevel(){
        LoadLevel(world, stage +1); //may need to be carefull add more logic to change world etc...
    }

    public void ResetLevel(float delay){
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel()
    {
        lives--;

        if(lives > 0){
            LoadLevel(world,stage);
        }else{
            GameOver();
        }
    }
    private void GameOver()
    {
        //load like a game over scene or whatever 
        NewGame();
    }
}
//this is a singleton class that means there is only ever one instance