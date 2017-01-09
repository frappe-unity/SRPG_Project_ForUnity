using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponScrollController : MonoBehaviour {

	[SerializeField] private RectTransform node = null;
    [SerializeField] private UnitController unitcontroller;
    [SerializeField] private CharacterMoveController chara;
    [SerializeField] private WeaponData weapondata;

    void Start()
    {
        unitcontroller = GameObject.FindGameObjectWithTag("UniCon").GetComponent<UnitController>();
    }

    public void NodeInstance()
    {
        unitcontroller = GameObject.FindGameObjectWithTag("UniCon").GetComponent<UnitController>();
        chara = GameObject.Find("CharacterMoveManager").GetComponent<CharacterMoveController>();
        var nodes = GameObject.FindGameObjectsWithTag("Node");
        if(nodes.Length > 0)
        {
            foreach (var node in nodes)
            {
                Destroy(node);
            }
        }
        for (int i = 0; i < unitcontroller.playerController[chara.playerID].weapon.Length; i++)
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
            text.text = weapondata.blade[unitcontroller.playerController[chara.playerID].weapon[i]].bladeName.ToString();
            commandWindowController.weaponNumber = weapondata.blade[unitcontroller.playerController[chara.playerID].weapon[i]].weaponID;
        }
    }

}
