using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 使用一个脚本，来管理场景中几乎所有object，方便查找，自定义化创建
public class TestMgr : MonoBehaviour
{
    public static TestMgr Instance = null; //! 只在一个场景中一直存在,故使用小的“单例模式”
    public GameObject player = null;
    public List<GameObject> enemies = new List<GameObject>();

    private void Awake() {
        TestMgr.Instance = this;
    }

    // 攻击找对象的时候，我们放GameMgr里面来提供策略;
    public List<GameObject> findCharactorInRaidus(Vector3 center, float radius) {
        // 四场树的场景管理
        // end

        // 九宫格的场景管理
        // end

        return this.enemies;
    }
    public void InitGame() {
        //todo 播放bgm
        AudioManager.Instance.playBgm("Angel fall down.wav");
        AudioManager.Instance.setBgmVolume(0.4f);
        
        //todo 加载场景内独立脚本并初始化
        //gameObject.AddComponent<DialogueManager>().init("Save&Load/TestDialogues.csv",0.1f);
        //end

        //todo 放我们的NPC;
        GameObject yumekuiPrefab = ResourceManager.Instance.GetAssetCache<GameObject>("Charactors/yumekui.prefab");
        
        enemies.Add(GameObject.Instantiate(yumekuiPrefab));
        
        // end

        //todo 放我们的Player角色
        GameObject charactorPrefab = ResourceManager.Instance.GetAssetCache<GameObject>("Charactors/Player_1.prefab");
        this.player = GameObject.Instantiate(charactorPrefab);
        //this.player.AddComponent<CharactorCtrl>().init(); //! 可以在这里挂脚本并初始化，可以传递参数，给出特定的数值
        this.player.name = "Player";
        //this.player.AddComponent<PlayerOpt>().init();
        //this.player.AddComponent<Player>().init(/*100, 10, 5,*/true,0,0);
        
        // end

        //todo 放我们的敌人
        //GameObject e = GameObject.Instantiate(charactorPrefab);
        //e.name = "enemy";
        //Vector3 pos = e.transform.position;
        //pos += new Vector3(2, 0, 2);
        //e.transform.position = pos;
        //e.AddComponent<CharactorCtrl>().init();
        //this.ememies.Add(e);
        // end
    }

    public void reLoadGame() {
        foreach(GameObject obj in enemies) {
            Destroy(obj);
        }
        Destroy(player);
        //todo 放我们的NPC;
        GameObject yumekuiPrefab = ResourceManager.Instance.GetAssetCache<GameObject>("Charactors/yumekui.prefab");

        enemies.Add(GameObject.Instantiate(yumekuiPrefab));

        // end

        //todo 放我们的Player角色
        GameObject charactorPrefab = ResourceManager.Instance.GetAssetCache<GameObject>("Charactors/Player_1.prefab");
        this.player = GameObject.Instantiate(charactorPrefab);
        //this.player.AddComponent<CharactorCtrl>().init(); //! 可以在这里挂脚本并初始化，可以传递参数，给出特定的数值
        this.player.name = "Player";
    }

    public void reLoadBoss() {
        GameObject yumekuiPrefab = ResourceManager.Instance.GetAssetCache<GameObject>("Charactors/yumekui.prefab");

        TestMgr.Instance.enemies.Add(GameObject.Instantiate(yumekuiPrefab));
    }
}
