using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    /// <summary>
    /// ユニットのステータス
    /// </summary>
    public int charaID = 0;  // キャラクターID
    public int hp = 18;                 // 体力
    public int attack = 7;              // 攻撃力
    public int deffence = 4;            // 防御力
    public int moveCost = 5;			// 移動力
    public bool isAct = false;
    
}
