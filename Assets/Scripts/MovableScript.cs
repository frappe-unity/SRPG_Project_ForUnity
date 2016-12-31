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
            if (unit.mapUnit[x, y].movable && step > -1)
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
                if (map.block[x, y - 1].blockNum != -20 && unit.mapUnit[x, y - 1].movable && step + 1 >= -map.block[x, y - 1].blockNum)
                {
                    if (map.block[x, y - 1].savestep < step)
                    {
                        map.block[x, y - 1].savestep = step;
                    }
                    if (map.block[x, y - 1].blockNum != -20 && unit.mapUnit[x, y - 1].movable && step >= -map.block[x, y - 1].blockNum)
                    {
                        moveSearch(x, y - 1, step + map.block[x, y - 1].blockNum, num);
                    }
                }
                //　下
                if (map.block[x, y + 1].blockNum != -20 && unit.mapUnit[x, y + 1].movable && step + 1 >= -map.block[x, y + 1].blockNum)
                {
                    if (map.block[x, y + 1].savestep < step)
                    {
                        map.block[x, y + 1].savestep = step;
                    }
                    if (map.block[x, y + 1].blockNum != -20 && unit.mapUnit[x, y - 1].movable && step >= -map.block[x, y + 1].blockNum)
                    {
                        moveSearch(x, y + 1, step + map.block[x, y + 1].blockNum, num);
                    }
                }
                // 右
                if (map.block[x + 1, y].blockNum != -20 && unit.mapUnit[x + 1, y].movable && step + 1 >= -map.block[x + 1, y].blockNum)
                {
                    if (map.block[x + 1, y].savestep < step)
                    {
                        map.block[x + 1, y].savestep = step;
                    }
                    if (map.block[x + 1, y].blockNum != -20 && unit.mapUnit[x, y - 1].movable && step >= -map.block[x + 1, y].blockNum)
                    {
                         moveSearch(x + 1, y, step + map.block[x + 1, y].blockNum, num);
                    }
                }
                // 左
                if (map.block[x - 1, y].blockNum != -20 && unit.mapUnit[x - 1, y].movable && step + 1 >= -map.block[x - 1, y].blockNum)
                {
                    if (map.block[x - 1, y].savestep < step)
                    {
                        map.block[x - 1, y].savestep = step;
                    }
                    if (map.block[x - 1, y].blockNum != -20 && unit.mapUnit[x, y - 1].movable && step >= -map.block[x - 1, y].blockNum)
                    {
                        moveSearch(x - 1, y, step + map.block[x - 1, y].blockNum, num);
                    }
                }
            }
        }  else if (unit.mapUnit[x, y].enemyMovable)
        { 
            {
                map.block[x, y].enemyMovable = true;
                map.block[x, y].color = 2;
            }
            if (step > 0)
            {
                // 上
                if (map.block[x, y - 1].blockNum != -20 && unit.mapUnit[x, y - 1].enemyMovable && step >= -map.block[x, y - 1].blockNum)
                    moveSearch(x, y - 1, step + map.block[x, y - 1].blockNum, num);
                //　下
                if (map.block[x, y + 1].blockNum != -20 && unit.mapUnit[x, y + 1].enemyMovable && step >= -map.block[x, y + 1].blockNum)
                    moveSearch(x, y + 1, step + map.block[x, y + 1].blockNum, num);
                // 右
                if (map.block[x + 1, y].blockNum != -20 && unit.mapUnit[x + 1, y].enemyMovable && step >= -map.block[x + 1, y].blockNum)
                    moveSearch(x + 1, y, step + map.block[x + 1, y].blockNum, num);
                // 左
                if (map.block[x - 1, y].blockNum != -20 && unit.mapUnit[x - 1, y].enemyMovable && step >= -map.block[x - 1, y].blockNum)
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
            map.block[x, y - 1].attackable = true;
            map.block[x, y - 1].color = 3;
            AttackRange(x, y - 1, range - 1);
        }
        if (range > 0 && map.block[x, y + 1].blockNum != -20)
        {
            //　下
            map.block[x, y + 1].attackable = true;
            map.block[x, y + 1].color = 3;
            AttackRange(x, y + 1, range - 1);
        }
        if(range > 0 && map.block[x + 1, y].blockNum != -20)
        {
            // 右
            map.block[x + 1, y].attackable = true;
            map.block[x + 1, y].color = 3;
            AttackRange(x + 1, y, range - 1);
        }
        if(range > 0 && map.block[x - 1, y].blockNum != -20)
        {
            // 左
            map.block[x - 1, y].attackable = true;
            map.block[x - 1, y].color = 3;
            AttackRange(x - 1, y, range - 1);
        }
        return;
    }
}
