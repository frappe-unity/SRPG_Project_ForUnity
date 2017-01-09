using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{

    /// <summary>
    /// 各種参照
    /// </summary>
    [SerializeField] private UnitController unitcontroller;
    [SerializeField] private WeaponData weapondata;
    [SerializeField] private GameController gm;

    /// <summary>
    /// バトルパラメータ
    /// </summary>
    public int attackPower = 0;
    public int deffencePower = 0;
    public int damage = 0;
    public int hit = 0;
    public int avoid = 0;
    public int critical = 0;
    public int criticalAvoid = 0;
    public int hitPer = 0;
    public int criticalPer = 0;

    /// <summary>
    /// プレイヤーの能力
    /// </summary>
    public int selectPlayer = 0;
    public int playerAttack = 0;
    public int playerDeffence = 0;
    public int playerHit = 0;
    public int playerSpeed = 0;
    public int playerLucky = 0;
    public int playerWeapon = 0;

    /// <summary>
    /// エネミーの能力
    /// </summary>
    public int selectEnemy = 0;
    public int enemyAttack = 0;
    public int enemyDeffence = 0;
    public int enemyHit = 0;
    public int enemySpeed = 0;
    public int enemyLucky = 0;
    public int enemyWeapon = 0;

    private int randomHit = 0;
    private int randomCritical = 0;

    void Update()
    {

    }

    public void PlayerAttack(int playerID)
    {
        selectPlayer = playerID;
        playerAttack = unitcontroller.playerController[selectPlayer].attack;
        playerDeffence = unitcontroller.playerController[selectPlayer].deffence;
        playerHit = unitcontroller.playerController[selectPlayer].hit;
        playerLucky = unitcontroller.playerController[selectPlayer].lucky;
        playerWeapon = unitcontroller.playerController[selectPlayer].selectWeapon;
    }

    public void EnemyAttack(int enemyID)
    {
        selectEnemy = enemyID;
        if(selectEnemy != 99)
        {
            // Debug.Log("EnemyID : " + selectEnemy);
            enemyAttack = unitcontroller.enemyController[selectEnemy].attack;
            enemyDeffence = unitcontroller.enemyController[selectEnemy].deffence;
            enemyHit = unitcontroller.enemyController[selectEnemy].hit;
            enemySpeed = unitcontroller.enemyController[selectEnemy].speed;
            enemyLucky = unitcontroller.enemyController[selectEnemy].lucky;
        } else
        {
            enemyAttack = 0;
            enemyDeffence = 0;
            enemyHit = 0;
            enemySpeed = 0;
            enemyLucky = 0;
        }
    }

    public void BattleParam()
    {
        if (gm.playerTurn)
        {
            attackPower = playerAttack + weapondata.blade[playerWeapon].attack;
            deffencePower = enemyDeffence;
            damage = attackPower - deffencePower;
            hit = weapondata.blade[playerWeapon].hitper + (playerHit / 2);
            avoid = enemySpeed * 2 + enemyLucky;
            critical = weapondata.blade[playerWeapon].criticalper + (playerHit / 2);
            criticalAvoid = enemyLucky;
            hitPer = hit - avoid;
            criticalAvoid = critical - criticalAvoid;
        } else
        {
            attackPower = enemyAttack;
            deffencePower = playerDeffence;
            damage = attackPower - deffencePower;
            hit = 100 + (enemyHit / 2);
            avoid = playerSpeed * 2 + playerLucky;
            critical = 5 + (enemyHit / 2);
            criticalAvoid = playerLucky;
            hitPer = hit - avoid;
            criticalPer = critical - criticalAvoid;
        }
        
    }

    public void Battle()
    {
        randomCritical = Random.Range(1, 100);
        randomHit = Random.Range(1, 100);

        if(randomCritical <= criticalPer || criticalPer >= 100)
        {
            damage *= 3;
            // Debug.Log("critial");
            // Debug.Log(attackPower);
        }
        if(randomHit <= hitPer || hitPer >= 100)
        {
            // Debug.Log("hit");
            // Debug.Log(attackPower);
            if (gm.playerTurn)
            {
                unitcontroller.enemyController[selectEnemy].Damage(damage);
            } else
            {
                unitcontroller.playerController[selectPlayer].Damage(damage);
            }
            Debug.Log(unitcontroller.enemyController[selectEnemy].hp);
        } else
        {
            // Debug.Log("miss");
            // Debug.Log(attackPower);
            if (gm.playerTurn)
            {
                unitcontroller.enemyController[selectEnemy].Damage(0);
            }
            else
            {
                unitcontroller.playerController[selectPlayer].Damage(0);
            }
            Debug.Log(unitcontroller.enemyController[selectEnemy].hp);
        }
    }
}
