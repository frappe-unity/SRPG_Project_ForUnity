using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    UnitController unitcontroller;

    public enum UnitType
    {
        プリンセス = 0,
        騎士 = 1,
    }

    public Text text;

    /// <summary>
    /// ユニットのステータス
    /// </summary>
    public int playerID = 0;         // キャラクターID
    public string name = "test";    // キャラクター名
    public Vector2 playerPos;         // 位置情報
    public Sprite icon;             // アイコン画像
    public int unitType = 0;        // 役職カウント
    public UnitType unit;           // 役職
    public int level = 1;           // レベル
    public int hp = 18;             // 体力
    public int attack = 7;          // 攻撃力
    public int deffence = 4;        // 防御力
    public int hit;                 // 技
    public int speed;               // 速さ
    public int lucky;               // 幸運
    public int moveCost = 5;		// 移動力
    public bool isAct = false;      // 待機か
    private int cross = 10;         // 10倍用変数

    // 装備
    public int[] weapon;

    public int selectWeapon;

    void Start()
    {
        text = gameObject.GetComponentInChildren<Text>();
        unitcontroller = GameObject.FindGameObjectWithTag("UniCon").GetComponent<UnitController>();
        unit = (UnitType)Enum.ToObject(typeof(UnitType), unitType);
        selectWeapon = weapon[0];
        ColorChange(0);
    }

    void Update()
    {
        transform.position = new Vector3(playerPos.x * cross, transform.position.y, playerPos.y * cross);

        // hpが0になったらデリート
        if(hp <= 0)
        {
            unitcontroller.PlayerListRemove(playerID);
        }
    }

    public void Damage(int damage)
    {
        ColorChange(1);

        if (damage > 0)
        {
            text.text = damage.ToString();
            hp -= damage;
        }
        else
        {
            text.text = "Miss!";
        }
        StartCoroutine("damageText");
        if (hp <= 0)
        {
            unitcontroller.PlayerListRemove(playerID);
            Destroy(gameObject);
        }
    }

    IEnumerator damageText()
    {
        Debug.Log("tex");
        yield return new WaitForSeconds(2);
        ColorChange(0);
        yield break;
    }

    public void ColorChange(int alpha)
    {
        var color = text.color;
        color.a = alpha;
        text.color = color;
    }

}
