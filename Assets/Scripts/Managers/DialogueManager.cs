using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    public static DialogueManager Instance = null;
    public TextAsset csv = null;
    public GameObject dialogueBox;
    public Text dialogueText, nameText;
    public Image speakerImg;
    public struct CsvLine {
        public int blockId;
        public char sign;
        public int id;
        public int to;
        public string speaker;
        public string img;
        public string content;
        public string dub;
        public CsvLine(int blockId, char sign, int id, int to, string speaker, string img, string content, string dub) {
            this.blockId = blockId;
            this.sign = sign;
            this.id = id;
            this.to = to;
            this.speaker = speaker;
            this.img = img;
            this.content = content;
            this.dub = dub;
        }
    }
    [TextArea(1, 3)]
    [SerializeField] private List<string> dialogueLines = null ;
    [SerializeField] private int currentLine;
    [SerializeField] private float textInterval;
    [SerializeField] private bool isScrolling;

    [SerializeField] private List<CsvLine> csvLines = new List<CsvLine>();
    [SerializeField] private int currentIdx;
    [SerializeField] private string currentContent;

    private Coroutine currentTask;

    private void Awake() {
        DialogueManager.Instance = this;
    }

    private void Start() {
        //todo ������
        //ShowDialogue(dialogueLines);
        //LoadCsvFile("SL/TestDialogues.csv");
        //ShowDialogueBlock(0);
    }
    private void Update() {
        if (dialogueBox.activeInHierarchy) {
            //if ((Input.GetMouseButtonUp(0) || Input.GetKeyDown(KeyCode.Space)) && dialogueText.text == dialogueLines[currentLine]) {
            //    if (isScrolling == false) {
            //        currentLine++;
            //        if (currentLine < dialogueLines.Count) {
            //            CheckName();
            //            CheckImg();
            //            StartCoroutine(ScrollingText()); //! ��ʼ��ʾ����
            //        } else {
            //            dialogueBox.SetActive(false);
            //        }
            //    } else {
            //        StopCoroutine(ScrollingText());
            //        dialogueText.text = dialogueLines[currentLine];
            //        isScrolling = false;
            //    }
            //}
            if ((Input.GetMouseButtonUp(0) || Input.GetKeyDown(KeyCode.Space)) /*&& currentContent == csvLines[preIdx].content*/) {
                if (isScrolling == false) {
                    upDateNextLine();
                    currentTask = StartCoroutine(ScrollingText2());
                    if (currentIdx==0) { //! ����Ի��������
                        dialogueBox.SetActive(false);
                    }
                } else {
                    Debug.Log("ǿ�����");
                    StopCoroutine(currentTask);
                    isScrolling = false;
                    dialogueText.text = currentContent;
                }
            }
        }
    }

    /// <summary>
    /// ������
    /// �ṩ�������õģ������ʽ�����ַ����Ϳ�����ɶԻ�����չʾ
    /// n-��ʾ˵�������������ӱ�ʾ˵��������
    /// </summary>
    public void ShowDialogue(List<string> newLines) {
        dialogueLines = newLines;
        currentLine = 0;

        CheckName();
        CheckImg();

        //dialogueText.text = dialogueLines[currentLine];//line by line
        StartCoroutine(ScrollingText());

        dialogueBox.SetActive(true);
    }

    public void ShowDialogueBlock(int index) {
        int i = 0;
        while (csvLines[i].blockId != index) {
            i++;
        }
        currentIdx = i;
        dialogueBox.SetActive(true);
    }
    private void upDateNextLine() {
        switch (csvLines[currentIdx].sign) {
            case '@':
                currentContent = csvLines[currentIdx].content;
                nameText.text = csvLines[currentIdx].speaker;
                if (csvLines[currentIdx].img == "") {
                    speakerImg.sprite = null;
                    speakerImg.color = new Color(255, 255, 255, 75);
                } else {
                    speakerImg.sprite = ResourceManager.Instance.GetAssetCache<Sprite>("Arts/" + csvLines[currentIdx].img);
                    speakerImg.color = new Color(255, 255, 255, 255);
                }
                if (csvLines[currentIdx].dub != "") {
                    AudioClip clip = ResourceManager.Instance.GetAssetCache<AudioClip>("Media/Dub/" + csvLines[currentIdx].dub);
                    AudioManager.Instance.playDub(clip);
                }
                currentIdx = csvLines[currentIdx].to; //! ������һ��
                break;
            case '#':
                while (csvLines[currentIdx].sign == '#') {
                    //todo ���س�һЩѡ��͹���
                    currentIdx++;
                }
                //todo ֮��Ӧ����ѡ���������һ�仰��ָ�������ֱ�Ӹ���
                //todo ��������ѡ�����һ��ѡ��
                currentIdx = csvLines[currentIdx].to;
                break;
        }
    }

    /// <summary>
    /// ������������ֱ������һ��Ի�
    /// </summary>
    /// <param name="speaker">˵��������</param>
    /// <param name="content">˵��������</param>
    /// <param name="imgPath">����·����Ϊ""���գ�ʱ����ʾ����</param>
    public void ShowOneLine(string speaker,string content,string imgPath, string dubPath) {
        StartCoroutine(ScrollingText2());
        nameText.text = speaker;
        if (imgPath == "") {
            speakerImg.sprite = null;
            speakerImg.color = new Color(255, 255, 255, 75);
        } else {
            speakerImg.sprite = ResourceManager.Instance.GetAssetCache<Sprite>("Arts/" + imgPath);
            speakerImg.color = new Color(255, 255, 255, 255);
        }
        if (csvLines[currentIdx].dub != "") {
            AudioClip clip = ResourceManager.Instance.GetAssetCache<AudioClip>("Media/Dub/" + dubPath);
            AudioManager.Instance.playDub(clip);
        }
    }

    private void CheckName() {
        if (dialogueLines[currentLine].StartsWith("n-")) {
            nameText.text = dialogueLines[currentLine].Replace("n-", "");
            currentLine++;
        }
    }
    private void CheckImg() {
        if (dialogueLines[currentLine].StartsWith("i-")) {
            string path = dialogueLines[currentLine].Replace("i-", "");
            if(path == "") {
                speakerImg.sprite = null;
                speakerImg.color = new Color(255, 255, 255, 75);
            } else {
                speakerImg.sprite = ResourceManager.Instance.GetAssetCache<Sprite>("Arts/"+path);
                speakerImg.color = new Color(255, 255, 255, 255);
            }
        }
    }
    private IEnumerator ScrollingText() {
        isScrolling = true;
        dialogueText.text = "";

        foreach (char letter in dialogueLines[currentLine].ToCharArray()) {
            dialogueText.text += letter;//letter by letter
            yield return new WaitForSeconds(textInterval);
        }
        isScrolling = false;
    }

    private IEnumerator ScrollingText2() {
        isScrolling = true;
        dialogueText.text = "";

        foreach (char letter in currentContent.ToCharArray()) {
            dialogueText.text += letter;//letter by letter
            yield return new WaitForSeconds(textInterval);
        }
        isScrolling = false;
    }

    public void LoadCsvFile(string path) {
        csv = ResourceManager.Instance.GetAssetCache<TextAsset>(path);
        string[] all = csv.text.Split('\n');
        
        for (int i =1;i<all.Length-1;i++) {
            string[] cell = all[i].Split(',');
            CsvLine csv = new CsvLine(int.Parse(cell[0]), cell[1][0], int.Parse(cell[2]), int.Parse(cell[3]), cell[4], cell[5], cell[6], cell[7]);
            csvLines.Add(csv);
        }
    }

    /// <param name="path">�Ի�csv�ļ���·�����磺"SL/TestDialogues.csv"</param>
    public void init(string path) {
        dialogueBox =GameObject.Find("Canvas/TestUI/DialoguePanel");
        dialogueText = dialogueBox.transform.GetChild(0).GetComponent<Text>();
        nameText = dialogueBox.transform.GetChild(1).GetComponent<Text>();
        speakerImg = dialogueBox.transform.GetChild(2).GetComponent<Image>();

        LoadCsvFile(path);
        //nameText = GameObject.Find("TestDialogue/DialoguePanel/Text_speaker").GetComponent<Text>();
    }

}
