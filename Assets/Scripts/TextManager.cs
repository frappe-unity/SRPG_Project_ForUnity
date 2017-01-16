using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

    [SerializeField] private UnitController unitcontroller;
    [SerializeField] private GameController gm;
    [SerializeField] private AttackController attackController;
    [SerializeField] private CharacterMoveController chara;
    [SerializeField] private WeaponData weapondata;
    [SerializeField] private Text tex;

    public int weaponNumber;
    

    public enum TextCommand
    {
        STAY,
        TURN,
        TURN_NAME,
        Parameter,
        PlayerBattle,
        EnemyBattle,
        Weapon,
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
                number = unitcontroller.stayCount;
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
                
                if (!chara.isAttack)
                {
                    int playerNum = unitcontroller.paramPlayer;
                    int enemyNum = unitcontroller.paramEnemy;
                    if (playerNum != 99 && enemyNum == 99)
                    {
                        Debug.Log("PlayerNum" + playerNum);
                        for(int i = 0;i < unitcontroller.player.Count; i++)
                        {
                            if(unitcontroller.playerController[i].playerID == unitcontroller.paramPlayer)
                            {
                                playerNum = i;
                            }
                        }
                        var param = unitcontroller.playerController[playerNum];
                        tex.text = "名前 : " + param.name + "\n" + "HP : " + param.hp + "\n" + "攻撃 : " + param.attack + "\n" + "防御 : " + param.deffence + "\n" + "技 : " + param.hit + "\n" + "移動力 : " + param.moveCost;
                    }
                    else if (playerNum == 99 && enemyNum != 99)
                    {
                        Debug.Log("Chara is:" + chara.isAttack);
                        var param = unitcontroller.enemyController[enemyNum];
                        tex.text = "名前 : " + param.name + "\n" + "HP : " + param.hp + "\n" + "攻撃 : " + param.attack + "\n" + "防御 : " + param.deffence + "\n" + "技 : " + param.hit + "\n" + "移動力 : " + param.moveCost;
                    }
                    else if (playerNum == 99 && enemyNum == 99)
                    {
                        tex.text = "";
                    }
                }
                
                break;
            case TextCommand.PlayerBattle:
                int damage = attackController.damage;
                int hitPer = attackController.hitPer;
                int criticalPer = attackController.criticalPer;
                if(unitcontroller.selectEnemy != 99 && unitcontroller.turnState == UnitController.TurnState.ATTACK)
                {
                    if(attackController.playerSpeed >= attackController.enemySpeed * 2)
                    {
                        tex.text = hitPer + "\n" + damage + "\n" + criticalPer + "\n" + "x2";
                    } else
                    {
                        tex.text = hitPer + "\n" + damage + "\n" + criticalPer;
                    }
                } else
                {
                    tex.text = "";
                }
                break;
            case TextCommand.EnemyBattle:
                int enemyDamage = attackController.counterDamage;
                int enemyHitPer = attackController.counterHitPer;
                int enemyCriticalPer = attackController.counterCriticalPer;
                if (unitcontroller.selectEnemy != 99 && unitcontroller.turnState == UnitController.TurnState.ATTACK)
                {
                    if (attackController.enemySpeed >= attackController.playerSpeed * 2)
                    {
                        tex.text = enemyHitPer + "\n" + enemyDamage + "\n" + enemyCriticalPer + "\n" + "x2";
                    }
                    else
                    {
                        tex.text = enemyHitPer + "\n" + enemyDamage + "\n" + enemyCriticalPer;
                    }
                }
                else
                {
                    tex.text = "";
                }
                break;
            case TextCommand.Weapon:
                unitcontroller = GameObject.FindGameObjectWithTag("UniCon").GetComponent<UnitController>();
                weapondata = Resources.Load<WeaponData>("Database/Weapon");
                tex = gameObject.GetComponent<Text>();
                tex.text = weapondata.blade[weaponNumber].bladeName.ToString();
                break;
        }
    }
}
