using DG.Tweening;
using UnityEngine;

public class w_stick : IWeapon {
    public TrailRenderer tr;

    public override void NormalAttackAnim() {
        tr.enabled = true;
        c2d.enabled = true;
        transform.DOScale(new Vector3(1.5002F, 0.09955693F, 1), 0f);
        Debug.Log("���Ź�������");
        transform.DORotate(new Vector3(0, 0, user.transform.localScale.x * 75), 0.2f, RotateMode.Fast).SetEase(Ease.InExpo);

    }

    public override void NormalAttackEnd() {

        transform.DORotate(new Vector3(0, 0, 0), 0.1f, RotateMode.Fast).SetEase(Ease.InExpo).OnComplete(() => {
            transform.DOScale(new Vector3(0, 0, 0), 0f);
            tr.enabled = false;
            c2d.enabled = false;
        });
    }

    public override void NormalAttackHurt() {
        float computedAttack = user.attack + normalAttackPlus;

        this.computedAttack = computedAttack;
    }

    public override void SkillAttackAnim() {
        throw new System.NotImplementedException();
    }

    public override void SkillAttackEnd() {
        throw new System.NotImplementedException();
    }

    public override void SkillAttackHurt() {
        //! �������ǿת��ֻ����Ϊ����������
        float computedAttack = user.attack*((Player)user).nearMult * normalAttackMult;

        //todo һ�������ץ��һ����ȫ��ŷ��һ��(����beHurt)
        

        //todo ��һ�֣��ñ����Լ�ͨ����ײ������Ƿ��յ�������ֻ�ʵ���ֵ
        this.computedAttack = computedAttack;
    }

    public override void SwordAgainstAnim() {
        throw new System.NotImplementedException();
    }

    public override void SwordAgainstEnd() {
        throw new System.NotImplementedException();
    }

    public override void SwordAgainstHurt() {
        throw new System.NotImplementedException();
    }
}
