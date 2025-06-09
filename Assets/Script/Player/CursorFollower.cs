using UnityEngine;

public class CursorFollower : MonoBehaviour
{
    public Camera cam;
    public float FixedY = 0f;

    void Start()
    {
        // 標準カーソルを非表示にする
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; // カメラからの距離（カメラ設定に合わせて）

        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        worldPos.y = FixedY;
        transform.position = worldPos;
    }
}
