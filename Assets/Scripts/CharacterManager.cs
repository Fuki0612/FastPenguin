using System;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private RectTransform _rectTransform;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    public Sprite idlingCharacter;
    public Sprite dashCharacter;

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
        //キャラクターの姿勢の変更
        if (Input.GetKeyDown(KeyCode.D))
        {
            _spriteRenderer.sprite = dashCharacter;
        }
        else if (Input.GetKeyDown(KeyCode.A))
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
        else
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

    //何に接しているかをこんな感じで取得できる、はず(まだ試してない)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerState = PlayerState.GROUND;
        }
        else if (collision.gameObject.CompareTag("Ice"))
        {
            playerState = PlayerState.ICE;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            playerState = PlayerState.WATER;
        }
    }
}
