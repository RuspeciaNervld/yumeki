using DG.Tweening;
using UnityEngine;

public class w_stick : IWeapon {
    public TrailRenderer tr;

    public override void NormalAttackAnim() {
        tr.enabled = true;
        c2d.enabled = true;
        transform.DOComplete();
        transform.DOScale(new Vector3(1.5002F, 0.09955693F, 1), 0f);
        transform.DORotate(new Vector3(0, 0, user.transform.localScale.x * 75), 0.1f, RotateMode.Fast).SetEase(Ease.InExpo).OnComplete(() => {
            transform.DORotate(new Vector3(0, 0, 0), 0.05f, RotateMode.Fast).SetEase(Ease.InExpo).OnComplete(() => {
                transform.DOScale(new Vector3(0, 0, 0), 0f);
                tr.enabled = false;
                c2d.enabled = false;
            });
        });
    }

    public override void NormalAttackEnd() {
        this.computedAttack = 0;

    }

    public override void NormalAttackHurt() {
        float computedAttack;
        if (user as Player) {
            computedAttack = user.attack * ((Player)user).nearMult + normalAttackPlus;
        } else {
            computedAttack = user.attack  + normalAttackPlus;
        }

        this.computedAttack = computedAttack;
    }

    public override void SkillAttackAnim() {
        
    }

    public override void SkillAttackEnd() {
    }

    public override void SkillAttackHurt() {
        float computedAttack;
        if (user as Player) {//! �������ǿת��ֻ����Ϊ����������
            computedAttack = user.attack * ((Player)user).farMult * normalAttackMult;
        } else {
            computedAttack = user.attack  * normalAttackMult;
        }
        

        //todo һ�������ץ��һ����ȫ��ŷ��һ��(����beHurt)
        

        //todo ��һ�֣��ñ����Լ�ͨ����ײ������Ƿ��յ�������ֻ�ʵ���ֵ
        this.computedAttack = computedAttack;

        GameObject fireBall = ResourceManager.Instance.GetAssetCache<GameObject>("Weapons/FireBall.prefab");

        fireBall.transform.position = user.transform.position;
        IBullet bullet = fireBall.GetComponent<IBullet>();
        
        //int rand = RusRandomer.randNum(3, 7);
        for (int i = 0; i < 2; i++) {
            EventManager.Instance.DoDelayAction(() => {
                bullet.Init(computedAttack, user.transform.position);
                GameObject.Instantiate(fireBall);
                AudioManager.Instance.playSoundEffect("missile.wav");
            }, i/10f);
        }
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
