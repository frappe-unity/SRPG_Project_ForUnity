using UnityEngine;
using System.Collections;

public class JsonManager : MonoBehaviour {
    
    public static T ToObject<T>(string json)
    {
        return LitJson.JsonMapper.ToObject<T>(json);
    }

    public static string ToJson(Object obj)
    {
        return LitJson.JsonMapper.ToJson(obj);
    }
}
