using UnityEngine;

public class TileCursorFollower : MonoBehaviour
{
    [SerializeField] private Sprite 不可选Sprite; // 不可选图片
    [SerializeField] private Sprite Sprite; // 可选图片

    // 六边形参数（平顶）
    [SerializeField] private HexMapConfig config; // 引用 HexMapConfig.asset

    private float s;       // 六边形长
    private float scaleY;  // y 归一化系数

    private void Start()
    {
        float s = config.S;             // 半宽
        float scaleY = config.ScaleY;   // 3/4 * width = 3/2 * s = (sqrt(3)*s)/H
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
