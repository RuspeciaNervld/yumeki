using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))] //ָ����Ҫ�Ĺ������
public class PlayerController : MonoBehaviour {

    [Header("=== objects ===")]
    public LayerMask ground; //�����
    public LayerMask wall;
    public LayerMask rush;

    public GameObject dashShadow;
    public Collider2D c2d; // �������ײ��
    
    private Rigidbody2D rb;
    private IUserInput input;
    private Player player;

    [Header("=== value settings ===")]
    public float speed;
    public float jumpForce;
    public float jumpTime;
    public float dashCD;
    public float dashSpeed;
    public float dashTime;
    public float wallForce;
    public float targetG;


    [Header("=== ability settings ===")]
    public bool canDoubleJump;
    public bool canDash;
    public bool canStayWall;
    public bool canControl;

    private float jumpTimeCounter;
    private float dashTimeCounter;
    private float dashColdTime;

    private bool isJumping;
    private bool doubleJumped;
    private bool doubleJump;
    private bool holdingJump;

    private KeyboardInput keyInput;
    private JoystickInput joyInput;
    private float preG;

    private void Awake() {
        
    }

    // Start is called before the first frame update
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        keyInput = GetComponent<KeyboardInput>();
        joyInput = GetComponent<JoystickInput>();
        input = keyInput;
        preG = rb.gravityScale;
    }

    // Update is called once per frame
    private void Update() {
        //Debugger.Instance.log.text = "JoystickNames" + Input.GetJoystickNames()[0] +"len:"+ Input.GetJoystickNames()[0] .Length+ "   s    believe" + belive;


        //if (InputListener.Instance.keyHold ) {
        //    input = keyInput;
        //} else {
        //    input = joyInput;
        //}
        if (joyInput.joy) {
            input = joyInput;
        } else {
            input = keyInput;
        }

        if (!canControl) {
            input.enabled = false;
        } else {
            input.enabled = true;
        }


        if (input.isGrounded) {
            doubleJumped = false;
            holdingJump = true;
        } 

        //! ��������
        if (input.isGrounded && input.jump) { // �ڵ����ϰ�����Ծ
            rb.velocity = Vector2.up * jumpForce;
            isJumping = true;
            jumpTimeCounter = jumpTime;
        }
        if (input.jump && isJumping && holdingJump) { // ���������׶Σ�������ס��Ծ��
            if (jumpTimeCounter > 0) {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            } else {
                isJumping = false;
            }
        }
        if (input.attack) {
            player.NormalAttack();
        }
        if (input.skill) {
            player.Skill();
        }

        if (!input.isGrounded && !input.jump && !doubleJumped) { // �����ɿ���Ծ
            doubleJump = true;
            holdingJump = false;
        }
        //! ������ģ��
        if (canDoubleJump) {
            if (input.jump && !input.isGrounded && doubleJump) { // ���е�һ���ٴΰ�����Ծ
                rb.velocity = Vector2.up * jumpForce;
                doubleJump = false;
                doubleJumped = true;
            }
        }

        //! �ұ�ģ��
        if (canStayWall) {
            if (input.isOnWall && !input.isGrounded) {
                if (rb.gravityScale != targetG) {
                    rb.gravityScale = targetG;
                    rb.velocity = Vector2.zero;
                }
                if (input.jumpKeyDown) {
                    rb.velocity = new Vector2(0, jumpForce);
                }
            } else {
                rb.gravityScale = preG ;
            }
        }

        //! ����ģ��
        if (canDash) {
            dashColdTime -= Time.deltaTime;
            dashTimeCounter -= Time.deltaTime;
            if (input.dash) {
                if (dashColdTime <= 0) { // ��ȴ���������Գ��
                    dashShadow.transform.localScale = transform.localScale;
                    dashShadow.SetActive(true);
                    canControl = false;
                    player.canBeHurt = false; //! ��playercontroller�������Ƿ�ɱ��˺�
                    input.dash = false;
                    c2d.enabled = false;
                    input.dashTrigger = true; // �������ź�
                    dashTimeCounter = dashTime;
                    dashColdTime = dashCD;  // ��ȴ����
                }
            }
        }

        

    }

    private void FixedUpdate() { // ������صĸ��·�������
        if (dashTimeCounter < 0) {
            canControl = true;
            player.canBeHurt = true;
            c2d.enabled = true;
            dashShadow.SetActive(false);
        }
        rb.velocity = new Vector2( (dashTimeCounter < 0 ? input.xDir * speed : transform.localScale.x * dashSpeed), rb.velocity.y);
    }

    
}
