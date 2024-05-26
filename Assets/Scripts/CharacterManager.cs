using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private RectTransform _rectTransform;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    public Sprite idlingCharacter;
    public Sprite dashCharacter;
    public Sprite flyingCharacter;
    private const float maxSpeed = 15;

    //プレイヤーがどの移動方法をとる状態なのかを管理
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
        playerState = PlayerState.GROUND;
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
            //進行方向に合わせた画像の向きの反転
            if (_rectTransform.localScale.x > 0)
            {
                Vector2 temp = _rectTransform.localScale;
                temp.x *= -1;
                _rectTransform.localScale = temp;
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //進行方向に合わせた画像の向きの反転
            if (_rectTransform.localScale.x < 0)
            {
                Vector2 temp = _rectTransform.localScale;
                temp.x *= -1;
                _rectTransform.localScale = temp;
            }
        }
        //デフォルト姿勢
        else if (Mathf.Abs(_rb.velocity.x) < 1e-1 && playerState == PlayerState.GROUND)
        {
            _spriteRenderer.sprite = idlingCharacter;
        }

        //ジャンプ(飛べるキャラのみ)
        if (Input.GetKeyDown(KeyCode.W))
        {
            Vector3 currentVelocity = _rb.velocity;
            currentVelocity.y = 15;
            _rb.velocity = currentVelocity;
        }
    }

    private void FixedUpdate()
    {
        //移動
        if (Input.GetKey(KeyCode.D))
        {
            if (_rb.velocity.x < maxSpeed)
            {
                Vector2 temp = _rb.velocity;
                temp.x += 5;
                _rb.velocity = temp;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (_rb.velocity.x > -maxSpeed)
            {
                Vector2 temp = _rb.velocity;
                temp.x -= 5;
                _rb.velocity = temp;
            }
        }
        //減速
        if ((!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A)))
        {
            Vector2 temp = _rb.velocity;
            temp.x *= 0.9f;
            _rb.velocity = temp;
        }
    }

    //プレイヤーの状態を管理
    private void OnCollisionStay2D(Collision2D collision)
    {
        //氷に触れていたら滑る
        if (collision.gameObject.CompareTag("Ice") && playerState != PlayerState.ICE && playerState != PlayerState.WATER)
        {
            playerState = PlayerState.ICE;
            _spriteRenderer.sprite = flyingCharacter;
        }
        //地面に触れていたら走る
        if (collision.gameObject.CompareTag("Ground") && playerState != PlayerState.WATER && Mathf.Abs(_rb.velocity.y) < 1e-2)
        {
            playerState = PlayerState.GROUND;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //地面から離れたら飛ぶ
        if (collision.gameObject.CompareTag("Ground") && playerState == PlayerState.GROUND)
        {
            playerState = PlayerState.AIR;
            _spriteRenderer.sprite = flyingCharacter;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //水に入ったら泳ぐ
        if (other.gameObject.CompareTag("Water"))
        {
            playerState = PlayerState.WATER;
            _spriteRenderer.sprite = flyingCharacter;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //水から出たら飛ぶ
        if (other.gameObject.CompareTag("Water"))
        {
            playerState = PlayerState.AIR;
            _spriteRenderer.sprite = flyingCharacter;
        }
    }
}
