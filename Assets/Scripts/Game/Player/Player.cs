using UnityEngine;

public class Player : ICreature {
    private Animator anim;
    private PlayerController moveController;

    private float skillAttack;
    private float skillHurtTime;
    private float skillEndTime;
    private float skillAttackRadius;

    private void Awake() {
        anim = GetComponent<Animator>();
        moveController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private void Update() {
        if(canBeHurt && beHurtController.isRecovering()) {
            anim.enabled = false;
            moveController.enabled = false;
        } else {
            anim.enabled = true;
            moveController.enabled = true;
        }
    }

    private void onSkillHurt() {
        //todo �ҵ��ܵ��˺������������Լ��Ĺ����������Է������˺����������ɶԷ�����ʵ���˺�
    }

    private void onSkillEnd() {
        //todo ��������,�ָ�״̬��Ҳ���Ե��õ��˵����˽����������������˻��ߴ���Ч����
    }

    private void onPlayerHurt() {
        Debug.Log("�ܵ��˺�");

    }

    public void onPlayerSkill() {
        Debug.Log("���ܷ���");
        skillAttack = 2;
        skillHurtTime = 1.0f;
        skillEndTime = 1.4f;
        skillAttackRadius = 1.0f;

        if (this.attackController.doAttack(skillHurtTime, skillEndTime, onSkillHurt, onSkillEnd)) {
            //todo ���Ź�������
        }
    }
}
