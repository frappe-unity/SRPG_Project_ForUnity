using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitController : MonoBehaviour {

    public MapData mapdata;
    
    /// <summary>
    /// キャラクターの管理
    /// </summary>
    public const int playerCount = 0;
    public List<PlayerController> playerController = new List<PlayerController>();
    public List<EnemyController> enemyController = new List<EnemyController>();
    public List<PlayerSet> entryPlayer = new List<PlayerSet>();
    public List<EnemySet> entryEnemy = new List<EnemySet>();
    public GameObject[] playerObj;
    public GameObject[] enemyObj;
    public int selectUnit = 99;
    public bool isUnit = false;
    public int stayCount = 0;

    public class MapUnit
    {
        // public int height = 1; // 高さ
        // public int blockNum = 0; // ブロックの種類
        public bool movable = false; // 移動可能フラグ 
    }

    public MapUnit[,] mapUnit = new MapUnit[20, 20];

    // Use this for initialization
    void Start () {
        playerObj = GameObject.FindGameObjectsWithTag("Player");
        enemyObj = GameObject.FindGameObjectsWithTag("Enemy");
        // プレイヤー
        for (int i = 0; i < playerObj.Length; i++)
        {
            playerController.Add(playerObj[i].GetComponent<PlayerController>());
            entryPlayer.Add(mapdata.entryPlayer[i]);
            playerController[i].charaID = i;
            playerController[i].name = mapdata.entryPlayer[i].charaName;
            playerController[i].hp = mapdata.entryPlayer[i].hp;
            playerController[i].attack = mapdata.entryPlayer[i].attack;
            playerController[i].deffence = mapdata.entryPlayer[i].deffence;
            playerController[i].hit = mapdata.entryPlayer[i].hit;
            playerController[i].moveCost = mapdata.entryPlayer[i].moveCost;
            playerController[i].unitPos = mapdata.entryPlayer[i].playerPos;
        }
        // エネミー
        for (int i = 0; i < enemyObj.Length; i++)
        {
            enemyController.Add(enemyObj[i].GetComponent<EnemyController>());
            entryEnemy.Add(mapdata.entryEnemy[i]);
            enemyController[i].enemyID = i;
            enemyController[i].name = mapdata.entryEnemy[i].enemyName;
            enemyController[i].hp = mapdata.entryEnemy[i].hp;
            enemyController[i].attack = mapdata.entryEnemy[i].attack;
            enemyController[i].deffence = mapdata.entryEnemy[i].deffence;
            enemyController[i].hit = mapdata.entryEnemy[i].hit;
            enemyController[i].moveCost = mapdata.entryEnemy[i].moveCost;
            enemyController[i].enemyPos = mapdata.entryEnemy[i].enemyPos;
        }
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                mapUnit[x, y] = new MapUnit();
            }
        }
    }

    public void UnitMovable()
    {
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                mapUnit[x, y].movable = true;
            }
        }
        // プレイヤー
        for (int i = 0; i < playerObj.Length; i++)
        {
            mapUnit[Mathf.FloorToInt(playerController[i].unitPos.x), Mathf.FloorToInt(playerController[i].unitPos.y)].movable = false;
        }
        // エネミー
        for (int i = 0; i < enemyObj.Length; i++)
        {
            mapUnit[Mathf.FloorToInt(enemyController[i].enemyPos.x), Mathf.FloorToInt(enemyController[i].enemyPos.y)].movable = false;
        }
    }
}
