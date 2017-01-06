using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationScript : MonoBehaviour {

    [SerializeField] private GameController gm;
    [SerializeField] private UnitController unit;
    [SerializeField] private MapController map;
    Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "";
		for (int x = 0;x < 20;x++)
        {
            for(int y = 0;y < 20; y++)
            {
                if(map.block[x, y].enemyOn)
                {
                    text.text += ("ID : " + map.block[x, y] .enemyID + " Point :"  + x + "," + y + "=attackable\n").ToString();
                }
            }
        }
	}
}
