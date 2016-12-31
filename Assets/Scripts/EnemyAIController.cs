using UnityEngine;
using System.Collections;

public class EnemyAIController : MonoBehaviour {

    [SerializeField] private UnitController unitcontroller;
    [SerializeField] private MovableScript movablescript;
    [SerializeField] private MapController mapcontroller;
    [SerializeField] private GameController gamecontroller;
    public int cirsorEnemy;
    public GameObject enemy;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (gamecontroller.enemyTurn)
        {
            // Initialize();
            cirsorEnemy = unitcontroller.enemyController[0].enemyID;
            enemy = unitcontroller.enemyObj[cirsorEnemy];
            Search();
        }
        
        

	}

    public void Search()
    {
        var selectEnemy = unitcontroller.enemyController[cirsorEnemy];
        unitcontroller.UnitMovable();
        movablescript.moveSearch(Mathf.RoundToInt(selectEnemy.enemyPos.x), Mathf.RoundToInt(selectEnemy.enemyPos.y), selectEnemy.moveCost, 0);
        mapcontroller.DrawMap();
    }


    // 初期化
    public void Initialize()
    {
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                mapcontroller.block[x, y].enemyMovable = false;
                mapcontroller.block[x, y].color = 0;
            }
        }
        mapcontroller.DrawMap();
    }
}
