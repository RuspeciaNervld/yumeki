using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackController : MonoBehaviour
{
    private float hurtTime;
    private float endTime;
    private float currentTime;

    private UnityAction startAction;
    private UnityAction hurtAction;
    private UnityAction endAction;
    private bool isAttacking = false;

    //! �������ﵮ������ʼ��
    public void init() {
        this.hurtTime = 0;
        this.endTime = 0;
        this.currentTime = 0;

        this.startAction = null;
        this.hurtAction = null;
        this.endAction = null;

        this.isAttacking = false;
    }

    /// <summary>
    /// �Զ��弼�ܲ��������˺�ʱ�䡢����ʱ�䣻�˺�����������������
    /// ��ʼ��ͨ���ж�doAttack�ķ���ֵ������
    /// </summary>
    /// <param name="hurtTime">�˺�����ʱ��</param>
    /// <param name="endTime">��������ʱ�䣨�����Ϻ�ҡ��</param>
    /// <param name="hurtAction">�˺���������->�����磩������ֵ����˺���Ӧ</param>
    /// <param name="endAction">������������->�����磩�ָ�����״̬�����ж���</param>
    /// <returns>�����ͷųɹ���true������false</returns>
    public bool doAttack(float hurtTime,float endTime,UnityAction hurtAction,UnityAction endAction) {
        if (this.isAttacking) {
            return false;
        }
        this.isAttacking = true;
        this.hurtTime = hurtTime;
        this.endTime = endTime;
        this.currentTime = 0;
        this.hurtAction = hurtAction;
        this.endAction= endAction;
        return true;
    }

    // Update is called once per frame
    private void Update() {
        if (!this.isAttacking) {
            return;
        }

        if (startAction != null) {
            this.startAction();
            this.startAction = null;
        } ;

        currentTime += Time.deltaTime;

        if (currentTime >= this.hurtTime) {
            if (this.hurtAction!=null) {
                this.hurtAction();
                this.hurtAction = null;
            }
        }
        if (currentTime >= this.endTime) {
            if (this.endAction != null) {
                this.endAction();
                this.endAction = null;
            }
            this.isAttacking = false;
        }
    }
}
