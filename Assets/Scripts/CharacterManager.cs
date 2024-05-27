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
        else if (Mathf.Abs(_rb.velocity.x) < 1e-1 && playerState == PlayerState.GROUND)
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

    private void FixedUpdate()
    {
        //�ړ�
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
        //����
        if ((!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A)))
        {
            Vector2 temp = _rb.velocity;
            temp.x *= 0.9f;
            _rb.velocity = temp;
        }
    }

    //�v���C���[�̏�Ԃ��Ǘ�
    private void OnCollisionStay2D(Collision2D collision)
    {
        //�X�ɐG��Ă����犊��
        if (collision.gameObject.CompareTag("Ice") && playerState != PlayerState.ICE && playerState != PlayerState.WATER)
        {
            playerState = PlayerState.ICE;
            _spriteRenderer.sprite = flyingCharacter;
        }
        //�n�ʂɐG��Ă����瑖��
        if (collision.gameObject.CompareTag("Ground") && playerState != PlayerState.WATER && Mathf.Abs(_rb.velocity.y) < 1e-2)
        {
            playerState = PlayerState.GROUND;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //�n�ʂ��痣�ꂽ����
        if (collision.gameObject.CompareTag("Ground") && playerState == PlayerState.GROUND)
        {
            playerState = PlayerState.AIR;
            _spriteRenderer.sprite = flyingCharacter;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //���ɓ�������j��
        if (other.gameObject.CompareTag("Water"))
        {
            playerState = PlayerState.WATER;
            _spriteRenderer.sprite = flyingCharacter;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //������o������
        if (other.gameObject.CompareTag("Water"))
        {
            playerState = PlayerState.AIR;
            _spriteRenderer.sprite = flyingCharacter;
        }
    }
}
