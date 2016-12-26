using UnityEngine;
using System.Collections;

public class CommandWindowController : MonoBehaviour {

    [SerializeField] private CharacterMoveController chara;
    [SerializeField] private UnitController unitcontroller;
    int stateNumber = 0;

    public int weaponNumber;

    public enum Command
    {
        ATTACK,
        CANCEL,
        STAY,
        WEAPON,
    }

    public Command command;

    void Start()
    {
        switch (command)
        {
            case Command.ATTACK:
                stateNumber = 1;
                break;
            case Command.CANCEL:
                stateNumber = 2;
                break;
            case Command.STAY:
                stateNumber = 3;
                break;
            case Command.WEAPON:
                stateNumber = 4;
                break;
        }
        chara = GameObject.Find("CharacterMoveManager").GetComponent<CharacterMoveController>();
        unitcontroller = GameObject.FindGameObjectWithTag("UniCon").GetComponent<UnitController>();
    }

	public void OnClick()
    {
        // chara.MenuEnd();
        if (stateNumber == 1)
        {
            // chara.AttackRange();
            chara.stateCount = 3;
            chara.MenuEnd();
            chara.MoveState();
            Invoke("BackMenu", 1F);
        }
        else if(stateNumber == 2)
        {
            chara.backMenu = true;
            chara.MenuEnd();
            chara.Cancel();
            Invoke("BackMenu", 0.5F);
            //chara.ReturnPos();
            //chara.stateCount = 1;
            //chara.MoveState();
        } else if(stateNumber == 3)
        {
            chara.EndAct();
            chara.MenuEnd();
            chara.Initialize();
            chara.MoveState();
        } else if(stateNumber == 4)
        {
            NodeDelete();
            chara.MenuEnd();
            unitcontroller.playerController[unitcontroller.selectUnit].selectWeapon = weaponNumber;
            chara.backMenu = true;
            Invoke("BackMenu", 1F);
            chara.stateCount = 4;
            chara.MoveState();
        }
    }

    public void BackMenu()
    {
        chara.backMenu = false;
    }

    public void NodeDelete()
    {
        var nodes = GameObject.FindGameObjectsWithTag("Node");
        foreach(var node in nodes)
        {
            Destroy(node);
        }
    }
}
