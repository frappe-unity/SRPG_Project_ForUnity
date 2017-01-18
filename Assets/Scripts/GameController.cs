using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

    [SerializeField] private UnitController unit;
    [SerializeField] private GameObject cirsor;
    [SerializeField] private CharacterMoveController charactermovecontroller;
    [SerializeField] private MovableScript movablescript;
    [SerializeField] private CirsorController cirsorcontroller;
    [SerializeField] private EnemyAIController enemyAI;

    public int turnCount = 0;
    public int start = 0;
    public bool playerTurn = false;
    public bool isChange = false;
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
	    if(unit.stayCount == unit.player.Count && playerTurn)
        {
            unit.stayCount = 0;
            StartEnemy();
        }
        if(unit.enemy.Count == 0)
        {
            SceneManager.LoadScene("clear");
        }
        if (unit.player.Count == 0)
        {
            SceneManager.LoadScene("gameover");
        }
    }

    public void StartPlayer()
    {
        enemyAI.enemyStorage.Clear();
        // isChange = true;
        unit.UnitMovable();
        movablescript.Initialize();
        unit.stayCount = 0;
        cirsor.SetActive(true);
        charactermovecontroller.isCirsor = false;
        unit.selectPlayer = unit.playerController[0].playerID;
        // unit.paramPlayer = unit.playerController[0].playerID;
        cirsorcontroller.cirsorPos = unit.playerController[0].playerPos;
        for(int i = 0;i < unit.player.Count; i++)
        {
            unit.playerController[i].isAct = false;
        }
        turnCount++;
        // enemyTurn = false;
        playerTurn = true;
        // isChange = false;
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
            enemyAI.enemyStorage.Add(i);
        }
        cirsor.SetActive(false);
        playerTurn = false;
        // enemyTurn = true;
    }
}
