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
        Parameter,
    }

    public TextCommand textCommand;
    public int number = 0;

	// Use this for initialization
	void Start () {
        
	}

    // Update is called once per frame
    void Update()
    {
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

                }
                else
                {
                    tex.text = "Enemy".ToString();
                }
                break;
            case TextCommand.Parameter:
                int unitNum = unit.selectUnit;
                int enemyNum = unit.selectEnemy;
                if (unitNum != 99 && enemyNum == 99)
                {
                    Debug.Log("player");
                    var param = unit.playerController[unitNum];
                    tex.text = "名前 : " + param.name + "\n" + "HP : " + param.hp + "\n" + "攻撃 : " + param.attack + "\n" + "防御 : " + param.deffence + "\n" + "技 : " + param.hit + "\n" + "移動力 : " + param.moveCost;
                }
                else if (unitNum == 99 && enemyNum != 99)
                {
                    Debug.Log("Enemy");
                    var param = unit.enemyController[enemyNum];
                    tex.text = "名前 : " + param.name + "\n" + "HP : " + param.hp + "\n" + "攻撃 : " + param.attack + "\n" + "防御 : " + param.deffence + "\n" + "技 : " + param.hit + "\n" + "移動力 : " + param.moveCost;
                }
                else if (unitNum == 99 && enemyNum == 99)
                {
                    Debug.Log("null");
                    tex.text = "";
                }
                break;

        }
    }
}
