using UnityEngine;
using System.Collections;

public class CommandWindowController : MonoBehaviour {

    [SerializeField] private CharacterMoveController chara;

    public enum Command
    {
        ATTACK,
        CANCEL,
        STAY,
    }

    public Command command;

	public void OnClick()
    {
        switch (command)
        {
            case Command.ATTACK:
                break;
            case Command.CANCEL:
                chara.stateCount--;
                chara.moveState();
                break;
            case Command.STAY:
                chara.Initialize();
                chara.stateCount = 0;
                chara.moveState();
                break;
        }
    }
}
