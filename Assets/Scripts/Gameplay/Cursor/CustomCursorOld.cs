using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursorOld : MonoBehaviour
{
    // Start is called before the first frame update
    // 普通光标
    [SerializeField] private Sprite 普通光标;
    // 按下光标
    [SerializeField] private Sprite 按下光标;

    private SpriteRenderer _skinRenderer;

    private void Awake()
    {
        _skinRenderer= GetComponentInChildren<SpriteRenderer>();
    }

    private void Update() 
    {
    if(_skinRenderer == null) return;

    if (Input.GetMouseButtonDown(0))
    {
        Cursor.visible = false;
        _skinRenderer.sprite = 按下光标;
    }
    if (Input.GetMouseButtonUp(0))
    {
        _skinRenderer.sprite = 普通光标;
    }
        // 让光标跟随鼠标位置
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnApplicationFocus(bool focus)
    {
        Cursor.visible = false;
    }
}
