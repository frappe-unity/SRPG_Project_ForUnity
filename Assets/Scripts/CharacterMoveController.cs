using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterMoveController : MonoBehaviour {


    /// <summary>
    /// 各種参照
    /// </summary>
    [SerializeField] private MapController map;
    [SerializeField] private MovableScript movablescript;
    [SerializeField] private AttackController attackcontroller;
    [SerializeField] private CirsorController cirsorcontroller;
    [SerializeField] private UnitController unitcontroller;
    [SerializeField] private PlayerController player;
    [SerializeField] private GameController gm;
    [SerializeField] private WeaponData weapondata;
    [SerializeField] private GameObject cirsor;
    [SerializeField] private GameObject[] window;
    [SerializeField] private GameObject eventSystem;

    /// <summary>
    /// 各種ブーリアン
    /// </summary>
    public bool isCirsor = false;   // カーソルがキャラ上にあるか
    public bool isMove = false;
    public bool isMenu = false;
    public bool isAttack = false;
    public bool backMenu = false;
    public int stateCount = -1; // ステート番号
    private int color = 0;

    /// <summary>
    /// ユニット情報
    /// </summary>
    public int playerID = 0;
    public int enemyNumber = 0;
    // GameObject unitObj;
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
        SELECT_ATTACK,
        ATTACK,
    }

    public PlayerState playerState;

    void Start()
    {
        eventSystem.SetActive(true);
        for(int i = 0;i < window.Length; i++)
        {
            window[i].SetActive(false);
        }
    }

    void Update()
    {
        /// <summary>
        /// Playerターンなら
        /// </summary>
        if (gm.playerTurn && unitcontroller.selectPlayer != 99 && unitcontroller.player.Count > 0)
        {
            
            if(stateCount <= 1)
            {
                MoveSearch();
            }
            
            if (!unitcontroller.playerController[playerID].isAct)
            {
                if (stateCount <= 1)
                {
                    MoveSearch();
                }
                switch (playerState)
                {
                    case PlayerState.START:
                        unitcontroller.turnState = UnitController.TurnState.START;
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
                            playerID = 99;
                            movablescript.Initialize();
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
                        unitcontroller.turnState = UnitController.TurnState.MOVE;
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
                                savePos = unitcontroller.playerController[playerID].playerPos;    // posを保存しておく
                                                                                        // カーソルの位置にキャラを移動
                                unitcontroller.playerController[playerID].playerPos = cirsorcontroller.cirsorPos;
                                isMove = false;
                                unitcontroller.UnitMovable();
                                movablescript.Initialize();
                                MenuFunc(0);
                                playerState = PlayerState.MENU;         // ステート移動
                                unitcontroller.turnState = UnitController.TurnState.MENU;
                            }
                        }
                        // 色塗り
                        map.DrawMap();
                        break;
                    case PlayerState.MENU:
                        movablescript.Initialize();
                        AttackRange();
                        // ステート番号
                        stateCount = 2;
                        // メニュー関数を実行
                        break;
                    case PlayerState.SELECT_ATTACK:
                        unitcontroller.turnState = UnitController.TurnState.SELECT_ATTACK;
                        movablescript.Initialize();
                        MenuFunc(1);
                        break;
                    case PlayerState.ATTACK:
                        unitcontroller.turnState = UnitController.TurnState.ATTACK;
                        movablescript.Initialize();
                        AttackRange();
                        // Debug.Log("AttackFase");
                        if (map.block[cirsorX, cirsorY].enemyOn && !backMenu && !isAttack)
                        {
                            // Debug.Log("if");
                            EnemySearch();
                            attackcontroller.PlayerAttack(unitcontroller.selectPlayer);
                            attackcontroller.EnemyAttack(unitcontroller.selectEnemy);
                            attackcontroller.BattleParam();
                            if (Input.GetButtonDown("Submit") && !isAttack)
                            {
                                isAttack = true;
                                // Debug.Log("enter");
                                attackcontroller.Battle();
                                EndAct();
                                movablescript.Initialize();
                                MoveState();
                                isAttack = false;
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
        unitcontroller.UnitMovable();
        movablescript.Initialize();
        /// <summary>
        /// 必要な情報の取得
        /// </summary>
        for(int i = 0;i < unitcontroller.player.Count; i++)
        {
            if(unitcontroller.selectPlayer == unitcontroller.playerController[i].playerID)
            {
                playerID = i;
            }
        }
        // Debug.Log(playerID);
        // playerID = unitcontroller.selectPlayer;                                       // 選択ユニットの番号
        // unitObj = unitcontroller.player[playerID];                               // ユニットの取得
        x = Mathf.RoundToInt(unitcontroller.playerController[playerID].playerPos.x);       // ユニットのX座標
        y = Mathf.RoundToInt(unitcontroller.playerController[playerID].playerPos.y);       // ユニットのZ座標
        cirsorX = Mathf.FloorToInt(cirsorcontroller.cirsorPos.x);  // カーソルのX座標
        cirsorY = Mathf.FloorToInt(cirsorcontroller.cirsorPos.y);  // カーソルのZ座標
        if (!unitcontroller.playerController[playerID].isAct)
        {
            movablescript.MoveSearch(x, y, unitcontroller.playerController[playerID].moveCost, stateCount);
            int range = 0;
            range = WeaponRangeSearch(range);
            // Debug.Log(range);
            movablescript.AttackSearch(range);
        }
    }

    public int WeaponRangeSearch(int range)
    {
        for(int i = 0;i < unitcontroller.playerController[playerID].weapon.Length; i++)
        {
            if(weapondata.blade[unitcontroller.playerController[playerID].weapon[i]].range > range)
            {
                range = weapondata.blade[unitcontroller.playerController[playerID].weapon[i]].range;
            }
        }
        // Debug.Log("range:" + range);
        return range;
    }

    /// <summary>
    /// state移動
    /// </summary>
    public void MoveState()
    {
        if (stateCount == 0)
        {
            movablescript.Initialize();
            playerState = PlayerState.START;
        }
        else if (stateCount == 1)
        {
            playerState = PlayerState.MOVE;
        }
        else if (stateCount == 3)
        {
            playerState = PlayerState.SELECT_ATTACK;
        }
        else if(stateCount == 4)
        {
            playerState = PlayerState.ATTACK;
        }
    }

    /// <summary>
    /// メニュー処理
    /// </summary>
    public void MenuFunc(int i)
    {
        if (!isMenu)
        {
            isMenu = true;
            eventSystem.SetActive(false);
            window[i].SetActive(true);
            if(i == 1)
            {
                GameObject.Find("Content").GetComponent<WeaponScrollController>().NodeInstance();
            }
        } 
    }

    public void MenuEnd()
    {
        isMenu = false;
        eventSystem.SetActive(true);
        for(int i = 0;i < window.Length; i++)
        {
            window[i].SetActive(false);
        }
        //stateCount = 0;
    }

    public void EndAct()
    {
        isCirsor = false;
        isMove = false;
        isMenu = false;
        backMenu = false;
        unitcontroller.playerController[playerID].isAct = true;
        unitcontroller.stayCount++;
        stateCount = 0;
        MoveState();
    }

    public void ReturnPos()
    {
        isMove = true;
        unitcontroller.playerController[playerID].playerPos = savePos;
        cirsor.transform.position = new Vector3(savePos.x * div, cirsor.transform.position.y, savePos.y * div);
        cirsorcontroller.cirsorPos = new Vector2(savePos.x, savePos.y);
        cirsorcontroller.isMove = true;
        cirsorcontroller.Move();
        movablescript.Initialize();

    }

    public void Cancel()
    {
        if (stateCount == 2 || stateCount == 3)
        {
            MenuEnd();
            ReturnPos();
        }
        stateCount--;
        unitcontroller.UnitMovable();
        // キャンセル後のstate移動
        MoveState();
    }

    
    public void EnemySearch()
    {
        for(int i = 0;i < unitcontroller.enemy.Count; i++)
        {
            if(cirsorX == unitcontroller.enemyController[i].enemyPos.x && cirsorY == unitcontroller.enemyController[i].enemyPos.y)
            {
                unitcontroller.selectEnemy = i;
            }
        }
    }

    /// <summary>
    /// 攻撃範囲取得
    /// </summary>
    public void AttackRange()
    {
        movablescript.Initialize();
        unitcontroller.UnitMovable();

        /// <summary>
        /// 必要な情報の取得
        /// </summary>
        for (int i = 0;i < unitcontroller.player.Count; i++)
        {
            if(unitcontroller.selectPlayer == unitcontroller.playerController[i].playerID)
            {
                playerID = i;
            }
        }
        // playerID = unitcontroller.selectPlayer;                                       // 選択ユニットの番号
        // unitObj = unitcontroller.playerObj[playerID];                               // ユニットの取得

        x = Mathf.RoundToInt(unitcontroller.playerController[playerID].playerPos.x);       // ユニットのX座標
        y = Mathf.RoundToInt(unitcontroller.playerController[playerID].playerPos.y);       // ユニットのZ座標
        cirsorX = Mathf.FloorToInt(cirsorcontroller.cirsorPos.x);  // カーソルのX座標
        cirsorY = Mathf.FloorToInt(cirsorcontroller.cirsorPos.y);  // カーソルのZ座標

        movablescript.AttackRange(x, y, weapondata.blade[unitcontroller.playerController[playerID].selectWeapon].range);
        map.DrawMap();
    }
}
