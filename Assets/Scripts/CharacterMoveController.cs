using UnityEngine;
using System.Collections;

public class CharacterMoveController : MonoBehaviour {


    /// <summary>
    /// 各種参照
    /// </summary>
    MapController map;
    MovableScript movableScript;
    CirsorController cirsorController;
    UnitController unit;
    PlayerController player;
    GameObject cirsor;

    /// <summary>
    /// 各種ブーリアン
    /// </summary>
    public bool isCirsor = false;   // カーソルがキャラ上にあるか
    public bool isMove = false;
    public int moveStep = 0; // 移動

    /// <summary>
    /// ユニット情報
    /// </summary>
    private int unitNumber = 0;
    GameObject unitObj;

    public Vector3 pos;
    private int div = 10;
    private float divF = 10F;

    void Start()
    {
        // 各種参照
        map = GameObject.Find("MapManager").GetComponent<MapController>();
        movableScript = GameObject.Find("MapManager").GetComponent<MovableScript>();
        cirsor = GameObject.Find("Cirsor");
        cirsorController = cirsor.GetComponent<CirsorController>();
        unit = GameObject.FindGameObjectWithTag("UniCon").GetComponent<UnitController>();

        
    }

    void Update()
    {

        

        /// <summary>
        /// 初期化
        /// </summary>
        if (!isMove)
        {
            moveStep = 0;
            Initialize();
        }
        

        /// <summary>
        /// 必要な情報の取得
        /// </summary>
        unitNumber = unit.selectUnit;
        unitObj = unit.playerObj[unitNumber];
        int x = Mathf.FloorToInt(unitObj.transform.position.x) / div;
        int z = Mathf.FloorToInt(unitObj.transform.position.z) / div;
        int cirsorX = Mathf.FloorToInt(cirsor.transform.position.x) / div;
        int cirsorZ = Mathf.FloorToInt(cirsor.transform.position.z) / div;

        /// <summary>
        /// カーソル判定
        /// </summary>
        if (x == cirsorX && z == cirsorZ)
        {
            isCirsor = true;
            moveStep = 1;
        } else
        {
            isCirsor = false;
        }

        /// <summary>
        /// 探索
        /// </summary>
        movableScript.moveSearch(x, z, unit.playerController[unitNumber].moveCost);
        if (map.block[cirsorX, cirsorZ].movable)
        {
            if (Input.GetButtonDown("Submit") && isCirsor)
            {
                isMove = true;
                moveStep = 2;
            }
            
        }

        /// <summary>
        /// 移動処理
        /// </summary>
        if (isMove)
        {
            moveStep = 2;
            if (Input.GetButtonDown("Submit"))
            {
                pos.x = cirsor.transform.position.x;
                pos.z = cirsor.transform.position.z;
                unitObj.transform.position = pos;
                moveStep = 0;
                isMove = false;
            }
        }

        /// <summary>
        /// キャンセル
        /// </summary>
        if (Input.GetButtonDown("Cancel"))
        {
            moveStep = 0;
        }

        ChangeColor(moveStep);
        map.DrawMap();
    }

    /// <summary>
    /// 色変え
    /// </summary>
    public void ChangeColor(int i)
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

        // 0なら透明
        else if (i == 0)
        {
            map.isAlpha = true;
            map.colorR = 1F;
            map.colorG = 1F;
            map.colorB = 1F;
            map.alphaA = 0F;
        }
    }

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        for (int x = 0; x < unit.playerObj.Length; x++)
        {
            for (int y = 0; y < unit.playerObj.Length; y++)
            {
                map.block[x, y].movable = false;
                map.block[x, y].step = -1;
            }
        }
        ChangeColor(moveStep);
        map.DrawMap();         
    }
}
