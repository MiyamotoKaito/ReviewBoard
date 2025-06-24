using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float AttackTimer;
    [SerializeField] float AttackCooldown;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private Text nameText;
    [SerializeField] private Text dialogText;
    [SerializeField] private Text NextTalk;
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private string characterName;

    private bool inDialogue = false;
    public Slider healthSlider;
    void Start()
    {
        healthSlider.value = 300f;
        dialogPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!inDialogue && healthSlider.value <= 150f)
        {
            inDialogue = true;
            StartCoroutine(HandleDialogue());
        }
    }

    IEnumerator HandleDialogue()
    {
        Time.timeScale = 0f;
        dialogPanel.SetActive(true);
        nameText.gameObject.SetActive(true);
        dialogText.gameObject.SetActive(true);
        NextTalk.gameObject.SetActive(true);
        nameText.text = "���l";

        for (int i = 0; i < dialogueLines.Length; i++)
        {
            dialogText.text = dialogueLines[i];

            // ���͂�����܂őҋ@�i�X�y�[�X�����N���b�N�j
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            // ���͂��������܂őҋ@�i�������ςȂ��h�~�j
            yield return new WaitUntil(() => !Input.GetKeyDown(KeyCode.Space));
        }

        Endialogue();

    }

    private void Endialogue()
    {
        dialogPanel.SetActive(false);
        nameText.gameObject.SetActive(false);
        dialogText.gameObject.SetActive(false);
        NextTalk.gameObject.SetActive(false);
        Time.timeScale = 1f;

        //�o�g���ĊJ
        FindAnyObjectByType<Boss2>().StartBattle();

        healthSlider.value = 200f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Punch"))
        {
            healthSlider.value -= 10f;

        }
    }
}
