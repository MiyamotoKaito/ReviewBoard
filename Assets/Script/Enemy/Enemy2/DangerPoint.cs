using UnityEngine;
using System.Collections;

public class ToggleVisibility : MonoBehaviour
{
    public GameObject targetObject;
    public float interval = 1.0f;
    public float initialDelay = 0f; // �����\������

    private void Start()
    {
        StartCoroutine(LoopVisibility());
    }

    private IEnumerator LoopVisibility()
    {
        // �ŏ��Ɏw�莞�ԕ\�����Ă���_�ŊJ�n
        yield return new WaitForSeconds(initialDelay);

        while (targetObject != null)
        {
            targetObject.SetActive(!targetObject.activeSelf);
            yield return new WaitForSeconds(interval);
        }
    }
}