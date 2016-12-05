using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    [SerializeField] private UnitController unit;
    [SerializeField] private GameObject cirsor;

    public int turnCount = 0;
    public bool playerTurn = false;
    public bool enemyTurn = false;

	// Use this for initialization
	void Start () {
        turnCount = 1;
        playerTurn = true;
        enemyTurn = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if(unit.stayCount == unit.playerObj.Length && playerTurn)
        {
            StartEnemy();
        }
	}

    public void StartPlayer()
    {
        cirsor.SetActive(true);
        turnCount++;
        playerTurn = true;
        enemyTurn = false;
    }

    public void StartEnemy()
    {
        cirsor.SetActive(false);
        enemyTurn = true;
        playerTurn = false;
    }
}
