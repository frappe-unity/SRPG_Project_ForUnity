using UnityEngine;
using System.Collections;

public class CommandWindowController : MonoBehaviour {

    [SerializeField] private CharacterMoveController chara;
    int stateNumber = 0;

    public enum Command
    {
        ATTACK,
        CANCEL,
        STAY,
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
        }
    }

	public void OnClick()
    {
        chara.MenuEnd(0);
        if (stateNumber == 1)
        {
            // chara.AttackRange();
            chara.stateCount = 3;
            chara.MenuEnd(0);
            chara.MoveState();
            Invoke("BackMenu", 1F);
        }
        else if(stateNumber == 2)
        {
            chara.backMenu = true;
            chara.MenuEnd(0);
            chara.Cancel();
            Invoke("BackMenu", 0.5F);
            //chara.ReturnPos();
            //chara.stateCount = 1;
            //chara.MoveState();
        } else if(stateNumber == 3)
        {
            chara.EndAct();
            chara.MenuEnd(0);
            chara.Initialize();
            chara.MoveState();
        }
    }

    public void BackMenu()
    {
        chara.backMenu = false;
    }
}
