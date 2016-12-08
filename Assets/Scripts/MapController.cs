﻿using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour {

	//PlayerController playerController;

	public const int m_Width = 20;
	public const int m_Height = 20;
	public float size = 10F;
    public bool insMap = false;
	public bool isAlpha = false;

    /// <summary>
    /// タイルカラー用マテリアル
    /// </summary>
    public Material[] mat;
    public int matNum = 0;
	public float colorR = 0;
	public float colorG = 0;
	public float colorB = 0;
	public float alphaA = 0;

	public class Block{
		public int height = 1; // 高さ
		public int blockNum = 0; // ブロックの種類
		public int step = 0; // ステップ格納 
		public bool movable = false; // 移動可能フラグ 
	}

	public Block[,] block = new Block[m_Width,m_Height] ;

    public void Start()
    {
        // マップの描画関係
        MapInit();
        DrawMap();
        insMap = true;  // 初期描画完了
    }

	public void MapInit() { 
		int[,] mapSet = new int[m_Height, m_Width] { // xとyを逆にしないように注意
			{ -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20} ,
			{ -20, -1, -1, -1, -2, -1, -1, -20, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -20} ,
			{ -20, -1, -20, -1, -3, -1, -1, -20, -1, -1, -20, -20, -20, -1, -1, -1, -1, -1, -1, -20} ,
			{ -20, -1, -1, -2, -1, -1, -20, -1, -1, -1, -20, -1, -20, -1, -1, -1, -1, -1, -1, -20} ,
			{ -20, -1, -3, -1, -1, -1, -1, -1, -1, -1, -20, -1, -20, -1, -1, -1, -1, -1, -1, -20} ,
			{ -20, -1, -1, -1, -20, -20, -1, -1, -1, -1, -20, -20, -20, -1, -1, -1, -1, -1, -1, -20} ,
            { -20, -1, -1, -1, -1, -1, -1, -20, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -20} ,
            { -20, -1, -20, -1, -1, -1, -1, -20, -1, -1, -20, -20, -20, -1, -1, -1, -1, -1, -1, -20} ,
            { -20, -1, -1, -1, -1, -1, -20, -1, -1, -1, -20, -1, -20, -1, -1, -1, -1, -1, -1, -20} ,
            { -20, -1, -1, -1, -1, -1, -1, -1, -1, -1, -20, -1, -20, -1, -1, -1, -1, -1, -1, -20} ,
            { -20, -1, -1, -1, -20, -20, -1, -1, -1, -1, -20, -20, -20, -1, -1, -1, -1, -1, -1, -20} ,
            { -20, -1, -1, -1, -1, -1, -1, -20, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -20} ,
            { -20, -1, -20, -1, -1, -1, -1, -20, -1, -1, -20, -20, -20, -1, -1, -1, -1, -1, -1, -20} ,
            { -20, -1, -1, -1, -2, -1, -20, -1, -1, -1, -20, -1, -20, -1, -1, -1, -1, -1, -1, -20} ,
            { -20, -1, -1, -1, -1, -2, -1, -1, -1, -1, -20, -1, -20, -1, -1, -1, -1, -1, -1, -20} ,
            { -20, -1, -1, -1, -20, -20, -3, -1, -1, -1, -20, -20, -20, -1, -1, -1, -1, -1, -1, -20} ,
            { -20, -1, -1, -2, -1, -2, -1, -20, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -20} ,
            { -20, -1, -20, -1, -1, -1, -1, -20, -1, -1, -20, -20, -20, -1, -1, -1, -1, -1, -1, -20} ,
            { -20, -1, -1, -1, -1, -1, -20, -1, -1, -1, -20, -1, -20, -1, -1, -1, -1, -1, -1, -20} ,
            { -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20, -20} ,
        };

		for (int x = 0;x < m_Width; x++){
			for(int y = 0; y < m_Height; y++){
				block[x,y] = new Block() ;
				block[x,y].blockNum = mapSet[x,y];
			}
		}
	}

	public GameObject[,] panel = new GameObject[m_Width, m_Height];

    public void DrawMap(){
		for (int x = 0; x < m_Width; x++) {
			for (int y = 0; y < m_Height; y++) {
				if (insMap == false) {
					panel [x, y] = Instantiate (Resources.Load ("MapTile"), new Vector3 (x * size, 0, y * size), Quaternion.identity) as GameObject;
                    panel[x, y].GetComponent<MapTileController>().tipCost = block[x, y].blockNum;
                    if (block[x, y].blockNum == -20)
                    {
                        Instantiate(Resources.Load("Cube"), new Vector3(x * size,5, y * size), Quaternion.identity);
                    }
                }
				panel [x, y].GetComponent<Renderer> ().enabled = false;

				if (insMap == false) {
					//panel [x, y] = Instantiate (Resources.Load ("MapTile"), new Vector3 (x * size, 0, y * size), Quaternion.identity) as GameObject;
                    panel[x, y].GetComponent<MapTileController>().map_x = x;
                    panel[x, y].GetComponent<MapTileController>().map_y = y;
                    panel[x, y].GetComponent<Renderer> ().enabled = false;
					
				} else if (block [x, y].movable == true) {
					if (!isAlpha) {
						panel [x, y].GetComponent<Renderer> ().enabled = true;
						panel [x, y].GetComponent<Renderer> ().material.color = new Color (colorR, colorG, colorB, alphaA);
					} else {
						panel [x, y].GetComponent<Renderer> ().enabled = false;
					}
				}
			}
		}
	}  
}
