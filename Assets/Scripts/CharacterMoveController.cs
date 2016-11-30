using UnityEngine;
using System.Collections;

public class CharacterMoveController : MonoBehaviour {

    // 参照
    MapController map;
    MovableScript movableScript;
    GameObject cirsor;
    CirsorController circon;
    UnitController unit;
    public int unitNumber;
    GameObject unitObj;

    public Vector3 pos;

    /// <summary>
    /// 各種ブーリアン
    /// </summary>
    public bool isMove = false;         // 移動中か
    public bool isUpCirsor = false;     // カーソルがキャラの上にいるか
    public bool isPress = false;        // 移動ボタン選択中か
    public bool isMovable = false;      // その位置にキャラを移動可能か
    public bool isMoving = false;



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
        if (i == 1)
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
        else if (i == 3)
        {
            map.isAlpha = true;
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
        if (unitObj.transform.position.x == cirsor.transform.position.x && unitObj.transform.position.z == cirsor.transform.position.z)
        {
            isUpCirsor = true;
        }
        else
        {
            isUpCirsor = false;
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

            if (map.block[Mathf.RoundToInt(fx), Mathf.RoundToInt(fz)].movable)
            {
                isMovable = true;
            }
            else
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
        // 移動中かつ移動可能なマスの時
        if (isMove && isMovable)
        {
            if (Input.GetButtonDown("Submit"))
            {
                // isMoving = true;
                // movableScript.moveSearch(Mathf.RoundToInt(pos.x) / posDiv, Mathf.RoundToInt(pos.z) / posDiv, moveCost, name);
                float ix = cirsor.transform.position.x / posDivF;
                float iz = cirsor.transform.position.z / posDivF;
                unitObj.transform.position = new Vector3(Mathf.CeilToInt(ix) * posDivF, unitObj.transform.position.y, Mathf.CeilToInt(iz) * posDivF);
                isMove = false;
                isMoving = true;
                // isMovable = false;
                // isMoving = false;
            }
        }
    }

    
    

    /// <summary>
    /// 移動キャンセル
    /// </summary>
    public void Cancel()
    {
        if (isMove)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                isMove = false;
            }
        }
    }

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        if (isMoving == true)
        {
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    map.block[x, y].movable = false;
                    map.block[x, y].step = -1;
                    // Debug.Log (map.block [x, y]);
                    isMoving = false;
                }
            }
            // movableScript.moveSearch(Mathf.RoundToInt(pos.x) / posDiv, Mathf.RoundToInt(pos.z) / posDiv, unit.playerController[unitNumber].moveCost);	// 移動可能なマスの取得
            ChangeColor();
            map.DrawMap();          // 色塗り
        }
    }


    void Start()
    {
        // Find
        map = GameObject.Find("MapManager").GetComponent<MapController>();
        movableScript = GameObject.Find("MapManager").GetComponent<MovableScript>();
        cirsor = GameObject.Find("Cirsor");
        circon = cirsor.GetComponent<CirsorController>();
        unit = GameObject.FindGameObjectWithTag("UniCon").GetComponent<UnitController>();
    }

    void Update()
    {
#if _Debug
        //CharaUpCorsor();        // カーソルがキャラの上にいるかどうか
        if (unit.isUnit)
        {
            unitNumber = unit.selectUnit;
            unitObj = unit.playerObj[unitNumber];
            circon.not = 1;
            // 関数実行
            
            EnterButton();          // 決定キーを押しているかの判定
            pos = unitObj.transform.position;
            Menu();
            movableScript.moveSearch(Mathf.RoundToInt(pos.x) / posDiv, Mathf.RoundToInt(pos.z) / posDiv, unit.playerController[unitNumber].moveCost);   // 移動可能なマスの取得
            ChangeColor();          // 色変え
            map.DrawMap();          // 色塗り
            CirsorIn();             // カーソルが移動可能範囲のますかどうか
            Cancel();               // キャンセル機能
            CharacterMove();        // キャラクターの移動
        }
        else
        {
            circon.not = -1;
            unit.isUnit = false;
        }
        if (!isUpCirsor)
        {
            unit.isUnit = false;
            circon.not = -1;
            unit.selectUnit = 99;
        }
        Initialize();
        map.DrawMap();
#endif
        CharaUpCorsor();
        if (isUpCirsor)
        {
            // 移動範囲のサーチ
            movableScript.moveSearch(Mathf.RoundToInt(pos.x) / posDiv, Mathf.RoundToInt(pos.z) / posDiv, unit.playerController[unitNumber].moveCost);   // 移動可能なマスの取得
        }
        
        for (int i = 0;i < unit.playerObj.Length; i++)
        {
            if(map.block[Mathf.RoundToInt(circon.transform.position.x), Mathf.RoundToInt(circon.transform.position.z)].movable)
            {
                isMovable = true;
            }
            if (!isMovable)
            {
                isMovable = false;
            }
        }
        circon.Search();
        EnterButton();
        Menu();
        ChangeColor();
        CharacterMove();
        Initialize();
        // map.DrawMap();
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
        else if (isMove == false && !isUpCirsor)
        {
            ColorChange(3);
        }
    }

    /// <summary>
    /// メニューウィンドウの切り替え
    /// カーソルがキャラの上かつボタンを押したら
    /// </summary>
    public void Menu()
    {
        if (isPress)
        {
            // メニューの表示
            // ウィンドウ入力のboolをOnに
            // 
            isMove = true;
        }
    }

    /// <summary>
    /// 決定キーを押しているかの判定
    /// </summary>
    public void EnterButton()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (!isPress)
            {
                isPress = true;
            }
            else
            {
                isPress = false;
            }
        }
    }
}
