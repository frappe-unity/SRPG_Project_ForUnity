﻿using UnityEngine;
using System;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public enum UnitType
    {
        プリンセス = 0,
        騎士 = 1,
    }

    /// <summary>
    /// ユニットのステータス
    /// </summary>
    public int charaID = 0;  // キャラクターID
    public string name = "test";
    public Vector2 unitPos;
    public int unitType = 0;
    public UnitType unit;
    public int hp = 18;                 // 体力
    public int attack = 7;              // 攻撃力
    public int deffence = 4;            // 防御力
    public int hit;
    public int moveCost = 5;			// 移動力
    public bool isAct = false;
    private int cross = 10;

    void Start()
    {
        unit = (UnitType)Enum.ToObject(typeof(UnitType), unitType);
    }

    void Update()
    {
        transform.position = new Vector3(unitPos.x * cross, transform.position.y, unitPos.y * cross);
    }


    
}
