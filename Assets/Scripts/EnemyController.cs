using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public int enemyID = 0;  // キャラクターID
    public string name = "test";
    public Vector2 enemyPos;
    public int hp = 18;                 // 体力
    public int attack = 7;              // 攻撃力
    public int deffence = 4;            // 防御力
    public int hit;
    public int moveCost = 5;			// 移動力
    public bool isAct = false;
    private int cross = 10;

    void Update()
    {
        transform.position = new Vector3(enemyPos.x * cross, transform.position.y, enemyPos.y * cross);
    }
}
