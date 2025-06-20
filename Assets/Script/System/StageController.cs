using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] private int currentStageNumber;

    // ステージクリア時に呼び出すメソッド
    public void CompleteStage()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.ClearStage(currentStageNumber);
            Debug.Log($"ステージ{currentStageNumber}をクリアしました！");
        }
    }
}
