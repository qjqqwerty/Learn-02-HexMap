using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // 玩家移动速度
    private Vector2 velocity;
    // 移动方向 
    private Vector3 direction;
    // 是否有移动
    private bool hasMove;


    // Update is called once per frame
    void Update()
    {
        // 鼠标点击
        if (Input.GetMouseButtonDown(0))
        {
            // 计算鼠标位置
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log($"Mouse Position: {mousePos}");
            // 计算移动方向
            Vector2 playerPos = new Vector2(this.transform.position.x, this.transform.position.y);
            Vector2 velocity = (mousePos - playerPos);
            Debug.Log($"Mouse Position: {mousePos}, Player Position: {playerPos}, Velocity: {velocity}");
            
        }
    }
    // 处理移动
    private void MoveByMouseInput()
    {
        //需要补充的部分
    }
}
