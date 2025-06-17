using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth3 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float AttackTimer = 1.0f;
    [SerializeField] float AttackCooldown = 2f;
    //[SerializeField] GameObject RightShuriken;
    //[SerializeField] GameObject LeftShuriken;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private Text nameText;
    [SerializeField] private Text dialogText;
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private string characterName = "先生";

    private int currentLines = 0;
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
        if (!inDialogue && healthSlider.value <= 100f)
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

        for (int i = 1; i < dialogueLines.Length; i++)
        {
            dialogText.text = dialogueLines[i];

            // 入力があるまで待機（スペースか左クリック）
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0));

            // 入力が離されるまで待機（押しっぱなし防止）
            yield return new WaitUntil(() => !Input.GetKeyDown(KeyCode.Space) || !Input.GetMouseButtonDown(0));
        }

        Endialogue();

    }

    private void Endialogue()
    {
        dialogPanel.SetActive(false);
        nameText.gameObject.SetActive(false);
        dialogText.gameObject.SetActive(false);
        Time.timeScale = 1f;

        //バトル再開
        FindAnyObjectByType<Enemy3>().StartBattle();

        healthSlider.value = 250f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Punch"))
        {
            healthSlider.value -= 10f;
            
        }
    }
}
