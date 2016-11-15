using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// 参照
	MapManager map;
	MovableScript movableScript;
	GameObject cirsor;

	/// <summary>
	/// 各種ブーリアン
	/// </summary>
	public bool isMove = false;			// 移動中か
    public bool isUpCirsor = false;		// カーソルがキャラの上にいるか
	public bool isPress = false;		// 移動ボタン選択中か

    /// <summary>
    /// ユニットのステータス
    /// </summary>
    public string charaName = "Lin";	// キャラクターの名前
	public int hp = 18;					// 体力
	public int attack = 7;				// 攻撃力
	public int deffence = 4;			// 防御力
	public int moveCost = 5;			// 移動力

    private int posDiv = 10; // ユニットの座標を1単位にするために割る

	/// <summary>
	/// 各種メニュー処理
	/// </summary>

	void Start () {
		// Find
		map = GameObject.Find ("MapController").GetComponent<MapManager> ();
		movableScript = GameObject.Find ("MapController").GetComponent<MovableScript> ();
		cirsor = GameObject.Find ("Cirsor");

		// マップの描画関係
		map.MapInit();
		map.DrawMap ();
        map.insMap = true;	// 初期描画完了
	}

	void Update () {
		// カーソルがキャラの上に乗っているかの判定
        if(this.transform.position.x == cirsor.transform.position.x && this.transform.position.x == cirsor.transform.position.z)
        {
            isUpCirsor = true;
        } else
        {
            isUpCirsor = false;
        }


		// カーソルがキャラの上にある
		if (isUpCirsor == true) { 
			map.isAlpha = false;
			// 移動ボタン選択中か否かの判定
			if (Input.GetKeyDown (KeyCode.Space) || Input.GetButtonDown ("A_Button")) {
				if (!isPress) {
					isPress = true;
					isMove = true;
				} else {
					isMove = false;
					isPress = false;
				}
			}

			if (!isPress) {
				map.colorR = 0F;
				map.colorG = 0.5F;
				map.colorB = 0.5F;
				map.alphaA = 0.7F;
			} else {
				map.colorR = 0.5F;
				map.colorG = 1F;
				map.colorB = 1F;
				map.alphaA = 0.7F;
			}
		} else {
			map.isAlpha = true;
			// カーソルがキャラの上でなければ、マップチップの色をデフォルトに
			map.colorR = 0F;
			map.colorG = 0.5F;
			map.colorB = 0.3F;
			map.alphaA = 1.0F;
		}

		// 移動可能範囲の算出と色の変更
		Vector3 pos;
		pos = transform.position;
		movableScript.moveSearch(Mathf.RoundToInt(pos.x) / posDiv, Mathf.RoundToInt(pos.z) / posDiv, moveCost);
		map.DrawMap();
	}
        
		/*if (Input.GetKeyDown (KeyCode.Space) || Input.GetButtonDown ("A_Button")) {
			if (isPress == false && isUpCirsor == true) {
				isPress = true;
				if (isMove == false) {
					map.matNum = 1;
					// map.insMap = true;
					isMove = true;
				} else {
					map.matNum = 0;
					// map.insMap = false;
					isMove = false;
				}
                
			}
			// 移動可能範囲の算出と色の変更
			Vector3 pos;
			pos = transform.position;
			movableScript.moveSearch (Mathf.RoundToInt (pos.x) / posDiv, Mathf.RoundToInt (pos.z) / posDiv, moveCost);
			map.DrawMap ();
			isPress = false;
		}


		if (this.transform.position.x == cirsor.transform.position.x && this.transform.position.x == cirsor.transform.position.z) {
			isUpCirsor = true;
		} else {
			isUpCirsor = false;
		}


	}*/
}
