using UnityEngine;
using System.Collections;

public class MovableScript : MonoBehaviour {
	[SerializeField] private MapController map;
    [SerializeField] private UnitController unitcontroller;
    [SerializeField] private GameController gamecontroller;

    public int stepCount;
   
	void Start () {
    }

    void Update()
    {
        
    }

	public void MoveSearch( int x, int y, int step, int num){
        var up = map.block[x, y - 1];
        var down = map.block[x, y + 1];
        var right = map.block[x + 1, y];
        var left = map.block[x - 1, y];
        if (step == 0 && map.block[x, y].savestep == -1)
        {
            map.block[x, y].savestep = 0;
            map.block[x, y].color = 3;
        }
        if (map.block[x, y].movable && step > -1)
        {
            map.block[x, y].movable = true;
            if (num == 0)
            {
                map.block[x, y].color = 1;
            }
            else if (num == 1)
            {
                map.block[x, y].color = 2;
            }
        } else if(map.block[x, y].movable && step <= -1)
        {
            map.block[x, y].savestep = 0;
            map.block[x, y].color = 3;
        }
        if (step > -1)
        {
            if (gamecontroller.playerTurn)
            {
                // 上
                if (up.blockNum != -20 && !up.enemyOn && step + 1 >= -up.blockNum)
                {
                    if (up.savestep < step)
                    {
                        up.savestep = step;
                    }
                    if (up.blockNum != -20 && !up.enemyOn && step >= -up.blockNum)
                    {
                        up.movable = true;
                        MoveSearch(x, y - 1, step + up.blockNum, num);
                    }
                } else if (up.blockNum != -20 && !up.enemyOn && step + 1 < -up.blockNum && !up.movable)
                {
                    up.savestep = 0;
                    up.color = 3;
                }
                //　下
                if (down.blockNum != -20 && !down.enemyOn && step + 1 >= -down.blockNum)
                {
                    if (down.savestep < step)
                    {
                        down.savestep = step;
                    }
                    if (down.blockNum != -20 && !down.enemyOn && step >= -down.blockNum)
                    {
                        down.movable = true;
                        MoveSearch(x, y + 1, step + down.blockNum, num);
                    }
                } else if (down.blockNum != -20 && !down.enemyOn && step + 1 < -down.blockNum && !down.movable)
                {
                    down.savestep = 0;
                    down.color = 3;
                }
                // 右
                if (right.blockNum != -20 && !right.enemyOn && step + 1 >= -right.blockNum)
                {
                    if (right.savestep < step)
                    {
                        right.savestep = step;
                    }
                    if (right.blockNum != -20 && !right.enemyOn && step >= -right.blockNum)
                    {
                        right.movable = true;
                        MoveSearch(x + 1, y, step + right.blockNum, num);
                    }
                } else if (right.blockNum != -20 && !right.enemyOn && step + 1 < -right.blockNum && !right.movable)
                {
                    right.savestep = 0;
                    right.color = 3;
                }
                // 左
                if (left.blockNum != -20 && !left.enemyOn && step + 1 >= -left.blockNum)
                {
                    if (left.savestep < step)
                    {
                        left.savestep = step;
                    }
                    if (left.blockNum != -20 && !left.enemyOn && step >= -left.blockNum)
                    {
                        left.movable = true;
                        MoveSearch(x - 1, y, step + left.blockNum, num);
                    }
                } else if (left.blockNum != -20 && !left.enemyOn && step + 1 < -left.blockNum && !left.movable)
                {
                    left.savestep = 0;
                    left.color = 3;
                }
            } else if (!gamecontroller.playerTurn)
            {
                // 上
                if (up.blockNum != -20 && !up.playerOn  && step + 1 >= -up.blockNum)
                {
                    if (up.savestep < step)
                    {
                        up.savestep = step;
                    }
                    if (up.blockNum != -20 && !up.playerOn && step >= -up.blockNum)
                    {
                        up.movable = true;
                        MoveSearch(x, y - 1, step + up.blockNum, num);
                    }
                } else if (up.blockNum != -20 && !up.playerOn && step + 1 < -up.blockNum && !up.movable)
                {
                    up.savestep = 0;
                    up.color = 3;
                }
                //　下
                if (down.blockNum != -20 && !down.playerOn && step + 1 >= -down.blockNum)
                {
                    if (down.savestep < step)
                    {
                        down.savestep = step;
                    }
                    if (down.blockNum != -20 && !down.playerOn && step >= -down.blockNum)
                    {
                        down.movable = true;
                        MoveSearch(x, y + 1, step + down.blockNum, num);
                    } 
                } else if (down.blockNum != -20 && !down.playerOn && step + 1 < -down.blockNum && !down.movable)
                {
                    down.savestep = 0;
                    down.color = 3;
                }
                // 右
                if (right.blockNum != -20 && !right.playerOn && step + 1 >= -right.blockNum)
                {
                    if (right.savestep < step)
                    {
                        right.savestep = step;
                    }
                    if (right.blockNum != -20 && !right.playerOn && step >= -right.blockNum)
                    {
                        right.movable = true;
                        MoveSearch(x + 1, y, step + right.blockNum, num);
                    }
                } else if (right.blockNum != -20 && !right.enemyOn && step + 1 < -right.blockNum && !right.movable)
                {
                    right.savestep = 0;
                    right.color = 3;
                }
                // 左
                if (left.blockNum != -20 && !left.playerOn && step + 1 >= -left.blockNum)
                {
                    if (left.savestep < step)
                    {
                        left.savestep = step;
                    }
                    if (left.blockNum != -20 && !left.playerOn && step >= -left.blockNum)
                    {
                        left.movable = true;
                        MoveSearch(x - 1, y, step + left.blockNum, num);
                    }
                } else if (left.blockNum != -20 && !left.enemyOn && step + 1 < -left.blockNum && !left.movable)
                {
                    left.savestep = 0;
                    left.color = 3;
                }
            }
            
        }
        /*
        else if (map.block[x, y].enemyMovable)
        { 
            {
                map.block[x, y].enemyMovable = true;
                map.block[x, y].color = 2;
            }
            if (step > 0)
            {
                // 上
                if (map.block[x, y - 1].blockNum != -20 && map.block[x, y - 1].enemyMovable && step >= -map.block[x, y - 1].blockNum)
                    MoveSearch(x, y - 1, step + map.block[x, y - 1].blockNum, num);
                //　下
                if (map.block[x, y + 1].blockNum != -20 && map.block[x, y + 1].enemyMovable && step >= -map.block[x, y + 1].blockNum)
                    MoveSearch(x, y + 1, step + map.block[x, y + 1].blockNum, num);
                // 右
                if (map.block[x + 1, y].blockNum != -20 && map.block[x + 1, y].enemyMovable && step >= -map.block[x + 1, y].blockNum)
                    MoveSearch(x + 1, y, step + map.block[x + 1, y].blockNum, num);
                // 左
                if (map.block[x - 1, y].blockNum != -20 && map.block[x - 1, y].enemyMovable && step >= -map.block[x - 1, y].blockNum)
                    MoveSearch(x - 1, y, step + map.block[x - 1, y].blockNum, num);
            }
        }
        */
        return;
	}

    public void AttackSearch(int range)
    {
        for(int x = 0;x < 20; x++)
        {
            for (int y = 0;y < 20; y++)
            {
                if (map.block[x, y].savestep != -1 && map.block[x, y].color == 0)
                {
                    map.block[x, y].savestep = 0;
                }
                if (map.block[x, y].savestep == 0)
                {
                    map.block[x, y].color = 3;
                    map.block[x, y].attackArea = true;
                    AttackAreaSearch(x, y, range - 2);
                }
            }
        }
        map.DrawMap();
    }

    public void AttackAreaSearch(int x, int y, int range)
    {
        var up = map.block[x, y - 1];
        var down = map.block[x, y + 1];
        var right = map.block[x + 1, y];
        var left = map.block[x - 1, y];
        if (range >= 0)
        {
            // 上
            if (up.blockNum != -20 && up.savestep == -1 && !up.attackArea)
            {
                
                up.attackArea = true;
                up.color = 3;
                if (range > 0)
                {
                    AttackAreaSearch(x, y - 1, range - 1);
                }
            }
            // 下
            if (down.blockNum != -20 && down.savestep == -1 && !down.attackArea)
            {
                
                down.attackArea = true;
                down.color = 3;
                if (range > 0)
                {
                    AttackAreaSearch(x, y + 1, range - 1);
                }
            }
            // 右
            if (right.blockNum != -20 && right.savestep == -1 && !right.attackArea)
            {
                
                right.attackArea = true;
                right.color = 3;
                if (range > 0)
                {
                    AttackAreaSearch(x + 1, y, range - 1);
                }
            }
            // 左
            if (left.blockNum != -20 && left.savestep == -1 && !left.attackArea)
            {
                
                left.attackArea = true;
                left.color = 3;
                if (range > 0)
                {
                    AttackAreaSearch(x - 1, y, range - 1);
                }
            }
        }
        
    }
    
    public void AttackRange(int x, int y, int range)
    {
        if (range > 0 && map.block[x, y - 1].blockNum != -20)
        {
            // 上
            map.block[x, y - 1].color = 3;
            if(map.block[x, y - 1].enemyOn)
            {
                map.block[x, y - 1].attackable = true;
            }
            AttackRange(x, y - 1, range - 1);
        }
        if (range > 0 && map.block[x, y + 1].blockNum != -20)
        {
            //　下
            map.block[x, y + 1].color = 3;
            if(map.block[x, y + 1].enemyOn)
            {
                map.block[x, y + 1].attackable = true;
            }
            AttackRange(x, y + 1, range - 1);
        }
        if(range > 0 && map.block[x + 1, y].blockNum != -20)
        {
            // 右
            map.block[x + 1, y].color = 3;
            if (map.block[x + 1, y].movable)
            {
                map.block[x + 1, y].attackable = true;
            }
            AttackRange(x + 1, y, range - 1);
        }
        if(range > 0 && map.block[x - 1, y].blockNum != -20)
        {
            // 左
            map.block[x - 1, y].color = 3;
            if(map.block[x - 1, y].movable)
            {
                map.block[x - 1, y].attackable = true;
            }
            AttackRange(x - 1, y, range - 1);
        }
        return;
    }

    public void Initialize()
    {
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                map.block[x, y].movable = false;
                map.block[x, y].attackArea = false;
                map.block[x, y].attackable = false;
                map.block[x, y].enemyMovable = false;
                map.block[x, y].savestep = -1;
                map.block[x, y].count = 0;
                map.panel[x, y].GetComponent<MapTileController>().GetStep(map.block[x, y].savestep, map.block[x, y].count);
                map.block[x, y].color = 0;
            }
        }
        map.DrawMap();
    }
}
