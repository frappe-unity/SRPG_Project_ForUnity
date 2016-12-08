using UnityEngine;
using System.Collections;

public class CharacterMoveController : MonoBehaviour {


    /// <summary>
    /// 各種参照
    /// </summary>
    [SerializeField] private MapController map;
    [SerializeField] private MovableScript movableScript;
    [SerializeField] private CirsorController cirsorController;
    [SerializeField] private UnitController unit;
    [SerializeField] private PlayerController player;
    [SerializeField] private GameController gm;
    [SerializeField] private GameObject cirsor;
    [SerializeField] private GameObject window;
    [SerializeField] private GameObject eventSystem;

    /// <summary>
    /// 各種ブーリアン
    /// </summary>
    public bool isCirsor = false;   // カーソルがキャラ上にあるか
    public bool isMove = false;
    public bool isMenu = false;
    public int stateCount = -1; // ステート番号
    private int color = 0;

    /// <summary>
    /// ユニット情報
    /// </summary>
    public int unitNumber = 0;
    GameObject unitObj;
    /// <summary>
    /// ユニット情報
    /// </summary>
    public int x = 0;
    public int y = 0;
    public int cirsorX = 0;
    public int cirsorY = 0;

    public Vector3 pos;
    public Vector2 savePos;
    private int div = 10;
    private float divF = 10F;

    public enum PlayerState
    {
        START,
        MOVE,
        MENU,
    }

    public PlayerState playerState;

    void Start()
    {
        eventSystem.SetActive(true);
        window.SetActive(false);
    }

    void Update()
    {
        /// <summary>
        /// Playerターンなら
        /// </summary>
        if (gm.playerTurn)
        {
            MoveSearch();

            if (!unit.playerController[unitNumber].isAct)
            {
                switch (playerState)
                {
                    case PlayerState.START:
                        // ステート番号
                        stateCount = 0;

                        /// <summary>
                        /// カーソル判定
                        /// </summary>
                        if (x == cirsorX && y == cirsorY)
                        {
                            isCirsor = true;
                            color = 1;

                        }
                        else
                        {
                            Initialize();
                            isMove = false;
                            isCirsor = false;
                            color = 0;
                        }

                        // キャラ上でボタンを押したらMOVEに移る
                        if (Input.GetButtonDown("Submit") && isCirsor && stateCount == 0)
                        {
                            isMove = true;
                            playerState = PlayerState.MOVE;     // ステート移動
                        }

                        // 色塗り
                        ChangeColor(color);
                        map.DrawMap();
                        break;

                    case PlayerState.MOVE:
                        // ステート番号
                        stateCount = 1;

                        /// <summary>
                        /// 探索
                        /// </summary>
                        if (map.block[cirsorX, cirsorY].movable)
                        {
                            /// <summary>
                            /// 移動処理
                            /// </summary>
                            if (Input.GetButtonDown("Submit") && isMove)
                            {
                                savePos = unit.playerController[unitNumber].unitPos;    // posを保存しておく
                                                                                        // カーソルの位置にキャラを移動
                                unit.playerController[unitNumber].unitPos = cirsorController.cirsorPos;
                                isMove = false;
                                Initialize();
                                playerState = PlayerState.MENU;         // ステート移動
                            }
                        }

                        // 色塗り
                        color = 2;
                        ChangeColor(color);
                        map.DrawMap();
                        break;

                    case PlayerState.MENU:
                        // ステート番号
                        stateCount = 2;
                        Initialize();
                        // メニュー関数を実行
                        MenuFunc();

                        // 色塗り
                        color = 0;
                        ChangeColor(color);
                        map.DrawMap();
                        break;
                }

                /// <summary>
                /// キャンセル
                /// </summary>
                if (Input.GetButtonDown("Cancel"))
                {
                    stateCount--;
                    // キャンセル後のstate移動
                    MoveState();
                }
                map.DrawMap();
            }
        }
        
    }

    /// <summary>
    /// 移動範囲探索
    /// </summary>
    public void MoveSearch()
    {
        Initialize();

        /// <summary>
        /// 必要な情報の取得
        /// </summary>
        unitNumber = unit.selectUnit;                                       // 選択ユニットの番号
        unitObj = unit.playerObj[unitNumber];                               // ユニットの取得
        
        x = Mathf.RoundToInt(unit.playerController[unitNumber].unitPos.x);       // ユニットのX座標
        y = Mathf.RoundToInt(unit.playerController[unitNumber].unitPos.y);       // ユニットのZ座標
        cirsorX = Mathf.FloorToInt(cirsorController.cirsorPos.x);  // カーソルのX座標
        cirsorY = Mathf.FloorToInt(cirsorController.cirsorPos.y);  // カーソルのZ座標

        movableScript.moveSearch(x, y, unit.playerController[unitNumber].moveCost);
    }

    /// <summary>
    /// state移動
    /// </summary>
    public void MoveState()
    {
        if (stateCount == 0)
        {
            Initialize();
            playerState = PlayerState.START;
        }
        else if (stateCount == 1)
        {
            playerState = PlayerState.MOVE;
        }
    }

    /// <summary>
    /// メニュー処理
    /// </summary>
    public void MenuFunc()
    {
        if (!isMenu)
        {
            isMenu = true;
            eventSystem.SetActive(false);
            window.SetActive(true);
        } 
    }

    public void MenuEnd()
    {
        isMenu = false;
        eventSystem.SetActive(true);
        window.SetActive(false);
        stateCount = 0;
    }

    public void EndAct()
    {
        unit.playerController[unitNumber].isAct = true;
        unit.stayCount++;
        stateCount = 0;
    }

    public void ReturnPos()
    {
        isMove = true;
        unit.playerController[unitNumber].unitPos = savePos;
        cirsor.transform.position = new Vector3(savePos.x * div, cirsor.transform.position.y, savePos.y * div);
        cirsorController.cirsorPos = new Vector2(savePos.x, savePos.y);
        cirsorController.isMove = true;
        cirsorController.Move();
        Initialize();
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
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                map.block[x, y].movable = false;
                map.block[x, y].step = -1;
            }
        }
        ChangeColor(0);
        map.DrawMap();         
    }
}
