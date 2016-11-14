using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour {

	const int m_Width = 10;
	const int m_Height = 10;
	public float size = 10F;

	public class Block{
		public int height = 1; // 高さ
		public int blockNum = 0; // ブロックの種類
		public int step = 0; // ステップ格納 
		public bool movable = false; // 移動可能フラグ 
	}

	public Block[,] block = new Block[m_Width,m_Height] ;

	public void MapInit() { 
		int[,] mapSet = new int[m_Height, m_Width] { // xとyを逆にしないように注意
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1} ,
			{ 1, 0, 0, 0, 0, 0, 0, 1, 0, 1} ,
			{ 1, 0, 1, 0, 0, 0, 0, 1, 0, 1} ,
			{ 1, 0, 0, 0, 0, 0, 1, 0, 0, 1} ,
			{ 1, 0, 0, 0, 0, 0, 0, 0, 0, 1} ,
			{ 1, 0, 0, 0, 1, 1, 0, 0, 0, 1} ,
			{ 1, 0, 0, 0, 0, 1, 0, 0, 0, 1} ,
			{ 1, 0, 0, 0, 0, 1, 0, 0, 0, 1} ,
			{ 1, 0, 0, 0, 0, 1, 0, 0, 0, 1} ,
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1} ,
		};

		for (int x = 0;x < m_Width; x++){
			for(int y = 0; y < m_Height; y++){
				block[x,y] = new Block() ;
				block[x,y].blockNum = mapSet[y,x];
			}
		}
	}

	public GameObject[,] panel = new GameObject[m_Width, m_Height];

	public void DrawMap(){
		for (int x = 0; x < m_Width; x++) {
			for (int y = 0; y < m_Height; y++) {

				panel[x,y] = Instantiate (Resources.Load ("MapTile"), new Vector3 (x * size, 0, y * size), Quaternion.identity) as GameObject;

				if (block [x, y].blockNum == 1) {
					Instantiate (Resources.Load ("Cube"), new Vector3 (x * size, 0, y * size), Quaternion.identity);
				} else if (block [x, y].movable == true) {
					panel [x, y].GetComponent<Renderer> ().material.color = new Color(0,0.5F ,0.5F);
				}
			}
		}
	}
}
