using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! ʹ��һ���ű������������м�������object��������ң��Զ��廯����
public class TestMgr : MonoBehaviour
{
    public static TestMgr Instance = null; //! ֻ��һ��������һֱ����,��ʹ��С�ġ�����ģʽ��
    public GameObject player = null;
    public List<GameObject> ememies = new List<GameObject>();

    private void Awake() {
        TestMgr.Instance = this;
    }

    // �����Ҷ����ʱ�����Ƿ�GameMgr�������ṩ����;
    public List<GameObject> findCharactorInRaidus(Vector3 center, float radius) {
        // �ĳ����ĳ�������
        // end

        // �Ź���ĳ�������
        // end

        return this.ememies;
    }
    public void InitGame() {
        //todo ���س����ڶ����ű�����ʼ��
        gameObject.AddComponent<DialogueManager>().init();
        //end

        //todo �����ǵ�NPC;
        // end

        //todo �����ǵ�Player��ɫ
        GameObject charactorPrefab = ResourceManager.Instance.GetAssetCache<GameObject>("Charactors/Player.prefab");
        this.player = GameObject.Instantiate(charactorPrefab);
        //this.player.AddComponent<CharactorCtrl>().init(); //! ����������ҽű�����ʼ�������Դ��ݲ����������ض�����ֵ
        this.player.name = "Player";
        //this.player.AddComponent<PlayerOpt>().init();
        this.player.AddComponent<Player>().init(100, 10, 5,true,0,0);
        
        // end

        //todo �����ǵĵ���
        //GameObject e = GameObject.Instantiate(charactorPrefab);
        //e.name = "enemy";
        //Vector3 pos = e.transform.position;
        //pos += new Vector3(2, 0, 2);
        //e.transform.position = pos;
        //e.AddComponent<CharactorCtrl>().init();
        //this.ememies.Add(e);
        // end
    }
}
