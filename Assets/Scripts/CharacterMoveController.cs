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
    [SerializeField] private WeaponData weapondata;
    [SerializeField] private GameObject cirsor;
    [SerializeField] private GameObject window;
    [SerializeField] private GameObject eventSystem;

    /// <summary>
    /// 各種ブーリアン
    /// </summary>
    public bool isCirsor = false;   // カーソルがキャラ上にあるか
    public bool isMove = false;
    public bool isMenu = false;
    public bool backMenu = false;
    public int stateCount = -1; // ステート番号
    private int color = 0;

    /// <summary>
    /// ユニット情報
    /// </summary>
    public int unitNumber = 0;
    public int enemyNumber = 0;
    GameObject unitObj;
    GameObject enemyObj;

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
        ATTACK,
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
        if (gm.playerTurn && unit.selectUnit != 99)
        {
            if(stateCount <= 1)
            {
                MoveSearch();
            }
            

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
                            if (Input.GetButtonDown("Submit") && isMove && !backMenu)
                            {
                                savePos = unit.playerController[unitNumber].unitPos;    // posを保存しておく
                                                                                        // カーソルの位置にキャラを移動
                                unit.playerController[unitNumber].unitPos = cirsorController.cirsorPos;
                                isMove = false;
                                Initialize();
                                MenuFunc();
                                playerState = PlayerState.MENU;         // ステート移動
                            }
                        }
                        // 色塗り
                        map.DrawMap();
                        break;
                    case PlayerState.MENU:
                        Initialize();
                        AttackRange();
                        // ステート番号
                        stateCount = 2;
                        // メニュー関数を実行
                        break;
                    case PlayerState.ATTACK:
                        enemyNumber = unit.selectEnemy;
                        Initialize();
                        AttackRange();
                        if (map.block[cirsorX, cirsorY].attackable)
                        {
                            if (Input.GetButtonDown("Submit"))
                            {
                                Attack(unitNumber, enemyNumber);
                            }
                        }
                        break;
                }

                /// <summary>
                /// キャンセル
                /// </summary>
                if (Input.GetButtonDown("Cancel"))
                {
                    Cancel();
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

        unit.UnitMovable();
        movableScript.moveSearch(x, y, unit.playerController[unitNumber].moveCost, stateCount);
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
        } else if (stateCount == 3)
        {
            playerState = PlayerState.ATTACK;
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
        //stateCount = 0;
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

    public void Cancel()
    {
        if (stateCount == 2)
        {
            MenuEnd();
            ReturnPos();
        }
        stateCount--;
        // キャンセル後のstate移動
        MoveState();
    }

    

    /// <summary>
    /// 攻撃範囲取得
    /// </summary>
    public void AttackRange()
    {
        Initialize();
        movableScript.AttackRange(x, y, 2);
        map.DrawMap();
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    public void Attack(int playernum, int enemynum)
    {
        int power, guard, damage, hit, avoid, critical, c_avoid, hitper, criticalper, rhit, rcritical;
        power = unit.playerController[playernum].attack + weapondata.blade[unit.playerController[playernum].weapon[playernum]].attack;
        guard = unit.enemyController[enemynum].deffence;
        hit = weapondata.blade[unit.playerController[playernum].weapon[playernum]].hitper + (unit.playerController[playernum].hit / 2);
        avoid = unit.enemyController[enemynum].speed * 2 + unit.enemyController[enemynum].lucky;
        critical = weapondata.blade[unit.playerController[playernum].weapon[playernum]].criticalper + (unit.playerController[playernum].hit / 2);
        c_avoid = unit.enemyController[enemynum].lucky;
        hitper = hit - avoid;
        criticalper = critical - c_avoid;
        rcritical = Random.Range(1, criticalper);
        if(rcritical <= criticalper || criticalper >= 100)
        {
            damage = (power - guard) * 3;
        } else
        {
            damage = power - guard;
        }
        rhit = Random.Range(1, hitper);
        if(rhit <= hitper || hitper >= 100)
        {
            unit.enemyController[enemynum].hp -= damage;
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
                map.block[x, y].color = 0;
            }
        }
        map.DrawMap();         
    }
}
