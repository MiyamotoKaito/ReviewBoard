using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth3 : MonoBehaviour
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
        healthSlider.value = 250f;
        dialogPanel.SetActive(false);

        if(healthSlider.value <= 30 ) 
        {
            var greenImage = healthSlider.fillRect.GetComponent<Image>();
            greenImage.DOFade(0.5f, 0.5f).SetLoops(-1, LoopType.Yoyo);//HPバーの点滅（緑）
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!inDialogue && healthSlider.value <= 50f)
        {
            Debug.Log("aaa");
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
        nameText.text = "トレーナー";

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
        Time.timeScale = 1f;

        //バトル再開
        FindAnyObjectByType<Enemy3>().StartBattle();

        healthSlider.value = 100f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Punch"))
        {
            healthSlider.value -= 10f;
            
        }
    }
}
