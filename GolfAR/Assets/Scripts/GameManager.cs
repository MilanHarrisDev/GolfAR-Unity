using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public ShootController shootControl;

    [SerializeField]
    private Transform course;
    [SerializeField]
    private Transform imageMarker;

    private Vector3 playerPosDifference = Vector3.zero;

    private int turn = 0;
    public int Turn { get { return turn; }}


    [SerializeField]
    private GameObject player;

    private Vector3 lastPlayerPos = Vector3.zero;

    private void Awake()
    {
        Instance = this;
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

        turn++;
        lastPlayerPos = player.transform.position;
        Debug.Log("Turn Ended. Current Turn: " + turn);
        shootControl.ResetShoot();
    }
}
