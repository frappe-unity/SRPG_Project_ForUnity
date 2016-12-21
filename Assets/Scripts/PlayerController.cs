using UnityEngine;
using System;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public enum UnitType
    {
        プリンセス = 0,
        騎士 = 1,
    }

    /// <summary>
    /// ユニットのステータス
    /// </summary>
    public int charaID = 0;         // キャラクターID
    public string name = "test";    // キャラクター名
    public Vector2 unitPos;         // 位置情報
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
        unit = (UnitType)Enum.ToObject(typeof(UnitType), unitType);
        selectWeapon = weapon[0];
    }

    void Update()
    {
        transform.position = new Vector3(unitPos.x * cross, transform.position.y, unitPos.y * cross);
    }



}
