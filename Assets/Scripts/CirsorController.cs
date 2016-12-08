using UnityEngine;
using System.Collections;

public class CirsorController : MonoBehaviour {

    [SerializeField] private UnitController unit;
    [SerializeField] private MapController map;
    [SerializeField] private CharacterMoveController chara;

    private PlayerController player;
    public bool debugKey = false;
    // public Vector2 targetPos = new Vector2(0.0F,0.0F); //プレイヤーのターゲット座標
    public Vector3 moveVec = new Vector3(0.0f, 0.0f, 0.0f); //プレイヤーの移動ベクトル
    public float speed = 0.0625f; //（デフォルト値0.5f）遅くするには[0.25][0.125][0.0625]と元の数字を２で割ってやる
    public float threshold = 0.05f; //ブロック停止座標のしきい値を設定speedの設定値に応じて精度を変える
    public bool dontPlayKey; //キー入力の制限用フラグ（falseでキー操作が可能・trueでキー操作不可）
    public float KeytimerMax = 0.35f; //（デフォルト値0.35f）キー入力の不可時間設定
    private float keyTimer = 0.0f; //キー入力不可時間カウント用タイマーを入れる入れ物
    public bool slantingTrigger = false; //斜め移動（Rボタン・Lキー）が押されたときのトリガーフラグ
    public float moveSpeed = 1.0F;

    public Vector2 cirsorPos = new Vector2(0, 0);
    public int unitCount = 0;




    private Vector3 startPos;
    private Vector3 endPos;
    private bool isMove = false;
    private int size = 10;
    private float rate = 10F;
    public float cirsorSpeed = 20F;




    void Update()
    {
        for (int i = 0; i < unit.playerObj.Length; i++)
        {
            if (Mathf.RoundToInt(unit.playerController[i].unitPos.x) == Mathf.RoundToInt(cirsorPos.x) && Mathf.RoundToInt(unit.playerController[i].unitPos.y) == Mathf.RoundToInt(cirsorPos.y) && !unit.playerController[i].isAct && !isMove)
            {
                unit.selectUnit = i;
                unitCount = i;
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
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(cirsorPos.x * size, transform.position.y, cirsorPos.y * size), cirsorSpeed * Time.deltaTime);
            if(transform.position == new Vector3(cirsorPos.x * size, transform.position.y, cirsorPos.y * size))
            {
                isMove = false;
            }
        }
    }










    void Start()
    {
        transform.position = new Vector3(0.0f, 25, 0.0f); //プレイヤーの座標を初期化（数値が狂うのを防止するため）
        unitCount = 0;
        cirsorPos = new Vector2(0.0f,0.0f);
        player = unit.GetComponent<PlayerController>();
    }

    void Update2()
    {
        for (int i = 0; i < unit.playerObj.Length; i++)
        {
            if (Mathf.RoundToInt(unit.playerController[i].unitPos.x)  == Mathf.RoundToInt(cirsorPos.x) && Mathf.RoundToInt(unit.playerController[i].unitPos.y) == Mathf.RoundToInt(cirsorPos.y) && !unit.playerController[i].isAct)
            {
                unit.selectUnit = i;
                unitCount = i;
            }
        }

        ///////////////////////////////////////////////////////斜め移動//////////////////////////////////////////////////////////
        if(chara.stateCount != 2)
        {
            debugKey = true;
            if (Input.GetKey(KeyCode.JoystickButton5) || Input.GetKey(KeyCode.L))
            {
                slantingTrigger = true;//slantingTriggerをtrueにして斜め移動処理を可能にする
            }
            else
            {
                slantingTrigger = false;//押されていない時はfalseに戻す(斜め移動処理を不可能にする)
            }

            if (dontPlayKey == false && slantingTrigger == true)
            {
                //Debug.Log(“斜め移動操作OK”);
                if (Input.GetAxis("Horizontal") >= 0.4 && Input.GetAxis("Vertical") >= 0.4 ||
                Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
                { //キー入力が「→↑」
                  //Debug.Log(“右斜め上”);
                    dontPlayKey = true; //操作不化可能判定フラグをtrueにしてキー操作を受け付けなくする
                    cirsorPos.x += moveSpeed; //目標座標Xに１
                    cirsorPos.y += moveSpeed; //目標座標Zに１
                    moveVec.x = 5.0f; //移動ベクトルXに0.5代入
                    moveVec.z = 5.0f; //移動ベクトルZに0.5代入

                }
                else if (Input.GetAxis("Horizontal") <= -0.4 && Input.GetAxis("Vertical") >= 0.4 ||
              Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
                { //キー入力が「←↑」
                  //Debug.Log (“左斜め上”);
                    dontPlayKey = true; //操作不化可能判定フラグをtrueにしてキー操作を受け付けなくする
                    cirsorPos.x -= moveSpeed; //目標座標Xに-１
                    cirsorPos.y += moveSpeed; //目標座標Zに１
                    moveVec.x = -5.0f; //移動ベクトルXに-0.5代入
                    moveVec.z = 5.0f; //移動ベクトルZに0.5代入

                }
                else if (Input.GetAxis("Horizontal") >= 0.4 && Input.GetAxis("Vertical") <= -0.4 ||
              Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
                { //キー入力が「→↓」
                  //Debug.Log(“右斜め下”);
                    dontPlayKey = true; //操作不化可能判定フラグをtrueにしてキー操作を受け付けなくする
                    cirsorPos.x += moveSpeed; //目標座標Xに１
                    cirsorPos.y -= moveSpeed; //目標座標Zに-１
                    moveVec.x = 5.0f; //移動ベクトルXに0.5代入
                    moveVec.z = -5.0f; //移動ベクトルZに-0.5代入

                }
                else if (Input.GetAxis("Horizontal") <= -0.4 && Input.GetAxis("Vertical") <= -0.4 ||
              Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
                { //キー入力が「←↓」
                  //Debug.Log (“左斜め下”);
                    dontPlayKey = true; //操作不化可能判定フラグをtrueにしてキー操作を受け付けなくする
                    cirsorPos.x -= moveSpeed; //目標座標Xに-１
                    cirsorPos.y -= moveSpeed; //目標座標Zに-１
                    moveVec.x = -5.0f; //移動ベクトルXに-0.5代入
                    moveVec.z = -5.0f; //移動ベクトルZに-0.5代入

                }
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////通常移動//////////////////////////////////////////////////////////
            if (dontPlayKey == false && slantingTrigger == false)
            {
                //dontPlayKeyフラグがfalseでslantingTriggerがfalseなら（操作不化可能判定フラグと斜め移動トリガーがfalseだったら）

                if (Input.GetAxis("Horizontal") >= 0.8 || Input.GetKey(KeyCode.RightArrow))
                { //キー入力が「→」
                    if (cirsorPos.x == transform.position.x / size)
                    { //今のX座標とターゲットX座標が同じなら
                        dontPlayKey = true; //操作不化可能判定フラグをtrueにしてキー操作を受け付けなくする
                        cirsorPos.x += moveSpeed; //目標座標Xに１
                        moveVec.x = 10.0f; //移動ベクトルXに1代入
                    }
                }
                else if (Input.GetAxis("Horizontal") <= -0.8 || Input.GetKey(KeyCode.LeftArrow))
                { //キー入力が「←」
                    if (cirsorPos.x == transform.position.x / size)
                    { //今のX座標とターゲットX座標が同じなら
                        dontPlayKey = true; //操作不化可能判定フラグをtrueにしてキー操作を受け付けなくする
                        cirsorPos.x -= moveSpeed; //目標座標Xに-１
                        moveVec.x = -10.0f; //移動ベクトルXに-1代入
                    }
                }
                else if (Input.GetAxis("Vertical") >= 0.8 || Input.GetKey(KeyCode.UpArrow))
                { //キー入力が「↑」
                    if (cirsorPos.y == transform.position.y / size)
                    { //今のZ座標とターゲットZ座標が同じなら
                        dontPlayKey = true; //操作不化可能判定フラグをtrueにしてキー操作を受け付けなくする
                        cirsorPos.y += moveSpeed; //目標座標Zに１
                        moveVec.z = 10.0f; //移動ベクトルXに1代入
                    }
                }
                else if (Input.GetAxis("Vertical") <= -0.8 || Input.GetKey(KeyCode.DownArrow))
                { //キー入力が「↓」
                    if (cirsorPos.y == transform.position.y / size)
                    { //今のZ座標とターゲットZ座標が同じなら
                        dontPlayKey = true; //操作不化可能判定フラグをtrueにしてキー操作を受け付けなくする
                        cirsorPos.y -= moveSpeed; //目標座標Zに-１
                        moveVec.z = -10.0f; //移動ベクトルZに-1代入
                    }
                }

            }
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
            if (cirsorPos.x != transform.position.x / size)
            { //今の座標がtargetPos.xと違うなら
                transform.Translate(moveVec * speed); //移動処理
            }
            else if (transform.position.x + transform.position.x + threshold >= cirsorPos.x  * size ||
          transform.position.x + transform.position.x - threshold <= cirsorPos.x * size)
            { //数値狂い対策でしきい値を含めた（又は引いた）値以上（以下）なら

                keyTimer += Time.deltaTime; //キー入力不可タイマー発動
                moveVec.x = 0.0f; //移動を止める

                if (keyTimer >= KeytimerMax && moveVec == new Vector3(0.0f, 0.0f, 0.0f))
                { //キー入力不可タイマーの値がKeytimerMaxの値以上かつmoveVecがzeroなら
                    dontPlayKey = false; //操作不化可能判定フラグをfalseにしてキー入力を可能にする
                    keyTimer = 0.0f; //キー入力不可タイマーの値をリセットする
                }
            }

            if (cirsorPos.y != transform.position.y / size)
            { //今の座標がtargetPos.xと違うなら
                transform.Translate(moveVec * speed); //移動処理
            }
            else if (transform.position.x + transform.position.y + threshold >= cirsorPos.y * size ||
          transform.position.y + transform.position.y - threshold <= cirsorPos.y * size)
            { //数値狂い対策でしきい値を含めた（又は引いた）値以上（以下）

                keyTimer += Time.deltaTime; //キー入力不可タイマー発動
                moveVec.z = 0.0f; //移動を止める

                if (keyTimer >= KeytimerMax && moveVec == new Vector3(0.0f, 0.0f, 0.0f))
                { //キー入力不可タイマーの値がKeytimerMaxの値以上かつmoveVecがzeroなら
                    dontPlayKey = false; //操作不化可能判定フラグをfalseにしてキー入力を可能にする
                    keyTimer = 0.0f; //キー入力不可タイマーの値をリセットする
                }
            }
        } else
        {
            debugKey = false;
        }
    }
        
}
