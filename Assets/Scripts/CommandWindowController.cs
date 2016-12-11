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
        chara.MenuEnd();
        if (stateNumber == 1)
        {
            chara.AttackRange();
            chara.MenuEnd();
        }
        else if(stateNumber == 2)
        {
            chara.MenuEnd();
            chara.ReturnPos();
            chara.stateCount = 0;
            chara.MoveState();
        } else if(stateNumber == 3)
        {
            chara.EndAct();
            chara.MenuEnd();
            chara.Initialize();
            chara.MoveState();
        }
    }
}
