using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private RectTransform _rectTransform;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    public Sprite idlingCharacter;
    public Sprite dashCharacter;
    public Sprite flyingCharacter;
    private bool isGround = false;
    private const float maxSpeed = 30;
    private const float gravity = 50;
    private float swim = 50;
    private float slip = 50;
    private float run = 50;
    private float fly = 50;

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
}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && playerState == PlayerState.GROUND)
        {
            _spriteRenderer.sprite = dashCharacter;
        }
        if (Input.GetKeyDown(KeyCode.A) && playerState == PlayerState.GROUND)
        {
            _spriteRenderer.sprite = dashCharacter;
        }

        if (Input.GetKey(KeyCode.D))
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
        if (Input.GetKeyDown(KeyCode.W) && (playerState == PlayerState.GROUND || playerState == PlayerState.ICE))
        {
            Vector2 currentVelocity = _rb.velocity;
            currentVelocity.y = 15;
            _rb.velocity = currentVelocity;
        }

        //空中ジャンプ
        if (Input.GetKeyDown(KeyCode.W) && playerState == PlayerState.AIR && fly > 0)
        {
            if (_spriteRenderer.sprite != flyingCharacter)
            {
                _spriteRenderer.sprite = flyingCharacter;
            }
            Vector2 currentVelocity = _rb.velocity;
            currentVelocity.y = fly/5;
            _rb.velocity = currentVelocity;
        }

        //水中縦移動
        if (Input.GetKeyDown(KeyCode.W) && playerState == PlayerState.WATER)
        {
            Vector2 currentVelocity = _rb.velocity;
            currentVelocity.y = swim/5;
            _rb.velocity = currentVelocity;
        }
    }

    private void FixedUpdate()
    {
        //移動(最大速度未満ならその速度に、最大速度を超えているなら徐々に減速)
        if (Input.GetKey(KeyCode.D))
        {
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
        if (Input.GetKey(KeyCode.A))
        {
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
        //摩擦減速
        if ((!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A)))
        {
            Vector2 temp = _rb.velocity;
            temp.x *= 0.9f;
            _rb.velocity = temp;
        }
        //重力
        if (playerState != PlayerState.WATER)
        {
            _rb.AddForce(new(0,-gravity));
        }
        else
        {
            _rb.AddForce(new(0, -gravity/4));
        }
    }

    //移動方法の管理
    private void OnCollisionStay2D(Collision2D collision)
    {
        //滑る
        if (collision.gameObject.CompareTag("Ice") && playerState != PlayerState.ICE && playerState != PlayerState.WATER)
        {
            playerState = PlayerState.ICE;
            _spriteRenderer.sprite = flyingCharacter;
        }
        //走る
        if (collision.gameObject.CompareTag("Ground") && playerState != PlayerState.WATER && isGround)
        {
            playerState = PlayerState.GROUND;
            if (_spriteRenderer.sprite == flyingCharacter)
            {
                _spriteRenderer.sprite = dashCharacter;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //飛ぶ
        if ((collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Ice")) && (playerState == PlayerState.GROUND || playerState == PlayerState.ICE))
        {
            playerState = PlayerState.AIR;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //地上判定の補佐
        if (other.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        //泳ぐ
        if (other.gameObject.CompareTag("Water") && !isGround && playerState != PlayerState.WATER)
        {
            playerState = PlayerState.WATER;
            _spriteRenderer.sprite = flyingCharacter;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //水中を出る
        if (other.gameObject.CompareTag("Water"))
        {
            playerState = PlayerState.AIR;
            _spriteRenderer.sprite = flyingCharacter;
        }
        //地上判定の補佐
        if (other.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
}
