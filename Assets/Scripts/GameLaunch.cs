using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! ������������ʱ��������Ҫ�࿪�����������ȷֳ�����������������ϸ�Ϊ�ȸ���
/// <summary>
/// ������Ϸ����ڣ�Ҳ����Զ�������ٵĶ���
/// </summary>
public class GameLaunch : ISingleton<GameLaunch>
{
    public override void Awake() {
        base.Awake();

        //todo ��ʼ����Ϸ���
        this.gameObject.AddComponent<ResourceManager>();
        this.gameObject.AddComponent<EventManager>();
        this.gameObject.AddComponent<UIManager>();
        this.gameObject.AddComponent<AudioManager>();
        this.gameObject.AddComponent<InputListener>();
        this.gameObject.AddComponent<SaveLoadManager>();
        //end

        //todo  ������Ϸ���߼����
        this.gameObject.AddComponent<GameApp>();
        //end
    }

    private void Start() {
        GameApp.Instance.EnterGame();
    }
}
