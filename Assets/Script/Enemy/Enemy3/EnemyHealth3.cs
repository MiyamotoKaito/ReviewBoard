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
    [SerializeField] private string characterName = "�搶";

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
        nameText.text = "�g���[�i�[";

        for (int i = 1; i < dialogueLines.Length; i++)
        {
            dialogText.text = dialogueLines[i];

            // ���͂�����܂őҋ@�i�X�y�[�X�����N���b�N�j
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0));

            // ���͂��������܂őҋ@�i�������ςȂ��h�~�j
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

        //�o�g���ĊJ
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
