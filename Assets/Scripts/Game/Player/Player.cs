using UnityEngine;

public class Player : ICreature {
    private const string PLAYER_DATA_KEY = "PlayerData";
    private const string PLAYER_DATA_FILE_NAME = "PlayerData.ruspecia";
    [Header("=== Player Data ===")]
    public string playerName;
    public int playerLevel;
    public int playerCoin;
    public int playerHp;
    public int playerDefine;
    public int playerAttack;


    private Animator anim;
    private PlayerController moveController;

    private float skillAttack;
    private float skillHurtTime;
    private float skillEndTime;
    private float skillAttackRadius;


    #region ���ݴ�ȡ
    //! �����л����ڲ��࣬���ڴ洢����
    [System.Serializable] class PlayerData {
        public string playerName;
        public int playerLevel;
        public int playerCoin;
        public Vector2 playerPosition;
        public int playerHp;
        public int playerDefine;
        public int playerAttack;

        
    }

    PlayerData GetPlayerData() {
        var playerData = new PlayerData();

        playerData.playerName = playerName;
        playerData.playerLevel = playerLevel;
        playerData.playerCoin = playerCoin;
        playerData.playerPosition = transform.position;
        playerData.playerHp = playerHp;
        playerData.playerDefine = playerDefine;
        playerData.playerAttack = playerAttack;

        return playerData;
    }

    void SetPlayerData(PlayerData loadData) {
        playerName = loadData.playerName;
        playerLevel = loadData.playerLevel;
        playerCoin = loadData.playerCoin;
        playerHp = loadData.playerHp;
        playerDefine = loadData.playerDefine;
        playerAttack = loadData.playerAttack;
        transform.position = loadData.playerPosition;
    }
    public void SaveByPlayerPrefs() {
        SaveLoadManager.Instance.SaveByPlayerPrefs(PLAYER_DATA_KEY, GetPlayerData());
    }

    public void LoadFromPlayerPrefs() {
        var json = SaveLoadManager.Instance.LoadFromPlayerPrefs(PLAYER_DATA_KEY);
        var loadData = JsonUtility.FromJson<PlayerData>(json);

        SetPlayerData(loadData);
    }

    [UnityEditor.MenuItem("Developer/Delete PlayerData Prefs")]
    public static void DeletePlayerPrefs() {
        //PlayerPrefs.DeleteAll();
        PlayerPrefs.DeleteKey(PLAYER_DATA_FILE_NAME);
    }

    public void SaveByJson() {
        SaveLoadManager.Instance.SaveByJson(PLAYER_DATA_KEY, GetPlayerData());
    }

    public void LoadFromJson() {
        var loadData = SaveLoadManager.Instance.LoadFromJson<PlayerData>(PLAYER_DATA_FILE_NAME);
        SetPlayerData(loadData);
    }

    [UnityEditor.MenuItem("Developer/Delete PlayerData File")]
    public static void DeletePlayerDataFile() {
        SaveLoadManager.Instance.DeleteSaveFile(PLAYER_DATA_FILE_NAME);
    }

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

    private void onSkillHurt() {
        //todo �ҵ��ܵ��˺������������Լ��Ĺ����������Է������˺����������ɶԷ�����ʵ���˺�
    }

    private void onSkillEnd() {
        //todo ��������,�ָ�״̬��Ҳ���Ե��õ��˵����˽����������������˻��ߴ���Ч����
    }

    private void onPlayerHurt() {
        Debug.Log("�ܵ��˺�");

    }

    public void onPlayerSkill() {
        Debug.Log("���ܷ���");
        skillAttack = 2;
        skillHurtTime = 1.0f;
        skillEndTime = 1.4f;
        skillAttackRadius = 1.0f;

        if (this.attackController.doAttack(skillHurtTime, skillEndTime, onSkillHurt, onSkillEnd)) {
            //todo ���Ź�������
        }
    }
}
