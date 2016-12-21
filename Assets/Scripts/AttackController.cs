using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{

    /// <summary>
    /// 各種参照
    /// </summary>
    [SerializeField]
    private UnitController unitController;
    [SerializeField]
    private WeaponData weapondata;

    /// <summary>
    /// プレーヤーバトルパラメータ
    /// </summary>
    public int attackPower;
    public int deffencePower;
    public int damage;
    public int hit;
    public int avoid;
    public int critical;
    public int criticalAvoid;
    public int hitPer;
    public int criticalPer;

    /// <summary>
    /// エネミーバトルパラメータ
    /// </summary>
    public int enemyAttackPower;
    public int enemyDeffencePower;
    public int enemyDamage;
    public int enemyHitPower;
    public int enemyAvoid;
    public int enemyCritical;
    public int enemyCriticalAvoid;
    public int enemyHitPer;
    public int enemyCriticalPer;


    /// <summary>
    /// プレイヤーの能力
    /// </summary>
    public int selectPlayer;
    public int playerAttack;
    public int playerDeffence;
    public int playerHit;
    public int playerSpeed;
    public int playerLucky;
    public int playerWeapon;

    /// <summary>
    /// エネミーの能力
    /// </summary>
    public int selectEnemy;
    public int enemyAttack;
    public int enemyDeffence;
    public int enemyHit;
    public int enemySpeed;
    public int enemyLucky;
    public int enemyWeapon;

    private int randomHit;
    private int randomCritical;

    void Update()
    {

    }

    public void PlayerAttack()
    {
        selectPlayer = unitController.selectUnit;
        playerAttack = unitController.playerController[selectPlayer].attack;
        playerDeffence = unitController.playerController[selectPlayer].deffence;
        playerHit = unitController.playerController[selectPlayer].hit;
        playerLucky = unitController.playerController[selectPlayer].lucky;
        playerWeapon = unitController.playerController[selectPlayer].selectWeapon;
    }

    public void EnemyAttack()
    {
        selectEnemy = unitController.selectEnemy;
        enemyAttack = unitController.enemyController[selectEnemy].attack;
        enemyDeffence = unitController.enemyController[selectEnemy].deffence;
        enemyHit = unitController.enemyController[selectEnemy].hit;
        enemySpeed = unitController.enemyController[selectEnemy].speed;
        enemyLucky = unitController.enemyController[selectEnemy].lucky;
    }

    public void PlayerBattleParam()
    {
        attackPower = playerAttack + weapondata.blade[playerWeapon].attack;
        deffencePower = enemyDeffence;
        hit = weapondata.blade[playerWeapon].hitper + (playerHit / 2);
        avoid = enemySpeed * 2 + enemyLucky;
        critical = weapondata.blade[playerWeapon].criticalper + (playerHit / 2);
        criticalAvoid = enemyLucky;
        hitPer = hit - avoid;
        criticalAvoid = critical - criticalAvoid;
    }

    public void EnemyBattleParam()
    {
        enemyAttackPower = enemyAttack;
        enemyDeffencePower = playerDeffence;
    }

    public void PlayerBattle()
    {
        randomCritical = Random.Range(1, 100);
        randomHit = Random.Range(1, 100);

        if(randomCritical <= criticalPer || criticalPer >= 100)
        {
            attackPower *= 3;
            Debug.Log("critial");
            // Debug.Log(attackPower);
        }
        if(randomHit <= hitPer || hitPer >= 100)
        {
            Debug.Log("hit");
            // Debug.Log(attackPower);
            unitController.enemyController[selectEnemy].hp -= attackPower;
            Debug.Log(unitController.enemyController[selectEnemy].hp);
        }
    }
}
