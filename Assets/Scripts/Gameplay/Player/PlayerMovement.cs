using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [Header("六边形参数（平顶）")]

    // 六边形宽高
    [SerializeField] private HexMapConfig config;

    // 玩家对象
    public Transform player;

    // 当前玩家所在格（轴坐标）
    public Vector2Int playerQR = new Vector2Int(0, 0);
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
    }

    // // 玩家移动速度
    // private Vector2 velocity;
    // // 移动方向 
    // private Vector3 direction;
    // // 六边形地图格子宽度
    // private float width = 1f; 
    // // 六边形地图格子高度
    // private float height = 0.4f; 

    // // 是否有移动
    // private bool hasMove;


    // Update 会在游戏运行的每一帧都执行一次
    void Update()
    {
        // 鼠标点击
        if (Input.GetMouseButtonDown(0))
        {
            // 1) 计算世界坐标 屏幕鼠标位置 → 世界坐标
            Vector3 mws = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mouseWorld = new(mws.x, mws.y);
            Debug.Log($"Mouse Position: {mouseWorld}");

            // 世界→轴坐标(四舍五入到最近格)
            // 立方体舍入：把(qf, rf)转为最邻近的整数(q, r)
            // Vector2 clickQR = WorldToAxialRound(mouseWorld);
            Vector2Int clickQR = HexUtils.WorldToAxialRound(mouseWorld, true);
            // Debug.Log($"Rounded q, r: {clickQR.x}, {clickQR.y}");

            // 计算移动方向
            Vector2 playerPos = new(this.transform.position.x, this.transform.position.y);
            Vector2 velocity = (mouseWorld - playerPos);
            Debug.Log($"Mouse Position: {mouseWorld}, Player Position: {playerPos}, Rounded q, r: {clickQR}");

            // 计算 q

            // int q = (int)Math.Round(0.75 * mouseWorld.x / W);
            // int r = (int)Math.Round((mouseWorld.y - (q % 2) * H / 2.0) / H);
            // Debug.Log($"qr Position: {q}, {r}");



        }
    }
    // 处理移动
    private void MoveByMouseInput()
    {
        //需要补充的部分
    }

}
