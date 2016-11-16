using UnityEngine;
using System.Collections;

public class MovableScript : MonoBehaviour {
	MapManager map;
    CirsorController cir;
    PlayerController player;
    public int stepCount;
   
	void Start () {
        map = this.gameObject.GetComponent<MapManager>();
    }

    void Update()
    {
        
    }

	public void moveSearch( int x, int y, int step, string name){
        player = GameObject.Find(name).GetComponent<PlayerController>();
		map.block [x, y].step = step;
		step--;
        if (player.isMoving == false)
        {
            map.block[x, y].movable = true;
        } else
        {
            map.block[x, y].movable = false;
        }

        if (step >= 0) {
			
			// 上
			if (map.block [x, y - 1].blockNum == 0 && map.block [x, y - 1].step <= step)
				moveSearch (x, y - 1, step, name);	
			//　下
			if (map.block [x, y + 1].blockNum == 0 && map.block [x, y + 1].step <= step)
				moveSearch (x, y + 1, step, name);
			// 右
			if (map.block [x + 1, y].blockNum == 0 && map.block [x + 1, y].step <= step)
				moveSearch (x + 1, y, step, name);
			// 左
			if (map.block [x - 1, y].blockNum == 0 && map.block [x - 1, y].step <= step)
				moveSearch (x - 1, y, step, name);
		}
	}
}
