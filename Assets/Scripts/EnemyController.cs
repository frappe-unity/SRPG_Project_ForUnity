using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    UnitController unitcontroller;

    public int enemyID = 0;  // キャラクターID
    public string name = "test";
    public Vector2 enemyPos;
    public int level = 1;           // レベル
    public int hp = 18;             // 体力
    public int attack = 7;          // 攻撃力
    public int deffence = 4;        // 防御力
    public int hit;                 // 技
    public int speed;               // 速さ
    public int lucky;               // 幸運
    public int moveCost = 5;		// 移動力
    public bool isAct = false;
    private int cross = 10;

    void Start()
    {
        unitcontroller = GameObject.FindGameObjectWithTag("UniCon").GetComponent<UnitController>();
    }

    void Update()
    {
        transform.position = new Vector3(enemyPos.x * cross, transform.position.y, enemyPos.y * cross);

        // hpが0になったらデリート
        if(hp <= 0)
        {
            unitcontroller.EnemyListRemove(enemyID);
            Destroy(gameObject);
        }
    }
}
