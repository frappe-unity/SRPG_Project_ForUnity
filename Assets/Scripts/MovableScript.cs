using UnityEngine;
using System.Collections;

public class MovableScript : MonoBehaviour {
	[SerializeField] private MapController map;
    [SerializeField] private UnitController unit;

    public int stepCount;
   
	void Start () {
    }

    void Update()
    {
        
    }

	public void moveSearch( int x, int y, int step){
		step--;
        if(unit.mapUnit[x, y].movable)
        {
		    map.block[x, y].movable = true;
        }

        if (step >= 0) {
			
			// 上
			if (map.block [x, y - 1].blockNum == 0 && map.block [x, y - 1].step <= step && unit.mapUnit[x, y - 1].movable)
				moveSearch (x, y - 1, step);	
			//　下
			if (map.block [x, y + 1].blockNum == 0 && map.block [x, y + 1].step <= step && unit.mapUnit[x, y - 1].movable)
				moveSearch (x, y + 1, step);
			// 右
			if (map.block [x + 1, y].blockNum == 0 && map.block [x + 1, y].step <= step && unit.mapUnit[x, y - 1].movable)
				moveSearch (x + 1, y, step);
			// 左
			if (map.block [x - 1, y].blockNum == 0 && map.block [x - 1, y].step <= step && unit.mapUnit[x, y - 1].movable)
				moveSearch (x - 1, y, step);
		}
	}
}
