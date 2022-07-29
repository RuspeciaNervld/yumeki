using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    [Header("=== can be hurt ===")]
    public float hurtColdTime;
    public float hurtRecoverTime;

    [Header("=== objects ===")]
    public SpriteRenderer sr;

    public virtual void beHurtAction() {
        //! ��춯��
        sr.DOComplete();
        transform.DOComplete();
        SpecialEManager.Instance.DoShake(0.05f, 0.15f);
        SpecialEManager.Instance.DoBulletTime(0.05f, 0.25f);
        sr.DOColor(new Color(1, 0, 0), 0.3f).OnComplete(() => {
            transform.DOShakePosition(0.2f, 0.2f, 1);
            sr.DOColor(new Color(1, 1, 1), 0f);
        });
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
            beHurtController.init(0, 0, this);
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

        beHurtController = gameObject.AddComponent<BeHurtController>();
        beHurtController.init(hurtColdTime, hurtRecoverTime,this);
        
    }

    private void Start() {
        init(canAttack, hurtColdTime,hurtRecoverTime);
        sr = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    //! ����һ����ʵ������˷�ʽ����һ���ɶԷ�ֱ�ӵ��ú���
    private void OnTriggerEnter2D(Collider2D collision) {
        if (canBeHurt && collision.CompareTag("Weapon")) {
            if (collision.gameObject.GetComponent<IWeapon>() != null) {
                beHurtController.beHurt(collision.gameObject.GetComponent<IWeapon>().computedAttack);
            }else{
                beHurtController.beHurt(collision.gameObject.GetComponent<IBullet>().attack);
            }
        }
    }
}
