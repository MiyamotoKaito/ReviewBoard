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

    public void OnPointerEnter(PointerEventData eventData)//カーソルが重なったらアニメーション
    {
        animator.SetTrigger("HighLighted");  
    }

    public void OnPointerExit(PointerEventData eventData)//カーソルがボタンから離れたらアニメーション
    {
        animator.SetTrigger("Normal");
    }
}
