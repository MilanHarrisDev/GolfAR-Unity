using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public enum GameState{

        MENU,
        GAME_SINGLE,
        GAME_MULTI,
        WIN
    }

    public GameState state = GameState.MENU;

    public static GameManager Instance;
    public ShootController shootControl;

    public List<GameObject> menuObjects;
    public List<GameObject> gameObjects;
    public GameObject winObject;

    [SerializeField]
    private List<GameObject> obstacles;
    private Transform[] obstacleSpawns;

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

    public void Win(){
        ChangeState(GameState.WIN);
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

                winObject.SetActive(false);
                break;
            case GameState.GAME_SINGLE:
                foreach (GameObject go in menuObjects)
                    go.SetActive(false);
                foreach (GameObject go in gameObjects)
                    go.SetActive(true);

                winObject.SetActive(false);

                EndTurn();
                shootControl.ResetShoot();
                break;
            case GameState.WIN:
                foreach (GameObject go in menuObjects)
                    go.SetActive(false);
                foreach (GameObject go in gameObjects)
                    go.SetActive(false);

                winObject.SetActive(true);
                break;
        }
    }

    private void Start()
    {
        ChangeState(GameState.MENU);
        obstacleSpawns = GameObject.Find("ObstacleSpawns").transform.GetComponentsInChildren<Transform>();
    }

    public void PlayerDeath()
    {
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.transform.position = lastPlayerPos;
        Debug.Log("Player died");
    }

    public void EndTurn()
    {
        Instantiate(obstacles[Random.Range(0,3)], obstacleSpawns[Random.Range(0, obstacleSpawns.Length)].position, Quaternion.identity);

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

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
