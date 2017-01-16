using UnityEngine;
using System.Collections;

public class CommandWindowController : MonoBehaviour {

    [SerializeField] private CharacterMoveController chara;
    [SerializeField] private UnitController unitcontroller;
    [SerializeField] private MovableScript movablescript;
    [SerializeField] private SoundController soundcontroller;
    int stateNumber = 0;

    public int weaponNumber;
    public bool selectOn = false;

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
        selectOn = false;
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
        movablescript = GameObject.Find("MapManager").GetComponent<MovableScript>();
        soundcontroller = GameObject.Find("SoundManager").GetComponent<SoundController>();
    }

	public void OnClick()
    {
        soundcontroller.SoundPlayer(1);
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
            movablescript.Initialize();
            chara.MoveState();
        } else if(stateNumber == 4)
        {
            Debug.Log(weaponNumber);
            for (int i = 0; i < unitcontroller.player.Count; i++)
            {
                if (unitcontroller.playerController[i].playerID == unitcontroller.selectPlayer)
                {
                    unitcontroller.playerController[i].selectWeapon = weaponNumber;
                }
            }
            chara.MenuEnd();
            chara.backMenu = true;
            Invoke("BackMenu", 1F);
            chara.stateCount = 4;
            chara.MoveState();
            NodeDelete();
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
