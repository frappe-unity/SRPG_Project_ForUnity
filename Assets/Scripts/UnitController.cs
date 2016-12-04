using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitController : MonoBehaviour {

    /// <summary>
    /// キャラクターの管理
    /// </summary>
    public const int playerCount = 0;
    // Unit[] player = new Unit[playerCount];
    public List<PlayerController> playerController = new List<PlayerController>();
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
            playerController[i].charaID = i;
        }
    }

    // Update is called once per frame
    void Update () {
	}
}
