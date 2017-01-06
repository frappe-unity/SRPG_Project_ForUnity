using UnityEngine;
using System.Collections;

public class MovableScript : MonoBehaviour {
	[SerializeField] private MapController map;
    [SerializeField] private UnitController unit;
    [SerializeField] private GameController gamecontroller;

    public int stepCount;
   
	void Start () {
    }

    void Update()
    {
        
    }

	public void moveSearch( int x, int y, int step, int num){
        if (gamecontroller.playerTurn)
        {
            if(step == 0 && map.block[x, y].savestep == -1)
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
            }
            if (step > -1)
            {
                // 上
                if (map.block[x, y - 1].blockNum != -20 && map.block[x, y - 1].movable && step + 1 >= -map.block[x, y - 1].blockNum)
                {
                    if (map.block[x, y - 1].savestep < step)
                    {
                        map.block[x, y - 1].savestep = step;
                    }
                    if (map.block[x, y - 1].blockNum != -20 && map.block[x, y - 1].movable && step >= -map.block[x, y - 1].blockNum)
                    {
                        moveSearch(x, y - 1, step + map.block[x, y - 1].blockNum, num);
                    }
                }
                //　下
                if (map.block[x, y + 1].blockNum != -20 && map.block[x, y + 1].movable && step + 1 >= -map.block[x, y + 1].blockNum)
                {
                    if (map.block[x, y + 1].savestep < step)
                    {
                        map.block[x, y + 1].savestep = step;
                    }
                    if (map.block[x, y + 1].blockNum != -20 && map.block[x, y - 1].movable && step >= -map.block[x, y + 1].blockNum)
                    {
                        moveSearch(x, y + 1, step + map.block[x, y + 1].blockNum, num);
                    }
                }
                // 右
                if (map.block[x + 1, y].blockNum != -20 && map.block[x + 1, y].movable && step + 1 >= -map.block[x + 1, y].blockNum)
                {
                    if (map.block[x + 1, y].savestep < step)
                    {
                        map.block[x + 1, y].savestep = step;
                    }
                    if (map.block[x + 1, y].blockNum != -20 && map.block[x, y - 1].movable && step >= -map.block[x + 1, y].blockNum)
                    {
                         moveSearch(x + 1, y, step + map.block[x + 1, y].blockNum, num);
                    }
                }
                // 左
                if (map.block[x - 1, y].blockNum != -20 && map.block[x - 1, y].movable && step + 1 >= -map.block[x - 1, y].blockNum)
                {
                    if (map.block[x - 1, y].savestep < step)
                    {
                        map.block[x - 1, y].savestep = step;
                    }
                    if (map.block[x - 1, y].blockNum != -20 && map.block[x, y - 1].movable && step >= -map.block[x - 1, y].blockNum)
                    {
                        moveSearch(x - 1, y, step + map.block[x - 1, y].blockNum, num);
                    }
                }
            }
        }  else if (map.block[x, y].enemyMovable)
        { 
            {
                map.block[x, y].enemyMovable = true;
                map.block[x, y].color = 2;
            }
            if (step > 0)
            {
                // 上
                if (map.block[x, y - 1].blockNum != -20 && map.block[x, y - 1].enemyMovable && step >= -map.block[x, y - 1].blockNum)
                    moveSearch(x, y - 1, step + map.block[x, y - 1].blockNum, num);
                //　下
                if (map.block[x, y + 1].blockNum != -20 && map.block[x, y + 1].enemyMovable && step >= -map.block[x, y + 1].blockNum)
                    moveSearch(x, y + 1, step + map.block[x, y + 1].blockNum, num);
                // 右
                if (map.block[x + 1, y].blockNum != -20 && map.block[x + 1, y].enemyMovable && step >= -map.block[x + 1, y].blockNum)
                    moveSearch(x + 1, y, step + map.block[x + 1, y].blockNum, num);
                // 左
                if (map.block[x - 1, y].blockNum != -20 && map.block[x - 1, y].enemyMovable && step >= -map.block[x - 1, y].blockNum)
                    moveSearch(x - 1, y, step + map.block[x - 1, y].blockNum, num);
            }
        }
        return;
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
                map.block[x, y].savestep = -1;
                map.block[x, y].color = 0;
            }
        }
        map.DrawMap();
    }
}
