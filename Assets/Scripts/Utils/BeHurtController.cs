using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeHurtController : MonoBehaviour
{
    private float hurtColdTime;
    private float hurtColdTimeCounter;
    private float hurtRecoverTime;
    private float hurtRecoverTimeCounter;

    /// <summary>
    /// ��ʼ�����˿�����
    /// </summary>
    /// <param name="hurtColdTime">���˺���޵�ʱ��</param>
    /// <param name="hurtRecoverTime">���˺��Ӳֱʱ��</param>
    internal void init(float hurtColdTime,float hurtRecoverTime) {
        this.hurtColdTime = hurtColdTime;
        this.hurtRecoverTime = hurtRecoverTime;
        this.hurtColdTimeCounter = 0;
        this.hurtRecoverTimeCounter = 0;
    }

    /// <summary>
    /// ���˺���
    /// </summary>
    /// <param name="computedAttack">ͨ��������������ġ������˺���</param>
    /// <param name="beHurtAction">���˻ص�����</param>
    /// <returns>�ܵ�ʵ���˺�����true������Ϊfalse</returns>
    public bool beHurt(float computedAttack,UnityAction beHurtAction) {
        if (hurtColdTimeCounter>0) {
            return false;
        }
        hurtColdTimeCounter = hurtColdTime; // �ܵ��˺���������ȴ����
        hurtRecoverTimeCounter = hurtRecoverTime; // �ܵ��˺�����ʼӲֱ
        return true;
    }

    public void beHurtOver() {

    }

    public bool isRecovering() {
        return hurtRecoverTimeCounter > 0;
    }

    private void onDeath() {

    }

    private void Update() {
        hurtColdTimeCounter -= Time.deltaTime;
        hurtRecoverTimeCounter -= Time.deltaTime;
    }
}
