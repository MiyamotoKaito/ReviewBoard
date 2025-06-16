using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth2 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float AttackTimer = 1.0f;
    [SerializeField] float AttackCooldown = 2f;
    [SerializeField] GameObject RightShuriken;
    [SerializeField] GameObject LeftShuriken;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private Text nameText;
    [SerializeField] private Text dialogText;
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private string characterName = "先生";

    private int currentLines = 0;
    private bool inDialogue = true;

    public Slider healthSlider;
    void Start()
    {
        healthSlider.value = 300f;
    }

    // Update is called once per frame
    void Update()
    {
        AttackTimer += Time.deltaTime;

        if (healthSlider.value <= 150)
        {
            Time.timeScale = 0f;

                StartDialogue();


                if (inDialogue && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
                {
                    DisplayNextLine();


                    if (AttackTimer > AttackCooldown)
                    {
                        Vector3 ShurikenPosition1 = new Vector3(-18f, 16f, 0f);
                        Vector3 ShurikenPosition2 = new Vector3(18f, 16f, 0f);
                        Instantiate(LeftShuriken, ShurikenPosition1, Quaternion.identity);
                        Instantiate(RightShuriken, ShurikenPosition2, Quaternion.identity);
                        AttackTimer = 0f;//タイマーリセット
                    }
                }

            
        }
    }



    public void StartDialogue()
    {
        dialogPanel.SetActive(true);
        Time.timeScale = 0f;
        nameText.text = characterName;
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
        Time.timeScale = 1f;
        inDialogue = false;
        Cursor.visible = false;

        FindObjectOfType<Enemy2>().StartBattle();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Punch"))
        {
            healthSlider.value -= 10f;
        }
    }
}
