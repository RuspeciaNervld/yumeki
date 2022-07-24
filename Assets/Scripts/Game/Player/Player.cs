using UnityEngine;

public class Player : ICreature {
    private const string PLAYER_DATA_KEY = "PlayerData";
    private const string PLAYER_DATA_FILE_NAME = "PlayerData.ruspecia";
    [Header("=== Player Data ===")]
    public string playerName;
    public int playerCoin;
    public int playerPoint;
    public float playerSpeed;
    public float nearMult;
    public float farMult;
    public float power;


    private Animator anim;
    private SpriteRenderer sr;
    private PlayerController moveController;


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
        public float playerSpeed;
        public float nearMult;
        public float farMult;
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
        playerData.playerSpeed = playerSpeed;
        playerData.nearMult = nearMult;
        playerData.farMult = farMult;
        playerData.power = power;

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
        playerSpeed = loadData.playerSpeed;
        nearMult = loadData.nearMult;
        farMult = loadData.farMult;
        power = loadData.power;
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
        if(canBeHurt && beHurtController.isRecovering()) {
            anim.enabled = false;
            moveController.enabled = false;
        } else {
            anim.enabled = true;
            moveController.enabled = true;
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

    public void NormalAttack() {
        Debug.Log("ƽA");
        if (this.attackController.doAttack(weapon.normalAttackHurtTime, weapon.normalAttackEndTime, weapon.NormalAttackHurt, weapon.NormalAttackEnd)) {
            //todo ���Ź�������
            weapon.NormalAttackAnim();
        }
        
    }

    public override void beHurtAction() {
        base.beHurtAction();
    }

    //! ����һ����ʵ������˷�ʽ����һ���ɶԷ�ֱ�ӵ��ú���
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Weapon")) {
            beHurtController.beHurt(collision.gameObject.GetComponent<IWeapon>().computedAttack);
        }
    }


}
