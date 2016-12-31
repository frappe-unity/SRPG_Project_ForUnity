﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapTileController : MonoBehaviour {

    public int map_x, map_y = 0;
	public bool rend = true;
    public int savestep = -1;
    public int tipCost = 0;
    public Text text;
	// Use this for initialization
	void Start () {
        text = gameObject.GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void GetStep()
    {
        if(savestep != -1)
        text.text = savestep.ToString();
    }
}
