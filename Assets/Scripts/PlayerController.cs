using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	MapManager map;
	MovableScript movableScript;
	GameObject cirsor;

	public bool isMove = false;
    public bool isUpCirsor = false;

    /// <summary>
    /// ユニットのステータス
    /// </summary>
    public string charaName = "Lin";
	public int hp = 18;
	public int attack = 7;
	public int deffence = 4;
	public int moveCost = 5;

    private int posDiv = 10; // ユニットの座標を1単位にするために割る

    private bool isPress = false;


	/// <summary>
	/// 各種メニュー処理
	/// </summary>

	void Start () {
		map = GameObject.Find ("MapController").GetComponent<MapManager> ();
		movableScript = GameObject.Find ("MapController").GetComponent<MovableScript> ();
		cirsor = GameObject.Find ("Cirsor");
		map.MapInit();
		map.DrawMap ();
        map.insMap = true;
	}



	void Update () {
        if(this.transform.position.x == cirsor.transform.position.x && this.transform.position.x == cirsor.transform.position.z)
        {
            isUpCirsor = true;
        } else
        {
            isUpCirsor = false;
        }

        
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("A_Button"))
            {
                if (isPress == false && isUpCirsor == true)
                {
                isPress = true;
                if (isMove == false)
                {
                    map.matNum = 1;
                    // map.insMap = true;
                    isMove = true;
                }
                else
                {
                    map.matNum = 0;
                    // map.insMap = false;
                    isMove = false;
                }
                Vector3 pos;
                pos = transform.position;
                movableScript.moveSearch(Mathf.RoundToInt(pos.x) / posDiv, Mathf.RoundToInt(pos.z) / posDiv, moveCost);
                map.DrawMap();
                isPress = false;
            }
        }
	}
}
