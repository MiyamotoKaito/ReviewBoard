using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        {
            
        }
    }

    public void OnPointerEnter(PointerEventData eventData)//�J�[�\�����d�Ȃ�����A�j���[�V����
    {
        animator.SetTrigger("HighLighted");  
    }

    public void OnPointerExit(PointerEventData eventData)//�J�[�\�����{�^�����痣�ꂽ��A�j���[�V����
    {
        animator.SetTrigger("Normal");
    }
}
