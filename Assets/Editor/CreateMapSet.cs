using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateMapSet : MonoBehaviour {
    [MenuItem("Create/MapData")]
    public static void CreateMapData()
    {
        MapData data = ScriptableObject.CreateInstance<MapData>();

        AssetDatabase.CreateAsset(data, "Assets/Resources/MapData/Map.asset");
        AssetDatabase.Refresh();
    }
	
}
