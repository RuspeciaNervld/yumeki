using UnityEngine;

public class KeyboardInput : IUserInput {

    [Header("=== key settings ===")]
    public string keyUp = "w";
    public string keyDown = "s";
    public string keyLeft = "a";
    public string keyRight = "d";
    public string keyDash2 = "left shift";
    public string keyDash = "l";
    public string keyJump = "k";
    public string keyJump2 = "space";

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

    public override void Update() {
        base.Update();
        // ��ȡ��Ծ�źţ�����������
        jump = Input.GetKey(keyJump) || Input.GetKey(keyJump2);  //! GetKey�Ƕ�Ӧʵ�ʼ�ֵ��GetButton�������ֵ
        jumpKeyDown = Input.GetKeyDown(keyJump) || Input.GetKeyDown(keyJump2);

        // ��ȡˮƽ�����ƶ��ź�
        if (Input.GetKey(keyRight)) {
            xDir = 1;
        } else if (Input.GetKey(keyLeft)) {
            xDir = -1;
        } else {
            xDir = 0;
        }

        // ����ź�
        if (Input.GetKeyDown(keyDash) || Input.GetKeyDown(keyDash2)) {
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
