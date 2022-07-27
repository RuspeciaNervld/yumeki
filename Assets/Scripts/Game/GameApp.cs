using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameApp : ISingleton<GameApp>
{
    public void EnterGame() {
        this.EnterFightingScene();
    }

    public void EnterFightingScene() {
        // �ͷ����ǵ�UI
        UIManager.Instance.ShowUIView("TestUI");//! ����Ԥ��������
        // end



        // ��ȡ����,����ʼ��,�����ű�����,���崴���ͻ�ȡ
        GameObject mapPrefab = ResourceManager.Instance.GetAssetCache<GameObject>("Maps/Test.prefab");
        GameObject map = GameObject.Instantiate(mapPrefab);
        map.AddComponent<TestMgr>().InitGame();
        // end

    }

    public void EnterMainMenu() {

    }
}
