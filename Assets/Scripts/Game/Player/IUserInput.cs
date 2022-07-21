using UnityEngine;

public abstract class IUserInput : MonoBehaviour {

    [Header("=== objects ===")]
    public LayerMask ground; //�����
    public LayerMask wall;

    public Collider2D c2d; // �������ײ��

    [Header("=== output signals ===")]
    public int xDir; // ˮƽ�����ƶ���Ϊ1��0��-1
    public bool jump;
    public bool jumpKeyDown;
    public bool isGrounded;
    public bool isOnWall;
    public bool dash;
    public bool dashTrigger; // ����ר���ź�
}
