using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float 键盘移动速度 = 3; // 键盘移动相机速度
    public float 鼠标移动速度 = 0.2f; // 鼠标移动相机速度
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleKeyboardMove();
        HandleMouseDrag();
    }

    // 键盘移动
    private void HandleKeyboardMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new(x, y);

        transform.Translate(direction * 键盘移动速度 * Time.deltaTime);
    }

    // 鼠标右键拖动
    void HandleMouseDrag()
    {
        if (Input.GetMouseButton(1)) // 右键按下
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            transform.Translate(new Vector3(mouseX, mouseY, 0)* 鼠标移动速度, Space.Self);
        }
    }
}
