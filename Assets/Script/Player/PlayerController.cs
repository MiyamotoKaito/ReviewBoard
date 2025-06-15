using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float punchCooldown = 0.5f; // �N�[���^�C���i�b�j
    public GameObject RightHand;
    public GameObject LeftHand;
    public Camera mainCamera;
    
    private float punchTimer = 0f;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        punchTimer += Time.deltaTime; 
        Attack();
    }


    private void Attack()
    {
        if (punchTimer >= punchCooldown) // �N�[���^�C���o�߂��Ă邩
        {
            if (Input.GetMouseButtonDown(1))//���N���b�N�ō�����o��������
            {
                punchTimer = 0f;

                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 10f;
                mousePos.y = -2f;

                Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
                Instantiate(RightHand, worldPos, Quaternion.identity);
            }

            if (Input.GetMouseButtonDown(0))
            {
                punchTimer = 0f;

                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 10f;
                mousePos.y = -2f;

                Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
                Instantiate(LeftHand, worldPos, Quaternion.identity);
            }
        }
    }
}
