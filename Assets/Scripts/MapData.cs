using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MapData : ScriptableObject {

    public int chapter;
    public Vector2 map;

    public List<PlayerSet> entryPlayer;
    public List<EnemySet> entryEnemy;

}

[System.Serializable]
public class PlayerSet
{
    public int playerID;
    public string charaName;
    public Vector2 playerPos;
    public int hp;
    public int attack;
    public int deffence;
    public int hit;
    public int moveCost;

}


[System.Serializable]
public class EnemySet
{
    public int enemyID;
    public string enemyName;
    public Vector2 enemyPos;
    public int hp;
    public int attack;
    public int deffence;
    public int hit;
    public int moveCost;
}
