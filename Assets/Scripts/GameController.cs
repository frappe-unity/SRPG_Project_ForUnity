using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    [SerializeField] private UnitController unit;
    [SerializeField] private GameObject cirsor;
    [SerializeField] private CharacterMoveController charactermovecontroller;

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
        charactermovecontroller.Initialize();
        unit.stayCount = 0;
        charactermovecontroller.isCirsor = false;
        charactermovecontroller.unitNumber = 99;
        for(int i = 0;i < unit.playerObj.Length; i++)
        {
            unit.playerController[i].isAct = false;
        }
        cirsor.SetActive(true);
        turnCount++;
        playerTurn = true;
        enemyTurn = false;
    }

    public void StartEnemy()
    {
        charactermovecontroller.Initialize();
        unit.stayCount = 0;
        cirsor.SetActive(false);
        enemyTurn = true;
        playerTurn = false;
        Invoke("StartPlayer", 2.0F);
    }
}
