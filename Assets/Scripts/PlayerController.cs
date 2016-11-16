using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// 参照
	MapManager map;
	MovableScript movableScript;
	GameObject cirsor;

    public Vector3 pos;

    /// <summary>
    /// 各種ブーリアン
    /// </summary>
    public bool isMove = false;			// 移動中か
    public bool isUpCirsor = false;		// カーソルがキャラの上にいるか
	public bool isPress = false;		// 移動ボタン選択中か
    public bool isMovable = false;      // その位置にキャラを移動可能か
    public bool isMoving = false;

    /// <summary>
    /// ユニットのステータス
    /// </summary>
    public string charaName = "Lin";	// キャラクターの名前
	public int hp = 18;					// 体力
	public int attack = 7;				// 攻撃力
	public int deffence = 4;			// 防御力
	public int moveCost = 5;			// 移動力

    private int posDiv = 10; // ユニットの座標を1単位にするために割る
    private float posDivF = 10F; // Float版

    /// <summary>
    /// 各種メニュー処理
    /// </summary>

    public bool a = false;

    /// <summary>
    /// Material取得
    /// </summary>
    public void ColorChange(int i)
    {

        // 1なら濃い青
        if(i == 1)
        {
            map.isAlpha = false;
            map.colorR = 0F;
            map.colorG = 0.5F;
            map.colorB = 1.0F;
            map.alphaA = 0.7F;
        }

        // 2なら水色
        else if (i == 2)
        {
            map.isAlpha = false;
            map.colorR = 0.5F;
            map.colorG = 1F;
            map.colorB = 1F;
            map.alphaA = 0.7F;
        }

        // 3なら透明
        else if(i == 3)
        {
            map.isAlpha = false;
            map.colorR = 1F;
            map.colorG = 1F;
            map.colorB = 1F;
            map.alphaA = 0F;
        }
    }
    

    /// <summary>
    /// カーソルがキャラの上にあるかの判定
    /// </summary>
    public void CharaUpCorsor()
    {
        if (this.transform.position.x == cirsor.transform.position.x && this.transform.position.z == cirsor.transform.position.z)
        {
            isUpCirsor = true;
        }
        else
        {
            isUpCirsor = false;
        }
    }

    /// <summary>
    /// 決定キーを押しているかの判定
    /// </summary>
    public void EnterButton()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if(!isPress)
                isPress = true;
            } else
            {
                isPress = false;
        }
    }

    /// <summary>
    /// マップの色変え
    /// </summary>
    public void ChangeColor()
    {
        // 移動中なら水色に
        if (isMove)
        {
            ColorChange(2);
        }
        else if (isMove == false && isUpCirsor)
        {
            ColorChange(1);
        }
        else
        {
            ColorChange(3);
        }
    }

    /// <summary>
    /// カーソルが移動範囲に入っているか
    /// </summary>
    public void CirsorIn()
    {
        if (isMove)
        {
           
            float fx = cirsor.transform.position.x / posDivF;
            float fz = cirsor.transform.position.z / posDivF;

            if (map.block[Mathf.RoundToInt(fx),Mathf.RoundToInt(fz)].movable)
            {
                isMovable = true;
            } else
            {
                isMovable = false;
            }
        }
    }




    /// <summary>
    /// キャラクターの移動
    /// </summary>
    public void CharacterMove()
    {
        if(isMove && isMovable)
        {
            if (Input.GetButtonDown("Submit"))
            {
                isMoving = true;
                movableScript.moveSearch(Mathf.RoundToInt(pos.x) / posDiv, Mathf.RoundToInt(pos.z) / posDiv, moveCost, name);
                float ix = cirsor.transform.position.x / posDivF;
                float iz = cirsor.transform.position.z / posDivF;
                this.transform.position = new Vector3(Mathf.CeilToInt(ix) * posDivF, this.transform.position.y,Mathf.CeilToInt(iz) * posDivF);
                isMove = false;
                isMovable = false;
                isMoving = false;
            }
        }
    }

    /// <summary>
    /// 移動可能範囲の初期化
    /// </summary>
    public void MovableOff()
    {
        if (isMoving)
        {
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    if (map.block[x, y].movable)
                    {
                        map.block[x, y].movable = false;
                    }

                }
            }


            isMoving = false;
        }
    }


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
        pos = this.transform.position;
        // 関数実行
        CharaUpCorsor();
        EnterButton();
        movableScript.moveSearch(Mathf.RoundToInt(pos.x) / posDiv, Mathf.RoundToInt(pos.z) / posDiv, moveCost, name);
        CirsorIn();
        CharacterMove();

        // メニューウィンドウの切り替え
        if (isUpCirsor && isPress)
        {
            // メニューの表示
            // ウィンドウ入力のboolをOnに
            isMove = true;
        }
        if (isMove)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                isMove = false;
            }
        }
        ChangeColor();

        // 色の変更
		map.DrawMap();
        isMoving = false;
	}
}
