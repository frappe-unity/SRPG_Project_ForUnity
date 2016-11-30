using UnityEngine;
using System.Collections;

public class CirsorController : MonoBehaviour {

    [SerializeField] private UnitController unit;
    [SerializeField] private MapController map;

    public Vector3 targetPos = new Vector3(0.0f, 0.0F, 0.0f); //プレイヤーのターゲット座標
    public Vector3 moveVec = new Vector3(0.0f, 0.0f, 0.0f); //プレイヤーの移動ベクトル
    public float speed = 0.0625f; //（デフォルト値0.5f）遅くするには[0.25][0.125][0.0625]と元の数字を２で割ってやる
    public float threshold = 0.05f; //ブロック停止座標のしきい値を設定speedの設定値に応じて精度を変える
    public bool dontPlayKey; //キー入力の制限用フラグ（falseでキー操作が可能・trueでキー操作不可）
    public float KeytimerMax = 0.35f; //（デフォルト値0.35f）キー入力の不可時間設定
    private float keyTimer = 0.0f; //キー入力不可時間カウント用タイマーを入れる入れ物
    public bool slantingTrigger = false; //斜め移動（Rボタン・Lキー）が押されたときのトリガーフラグ
    public float moveSpeed = 10.0F;
    
    public int unitCount = 0;

    public int not = -1;

    void Start()
    {
        transform.position = new Vector3(0.0f, 25, 0.0f); //プレイヤーの座標を初期化（数値が狂うのを防止するため）
        
        targetPos = new Vector3(0.0f,25, 0.0f);
        
    }

    void Update()
    {
        if(not == -1)
        {
            for (int i = 0; i < unit.playerObj.Length; i++)
            {
                if (Mathf.RoundToInt(unit.playerObj[i].transform.position.x)  == Mathf.RoundToInt(this.transform.position.x) && Mathf.RoundToInt(unit.playerObj[i].transform.position.z) == Mathf.RoundToInt(this.transform.position.z) && !unit.isUnit)
                {
                    unitCount++;
                    unit.isUnit = true;
                    unit.selectUnit = i;
                }
                if (unitCount < 1)
                {
                    unit.isUnit = false;
                    unit.selectUnit = 99;

                } 
            }
        }
        unitCount = 0;
        if(map.block[(int)this.transform.position.x / 10, (int)this.transform.position.z / 10].blockNum == 1)
        {
            this.transform.position = new Vector3(this.transform.position.x,35,this.transform.position.z);
        } else
        {
            this.transform.position = new Vector3(this.transform.position.x,25, this.transform.position.z);
        }

        ///////////////////////////////////////////////////////斜め移動//////////////////////////////////////////////////////////

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
                targetPos.x+=moveSpeed; //目標座標Xに１
                targetPos.z+=moveSpeed; //目標座標Zに１
                moveVec.x = 0.5f; //移動ベクトルXに0.5代入
                moveVec.z = 0.5f; //移動ベクトルZに0.5代入

            }
            else if (Input.GetAxis("Horizontal") <= -0.4 && Input.GetAxis("Vertical") >= 0.4 ||
          Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
            { //キー入力が「←↑」
              //Debug.Log (“左斜め上”);
                dontPlayKey = true; //操作不化可能判定フラグをtrueにしてキー操作を受け付けなくする
                targetPos.x-=moveSpeed; //目標座標Xに-１
                targetPos.z+=moveSpeed; //目標座標Zに１
                moveVec.x = -0.5f; //移動ベクトルXに-0.5代入
                moveVec.z = 0.5f; //移動ベクトルZに0.5代入

            }
            else if (Input.GetAxis("Horizontal") >= 0.4 && Input.GetAxis("Vertical") <= -0.4 ||
          Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
            { //キー入力が「→↓」
              //Debug.Log(“右斜め下”);
                dontPlayKey = true; //操作不化可能判定フラグをtrueにしてキー操作を受け付けなくする
                targetPos.x+=moveSpeed; //目標座標Xに１
                targetPos.z-=moveSpeed; //目標座標Zに-１
                moveVec.x = 0.5f; //移動ベクトルXに0.5代入
                moveVec.z = -0.5f; //移動ベクトルZに-0.5代入

            }
            else if (Input.GetAxis("Horizontal") <= -0.4 && Input.GetAxis("Vertical") <= -0.4 ||
          Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
            { //キー入力が「←↓」
              //Debug.Log (“左斜め下”);
                dontPlayKey = true; //操作不化可能判定フラグをtrueにしてキー操作を受け付けなくする
                targetPos.x-=moveSpeed; //目標座標Xに-１
                targetPos.z-=moveSpeed; //目標座標Zに-１
                moveVec.x = -0.5f; //移動ベクトルXに-0.5代入
                moveVec.z = -0.5f; //移動ベクトルZに-0.5代入

            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////通常移動//////////////////////////////////////////////////////////
        if (dontPlayKey == false && slantingTrigger == false)
        {
            //dontPlayKeyフラグがfalseでslantingTriggerがfalseなら（操作不化可能判定フラグと斜め移動トリガーがfalseだったら）

            if (Input.GetAxis("Horizontal") >= 0.8 || Input.GetKey(KeyCode.RightArrow))
            { //キー入力が「→」
                if (transform.position.x == targetPos.x)
                { //今のX座標とターゲットX座標が同じなら
                    dontPlayKey = true; //操作不化可能判定フラグをtrueにしてキー操作を受け付けなくする
                    targetPos.x+=moveSpeed; //目標座標Xに１
                    moveVec.x = 1.0f; //移動ベクトルXに1代入
                }
            }
            else if (Input.GetAxis("Horizontal") <= -0.8 || Input.GetKey(KeyCode.LeftArrow))
            { //キー入力が「←」
                if (transform.position.x == targetPos.x)
                { //今のX座標とターゲットX座標が同じなら
                    dontPlayKey = true; //操作不化可能判定フラグをtrueにしてキー操作を受け付けなくする
                    targetPos.x-=moveSpeed; //目標座標Xに-１
                    moveVec.x = -1.0f; //移動ベクトルXに-1代入
                }
            }
            else if (Input.GetAxis("Vertical") >= 0.8 || Input.GetKey(KeyCode.UpArrow))
            { //キー入力が「↑」
                if (transform.position.z == targetPos.z)
                { //今のZ座標とターゲットZ座標が同じなら
                    dontPlayKey = true; //操作不化可能判定フラグをtrueにしてキー操作を受け付けなくする
                    targetPos.z+=moveSpeed; //目標座標Zに１
                    moveVec.z = 1.0f; //移動ベクトルXに1代入
                }
            }
            else if (Input.GetAxis("Vertical") <= -0.8 || Input.GetKey(KeyCode.DownArrow))
            { //キー入力が「↓」
                if (transform.position.z == targetPos.z)
                { //今のZ座標とターゲットZ座標が同じなら
                    dontPlayKey = true; //操作不化可能判定フラグをtrueにしてキー操作を受け付けなくする
                    targetPos.z-=moveSpeed; //目標座標Zに-１
                    moveVec.z = -1.0f; //移動ベクトルZに-1代入
                }
            }

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        if (transform.position.x != targetPos.x)
        { //今の座標がtargetPos.xと違うなら
            transform.Translate(moveVec * speed); //移動処理
        }
        else if (transform.position.x + transform.position.x + threshold >= targetPos.x ||
      transform.position.x + transform.position.x - threshold <= targetPos.x)
        { //数値狂い対策でしきい値を含めた（又は引いた）値以上（以下）なら

            keyTimer += Time.deltaTime; //キー入力不可タイマー発動
            moveVec.x = 0.0f; //移動を止める

            if (keyTimer >= KeytimerMax && moveVec == new Vector3(0.0f, 0.0f, 0.0f))
            { //キー入力不可タイマーの値がKeytimerMaxの値以上かつmoveVecがzeroなら
                dontPlayKey = false; //操作不化可能判定フラグをfalseにしてキー入力を可能にする
                keyTimer = 0.0f; //キー入力不可タイマーの値をリセットする
            }
        }

        if (transform.position.z != targetPos.z)
        { //今の座標がtargetPos.xと違うなら
            transform.Translate(moveVec * speed); //移動処理
        }
        else if (transform.position.x + transform.position.z + threshold >= targetPos.z ||
      transform.position.x + transform.position.z - threshold <= targetPos.z)
        { //数値狂い対策でしきい値を含めた（又は引いた）値以上（以下）

            keyTimer += Time.deltaTime; //キー入力不可タイマー発動
            moveVec.z = 0.0f; //移動を止める

            if (keyTimer >= KeytimerMax && moveVec == new Vector3(0.0f, 0.0f, 0.0f))
            { //キー入力不可タイマーの値がKeytimerMaxの値以上かつmoveVecがzeroなら
                dontPlayKey = false; //操作不化可能判定フラグをfalseにしてキー入力を可能にする
                keyTimer = 0.0f; //キー入力不可タイマーの値をリセットする
            }
        }
    }
}
