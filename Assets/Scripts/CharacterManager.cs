using System;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private RectTransform _rectTransform;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    public Sprite idlingCharacter;
    public Sprite dashCharacter;

    //�v���C���[���ǂ̈ړ����@���Ƃ��ԂȂ̂����Ǘ�
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
        //�L�����N�^�[�̎p���̕ύX
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
            //�i�s�����ɍ��킹���摜�̌����̔��]
            if (_rectTransform.localScale.x > 0)
            {
                Vector2 temp = _rectTransform.localScale;
                temp.x *= -1;
                _rectTransform.localScale = temp;
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //�i�s�����ɍ��킹���摜�̌����̔��]
            if (_rectTransform.localScale.x < 0)
            {
                Vector2 temp = _rectTransform.localScale;
                temp.x *= -1;
                _rectTransform.localScale = temp;
            }
        }
        //�f�t�H���g�p��
        else
        {
            _spriteRenderer.sprite = idlingCharacter;
        }

        //�W�����v(��ׂ�L�����̂�)
        if (Input.GetKeyDown(KeyCode.W))
        {
            Vector3 currentVelocity = _rb.velocity;
            currentVelocity.y = 15;
            _rb.velocity = currentVelocity;
        }
    }

    //���ɐڂ��Ă��邩������Ȋ����Ŏ擾�ł���A�͂�(�܂������ĂȂ�)
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
