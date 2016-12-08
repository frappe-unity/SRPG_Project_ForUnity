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
		// step --;
        if (unit.mapUnit[x, y].movable)
        {
		    map.block[x, y].movable = true;
        }

        if (step > 0) {
            //step += map.block[x, y].blockNum;
            // map.panel[x, y].GetComponent<MapTileController>().tipStep = step;
            // 上
            if (map.block[x, y - 1].blockNum != -20 && unit.mapUnit[x, y - 1].movable && step >= -map.block[x, y - 1].blockNum)
				moveSearch (x, y - 1, step + map.block[x, y - 1].blockNum);	
			//　下
			if (map.block [x, y + 1].blockNum != -20 && unit.mapUnit[x, y + 1].movable && step >= -map.block[x, y + 1].blockNum)
				moveSearch (x, y + 1, step + map.block[x, y + 1].blockNum);
            // 右
            if (map.block [x + 1, y].blockNum != -20 && unit.mapUnit[x + 1, y].movable && step >= -map.block[x + 1, y].blockNum)
				moveSearch (x + 1, y, step + map.block[x + 1, y].blockNum);
            // 左
            if (map.block [x - 1, y].blockNum != -20 && unit.mapUnit[x - 1, y].movable && step >= -map.block[x - 1, y].blockNum)
				moveSearch (x - 1, y, step + map.block[x - 1, y].blockNum);
        }
        return;
	}
}
