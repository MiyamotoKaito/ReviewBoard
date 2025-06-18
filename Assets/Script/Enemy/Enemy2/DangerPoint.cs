using UnityEngine;
using System.Collections;

public class ToggleVisibility : MonoBehaviour
{
    public GameObject targetObject;
    public float interval = 1.0f;
    public float initialDelay = 0f; // 初期表示時間

    private void Start()
    {
        StartCoroutine(LoopVisibility());
    }

    private IEnumerator LoopVisibility()
    {
        // 最初に指定時間表示してから点滅開始
        yield return new WaitForSeconds(initialDelay);

        while (targetObject != null)
        {
            targetObject.SetActive(!targetObject.activeSelf);
            yield return new WaitForSeconds(interval);
        }
    }
}