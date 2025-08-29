using UnityEngine;
using System;

/// <summary>
/// 全局六边形地图的配置文件（ScriptableObject）
/// 用来统一存储瓦片（Tile）的宽和高，避免多个脚本里写死参数。
/// 修改它后，引用它的脚本（如 GridController、玩家移动等）都能自动读取到新值。
/// [CreateAssetMenu(fileName = "HexMapConfig", menuName = "Configs/HexMapConfig")]
///作用：允许你在 Unity 编辑器 → Assets → Create → Configs → HexMapConfig 菜单下创建该配置文件。
///fileName：新建 asset 文件的默认名字。
///menuName：在 Create 菜单里显示的路径。
/// </summary>
[CreateAssetMenu(fileName = "HexMapConfig", menuName = "Configs/HexMapConfig")]
public class HexMapConfig : ScriptableObject // 轻量级的数据容器类
{
    [Header("六边形瓦片参数")]
    [Tooltip("平顶六边形的宽度（单位：世界坐标）")]
    public float tileWidth = 1.06f;

    [Tooltip("平顶六边形的高度（单位：世界坐标）")]
    public float tileHeight = 0.39f;

    // 半宽
    public float S => tileWidth / 2f;                           // 半宽

    // ScaleY 表示六边形在 Y 方向的缩放比例
    // y归一化系数
    public float ScaleY => Mathf.Sqrt(3f) * S / tileHeight;     // 3/4 * width = 3/2 * s = (sqrt(3)*s)/H

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
