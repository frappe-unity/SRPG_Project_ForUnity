using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	MapManager map;
	MovableScript movableScript;
	GameObject cirsor;

	public bool isMove = false;

	/// <summary>
	/// ユニットのステータス
	/// </summary>
	public int hp = 18;
	public int attack = 7;
	public int deffence = 4;
	public int moveCount = 5;
	public int x,y,z = 0;

	/// <summary>
	/// 各種メニュー処理
	/// </summary>

	void Start () {
		map = GameObject.Find ("MapController").GetComponent<MapManager> ();
		movableScript = GameObject.Find ("MapController").GetComponent<MovableScript> ();
		cirsor = GameObject.Find ("Cirsor");
		cirsor.transform.position = this.transform.position;
		map.MapInit();
		map.DrawMap ();
	}



	void Update () {
		if (Input.GetKey(KeyCode.Space) && isMove == false) {
			isMove = true;
		} else if(Input.GetKey(KeyCode.Space) && isMove == true){
			isMove = false;
		}
		if (isMove) {
			movableScript.moveSearch (Mathf.CeilToInt(this.transform.position.x / 10),moveCount,Mathf.CeilToInt(this.transform.position.y / 10));
			map.DrawMap ();
		}

		// カーソル移動処理
		if (Input.GetAxis ("Horizontal") > 0.4F) {
			cirsor.transform.position += new Vector3 (1F, 0, 0);
		} else if (Input.GetAxis ("Horizontal") < -0.4F) {
			cirsor.transform.position += new Vector3 (-1F, 0, 0);
		}
		if (Input.GetAxis ("Vertical") > 0.4F) {
			cirsor.transform.position += new Vector3 (0, 0, 1F);
		} else if (Input.GetAxis ("Vertical") < -0.4F) {
			cirsor.transform.position += new Vector3 (0, 0, -1f);
		}
	}
}
