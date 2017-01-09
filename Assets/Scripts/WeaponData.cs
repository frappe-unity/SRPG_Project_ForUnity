using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData : ScriptableObject {

    public List<Blade> blade;

    [System.Serializable]
    public class Blade
    {
        public int weaponID;         // 剣のID
        public string bladeName;    // 剣の名前
        public Sprite icon;         // アイコン画像
        public int level;           // 剣の使用レベル
        public int range;           // 剣の攻撃範囲
        public int attack;          // 剣の攻撃力
        public int hitper;          // 剣の命中率
        public int criticalper;     // 剣の必殺率
    }
}
