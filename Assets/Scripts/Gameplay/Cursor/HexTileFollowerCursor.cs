using UnityEngine;

public class HexTileFollowerCursor : MonoBehaviour
{
    [SerializeField] private Sprite 不可选Sprite; // 不可选图片
    [SerializeField] private Sprite 可选Sprite;   // 可选图片

    // 六边形参数（平顶）
    [SerializeField] private HexMapConfig config;  // 引用 HexMapConfig.asset
    // 玩家角色参数
    [SerializeField] private Transform playerTransform;     // 玩家角色 Transform

    private SpriteRenderer spriteRenderer;
    Camera cam;

    private void Awake()
    {
        /// <summary>
        /// 2D 精灵渲染组件
        /// sprite：显示哪张图片
        /// color：给图片加颜色或透明度
        /// flipX / flipY：水平或垂直翻转
        /// sortingLayer / orderInLayer：渲染顺序
        /// </summary>
        spriteRenderer = GetComponent<SpriteRenderer>();
        cam = Camera.main;
    }

    private void Update()
    {
        // 获取鼠标世界坐标
        Vector3 mws = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseWorld = new(mws.x, mws.y);

        // 转换为最近六边形坐标
        Vector2Int mouseHex = HexUtils.WorldToAxialRound(mouseWorld);
        Vector2Int playerHex = HexUtils.WorldToAxialRound(playerTransform.position);

        // 计算 玩家 到 鼠标光标 的六边形中心的距离
        int distance = HexUtils.HexDistance(playerHex, mouseHex);
        // 判断是否可达
        bool isTileReachable = HexUtils.CanPlayerMoveToTile(distance);
        // 根据距离切换 Sprite
        if (isTileReachable)
            spriteRenderer.sprite = 可选Sprite;
        else
            spriteRenderer.sprite = 不可选Sprite;

        // 六边形坐标转换为世界坐标
        Vector2 worldPos = HexUtils.AxialToWorld(mouseHex);
        // 更新图片位置
        transform.position = worldPos;
    }
}
