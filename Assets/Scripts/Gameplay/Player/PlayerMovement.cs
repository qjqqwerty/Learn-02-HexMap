using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  // 必须引入才能用 EventSystem


public class PlayerMovement : MonoBehaviour
{
    [Header("六边形参数（平顶）")]

    // 六边形宽高
    [SerializeField] private HexMapConfig config;

    // 玩家对象
    public Transform player;

    // 当前玩家所在格（轴坐标）
    public Vector2Int playerQR;
    // 主摄像机
    Camera cam;

    // Awake()：只在对象加载时调用一次
    // Start()：在对象启用的第一帧调用一次
    // Update()：每帧都调用
    // FixedUpdate()：按照物理时间步长调用（通常 50 次/秒），专门用于物理计算
    // LateUpdate()：在所有 Update() 执行完之后调用，常用于相机跟随

    // Awake 会在游戏对象加载时调用一次
    void Awake()
    {
        cam = Camera.main;
        float s = config.S;             // 半宽
        float scaleY = config.ScaleY;   // 3/4 * width = 3/2 * s = (sqrt(3)*s)/H
        // HexUtils 初始 六边形工具类
        HexUtils.Init(config);
        playerQR = new(0, 0);
    }


    // Update 会在游戏运行的每一帧都执行一次
    void Update()
    {
        // 鼠标点击
        if (Input.GetMouseButtonDown(0))
        {
            // ① 判断是否点击在 UI 上
            // 用于保存被点击到的 UI
            List<RaycastResult> results = new List<RaycastResult>();

            // 构造当前点击的 pointer 数据
            PointerEventData data = new PointerEventData(EventSystem.current);
            data.position = Input.mousePosition;

            // 对所有 UI 进行检测
            EventSystem.current.RaycastAll(data, results);

            if (results.Count > 0)
            {
                Debug.Log("点击到了 UI 元素：");
                GameObject blockingUI = null;

                foreach (var r in results)
                {
                    Debug.Log(" → " + r.gameObject.name);
                    var graphic = r.gameObject.GetComponent<UnityEngine.UI.Graphic>();
                    if (graphic != null && graphic.raycastTarget)
                    {
                        blockingUI = r.gameObject;
                        break; // 找到最上层阻挡的 UI
                    }
                }

                // 最前面的 UI（真正挡住你的那个）
                Debug.Log("最前方挡住你的 UI 是：" + results[0].gameObject.name);

                if (blockingUI != null)
                {
                    Debug.Log("被阻挡，挡住你的 UI 是：" + blockingUI.name);
                    return; // 阻止 Tilemap 继续执行
                }
                else
                {
                    Debug.Log("没有可阻挡 UI");
                    // 继续 Tilemap 逻辑
                }
            }
            
            // 1) 获取 屏幕鼠标位置 (世界坐标)
            Vector3 mws = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mouseWorld = new(mws.x, mws.y);
            Debug.Log($"Mouse Position: {mouseWorld}");

            // 光标世界坐标→轴坐标(立方体舍入到最近格)
            // 计算鼠标点击处轴坐标, 立方体舍入：把(qf, rf)转为最邻近的整数(q, r)
            Vector2Int clickQR = HexUtils.WorldToAxialRound(mouseWorld, true);

            // 移动处理
            // 获取玩家位置
            Vector2 playerPos = new(this.transform.position.x, this.transform.position.y);
            // 输出日志 (鼠标的世界坐标, 玩家的世界坐标, 鼠标的轴坐标)
            Debug.Log($"鼠标的世界坐标: {mouseWorld}, 玩家的世界坐标: {playerPos}, 鼠标的轴坐标: {clickQR}");
            // 2) 计算玩家所在格到鼠标点击格的距离
            int distance = HexUtils.HexDistance(playerQR, clickQR);

            // 3) 判断是否可达
            bool isTileReachable = HexUtils.CanPlayerMoveToTile(distance);
            if (isTileReachable)
            {
                // 4) 目标格中心点（世界坐标）
                // 4) 鼠标的轴坐标 → 鼠标所在格中心点的世界坐标
                Vector2 target = HexUtils.AxialToWorld(clickQR);

                // 5) 更新玩家位置
                // 5) 移动（示例：瞬移；你也可以改成协程/Lerp）
                if (player != null)
                {
                    player.position = new Vector3(target.x, target.y, player.position.z);
                }
                // 6) 更新玩家所在格坐标
                playerQR = clickQR;
                Debug.Log($"玩家移动到格子: {playerQR}");
            }
            else if (HexUtils.HexDistance(playerQR, clickQR) == 0)
            {
                // 自身格点击：额外提示
                Debug.Log("这里是当前格子");
            }
            else
            {
                // 非自身/邻格：忽略或提示
                Debug.Log("目标格子距离太远，无法移动");
            }

        }
    }
    // 处理移动
    private void MoveByMouseInput()
    {
        //需要补充的部分
    }

}
