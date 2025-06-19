using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private Text nameText;
    [SerializeField] private Text dialogText;
    [SerializeField] private Text NextTalk;
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private string characterName;

    private int currentLines = 0;
    private bool inDialogue = true;
    void Start()
    {
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (inDialogue && (Input.GetKeyDown(KeyCode.Space)))
        {
            DisplayNextLine();
        }
    }

    /// <summary>
    /// 会話を始めるメソッド
    /// </summary>
    public void StartDialogue()
    {
        dialogPanel.SetActive(true);
        Time.timeScale = 0f;
        nameText.text = characterName;
        NextTalk.text = NextTalk.text;
        currentLines = 0;
        dialogText.text = dialogueLines[currentLines];
        inDialogue = true;
        Cursor.visible = false;
    }
    /// <summary>
    /// 次の会話に切り替えるメソッド
    /// </summary>
    public void DisplayNextLine()
    {
        currentLines++;
        if (currentLines >= dialogueLines.Length)
        {
            EndDialogue();
        }

        else
        {
            dialogText.text = dialogueLines[currentLines];
        }
    }
    /// <summary>
    /// 会話を終了させるメソッド
    /// </summary>
    public void EndDialogue()
    {
        dialogPanel.SetActive(false);
        nameText.gameObject.SetActive(false);
        dialogText.gameObject.SetActive(false);
        NextTalk.gameObject.SetActive(false);
        Time.timeScale = 1f;
        inDialogue = false;
        Cursor.visible = false;

        FindObjectOfType<Enemy>().StartBattle();
    }
}
