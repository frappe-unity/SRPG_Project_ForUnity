using UnityEngine;
using System.Collections;

public class MovableScript : MonoBehaviour {
	MapManager map;
   
	void Start () {
        map = this.gameObject.GetComponent<MapManager>();
    }

    void Update()
    {
        
    }

	public void moveSearch( int x, int y, int step){
        
		map.block [x, y].step = step; 
		step--;
		map.block [x, y].movable = true;

		if (step >= 0) {
			
			// 上
			if (map.block [x, y - 1].blockNum == 0 && map.block [x, y - 1].step <= step)
				moveSearch (x, y - 1, step);
				
			//　下
			if (map.block [x, y + 1].blockNum == 0 && map.block [x, y + 1].step <= step)
				moveSearch (x, y + 1, step);
			// 右
			if (map.block [x + 1, y].blockNum == 0 && map.block [x + 1, y].step <= step)
				moveSearch (x + 1, y, step);
			// 左
			if (map.block [x - 1, y].blockNum == 0 && map.block [x - 1, y].step <= step)
				moveSearch (x - 1, y, step);
		}
	}	
}
