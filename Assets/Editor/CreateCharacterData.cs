using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateCharacterData : MonoBehaviour {

    [MenuItem("Create/CharaData")]
    public static void CreateCharaData()
    {
        CharacterParameter param = ScriptableObject.CreateInstance<CharacterParameter>();

        AssetDatabase.CreateAsset(param, "Assets/Resources/PlayerData/charaParam.asset");
        AssetDatabase.Refresh();
    }
}
