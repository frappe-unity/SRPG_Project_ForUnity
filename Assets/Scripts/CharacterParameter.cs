using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

[System.Serializable]
public class CharacterParameter : ScriptableObject {
    public List<PlayerSet> entryPlayer;
    

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
    public class JsonMapper
    {
        public static CharacterParameter ToObject<CharacterParameter>( string json)
        {
            return LitJson.JsonMapper.ToObject<CharacterParameter>(json);
        }

        public static string ToJson( Object obj)
        {
            return LitJson.JsonMapper.ToJson ( obj );
        }
    }
    
}
