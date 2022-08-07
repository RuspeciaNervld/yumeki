using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player : ICreature {
    private const string PLAYER_DATA_KEY = "PlayerData";
    private const string PLAYER_DATA_FILE_NAME = "PlayerData.ruspecia";
    [Header("=== Player Data ===")]
    public string playerName;
    public int playerCoin;
    public int playerPoint;
    public float power;
    public float tendency;
    public float playerSpeedMult; //������ֵ�ֵ,һ��Ϊ�ٷֱȳ��ֵ�ֵ
    public float nearMult;
    public float farMult;

    [Header("=== Saved Data ===")]
    public float _playerSpeedMult;//�洢��ֵ,����tendency��װ��Ӱ�죬������ҵȼ�������������Ӱ��
    public float _nearMult;
    public float _farMult;
    public float _accept;

    private float time;

    [Header("=== attack area ===")]
    public Transform attackCenter;
    public float attackRadius;
    public LayerMask weaponLayer;

    private Animator anim;
    private PlayerController moveController;

    [Header("=== objects ===")]
    public Rigidbody2D rb;
    public WeaponIn wi;

    #region ���ݴ�ȡ
    //! �����л����ڲ��࣬���ڴ洢����
    [System.Serializable]
    class PlayerData {
        public string playerName;
        //public int playerLevel;
        public int playerCoin;
        public int playerPoint;
        public Vector2 playerPosition;
        public float playerHp;
        public float playerAccept;
        public float playerAttack;
        public float _playerSpeedMult;
        public float _nearMult;
        public float _farMult;
        public float tendency;
        public float power;
    }

    PlayerData GetPlayerData() {
        var playerData = new PlayerData();

        playerData.playerName = playerName;
        //playerData.playerLevel = playerLevel;
        playerData.playerCoin = playerCoin;
        playerData.playerPoint = playerPoint;
        playerData.playerPosition = transform.position;
        playerData.playerHp = hp;
        playerData.playerAccept = accept;
        playerData.playerAttack = attack;
        playerData._playerSpeedMult = _playerSpeedMult;
        playerData._nearMult = _nearMult;
        playerData._farMult = _farMult;
        playerData.power = power;
        playerData.tendency = tendency;

        return playerData;
    }

    void SetPlayerData(PlayerData loadData) {
        playerName = loadData.playerName;
        //playerLevel = loadData.playerLevel;
        playerCoin = loadData.playerCoin;
        playerPoint = loadData.playerPoint;
        hp = loadData.playerHp;
        accept = loadData.playerAccept;
        attack = loadData.playerAttack;
        transform.position = loadData.playerPosition;
        _playerSpeedMult = loadData._playerSpeedMult;
        _nearMult = loadData._nearMult;
        _farMult = loadData._farMult;
        power = loadData.power;
        tendency = loadData.tendency;
    }
    public void SaveByPlayerPrefs() {
        SaveLoadManager.Instance.SaveByPlayerPrefs(PLAYER_DATA_KEY, GetPlayerData());
    }

    public void LoadFromPlayerPrefs() {
        var json = SaveLoadManager.Instance.LoadFromPlayerPrefs(PLAYER_DATA_KEY);
        var loadData = JsonUtility.FromJson<PlayerData>(json);

        SetPlayerData(loadData);
    }

    //#if UNITY_EDITOR
    //    [UnityEditor.MenuItem("Developer/Delete PlayerData Prefs")]
    //    public static void DeletePlayerPrefs() {
    //        //PlayerPrefs.DeleteAll();
    //        PlayerPrefs.DeleteKey(PLAYER_DATA_FILE_NAME);
    //    }
    //#endif

    public void SaveByJson() {
        SaveLoadManager.Instance.SaveByJson(PLAYER_DATA_KEY, GetPlayerData());
    }

    public void LoadFromJson() {
        var loadData = SaveLoadManager.Instance.LoadFromJson<PlayerData>(PLAYER_DATA_FILE_NAME);
        SetPlayerData(loadData);
    }

    //#if UNITY_EDITOR
    //    [UnityEditor.MenuItem("Developer/Delete PlayerData File")]
    //    public static void DeletePlayerDataFile() {
    //        SaveLoadManager.Instance.DeleteSaveFile(PLAYER_DATA_FILE_NAME);
    //    }
    //#endif

    #endregion


    private void Awake() {
        anim = GetComponent<Animator>();
        moveController = GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    private void Update() {
        Debugger.Instance.logs[0].text = "��ɫѪ����" + hp;
        Debugger.Instance.logs[3].text = "���˱��ʣ�" + nearMult;
        Debugger.Instance.logs[4].text = "Զ�˱��ʣ�" + farMult;
        Debugger.Instance.logs[5].text = "���ٱ��ʣ�" + playerSpeedMult;
        Debugger.Instance.logs[6].text = "���˱��ʣ�" + accept;



        if (hp <= 0) {
            OnDie();
        }
        
        if (canBeHurt && beHurtController.isRecovering()) {
            anim.enabled = false;
            moveController.enabled = false;
        } else {
            anim.enabled = true;
            moveController.enabled = true;
        }

        //todo ������ҵĶ�����ֵ
        time += Time.deltaTime;
        int actualTendency = (int)Mathf.Floor(tendency);
        if (actualTendency > 0) {
            //todo �������
            farMult = _farMult + actualTendency * 0.04f;
            nearMult = _nearMult - actualTendency * 0.02f;
            accept = _accept + actualTendency * 0.03f;
            if (time >= 5f) {
                time = 0;
                hp += actualTendency * 0.04f;
                if (hp >= 100) {
                    hp = 100;
                }
            }
            playerSpeedMult = _playerSpeedMult;
        } else if(actualTendency <= 0) {
            actualTendency = -actualTendency;

            //todo ��������
            nearMult = _nearMult + actualTendency * 0.05f;
            farMult = _farMult - actualTendency * 0.02f;
            accept = _accept + actualTendency * 0.02f;
            playerSpeedMult = _playerSpeedMult + actualTendency * 0.01f;

            actualTendency = -actualTendency;
        }

    }

    //private void onSkillHurt() {
    //    //todo �ҵ��ܵ��˺������������Լ��Ĺ����������Է������˺����������ɶԷ�����ʵ���˺�
    //}

    //private void onSkillEnd() {
    //    //todo ��������,�ָ�״̬��Ҳ���Ե��õ��˵����˽����������������˻��ߴ���Ч����
    //}



    //public void onSkillAttack() {
    //    Debug.Log("���ܷ���");
    //    skillAttack = 2;
    //    skillHurtTime = 1.0f;
    //    skillEndTime = 1.4f;
    //    skillAttackRadius = 1.0f;

    //    if (this.attackController.doAttack(skillHurtTime, skillEndTime, onSkillHurt, onSkillEnd)) {
    //        //todo ���Ź�������
    //    }
    //}

   new public void  NormalAttack() {
        if (this.attackController.doAttack(weapon.normalAttackHurtTime, weapon.normalAttackEndTime, weapon.NormalAttackHurt, weapon.NormalAttackEnd)) {
            //todo ���Ź���������Ч
            weapon.NormalAttackAnim();
            AudioManager.Instance.playSoundEffect($"knife{RusRandomer.randNum(1, 3)}.wav");

            //if (Physics2D.OverlapCircle(attackCenter.position, attackRadius, weaponLayer)) {
            //    canBeHurt = false;
            //    Debug.Log("bh" + canBeHurt);
            //    EventManager.Instance.DoDelayAction(() => {
            //        canBeHurt = true;
            //        Debug.Log("bh" + canBeHurt);
            //    }, 1f);
            //}
        }
    }

    public override void beHurtAction(float hurt) {
        sr.DOComplete();
        sr.DOColor(new Color(255, 0, 0), 0.3f).OnComplete(() => {
            sr.DOColor(new Color(255, 255, 255), 0f);
        });
        rb.velocity = new Vector2(-4*transform.localScale.x, 12);
    }

    private void OnDie() {
        TestMgr.Instance.reLoadGame();
    }

    //private void OnDrawGizmos() {
    //    Gizmos.DrawWireSphere(attackCenter.position, attackRadius);
    //}

    //! Ϊ�˿�����������������⣬���︲д
    private void OnTriggerEnter2D(Collider2D collision) {
        if(canBeHurt && collision.CompareTag("Enemy")) {
            beHurtController.beHurt(10);
        }
        //Debug.Log(collision.name);
        //if (canBeHurt && collision.CompareTag("Weapon")) {
        //    Debug.Log("in");
        //    beHurtController.beHurt(collision.gameObject.GetComponent<IWeapon>().computedAttack);
        //}
    }
}
