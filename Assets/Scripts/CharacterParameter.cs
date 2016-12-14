using UnityEngine;
using UnityEditor;
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
        public int playerID;
        public string charaName;
        public Sprite icon;
        public Unit unit;
        public int level;
        public int hp;
        public int attack;
        public int deffence;
        public int hit;
        public int moveCost;

        // 成長度
        public int l_hp;
        public int l_attack;
        public int l_deffence;
        public int l_hit;
        /*
        [SerializeField]
        public List<int> playerID;
        public List<string> charaName;
        public List<int> level;
        // public Vector2 playerPos;
        public List<int> hp;
        public List<int> attack;
        public List<int> deffence;
        public List<int> hit;
        public List<int> moveCost;

        public PlayerSet()
        {
            playerID = new List<int>();
            charaName = new List<string>();
            level = new List<int>();
            hp = new List<int>();
            attack = new List<int>();
            deffence = new List<int>();
            hit = new List<int>();
            moveCost = new List<int>();
            
        }
        */
    }
}
