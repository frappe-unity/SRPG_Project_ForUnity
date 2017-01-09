using UnityEngine;
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
		public int color = 3; // 色
		public int blockNum = 0; // 高さ(重み)
		public int step = 0; // ステップ格納 
        public int savestep = -1;
        public int playerID;
        public int enemyID;
		public bool movable = false; // 移動可能フラグ 
        public bool attackArea = false;
        public bool attackable = false; // 攻撃可能フラグ 
        public bool enemyMovable = false; // 敵移動可能フラグ
        public bool enemyAttackable = false; // 敵攻撃可能フラグ
        public bool playerOn = false;
        public bool enemyOn = false;
        public int count;
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
			{ -20, -1, -1, -1, -2, -1, -1, -19, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -20} ,
			{ -20, -1, -19, -1, -3, -1, -1, -19, -1, -1, -19, -19, -19, -1, -1, -1, -1, -1, -1, -20} ,
			{ -20, -1, -1, -2, -1, -1, -19, -1, -1, -1, -19, -1, -19, -1, -1, -1, -1, -1, -1, -20} ,
			{ -20, -1, -3, -1, -1, -1, -1, -1, -1, -1, -19, -1, -19, -1, -1, -1, -1, -1, -1, -20} ,
			{ -20, -1, -1, -1, -19, -19, -1, -1, -1, -1, -19, -19, -19, -1, -1, -1, -1, -1, -1, -20} ,
            { -20, -1, -1, -1, -1, -1, -1, -19, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -20} ,
            { -20, -1, -19, -1, -1, -1, -1, -19, -1, -1, -19, -19, -19, -1, -1, -1, -1, -1, -1, -20} ,
            { -20, -1, -1, -1, -1, -1, -19, -1, -1, -1, -19, -1, -19, -1, -1, -1, -1, -1, -1, -20} ,
            { -20, -1, -1, -1, -1, -1, -1, -1, -1, -1, -19, -1, -19, -1, -1, -1, -1, -1, -1, -20} ,
            { -20, -1, -1, -1, -19, -19, -1, -1, -1, -1, -19, -19, -19, -1, -1, -1, -1, -1, -1, -20} ,
            { -20, -1, -1, -1, -1, -1, -1, -19, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -20} ,
            { -20, -1, -19, -1, -1, -1, -1, -19, -1, -1, -19, -19, -19, -1, -1, -1, -1, -1, -1, -20} ,
            { -20, -1, -1, -1, -2, -1, -19, -1, -1, -1, -19, -1, -19, -1, -1, -1, -1, -1, -1, -20} ,
            { -20, -1, -1, -1, -1, -2, -1, -1, -1, -1, -19, -1, -19, -1, -1, -1, -1, -1, -1, -20} ,
            { -20, -1, -1, -1, -19, -19, -3, -1, -1, -1, -19, -19, -19, -1, -1, -1, -1, -1, -1, -20} ,
            { -20, -1, -1, -2, -1, -2, -1, -19, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -20} ,
            { -20, -1, -19, -1, -1, -1, -1, -19, -1, -1, -19, -19, -19, -1, -1, -1, -1, -1, -1, -20} ,
            { -20, -1, -1, -1, -1, -1, -19, -1, -1, -1, -19, -1, -19, -1, -1, -1, -1, -1, -1, -20} ,
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
                    if (block[x, y].blockNum == -20 || block[x, y].blockNum == -19)
                    {
                        Instantiate(Resources.Load("Cube"), new Vector3(x * size,5, y * size), Quaternion.identity);
                    }
                }
				// panel [x, y].GetComponent<Renderer> ().enabled = false;

				if (!insMap) {
					//panel [x, y] = Instantiate (Resources.Load ("MapTile"), new Vector3 (x * size, 0, y * size), Quaternion.identity) as GameObject;
                    panel[x, y].GetComponent<MapTileController>().map_x = x;
                    panel[x, y].GetComponent<MapTileController>().map_y = y;
                    panel[x, y].GetComponent<Renderer> ().enabled = false;
					
				}
                else
                {
                    /*
                    if(block[x, y].savestep != -1 && block[x, y].color == 0)
                    {
                        block[x, y].savestep = 0;
                    }
                    if(block[x, y].savestep == 0)
                    {
                        block[x, y].color = 3;
                    }
                    */
                    panel[x, y].GetComponent<MapTileController>().GetStep(block[x, y].savestep, block[x, y].count);
                    switch (block[x, y].color)
                    {
                        case 0:
                            isAlpha = true;
                            colorR = 1F;
                            colorG = 1F;
                            colorB = 1F;
                            alphaA = 0F;
                            panel[x, y].GetComponent<Renderer>().enabled = false;
                            break;
                        case 1:
                            isAlpha = false;
                            colorR = 0F;
                            colorG = 0.5F;
                            colorB = 1.0F;
                            alphaA = 0.7F;
                            panel[x, y].GetComponent<Renderer>().enabled = true;
                            panel[x, y].GetComponent<Renderer>().material.color = new Color(colorR, colorG, colorB, alphaA);
                            break;
                        case 2:
                            isAlpha = false;
                            colorR = 0.5F;
                            colorG = 1F;
                            colorB = 1F;
                            alphaA = 0.7F;
                            panel[x, y].GetComponent<Renderer>().enabled = true;
                            panel[x, y].GetComponent<Renderer>().material.color = new Color(colorR, colorG, colorB, alphaA);
                            break;
                        case 3:
                            isAlpha = false;
                            colorR = 1F;
                            colorG = 0F;
                            colorB = 0F;
                            alphaA = 0.7F;
                            panel[x, y].GetComponent<Renderer>().enabled = true;
                            panel[x, y].GetComponent<Renderer>().material.color = new Color(colorR, colorG, colorB, alphaA);
                            break;
                    }
				}
			}
		}
	}  
}
