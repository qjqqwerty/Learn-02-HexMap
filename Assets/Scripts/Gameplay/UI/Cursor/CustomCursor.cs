using UnityEngine;
using UnityEngine.UI;   // 引入 UI 命名空间

public class CustomCursor : MonoBehaviour
{
    [Header("光标贴图")]
    [SerializeField] private Sprite 普通光标;
    [SerializeField] private Sprite 按下光标;

    private Image _skinImage; // 用 Image 替代 SpriteRenderer

    private void Awake()
    {
        _skinImage = GetComponentInChildren<Image>();
    }

    private void Update()
    {
        if (_skinImage == null) return;

        // 鼠标按下切换贴图
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.visible = false;  
            _skinImage.sprite = 按下光标;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _skinImage.sprite = 普通光标;
        }

        // 光标跟随鼠标位置（UI 用屏幕坐标）
        transform.position = Input.mousePosition;
    }

    private void OnApplicationFocus(bool focus)
    {
        Cursor.visible = false;
    }
}
