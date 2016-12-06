using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitController : MonoBehaviour {

    public MapData mapdata;


    /// <summary>
    /// キャラクターの管理
    /// </summary>
    public const int playerCount = 0;
    // Unit[] player = new Unit[playerCount];
    public List<PlayerController> playerController = new List<PlayerController>();
    public List<PlayerSet> entryPlayer = new List<PlayerSet>();
    public GameObject[] playerObj;
    public int selectUnit = 99;
    public bool isUnit = false;
    public int stayCount = 0;

	// Use this for initialization
	void Start () {
        playerObj = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < playerObj.Length; i++)
        {
            playerController.Add(playerObj[i].GetComponent<PlayerController>());
            entryPlayer.Add(mapdata.entryPlayer[i]);
            playerController[i].charaID = i;
            playerController[i].hp = mapdata.entryPlayer[i].hp;
            playerController[i].attack = mapdata.entryPlayer[i].attack;
            playerController[i].deffence = mapdata.entryPlayer[i].deffence;
            playerController[i].hit = mapdata.entryPlayer[i].hit;
            playerController[i].moveCost = mapdata.entryPlayer[i].moveCost;
            playerController[i].unitPos = mapdata.entryPlayer[i].playerPos;
        }
    }

    // Update is called once per frame
    void Update () {
	}
}
