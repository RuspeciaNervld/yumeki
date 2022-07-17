using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator),typeof(Rigidbody2D))] //ָ����Ҫ�Ĺ������
public class PlayerController : MonoBehaviour
{
    [Header("=== objects ===")]
    private Rigidbody2D rb;
    private Animator anim;
    private IUserInput input;

    [Header("=== value settings ===")]
    public float speed;
    public float jumpForce;
    public float jumpTime;
    public float dashCD;
    public float dashSpeed;
    public float dashTime;

    [Header("=== ability settings ===")]
    public bool canDoubleJump;
    public bool canDash;

    private float jumpTimeCounter;
    private float dashTimeCounter;
    private float dashColdTime;
    private bool isJumping;
    private bool doubleJumped;
    private bool doubleJump;
    private bool holdingJump;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        input = GetComponent<IUserInput>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
            if(jumpTimeCounter > 0) {
                Debug.Log("3");
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            } else {
                isJumping = false;
            }
        }

        //! ������ģ��
        if (canDoubleJump) {
            if (!input.isGrounded && !input.jump && !doubleJumped) { // �����ɿ���Ծ
                Debug.Log("1");
                doubleJump = true;
                holdingJump = false;
            }
            if (input.jump && !input.isGrounded && doubleJump) { // ���е�һ���ٴΰ�����Ծ
                Debug.Log("2");
                rb.velocity = Vector2.up * jumpForce;
                doubleJump = false;
                doubleJumped = true;
            }
        }

        //! dashģ��
        if (canDash) {
            dashColdTime -= Time.deltaTime;
            dashTimeCounter -= Time.deltaTime;
            if (input.dash) {
                input.dash = false;
                if (dashColdTime <= 0) { // ��ȴ���������Գ��
                    input.dashTrigger = true; // �������ź�
                    dashTimeCounter = dashTime;
                    dashColdTime = dashCD;  // ��ȴ����
                }
            }
        }
        
    }

    void FixedUpdate() { // ������صĸ��·�������
        rb.velocity = new Vector2(input.xDir * (dashTimeCounter < 0?speed:dashSpeed), rb.velocity.y);
    }
}
