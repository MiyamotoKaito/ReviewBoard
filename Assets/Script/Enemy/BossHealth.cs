using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField] float AttackTimer;
    [SerializeField] float AttackCooldown;
    //[SerializeField] GameObject RightShuriken;
    //[SerializeField] GameObject LeftShuriken;
    [SerializeField] GameObject Rightdanger;
    [SerializeField] GameObject Leftdanger;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private Text nameText;
    [SerializeField] private Text dialogText;
    [SerializeField] private Text NextTalk;
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private string characterName;

    private bool inDialogue = false;
    private bool hasSpawnedDangerObjects = false; // danger �I�u�W�F�N�g�𐶐��ς݂��̃t���O
    private bool waitingForTimeScaleOne = false;  // TimeScale �� 1 �ɂȂ�̂�҂��Ă��邩�̃t���O

    public Slider healthSlider;

    void Start()
    {
        healthSlider.value = 300f;
        dialogPanel.SetActive(false);
    }

    void Update()
    {
        // HP �� 100 �ȉ��ɂȂ����Ƃ��̏���
        if (!inDialogue && healthSlider.value <= 100f && !hasSpawnedDangerObjects)
        {
            inDialogue = true;
            hasSpawnedDangerObjects = true;

            // danger �I�u�W�F�N�g�𐶐�
            var worldPosR = new Vector3(10.0f, 3.7f, 0f);
            var worldPosL = new Vector3(-10.0f, 3.7f, 0f);
            Quaternion rotL = Quaternion.Euler(0, 0, -45);
            Quaternion rotR = Quaternion.Euler(0, 0, -135);

            Instantiate(Rightdanger, worldPosR, rotR);
            Instantiate(Leftdanger, worldPosL, rotL);

            StartCoroutine(HandleDialogue());
        }

        // TimeScale �� 0 ���� 1 �ɖ߂������̏���
        if (waitingForTimeScaleOne && Time.timeScale == 1f)
        {
            waitingForTimeScaleOne = false;
            SpawnShurikens();
        }
    }

    IEnumerator HandleDialogue()
    {
        Time.timeScale = 0f;
        dialogPanel.SetActive(true);
        nameText.gameObject.SetActive(true);
        dialogText.gameObject.SetActive(true);
        NextTalk.text = NextTalk.text;
        healthSlider.gameObject.SetActive(false);
        nameText.text = "�E��";

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
        healthSlider.gameObject.SetActive(true);

        // TimeScale �� 1 �ɖ߂��O�Ƀt���O�𗧂Ă�
        waitingForTimeScaleOne = true;
        Time.timeScale = 1f;

        // �o�g���ĊJ
        FindAnyObjectByType<Enemy2>().StartBattle();
        var sr = GetComponent<SpriteRenderer>();
        sr.DOFade(0f, 1.0f).SetLoops(-1, LoopType.Yoyo); // �E�҂̓_��
        var greenImage = healthSlider.fillRect.GetComponent<Image>();
        greenImage.DOFade(0.5f, 0.5f).SetLoops(-1, LoopType.Yoyo); // HP�o�[�̓_�Łi�΁j
    }

    private void SpawnShurikens()
    {
        // �N�i�C�����iTimeScale �� 1 �ɂȂ������Ɏ��s�j
        //Instantiate(LeftShuriken, new Vector3(-18f, 16f, 0f), Quaternion.identity);
       //Instantiate(RightShuriken, new Vector3(18f, 16f, 0f), Quaternion.identity);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Punch"))
        {
            healthSlider.value -= 10f;
        }
    }
}