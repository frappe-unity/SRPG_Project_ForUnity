using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAIController : MonoBehaviour {

    [SerializeField] private UnitController unitcontroller;
    [SerializeField] private MovableScript movablescript;
    [SerializeField] private MapController mapcontroller;
    [SerializeField] private GameController gamecontroller;
    [SerializeField] private AttackController attackcontroller;
    [SerializeField] private WeaponData weapondata;
    public int cirsorEnemy;
    // public GameObject enemy;
    public bool section = true;
    public bool battle = false;
    public bool moveSkip = false;
    List<int> areaPlayer = new List<int>();
    List<Vector2> movable = new List<Vector2>();
    public int selectPlayer;
    public float moveTime = 1F;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (!gamecontroller.playerTurn)
        {
            if(section && unitcontroller.stayCount < unitcontroller.enemy.Count)
            {
                section = false;

                Initialize();
                cirsorEnemy = unitcontroller.enemyController[unitcontroller.stayCount].enemyID;
                // enemy = unitcontroller.enemy[cirsorEnemy];
                Search();                                                               

            }
            if (unitcontroller.stayCount >= unitcontroller.enemy.Count  && section)
            {
                // Debug.Log("startplayer");
                unitcontroller.stayCount = 0;
                gamecontroller.StartPlayer();
            }
        }
	}

    public void Search()
    {
        areaPlayer.Clear();
        movable.Clear();
        var selectEnemy = unitcontroller.enemyController[cirsorEnemy];
        unitcontroller.UnitMovable();
        CirclePlayerSearch(Mathf.RoundToInt(selectEnemy.enemyPos.x), Mathf.RoundToInt(selectEnemy.enemyPos.y));
        if (!moveSkip)
        {
            movablescript.Initialize();
            unitcontroller.UnitMovable();
            movablescript.MoveSearch(Mathf.RoundToInt(selectEnemy.enemyPos.x), Mathf.RoundToInt(selectEnemy.enemyPos.y), selectEnemy.moveCost, 1);
            int range = 0;
            range = WeaponRangeSearch(range);
            movablescript.AttackSearch(range);
            mapcontroller.DrawMap();
            PlayerSearch();
            MoveSquare(Mathf.RoundToInt(unitcontroller.playerController[selectPlayer].playerPos.x), Mathf.RoundToInt(unitcontroller.playerController[selectPlayer].playerPos.y), weapondata.blade[unitcontroller.enemyController[cirsorEnemy].selectWeapon].range);
            // Debug.Log(movable.Count);
            if (movable.Count > 0)
            {
                // 移動
                Invoke("Move", moveTime);
            }
            else if (movable.Count == 0)
            {
                Invoke("NextChara", moveTime);
            }
        }
        
    }

    public void CirclePlayerSearch(int x, int y)
    {
        var up = mapcontroller.block[x, y - 1];
        var down = mapcontroller.block[x, y + 1];
        var right = mapcontroller.block[x + 1, y];
        var left = mapcontroller.block[x - 1, y];
        if (up.playerOn)
        {
            Debug.Log("Skip");
            selectPlayer = up.playerID;
            MoveAttack();
        }
        if (down.playerOn)
        {
            Debug.Log("Skip");
            selectPlayer = down.playerID;
            MoveAttack();
        }
        if (right.playerOn)
        {
            Debug.Log("Skip");
            selectPlayer = right.playerID;
            MoveAttack();
        }
        if (left.playerOn)
        {
            Debug.Log("Skip");
            selectPlayer = left.playerID;
            MoveAttack();
        }
    }

    public void MoveAttack()
    {
        moveSkip = true;
        unitcontroller.UnitMovable();
        Invoke("Attack", moveTime);
    }

    public int WeaponRangeSearch(int range)
    {
        for (int i = 0; i < unitcontroller.enemyController[cirsorEnemy].weapon.Length; i++)
        {
            if (weapondata.blade[unitcontroller.enemyController[cirsorEnemy].weapon[i]].range > range)
            {
                range = weapondata.blade[unitcontroller.enemyController[cirsorEnemy].weapon[i]].range;
            }
        }
        return range;
    }

    public void PlayerSearch()
    {
        for(int i = 0;i < unitcontroller.playerController.Count; i++)
        {
            var player = unitcontroller.playerController[i];
            if(mapcontroller.block[Mathf.RoundToInt(player.playerPos.x), Mathf.RoundToInt(player.playerPos.y)].enemyAttackable)
            {
                areaPlayer.Add(i);
            }
        }
        for (int j = 0;j < areaPlayer.Count; j++)
        {
            if(j == 0)
            {
                selectPlayer = unitcontroller.playerController[areaPlayer[0]].playerID;
            } else
            {
                if(unitcontroller.playerController[selectPlayer].hp < unitcontroller.playerController[areaPlayer[j]].hp)
                {
                    selectPlayer = unitcontroller.playerController[areaPlayer[j]].playerID;
                }
            }
        }
    }

    public void MoveSquare(int x, int y, int step)
    {
        if(step > 1)
        {
            // 上
            if (mapcontroller.block[x, y - 1].movable)
            {
                MoveSquare(x, y - 1, step - 1);
            }
            //　下
            if (mapcontroller.block[x, y + 1].movable)
            {
                MoveSquare(x, y + 1, step - 1);
            }
            // 右
            if (mapcontroller.block[x + 1, y].movable)
            {
                MoveSquare(x + 1, y, step - 1);
            }
            // 左
            if (mapcontroller.block[x - 1, y].movable)
            {
                MoveSquare(x - 1, y, step - 1);
            }
        }
        else if(step == 1)
        {
            // 上
            if (mapcontroller.block[x, y - 1].movable && !mapcontroller.block[x, y - 1].enemyOn)
            {
                movable.Add(new Vector2(x, y - 1));
            }
            //　下
            if (mapcontroller.block[x, y + 1].movable && !mapcontroller.block[x, y + 1].enemyOn)
            {
                movable.Add(new Vector2(x, y + 1));

            }
            // 右                                     
            if (mapcontroller.block[x + 1, y].movable && !mapcontroller.block[x + 1, y].enemyOn)
            {
                movable.Add(new Vector2(x + 1, y));

            }                                         
            // 左
            if (mapcontroller.block[x - 1, y].movable && !mapcontroller.block[x - 1, y].enemyOn)
            {
                movable.Add(new Vector2(x - 1, y));
            }
        }
    }

    public void Move()
    {
        // 移動
        Vector2 pos = movable[Random.Range(0, movable.Count)];
        // Vector2 pos = movable[0];
        unitcontroller.enemyController[cirsorEnemy].enemyPos = pos;
        unitcontroller.UnitMovable();
        // section = true;
        Invoke("Attack", moveTime);
    }

    public void Attack()
    {
        Debug.Log(selectPlayer);
        Debug.Log(unitcontroller.playerController[selectPlayer]);
        attackcontroller.PlayerAttack(selectPlayer);
        attackcontroller.EnemyAttack(cirsorEnemy);
        attackcontroller.BattleParam();
        attackcontroller.Battle();
        battle = true;
    }

    public void NextChara()
    {
        unitcontroller.enemyController[cirsorEnemy].isAct = true;
        unitcontroller.stayCount++;
        battle = false;
        section = true;
        moveSkip = false;
    }

    // 初期化
    public void Initialize()
    {
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                mapcontroller.block[x, y].enemyMovable = false;
                mapcontroller.block[x, y].color = 0;
            }
        }
        mapcontroller.DrawMap();
    }
}
