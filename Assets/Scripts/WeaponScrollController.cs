using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponScrollController : MonoBehaviour {

	[SerializeField] private RectTransform node = null;
    [SerializeField] private UnitController unitController;
    [SerializeField] private WeaponData weapondata;


    void Start()
    {
        for (int i = 0; i < unitController.playerController[unitController.selectUnit].weapon.Length; i++)
        {
            var item = GameObject.Instantiate(node) as RectTransform;
            item.SetParent(transform, false);
            if(i == 0)
            {
                Selectable select = item.GetComponent<Selectable>();
                select.Select();
            }
            var text = item.GetComponentInChildren<Text>();
            var commandWindowController = item.GetComponentInChildren<CommandWindowController>();
            text.text = weapondata.blade[unitController.playerController[unitController.selectUnit].weapon[i]].bladeName.ToString();
            commandWindowController.weaponNumber = weapondata.blade[unitController.playerController[unitController.selectUnit].weapon[i]].weaponID;
        }
    }

}
