using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeHurtController : MonoBehaviour
{
    private ICreature user;

    private float hurtColdTime;
    private float hurtColdTimeCounter;
    private float hurtRecoverTime;
    private float hurtRecoverTimeCounter;

    /// <summary>
    /// ��ʼ�����˿�����
    /// </summary>
    /// <param name="hurtColdTime">���˺���޵�ʱ��</param>
    /// <param name="hurtRecoverTime">���˺��Ӳֱʱ��</param>
    internal void init(float hurtColdTime,float hurtRecoverTime,ICreature user) {
        this.hurtColdTime = hurtColdTime;
        this.hurtRecoverTime = hurtRecoverTime;
        this.hurtColdTimeCounter = 0;
        this.hurtRecoverTimeCounter = 0;
        this.user = user;
    }

    /// <summary>
    /// ���˺���
    /// </summary>
    /// <param name="computedAttack">ͨ��������������ġ������˺���</param>
    /// <param name="beHurtAction">���˻ص�����</param>
    /// <returns>�ܵ�ʵ���˺�����true������Ϊfalse</returns>
    public bool beHurt(float computedAttack) {
        if (hurtColdTimeCounter > 0) {
            return false;
        }
        if (computedAttack != 0 && user.canBeHurt) {
            user.hp -= computedAttack * user.accept;
            user.beHurtAction();
        }
        hurtColdTimeCounter = hurtColdTime; // �ܵ��˺���������ȴ����
        hurtRecoverTimeCounter = hurtRecoverTime; // �ܵ��˺�����ʼӲֱ
        return true;
    }


    public bool isRecovering() {
        return hurtRecoverTimeCounter > 0;
    }


    private void Update() {
        hurtColdTimeCounter -= Time.deltaTime;
        hurtRecoverTimeCounter -= Time.deltaTime;
    }

}
