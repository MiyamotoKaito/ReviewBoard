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
    private bool hasSpawnedDangerObjects = false; // danger オブジェクトを生成済みかのフラグ
    private bool waitingForTimeScaleOne = false;  // TimeScale が 1 になるのを待っているかのフラグ

    public Slider healthSlider;

    void Start()
    {
        healthSlider.value = 300f;
        dialogPanel.SetActive(false);
    }

    void Update()
    {
        // HP が 100 以下になったときの処理
        if (!inDialogue && healthSlider.value <= 100f && !hasSpawnedDangerObjects)
        {
            inDialogue = true;
            hasSpawnedDangerObjects = true;

            // danger オブジェクトを生成
            var worldPosR = new Vector3(10.0f, 3.7f, 0f);
            var worldPosL = new Vector3(-10.0f, 3.7f, 0f);
            Quaternion rotL = Quaternion.Euler(0, 0, -45);
            Quaternion rotR = Quaternion.Euler(0, 0, -135);

            Instantiate(Rightdanger, worldPosR, rotR);
            Instantiate(Leftdanger, worldPosL, rotL);

            StartCoroutine(HandleDialogue());
        }

        // TimeScale が 0 から 1 に戻った時の処理
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
        nameText.text = "忍者";

        for (int i = 0; i < dialogueLines.Length; i++)
        {
            dialogText.text = dialogueLines[i];
            // 入力があるまで待機（スペースか左クリック）
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            // 入力が離されるまで待機（押しっぱなし防止）
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

        // TimeScale を 1 に戻す前にフラグを立てる
        waitingForTimeScaleOne = true;
        Time.timeScale = 1f;

        // バトル再開
        FindAnyObjectByType<Enemy2>().StartBattle();
        var sr = GetComponent<SpriteRenderer>();
        sr.DOFade(0f, 1.0f).SetLoops(-1, LoopType.Yoyo); // 忍者の点滅
        var greenImage = healthSlider.fillRect.GetComponent<Image>();
        greenImage.DOFade(0.5f, 0.5f).SetLoops(-1, LoopType.Yoyo); // HPバーの点滅（緑）
    }

    private void SpawnShurikens()
    {
        // クナイ投げ（TimeScale が 1 になった時に実行）
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