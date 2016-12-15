using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using LitJson;

[System.Serializable]
public class CharacterParameter : ScriptableObject {
    public List<PlayerSet> entryPlayer;
    
    public enum Unit
    {
        プリンセス = 0,
        騎士 = 1,
    }

    [System.Serializable]
    public class PlayerSet
    {
        // パラメータ
        public int playerID;       // キャラクターのID
        public string charaName;   // キャラクター名
        public Sprite icon;        // アイコン画像
        public Unit unit;          // 役職
        public int level;          // レベル
        public int hp;             // 体力
        public int attack;         // 攻撃力
        public int deffence;       // 防御力
        public int hit;            // 技
        public int speed;          // 速さ
        public int lucky;          // 幸運
        public int moveCost;       // 移動力

        // 装備
        public int[] weapon;

        // 成長度
        public int l_hp;
        public int l_attack;
        public int l_deffence;
        public int l_hit;
        
    }
}
