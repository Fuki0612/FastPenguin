using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public RectTransform _rectTransform;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    public Sprite idlingCharacter;
    public Sprite dashCharacter;
    public Sprite flyingCharacter;
    private bool isGround = false;
    private bool isWater = false;
    private const float maxSpeed = 30;
    private const float gravity = 50;
    private float swim = 50;
    private float slip = 50;
    private float run = 50;
    private float fly = 50;
    private int jampCount = 0;
    private float groundTime = 0;
    private MainSceneManager mainSceneManager;
    public Text jampCountText;
    public Text timeText;
    public static float timeScore = 0;

    //プレイヤーの状態管理
    enum PlayerState
    {
        GROUND = 1,
        ICE = 2,
        WATER = 3,
        AIR = 4
    }
    private PlayerState playerState;

    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        playerState = PlayerState.GROUND;swim = GameManager.statusSwim;
        slip = GameManager.statusSlip;
        run = GameManager.statusRun;
        fly = GameManager.statusFly;
        GameObject mManager = GameObject.Find("MainSceneManager");
        mainSceneManager = mManager.GetComponent<MainSceneManager>();
        timeScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A) && Mathf.Abs(_rb.velocity.x) < 1e-1 && playerState == PlayerState.GROUND && _spriteRenderer.sprite != idlingCharacter)
        {
            _rb.velocity = Vector2.zero;
            _spriteRenderer.sprite = idlingCharacter;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //画像反転
            if (_rectTransform.localScale.x > 0)
            {
                Vector2 temp = _rectTransform.localScale;
                temp.x *= -1;
                _rectTransform.localScale = temp;
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //画像反転
            if (_rectTransform.localScale.x < 0)
            {
                Vector2 temp = _rectTransform.localScale;
                temp.x *= -1;
                _rectTransform.localScale = temp;
            }
        }
        //静止状態
        else if (Mathf.Abs(_rb.velocity.x) < 1e-1 && playerState == PlayerState.GROUND && _spriteRenderer.sprite != idlingCharacter)
        {
            _rb.velocity = Vector2.zero;
            _spriteRenderer.sprite = idlingCharacter;
        }

        //地上ジャンプ
        if (Input.GetKeyDown(KeyCode.W) && isGround)
        {
            Vector2 currentVelocity = _rb.velocity;
            currentVelocity.y = 15;
            _rb.velocity = currentVelocity;
            isGround = false;
        }

        //空中ジャンプ(5+fly/10回までしか連続で飛べない)
        else if (Input.GetKeyDown(KeyCode.W) && !isGround && !isWater && fly > 0 && jampCount < 5 + (int)fly/10)
        {
            playerState = PlayerState.AIR;
            if (_spriteRenderer.sprite != flyingCharacter)
            {
                _spriteRenderer.sprite = flyingCharacter;
            }
            Vector2 currentVelocity = _rb.velocity;
            currentVelocity.y = fly/5 + 10;
            _rb.velocity = currentVelocity;
            jampCount++;
        }

        //ジャンプ回数の回復
        if (playerState != PlayerState.AIR && jampCount > 0)
        {
            groundTime += Time.deltaTime;
            if (groundTime > 150f / (fly+50))
            {
                groundTime = 0;
                jampCount--;
            }
        }

        //空中に出た判定
        if (Mathf.Abs(_rb.velocity.y) > 1 && playerState != PlayerState.AIR)
        {
            isGround = false;
        }

        //水中縦移動
        if (Input.GetKeyDown(KeyCode.W) && playerState == PlayerState.WATER && isWater)
        {
            Vector2 currentVelocity = _rb.velocity;
            currentVelocity.y = 10 + swim/10;
            _rb.velocity = currentVelocity;
        }
        if (Input.GetKeyDown(KeyCode.S) && playerState == PlayerState.WATER)
        {
            _rb.AddForce(new(0, -5*swim));
        }
        //ゴール
        if (_rectTransform.anchoredPosition.x < -89000)
        {
            StartCoroutine(mainSceneManager.Goal());
        }

        //ジャンプ回数およびタイムの表示
        if (!CameraMover.goal)
        {
            if (fly == 0)
            {
                jampCountText.text = "ジャンプ回数　0";
            }
            else
            {
                jampCountText.text = "ジャンプ回数　" + jampCount.ToString() + "/" + (5 + (int)fly / 10).ToString();
            }
            timeScore += Time.deltaTime;
            timeText.text = String.Format("Time: {0:#.##}s", timeScore);
        }
        else
        {
            jampCountText.text = "";
        }
    }

    private void FixedUpdate()
    {
        //摩擦減速
        if ((!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A)))
        {
            Vector2 temp = _rb.velocity;
            temp.x *= 0.9f;
            _rb.velocity = temp;
        }
        //移動(最大速度未満ならその速度に、最大速度を超えているなら徐々に減速)
        else if (Input.GetKey(KeyCode.D))
        {
            //ダッシュ画像に切り替え
            if (playerState == PlayerState.GROUND && _spriteRenderer.sprite != dashCharacter)
            {
                _spriteRenderer.sprite = dashCharacter;
            }
            Vector2 temp = _rb.velocity;
            switch (playerState)
            {
                case PlayerState.GROUND:
                    if (temp.x <= maxSpeed * (run / 100))
                    {
                        temp.x = maxSpeed * (run / 100);
                    }
                    else
                    {
                        temp.x *= 0.9f;
                    }
                    break;
                case PlayerState.ICE:
                    if (temp.x <= maxSpeed * (slip / 100))
                    {
                        temp.x = maxSpeed * (slip / 100);
                    }
                    else
                    {
                        temp.x *= 0.9f;
                    }
                    break;
                case PlayerState.WATER:
                    if (temp.x <= maxSpeed * (swim / 100))
                    {
                        temp.x = maxSpeed * (swim / 100);
                    }
                    else
                    {
                        temp.x *= 0.9f;
                    }
                    break;
                case PlayerState.AIR:
                    if (temp.x <= Mathf.Max(maxSpeed * (fly / 100), 5))
                    {
                        temp.x = Mathf.Max(maxSpeed * (fly / 100), 5);
                    }
                    else
                    {
                        temp.x *= 0.9f;
                    }
                    break;
            }
            _rb.velocity = temp;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //ダッシュ画像に切り替え
            if (playerState == PlayerState.GROUND && _spriteRenderer.sprite != dashCharacter)
            {
                _spriteRenderer.sprite = dashCharacter;
            }
            Vector2 temp = _rb.velocity;
            switch (playerState)
            {
                case PlayerState.GROUND:
                    if (temp.x >= -maxSpeed * (run / 100))
                    {
                        temp.x = -maxSpeed * (run / 100);
                    }
                    else
                    {
                        temp.x *= 0.9f;
                    }
                    break;
                case PlayerState.ICE:
                    if (temp.x >= -maxSpeed * (slip / 100))
                    {
                        temp.x = -maxSpeed * (slip / 100);
                    }
                    else
                    {
                        temp.x *= 0.9f;
                    }
                    break;
                case PlayerState.WATER:
                    if (temp.x >= -maxSpeed * (swim / 100))
                    {
                        temp.x = -maxSpeed * (swim / 100);
                    }
                    else
                    {
                        temp.x *= 0.9f;
                    }
                    break;
                case PlayerState.AIR:
                    if (temp.x >= Mathf.Min(-maxSpeed * (fly / 100), -5))
                    {
                        temp.x = Mathf.Min(-maxSpeed * (fly / 100), -5);
                    }
                    else
                    {
                        temp.x *= 0.9f;
                    }
                    break;
            }
            _rb.velocity = temp;
        }

        //重力
        if (!isWater)
        {
            _rb.AddForce(new(0,-gravity));
        }
        else
        {
            _rb.AddForce(new(0, -gravity/4));
        }
    }

    //移動方法の管理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //走る
        if (collision.gameObject.CompareTag("Ground") && !isWater && isGround)
        {
            playerState = PlayerState.GROUND;
            _spriteRenderer.sprite = dashCharacter;
        }
        //滑る
        else if (collision.gameObject.CompareTag("Ice") && !isWater && isGround)
        {
            playerState = PlayerState.ICE;
            _spriteRenderer.sprite = flyingCharacter;
        }
    }

    //着地判定が上手くいかなかった時用
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && (playerState == PlayerState.AIR || playerState == PlayerState.WATER) && isGround)
        {
            playerState = PlayerState.GROUND;
            _spriteRenderer.sprite = dashCharacter;
        }
        else if (collision.gameObject.CompareTag("Ice") && (playerState == PlayerState.AIR || playerState == PlayerState.WATER) && isGround)
        {
            playerState = PlayerState.ICE;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //泳ぐ
        if (other.gameObject.CompareTag("Water") && !isGround)
        {
            playerState = PlayerState.WATER;
            _spriteRenderer.sprite = flyingCharacter;
            isWater = true;
        }
        //地上判定の補佐
        if ((other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Ice")) && !isWater)
        {
            isGround = true;
        }
        //地上で地面や氷に横から当たった場合の修正(継ぎ目でカクつくかと思ったが大丈夫そう？)
        if (other.gameObject.CompareTag("Ground") && playerState == PlayerState.ICE)
        {
            playerState = PlayerState.GROUND;
            _spriteRenderer.sprite = dashCharacter;
        }
        else if (other.gameObject.CompareTag("Ice") && playerState == PlayerState.GROUND)
        {
            playerState = PlayerState.ICE;
            _spriteRenderer.sprite = flyingCharacter;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //水から出ても移動速度は一旦変わらないようにするため状態はWATERのまま
        if (other.gameObject.CompareTag("Water"))
        {
            isWater = false;
        }
    }
}
