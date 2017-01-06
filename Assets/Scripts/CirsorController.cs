using UnityEngine;
using System.Collections;

public class CirsorController : MonoBehaviour {

    [SerializeField] private UnitController unit;
    [SerializeField] private MapController map;
    [SerializeField] private CharacterMoveController chara;
    private PlayerController player;
    private EnemyController enemy;

    public Vector2 cirsorPos = new Vector2(0, 0);
    public bool isMove = false;
    private int size = 10;
    private float rate = 10F;
    public float cirsorSpeed = 20F;
    public float speedUp = 1F;

    void Start()
    {
        transform.position = new Vector3(0.0f, 25, 0.0f); //プレイヤーの座標を初期化（数値が狂うのを防止するため）
        cirsorPos = new Vector2(0.0f, 0.0f);
        player = unit.GetComponent<PlayerController>();
    }


    void Update()
    {
        if (!chara.isMove)
        {
            if(chara.stateCount < 3)
            {
                if(map.block[Mathf.RoundToInt(cirsorPos.x), Mathf.RoundToInt(cirsorPos.y)].playerOn)
                {
                    unit.selectUnit = map.block[Mathf.RoundToInt(cirsorPos.x), Mathf.RoundToInt(cirsorPos.y)].playerID;
                } else
                {
                    unit.selectUnit = 99;
                }
            }
        }
        if(map.block[Mathf.RoundToInt(cirsorPos.x), Mathf.RoundToInt(cirsorPos.y)].enemyOn)
        {
            unit.selectEnemy = map.block[Mathf.RoundToInt(cirsorPos.x), Mathf.RoundToInt(cirsorPos.y)].enemyID;
            unit.paramEnemy = map.block[Mathf.RoundToInt(cirsorPos.x), Mathf.RoundToInt(cirsorPos.y)].enemyID;
        } else
        {
            unit.selectEnemy = 99;
            unit.paramEnemy = 99;
        }
        if (map.block[Mathf.RoundToInt(cirsorPos.x), Mathf.RoundToInt(cirsorPos.y)].playerOn)
        {
            unit.paramPlayer = map.block[Mathf.RoundToInt(cirsorPos.x), Mathf.RoundToInt(cirsorPos.y)].playerID;
        }
        else
        {
            unit.paramPlayer = 99;
        }
        if (chara.stateCount == 0 || chara.stateCount == 1 || chara.stateCount == 4)
        {
            if (!isMove)
            {
                // transform.position = startPos;
                if (Input.GetAxis("Horizontal") >= 0.8 || Input.GetKey(KeyCode.RightArrow))
                { //キー入力が「→」
                    isMove = true;
                    cirsorPos = new Vector2(cirsorPos.x + 1, cirsorPos.y);
                    // endPos = new Vector3(startPos.x + size, startPos.y, startPos.z);
                }
                if (Input.GetAxis("Horizontal") <= -0.8 || Input.GetKey(KeyCode.LeftArrow))
                { //キー入力が「←」
                    isMove = true;
                    cirsorPos = new Vector2(cirsorPos.x - 1, cirsorPos.y);
                    // endPos = new Vector3(startPos.x - size, startPos.y, startPos.z);
                }
                if (Input.GetAxis("Vertical") >= 0.8 || Input.GetKey(KeyCode.UpArrow))
                { //キー入力が「↑」
                    isMove = true;
                    cirsorPos = new Vector2(cirsorPos.x, cirsorPos.y + 1);
                    //endPos = new Vector3(startPos.x, startPos.y, startPos.z + size);

                }
                if (Input.GetAxis("Vertical") <= -0.8 || Input.GetKey(KeyCode.DownArrow))
                { //キー入力が「↑」
                    isMove = true;
                    cirsorPos = new Vector2(cirsorPos.x, cirsorPos.y - 1);
                    //endPos = new Vector3(startPos.x, startPos.y, startPos.z - size);
                }
               
            }
            Move();
        }

    }

    public void Move()
    {
        if (Input.GetButton("Cancel") && chara.stateCount == 0)
        {
            speedUp = 2;
        } else
        {
            speedUp = 1;
        }
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(cirsorPos.x * size, transform.position.y, cirsorPos.y * size), cirsorSpeed * Time.deltaTime * speedUp);
        if(transform.position == new Vector3(cirsorPos.x * size, transform.position.y, cirsorPos.y * size))
        {
            isMove = false;
        }
    }    
}
