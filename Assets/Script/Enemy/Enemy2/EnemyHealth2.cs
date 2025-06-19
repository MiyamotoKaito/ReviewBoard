using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

public class EnemyHealth2 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float AttackTimer = 1.0f;
    [SerializeField] float AttackCooldown = 2f;
    [SerializeField] GameObject RightShuriken;
    [SerializeField] GameObject LeftShuriken;
    [SerializeField] GameObject Rightdanger;
    [SerializeField] GameObject Leftdanger;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private Text nameText;
    [SerializeField] private Text dialogText;
    [SerializeField] private Text NextTalk;
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private string characterName;

    private int currentLines = 0;
    private bool inDialogue = false;

    public Slider healthSlider;
    void Start()
    {
        healthSlider.value = 200f;
        dialogPanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        var worldPosR = new Vector3(10.0f, 3.7f, 0f);
        var worldPosL = new Vector3(-10.0f, 3.7f, 0f);
        Quaternion rotL = Quaternion.Euler(0, 0, -45);  
        Quaternion rotR = Quaternion.Euler(0, 0, -135);
        if (!inDialogue && healthSlider.value <= 100f)
        {
            inDialogue = true;
            Instantiate(Rightdanger, worldPosR, rotR);
            Instantiate(Leftdanger, worldPosL, rotL);
            StartCoroutine(HandleDialogue());
        }
    }

    IEnumerator HandleDialogue()
    {
        Time.timeScale = 0f;
        dialogPanel.SetActive(true);
        nameText.gameObject.SetActive(true);
        dialogText.gameObject.SetActive(true);
        nameText.text = "忍者";

        for (int i = 1; i < dialogueLines.Length; i++)
        {
            dialogText.text = dialogueLines[i];

            // 入力があるまで待機（スペースか左クリック）
            yield return new WaitUntil(() =>Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0));

            // 入力が離されるまで待機（押しっぱなし防止）
            yield return new WaitUntil(() => !Input.GetKeyDown(KeyCode.Space)||!Input.GetMouseButtonDown(0));
        }

        Endialogue();

    }

    private void Endialogue()
    {
        dialogPanel.SetActive(false);
        nameText.gameObject.SetActive(false);
        dialogText.gameObject.SetActive(false);
        nameText.text = characterName;
        NextTalk.gameObject.SetActive(false);
        Time.timeScale = 1f;

        //バトル再開
        FindAnyObjectByType <Enemy2>().StartBattle();

        Instantiate(LeftShuriken, new Vector3(-18f, 16f, 0f), Quaternion.identity);
        Instantiate(RightShuriken, new Vector3(18f, 16f, 0f), Quaternion.identity);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Punch"))
        {
            healthSlider.value -= 10f;
        }
    }
}
