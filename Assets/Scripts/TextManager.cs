using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

    [SerializeField] private UnitController unit;
    [SerializeField] private GameController gm;
    [SerializeField] private Text tex;

    public enum TextCommand
    {
        STAY,
        TURN,
        TURN_NAME,
    }

    public TextCommand textCommand;
    public int number = 0;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        switch (textCommand)
        {
            case TextCommand.STAY:
                number = unit.stayCount;
                tex.text = number.ToString();
                break;
            case TextCommand.TURN:
                number = gm.turnCount;
                tex.text = number.ToString();
                break;
            case TextCommand.TURN_NAME:
                if (gm.playerTurn)
                {
                    tex.text = "Player".ToString();

                } else
                {
                    tex.text = "Enemy".ToString();
                }
                break;
        }
    }
}
