using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float punchCooldown = 0.5f; // クールタイム（秒）
    public GameObject RightHand;
    public GameObject LeftHand;
    public Camera mainCamera;
    
    private float punchTimer = 0f;
    private float punchTimer2 = 0f;

    void Start()
    {
        if (!mainCamera) //もし入れ忘れたらメインカメラを入れる
        {
            mainCamera = Camera.main;
        }

        punchTimer2 = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }


    private void Attack()
    {
        if (punchTimer2 <= Time.time)
        {
            punchTimer2 = Time.time + punchCooldown;
        }

        punchTimer += Time.deltaTime;
        if (punchTimer >= punchCooldown) // クールタイム経過してるか
        {
            if (Input.GetMouseButtonDown(0))//左クリックで左手を出現させる
            {
                punchTimer = 0f;

                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 10f;
                mousePos.y = -2f;

                Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
                Instantiate(RightHand, worldPos, Quaternion.identity);
            }
        }
    }
}
