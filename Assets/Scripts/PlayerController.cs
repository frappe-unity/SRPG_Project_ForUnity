using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// 参照
	MapManager map;
	MovableScript movableScript;
	GameObject cirsor;

	public bool isMove = false;
    public bool isUpCirsor = false;
	public bool isPress = false;
	public bool playButton = true;

	public float keyTimer = 1.0F;

    /// <summary>
    /// ユニットのステータス
    /// </summary>
    public string charaName = "Lin";
	public int hp = 18;
	public int attack = 7;
	public int deffence = 4;
	public int moveCost = 5;

    private int posDiv = 10; // ユニットの座標を1単位にするために割る

    


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
		Debug.Log (isPress);
        if(this.transform.position.x == cirsor.transform.position.x && this.transform.position.x == cirsor.transform.position.z)
        {
            isUpCirsor = true;
        } else
        {
            isUpCirsor = false;
        }
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetButtonDown ("A_Button")) {
			if (!isPress) {
				isPress = true;
			} else {
				isPress = false;
			}
		}
		if (isUpCirsor == true) { 
			if (isPress == true) {
				map.matNum = 2;
				// map.insMap = true;
				isMove = true;
			} else if(isPress == false){
				map.matNum = 1;
				// map.insMap = false;
				isMove = false;
			}
		} else {
			map.matNum = 0;
		}
		// 移動可能範囲の算出と色の変更
		Vector3 pos;
		pos = transform.position;
		movableScript.moveSearch(Mathf.RoundToInt(pos.x) / posDiv, Mathf.RoundToInt(pos.z) / posDiv, moveCost);
		map.DrawMap();
		Debug.Log (map.matNum);
	}
        
         /*if(Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("A_Button"))
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
                
            }
			// 移動可能範囲の算出と色の変更
			Vector3 pos;
			pos = transform.position;
			movableScript.moveSearch(Mathf.RoundToInt(pos.x) / posDiv, Mathf.RoundToInt(pos.z) / posDiv, moveCost);
			map.DrawMap();
			isPress = false;
        }


		if(this.transform.position.x == cirsor.transform.position.x && this.transform.position.x == cirsor.transform.position.z)
		{
			isUpCirsor = true;
		} else
		{
			isUpCirsor = false;
		}*/



}
