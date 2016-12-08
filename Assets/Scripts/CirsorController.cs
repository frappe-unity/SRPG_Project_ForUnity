using UnityEngine;
using System.Collections;

public class CirsorController : MonoBehaviour {

    [SerializeField] private UnitController unit;
    [SerializeField] private MapController map;
    [SerializeField] private CharacterMoveController chara;
    private PlayerController player;

    public Vector2 cirsorPos = new Vector2(0, 0);
    public int unitCount = 0;
    public bool isMove = false;
    private int size = 10;
    private float rate = 10F;
    public float cirsorSpeed = 20F;

    void Start()
    {
        transform.position = new Vector3(0.0f, 25, 0.0f); //プレイヤーの座標を初期化（数値が狂うのを防止するため）
        unitCount = 0;
        cirsorPos = new Vector2(0.0f, 0.0f);
        player = unit.GetComponent<PlayerController>();
    }


    void Update()
    {
        if (!chara.isMove)
        {
            for (int i = 0; i < unit.playerObj.Length; i++)
            {
                if (Mathf.RoundToInt(unit.playerController[i].unitPos.x) == Mathf.RoundToInt(cirsorPos.x) && Mathf.RoundToInt(unit.playerController[i].unitPos.y) == Mathf.RoundToInt(cirsorPos.y) && !unit.playerController[i].isAct)
                {
                    unit.selectUnit = i;
                    unitCount = i;
                }
            }
        }
        

        if (chara.stateCount != 2)
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
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(cirsorPos.x * size, transform.position.y, cirsorPos.y * size), cirsorSpeed * Time.deltaTime);
        if(transform.position == new Vector3(cirsorPos.x * size, transform.position.y, cirsorPos.y * size))
        {
            isMove = false;
        }
    }    
}
