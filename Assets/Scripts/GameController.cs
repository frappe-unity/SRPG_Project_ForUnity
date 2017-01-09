using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

    [SerializeField] private UnitController unit;
    [SerializeField] private GameObject cirsor;
    [SerializeField] private CharacterMoveController charactermovecontroller;
    [SerializeField] private MovableScript movablescript;

    public int turnCount = 0;
    public int start = 0;
    public bool playerTurn = false;
    // public bool enemyTurn = false;

	// Use this for initialization
	void Start () {
        unit.UnitMovable();
        turnCount = 1;
        playerTurn = true;
        // enemyTurn = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if(unit.stayCount == unit.playerObj.Length && playerTurn)
        {
            unit.stayCount = 0;
            StartEnemy();
        }
        if(unit.enemy.Count == 0)
        {
            SceneManager.LoadScene("clear");
        }
	}

    public void StartPlayer()
    {
        unit.UnitMovable();
        movablescript.Initialize();
        unit.stayCount = 0;
        charactermovecontroller.isCirsor = false;
        charactermovecontroller.playerID = 99;
        for(int i = 0;i < unit.playerObj.Length; i++)
        {
            unit.playerController[i].isAct = false;
        }
        cirsor.SetActive(true);
        turnCount++;
        // enemyTurn = false;
        playerTurn = true;
    }

    public void StartEnemy()
    {
        unit.UnitMovable();
        charactermovecontroller.stateCount = -1;
        // Debug.Log("Start :" + start);
        movablescript.Initialize();
        unit.stayCount = 0;
        for (int i = 0; i < unit.enemy.Count; i++)
        {
            unit.enemyController[i].isAct = false;
        }
        cirsor.SetActive(false);
        playerTurn = false;
        // enemyTurn = true;
    }
}
