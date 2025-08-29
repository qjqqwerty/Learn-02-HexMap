using UnityEngine;

public class TileCursorFollower : MonoBehaviour
{
    [SerializeField] private Sprite 不可选Sprite; // 图片1
    [SerializeField] private Sprite 可选Sprite;   // 图片2
    [SerializeField] private Sprite Sprite; // 可选图片
    [SerializeField] private float tileWidth = 1.06f;     // 平顶六边形宽
    [SerializeField] private float tileHeight = 0.39f;    // 平顶六边形高

    private float s;       // 六边形长
    private float scaleY;  // y 归一化系数

    private void Start()
    {
        s = tileWidth / 2f;                   // 半宽
        scaleY = (Mathf.Sqrt(3f) * s) / tileHeight;
    }

    private void Update()
    {
        // 获取鼠标世界坐标
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        // 转换为六边形坐标
        Vector2Int hex = HexUtils.WorldToAxialRound(mouseWorld);

        // 将六边形坐标转换回世界坐标
        Vector2 worldPos = HexUtils.AxialToWorld(hex);

        // 更新可选图片位置
        transform.position = worldPos;
    }

}
