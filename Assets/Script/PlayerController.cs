using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject RightHand;
    public GameObject LeftHand;
    public Camera mainCamera;


    void Start()
    {
        
    }

    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(1))//���N���b�N�ō�����o��������
        {
            Debug.Log("�ɂ���");
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f;
            mousePos.y = -2f;

            Instantiate(RightHand, worldPos, Quaternion.identity);
        }

        if (Input.GetMouseButtonDown(0))//�E�N���b�N�ŉE����o��������
        {
            Debug.Log("�ɂ���");
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f;
            mousePos.y = -2f;

            Instantiate(LeftHand, worldPos, Quaternion.identity);
        }
    }
}
