using UnityEngine;

public class CursorFollower : MonoBehaviour
{
    public Camera cam;
    public float FixedY = 0f;

    void Start()
    {
        // �W���J�[�\�����\���ɂ���
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; // �J��������̋����i�J�����ݒ�ɍ��킹�āj

        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        worldPos.y = FixedY;
        transform.position = worldPos;
    }
}
