using UnityEngine;
using System.Collections;

public class CirsorController : MonoBehaviour {

    [SerializeField] private UnitController unitcontroller;
    [SerializeField] private MapController map;
    [SerializeField] private CharacterMoveController chara;
    [SerializeField] private SoundController soundcontroller;
    [SerializeField] private AttackController attackcontroller;
    private PlayerController player;
    private EnemyController enemy;

    public Vector2 cirsorPos = new Vector2(0, 0);
    public bool isMove = false;
    private int size = 10;
    private float rate = 10F;
    public float cirsorSpeed = 20F;
    public float speed = 1F;
    public float speedUp = 1.5F;
    private float speedTo;

    void Start()
    {
        transform.position = new Vector3(0.0f, 25, 0.0f); //プレイヤーの座標を初期化（数値が狂うのを防止するため）
        cirsorPos = new Vector2(0.0f, 0.0f);
        player = unitcontroller.GetComponent<PlayerController>();
        speedUp = speed * 1.5F;
    }


    void Update()
    {
        if (!chara.isMove)
        {
            if(chara.stateCount < 3)
            {
                // unitcontroller.UnitMovable();
                if(map.block[Mathf.RoundToInt(cirsorPos.x), Mathf.RoundToInt(cirsorPos.y)].playerOn)
                {
                    for(int i = 0;i < unitcontroller.player.Count; i++)
                    {
                        if(unitcontroller.playerController[i].playerID == map.block[Mathf.RoundToInt(cirsorPos.x), Mathf.RoundToInt(cirsorPos.y)].playerID)
                        {
                            unitcontroller.selectPlayer = i;
                        }
                    }
                    // unitcontroller.selectPlayer = map.block[Mathf.RoundToInt(cirsorPos.x), Mathf.RoundToInt(cirsorPos.y)].playerID;
                } else
                {
                    unitcontroller.selectPlayer = 99;
                }
            }
        }
        if(map.block[Mathf.RoundToInt(cirsorPos.x), Mathf.RoundToInt(cirsorPos.y)].enemyOn)
        {
            /*
            for (int i = 0; i < unitcontroller.player.Count; i++)
            {
                if (unitcontroller.enemyController[i].enemyID == map.block[Mathf.RoundToInt(cirsorPos.x), Mathf.RoundToInt(cirsorPos.y)].enemyID)
                {
                    unitcontroller.selectEnemy = i;
                    unitcontroller.paramEnemy = i;
                }
            }
            */
            unitcontroller.selectEnemy = map.block[Mathf.RoundToInt(cirsorPos.x), Mathf.RoundToInt(cirsorPos.y)].enemyID;
            unitcontroller.paramEnemy = map.block[Mathf.RoundToInt(cirsorPos.x), Mathf.RoundToInt(cirsorPos.y)].enemyID;
        } else
        {
            unitcontroller.selectEnemy = 99;
            unitcontroller.paramEnemy = 99;
        }
        if (map.block[Mathf.RoundToInt(cirsorPos.x), Mathf.RoundToInt(cirsorPos.y)].playerOn)
        {
            unitcontroller.paramPlayer = map.block[Mathf.RoundToInt(cirsorPos.x), Mathf.RoundToInt(cirsorPos.y)].playerID;
            
            for(int i = 0; i < unitcontroller.player.Count; i++)
            {
                if(map.block[Mathf.RoundToInt(cirsorPos.x), Mathf.RoundToInt(cirsorPos.y)].playerID == unitcontroller.playerController[i].playerID)
                {
                    unitcontroller.paramPlayer = i;
                }
            }
            
        }
        else
        {
            unitcontroller.paramPlayer = 99;
        }
        if (chara.stateCount == 0 || chara.stateCount == 1 || chara.stateCount == 4)
        {
            if (!isMove && !attackcontroller.isBattle)
            {
                // transform.position = startPos;
                if (Input.GetAxis("Horizontal") >= 0.8 || Input.GetKey(KeyCode.RightArrow))
                { //キー入力が「→」
                    soundcontroller.SoundPlayer(0);
                    isMove = true;
                    cirsorPos = new Vector2(cirsorPos.x + 1, cirsorPos.y);
                    // endPos = new Vector3(startPos.x + size, startPos.y, startPos.z);
                }
                if (cirsorPos.x > 0 && (Input.GetAxis("Horizontal") <= -0.8 || Input.GetKey(KeyCode.LeftArrow)))
                { //キー入力が「←」
                    soundcontroller.SoundPlayer(0);
                    isMove = true;
                    cirsorPos = new Vector2(cirsorPos.x - 1, cirsorPos.y);
                    // endPos = new Vector3(startPos.x - size, startPos.y, startPos.z);
                }
                if (Input.GetAxis("Vertical") >= 0.8 || Input.GetKey(KeyCode.UpArrow))
                { //キー入力が「↑」
                    soundcontroller.SoundPlayer(0);
                    isMove = true;
                    cirsorPos = new Vector2(cirsorPos.x, cirsorPos.y + 1);
                    //endPos = new Vector3(startPos.x, startPos.y, startPos.z + size);

                }
                if (cirsorPos.y > 0 && (Input.GetAxis("Vertical") <= -0.8 || Input.GetKey(KeyCode.DownArrow)))
                { //キー入力が「↓」
                    soundcontroller.SoundPlayer(0);
                    isMove = true;
                    cirsorPos = new Vector2(cirsorPos.x, cirsorPos.y - 1);
                    //endPos = new Vector3(startPos.x, startPos.y, startPos.z - size);
                }
               
            }
            if (!isMove && cirsorPos.x == 0 && cirsorPos.y == 0)
            {

            }
            if (Input.GetButton("Cancel") && chara.stateCount == 0)
            {
                speedTo = speedUp;
            } else
            {
                speedTo = speed;
            }
            Move();
        }

    }

    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(cirsorPos.x * size, transform.position.y, cirsorPos.y * size), cirsorSpeed  * speedTo * Time.deltaTime);
        if(transform.position == new Vector3(cirsorPos.x * size, transform.position.y, cirsorPos.y * size))
        {
            isMove = false;
        }
    }    
}
