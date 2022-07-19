using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICreature : MonoBehaviour
{
    [Header("=== ability settings ===")]
    public bool canAttack;
    public bool canBeHurt;

    [Header("=== figure settings ===")]
    [SerializeField] protected float hp;
    [SerializeField] protected float define;
    [SerializeField] protected float attack;

    [Header("=== controllers ===")]
    protected AttackController attackController = null;//! �п��ܲ��ᱻ��ֵ��Ҫ������Ϊnull
    protected BeHurtController beHurtController = null;

    /// <summary>
    /// ���Դ����ɹ��������ɹ������ɱ����������ɱ����������Ĭ�����˺��޵��Ҳ�Ӳֱ
    /// </summary>
    /// <param name="hp">Ѫ������</param>
    /// <param name="attack">��������</param>
    /// <param name="define">��������</param>
    /// <param name="canAttack">�ܹ���</param>
    /// <param name="canBeHurt">������</param>
    public virtual void init(float hp,float attack,float define,bool canAttack,bool canBeHurt) {
        this.hp = hp;
        this.attack = attack;
        this.define = define;
        this.canAttack = canAttack;
        this.canBeHurt = canBeHurt;
        if (canAttack) {
            attackController = gameObject.AddComponent<AttackController>();
            attackController.init();
        }
        if (canBeHurt) {
            beHurtController = gameObject.AddComponent<BeHurtController>();
            beHurtController.init(0,0);
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
    public virtual void init(float hp, float attack, float define, bool canAttack, float hurtColdTime,float hurtRecoverTime) {
        this.hp = hp;
        this.attack = attack;
        this.define = define;
        this.canAttack = canAttack;
        this.canBeHurt = true;
        if (canAttack) {
            attackController = gameObject.AddComponent<AttackController>();
            attackController.init();
        }
        if (canBeHurt) {
            beHurtController = gameObject.AddComponent<BeHurtController>();
            beHurtController.init(hurtColdTime, hurtRecoverTime);
        }
    }
}
