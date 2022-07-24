using UnityEngine;

public class KeyboardInput : IUserInput {

    [Header("=== key settings ===")]
    public string keyUp = "w";
    public string keyDown = "s";
    public string keyLeft = "a";
    public string keyRight = "d";
    public string keyDash = "left shift";
    public string keyJump = "space";
    public string keyAttack = "j";

    //public string keyJRight = "right";
    //public string keyJLeft = "left";
    //public string keyJDown = "down";
    //public string keyJUp = "up";

    [Header("===== Mouse Settings =====")]
    public bool mouseEnable = true;
    public float mouseSensitivityX = 1.0f;
    public float mouseSensitivityY = 1.0f;

    // Start is called before the first frame update
    private void Start() {
    }

    private void Update() {
        // ��ȡ��Ծ�źţ�����������
        jump = Input.GetKey(keyJump);  //! GetKey�Ƕ�Ӧʵ�ʼ�ֵ��GetButton�������ֵ
        jumpKeyDown = Input.GetKeyDown(keyJump);

        // ��ȡˮƽ�����ƶ��ź�
        if (Input.GetKey(keyRight)) {
            xDir = 1;
        } else if (Input.GetKey(keyLeft)) {
            xDir = -1;
        } else {
            xDir = 0;
        }

        // ����ź�
        if (Input.GetKeyDown(keyDash)) {
            dash = true;
        } else {
            dash = false;
        }

        
        attack = Input.GetKeyDown(keyAttack);
        

        // �ŵ��ź�
        isGrounded = c2d.IsTouchingLayers(ground);
        // �ұ��ź�
        isOnWall = c2d.IsTouchingLayers(wall);
    }
}
