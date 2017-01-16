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
    [SerializeField] private EnemyAIController enemy;
    [SerializeField] private SoundController soundcontroller;

    public float moveTime = 1F;
    public bool isDestroy = false;
    public bool isBattle = false;
    public int count = 0;

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
    /// 反撃
    /// </summary>
    public int counterAttackPower = 0;
    public int counterDeffencePower = 0;
    public int counterDamage = 0;
    public int counterHit = 0;
    public int counterAvoid = 0;
    public int counterCritical = 0;
    public int counterCriticalAvoid = 0;
    public int counterHitPer = 0;
    public int counterCriticalPer = 0;

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
        playerSpeed = unitcontroller.playerController[selectPlayer].speed;
        playerLucky = unitcontroller.playerController[selectPlayer].lucky;
        playerWeapon = unitcontroller.playerController[selectPlayer].selectWeapon;
    }

    public void EnemyAttack(int enemyID)
    {
        selectEnemy = enemyID;
        enemyAttack = unitcontroller.enemyController[selectEnemy].attack;
        enemyDeffence = unitcontroller.enemyController[selectEnemy].deffence;
        enemyHit = unitcontroller.enemyController[selectEnemy].hit;
        enemySpeed = unitcontroller.enemyController[selectEnemy].speed;
        enemyLucky = unitcontroller.enemyController[selectEnemy].lucky;
        enemyWeapon = unitcontroller.enemyController[selectEnemy].selectWeapon;
    }

    public void BattleParam()
    {
        // 攻撃時
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
            criticalPer = critical - criticalAvoid;

            counterAttackPower = enemyAttack + weapondata.blade[enemyWeapon].attack;
            counterDeffencePower = playerDeffence;
            counterDamage = counterAttackPower - counterDeffencePower;
            counterHit = weapondata.blade[enemyWeapon].hitper + (enemyHit / 2);
            counterAvoid = playerSpeed * 2 + playerLucky;
            counterCritical = weapondata.blade[enemyWeapon].criticalper + (enemyHit / 2);
            counterCriticalAvoid = playerLucky;
            counterHitPer = counterHit - counterAvoid;
            counterCriticalPer = counterCritical - counterCriticalAvoid;
        } else if(!gm.playerTurn)
        {
            attackPower = enemyAttack + weapondata.blade[enemyWeapon].attack;
            deffencePower = playerDeffence;
            damage = attackPower - deffencePower;
            hit = weapondata.blade[enemyWeapon].hitper + (enemyHit / 2);
            avoid = playerSpeed * 2 + playerLucky;
            critical = weapondata.blade[enemyWeapon].criticalper + (enemyHit / 2);
            criticalAvoid = playerLucky;
            hitPer = hit - avoid;
            criticalPer = critical - criticalAvoid;

            counterAttackPower = playerAttack + weapondata.blade[playerWeapon].attack;
            counterDeffencePower = enemyDeffence;
            counterDamage = attackPower - deffencePower;
            counterHit = weapondata.blade[playerWeapon].hitper + (playerHit / 2);
            counterAvoid = enemySpeed * 2 + enemyLucky;
            counterCritical = weapondata.blade[playerWeapon].criticalper + (playerHit / 2);
            counterCriticalAvoid = enemyLucky;
            counterHitPer = hit - avoid;
            counterCriticalPer = critical - criticalAvoid;
        }

    }

    public void Battle()
    {
        isBattle = true;
        isDestroy = false;
        count++;
        // 攻撃時
        Debug.Log("PlayerTurn is" + gm.playerTurn);
        randomCritical = Random.Range(1, 100);
        randomHit = Random.Range(1, 100);

        
        if(randomHit <= hitPer || hitPer >= 100)
        {
            if (randomCritical <= criticalPer || criticalPer >= 100)
            {
                damage *= 3;
                soundcontroller.SoundPlayer(3);
            }
            else
            {
                soundcontroller.SoundPlayer(2);
            }
            if (gm.playerTurn)
            {
                Debug.Log("PlayerID :" + unitcontroller.playerController[selectPlayer].playerID + " → EnemyID :" + unitcontroller.enemyController[selectEnemy].enemyID + " damage :" + damage);
                if(unitcontroller.enemyController[selectEnemy].hp <= damage)
                {
                    isDestroy = true;
                }
                unitcontroller.enemyController[selectEnemy].Damage(damage);
            } else
            {
                Debug.Log("EnemyID :" + unitcontroller.enemyController[selectEnemy].enemyID+  "→ PlayerID :" + unitcontroller.playerController[selectPlayer].playerID  + " damage :" + damage);
                if (unitcontroller.playerController[selectPlayer].hp <= damage)
                {
                    isDestroy = true;
                }
                unitcontroller.playerController[selectPlayer].Damage(damage);
            }
        } else
        {
            soundcontroller.SoundPlayer(4);
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
        if(count == 3)
        {
            count = 0;
            isDestroy = false;
        }
        if (!isDestroy && count != 0)
        {
            Invoke("CounterBattle", moveTime);
        } else
        {
            count = 0;
            isDestroy = false;
            isBattle = false;
            if (!gm.playerTurn)
            {
                Invoke("EnemyEnd", moveTime);
            }
        }
    }

    public void CounterBattle()
    {
        count++;
        // 反撃時
        randomCritical = Random.Range(1, 100);
        randomHit = Random.Range(1, 100);

        
        if (randomHit <= counterHitPer || counterHitPer >= 100)
        {
            if (randomCritical <= counterCriticalPer || counterCriticalPer >= 100)
            {
                counterDamage *= 3;
                soundcontroller.SoundPlayer(3);
            }
            else
            {
                soundcontroller.SoundPlayer(2);
            }
            if (gm.playerTurn)
            {
                Debug.Log("EnemyID :" + unitcontroller.enemyController[selectEnemy].enemyID + "→ PlayerID :" + unitcontroller.playerController[selectPlayer].playerID + " damage :" + counterDamage);
                if (unitcontroller.playerController[selectPlayer].hp <= counterDamage)
                {
                    isDestroy = true;
                }
                unitcontroller.playerController[selectPlayer].Damage(counterDamage);
            }
            else
            {
                Debug.Log("PlayerID :" + unitcontroller.playerController[selectPlayer].playerID + " → EnemyID :" + unitcontroller.enemyController[selectEnemy].enemyID + " damage :" + counterDamage);
                if (unitcontroller.enemyController[selectEnemy].hp <= counterDamage)
                {
                    isDestroy = true;
                }
                unitcontroller.enemyController[selectEnemy].Damage(counterDamage);
            }
        }
        else
        {
            soundcontroller.SoundPlayer(4);
            if (gm.playerTurn)
            {
                unitcontroller.playerController[selectPlayer].Damage(0);
            }
            else
            {
                unitcontroller.enemyController[selectEnemy].Damage(0);
            }
            Debug.Log(unitcontroller.enemyController[selectEnemy].hp);
        }
        if (count == 3)
        {
            count = 0;
            isDestroy = false;
        }
        if (!isDestroy && count != 0)
        {
            if (gm.playerTurn)
            {
                if (unitcontroller.playerController[selectPlayer].speed >= unitcontroller.enemyController[selectEnemy].speed * 2)
                {
                    Invoke("Battle", moveTime);
                } else
                {
                    isDestroy = false;
                    isBattle = false;
                    count = 0;
                }
            } else
            {
                if (unitcontroller.enemyController[selectEnemy].speed >=  unitcontroller.playerController[selectPlayer].speed * 2)
                {
                    Invoke("CounterBattle", moveTime);
                } else
                {
                    isDestroy = false;
                    isBattle = false;
                    count = 0;
                    Invoke("EnemyEnd", moveTime);
                }
            }
        } else if (isDestroy)
        {
            isDestroy = false;
            isBattle = false;
            count = 0;
            if (!gm.playerTurn)
            {
                Invoke("EnemyEnd", moveTime);
            }
        }
        
    }

    public void EnemyEnd()
    {
        enemy.NextChara();
    }
}
