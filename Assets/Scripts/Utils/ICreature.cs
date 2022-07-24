using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICreature : MonoBehaviour
{
    [Header("=== ability settings ===")]
    public bool canAttack;
    public bool canBeHurt;

    [Header("=== figure settings ===")]
    public float hp;
    public float accept;
    public float attack;

    [Header("=== controllers ===")]
    public AttackController attackController = null;//! �п��ܲ��ᱻ��ֵ��Ҫ������Ϊnull
    public BeHurtController beHurtController = null;
    public IWeapon weapon = null;

    public virtual void beHurtAction() {
        Debug.Log("���˶���");
        //! ��춯��
    }
    public virtual bool NormalAttack() {
        if (this.attackController.doAttack(weapon.normalAttackHurtTime, weapon.normalAttackEndTime, weapon.NormalAttackHurt, weapon.NormalAttackEnd)) {
            //todo ���Ź�������
            weapon.NormalAttackAnim();
            return true;
        } else {
            return false;
        }
    }

    public virtual bool Skill() {
        if (this.attackController.doAttack(weapon.skillHurtTime, weapon.skillEndTime, weapon.SkillAttackHurt, weapon.SkillAttackEnd)) {
            //todo ���Ź�������
            Debug.Log("skill");
            weapon.SkillAttackAnim();
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// ���Դ����ɹ��������ɹ������ɱ����������ɱ����������Ĭ�����˺��޵��Ҳ�Ӳֱ
    /// </summary>
    /// <param name="hp">Ѫ������</param>
    /// <param name="attack">��������</param>
    /// <param name="define">��������</param>
    /// <param name="canAttack">�ܹ���</param>
    /// <param name="canBeHurt">������</param>
    public virtual void init(/*float hp,float attack,float define,*/bool canAttack,bool canBeHurt) {
        //this.hp = hp;
        //this.attack = attack;
        //this.define = define;
        this.canAttack = canAttack;
        this.canBeHurt = canBeHurt;
        if (canAttack) {
            attackController = gameObject.AddComponent<AttackController>();
            attackController.init();
            weapon = GetComponentInChildren<IWeapon>();
        }
        if (canBeHurt) {
            beHurtController = gameObject.AddComponent<BeHurtController>();
            beHurtController.init(0, 0);
        }
    }
    
    /// <summary>
    /// ֻ�ܴ����ɱ�����������
    /// </summary>
    /// <param name="hp">Ѫ������</param>
    /// <param name="attack">��������</param>
    /// <param name="define">��������</param>
    /// <param name="canAttack">�ܹ���</param>
    /// <param name="hurtColdTime">���˺���޵�ʱ��</param>
    /// <param name="hurtRecoverTime">���˺��Ӳֱʱ��</param>
    public virtual void init(/*float hp, float attack, float define,*/ bool canAttack, float hurtColdTime,float hurtRecoverTime) {
        //this.hp = hp;
        //this.attack = attack;
        //this.define = define;
        this.canAttack = canAttack;
        this.canBeHurt = true;
        if (canAttack) {
            attackController = gameObject.AddComponent<AttackController>();
            attackController.init();
            weapon = GetComponentInChildren<IWeapon>();
        }
        if (canBeHurt) {
            beHurtController = gameObject.AddComponent<BeHurtController>();
            beHurtController.init(hurtColdTime, hurtRecoverTime);
        }
    }

    private void Start() {
        init(canAttack, canBeHurt);
    }
}
