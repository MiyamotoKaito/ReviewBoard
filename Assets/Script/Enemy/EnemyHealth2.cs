using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth2 : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider healthSlider;
    void Start()
    {
        healthSlider.value = 300f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Punch"))
        {
            healthSlider.value -= 10f;
            Debug.Log("10ÇÃÉ_ÉÅÅ[ÉW");
        }
    }
}
