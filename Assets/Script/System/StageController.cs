using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] private int currentStageNumber;

    // �X�e�[�W�N���A���ɌĂяo�����\�b�h
    public void CompleteStage()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.ClearStage(currentStageNumber);
            Debug.Log($"�X�e�[�W{currentStageNumber}���N���A���܂����I");
        }
    }
}
