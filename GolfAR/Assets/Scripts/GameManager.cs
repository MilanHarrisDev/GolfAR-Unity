using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public enum GameState{

        MENU,
        GAME_SINGLE,
        GAME_MULTI
    }

    public GameState state = GameState.MENU;

    public static GameManager Instance;
    public ShootController shootControl;

    public List<GameObject> menuObjects;
    public List<GameObject> gameObjects;

    [SerializeField]
    private Transform course;
    [SerializeField]
    private Transform imageMarker;

    private Vector3 playerPosDifference = Vector3.zero;

    private int turn = 0;
    public int Turn { get { return turn; }}

    [SerializeField]
    private Text turnCounter;


    [SerializeField]
    private GameObject player;

    private Vector3 lastPlayerPos = Vector3.zero;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySingleplayer(){
        ChangeState(GameState.GAME_SINGLE);
    }

    private void ChangeState(GameState newState)
    {
        switch(newState)
        {
            case GameState.MENU:
                foreach (GameObject go in menuObjects)
                    go.SetActive(true);
                foreach (GameObject go in gameObjects)
                    go.SetActive(false);
                break;
            case GameState.GAME_SINGLE:
                foreach (GameObject go in menuObjects)
                    go.SetActive(false);
                foreach (GameObject go in gameObjects)
                    go.SetActive(true);

                EndTurn();
                shootControl.ResetShoot();
                break;
        }
    }

    private void Start()
    {
        ChangeState(GameState.MENU);
    }

    public void PlayerDeath()
    {
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.transform.position = lastPlayerPos;
        Debug.Log("Player died");
        EndTurn();
    }

    public void EndTurn()
    {
        playerPosDifference = player.transform.position - imageMarker.transform.position;
        player.transform.parent = null;
        course.parent = null;
        imageMarker.position += playerPosDifference;
        player.transform.parent = imageMarker;
        course.parent = imageMarker;

        shootControl.ResetShoot();

        turn++;
        lastPlayerPos = player.transform.position;
        Debug.Log("Turn Ended. Current Turn: " + turn);
        turnCounter.text = "turn: " + Turn;
    }
}
