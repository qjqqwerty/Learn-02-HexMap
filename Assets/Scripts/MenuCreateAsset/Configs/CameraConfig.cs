using UnityEngine;
using System;


public enum CameraType
{
    TopDown,
    RTS,
    Free
}

/// <summary>
/// 全局主相机的配置文件（ScriptableObject）
/// 用来统一存储主相机的数据，避免多个脚本里写死参数。
/// 修改它后，引用它的脚本（如 GridController、玩家移动等）都能自动读取到新值。
/// [CreateAssetMenu(fileName = "CameraConfig", menuName = "Configs/CameraConfig")]
/// 作用：允许你在 Unity 编辑器 → Assets → Create → Configs → CameraConfig 菜单下创建该配置文件。
/// fileName：新建 asset 文件的默认名字。
/// menuName：在 Create 菜单里显示的路径。
/// </summary>
[CreateAssetMenu(fileName = "CameraConfig", menuName = "Configs/CameraConfig")]
public class CameraConfig : ScriptableObject // 轻量级的数据容器类
{
    [Header("模式标识（仅用于识别）")]
    public CameraType 类型;

    [Header("平移")]
    [Tooltip("键盘WASD/方向键移动速度")]
    public float 键盘移动速度 = 3f;

    [Tooltip("右键拖拽时的移动系数")]
    public float 鼠标移动速度 = 0.07f;

    [Header("缩放(TODO)")]
    public float 缩放速度 = 5f;
    public float 最小尺度 = 5f;
    public float 最大尺度 = 50f;

    /// <summary>
    /// 当配置文件参数发生变化时触发的事件
    /// 例如：GridController 可以订阅这个事件，从而在参数被修改时自动更新 Grid。
    /// </summary>
    public event Action OnConfigChanged;

#if UNITY_EDITOR
    /// <summary>
    /// Unity 编辑器下，当 Inspector 中参数被修改时会自动调用这个函数。
    /// 用于触发事件，让引用该配置的对象（例如 GridController）实时更新。
    /// </summary>
    private void OnValidate()
    {
        OnConfigChanged?.Invoke(); // 如果有订阅者就调用事件
    }
#endif
}
