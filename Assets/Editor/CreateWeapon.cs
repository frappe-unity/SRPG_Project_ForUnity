using System.Collections;
using UnityEngine;
using UnityEditor;

public class CreateWeapon : MonoBehaviour {

    [MenuItem("Create/Weapon")]
    public static void CreateWeaponData()
    {
        WeaponData weapon = ScriptableObject.CreateInstance<WeaponData>();

        AssetDatabase.CreateAsset(weapon, "Assets/Resources/Database/Weapon.asset");
        AssetDatabase.Refresh();
    }
}
