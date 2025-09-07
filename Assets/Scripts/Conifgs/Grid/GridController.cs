using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 控制 Grid 组件的配置，让它和 HexMapConfig（ScriptableObject）保持同步。
/// 如果 HexMapConfig.asset 丢失，会自动生成默认 asset 并关联。
/// </summary>
[ExecuteAlways]                      // 🔹 即使在编辑器模式（非运行时）下，这个脚本也会运行
[RequireComponent(typeof(Grid))]     // 🔹 确保挂载该脚本的对象必须有 Grid 组件（没有会自动加上）
public class GridController : MonoBehaviour
{
    [SerializeField] private HexMapConfig config;   // 🔹 引用 HexMapConfig（ScriptableObject）配置文件
    private Grid grid;                              // 🔹 缓存本地的 Grid 组件

    /// <summary>
    /// 当脚本被启用时调用（包括进入 Play 模式和场景加载时）。
    /// </summary>
    private void OnEnable()
    {
        // 获取当前对象上的 Grid 组件
        grid = GetComponent<Grid>();

#if UNITY_EDITOR
        // 如果 config 丢失，自动生成默认 asset
        if (config == null)
        {
            CreateDefaultConfig();
        }
#endif

        // 如果 config 仍然存在，订阅事件并应用配置
        if (config != null)
        {
#if UNITY_EDITOR
            // 订阅配置文件的 OnConfigChanged 事件
            // 这样当 HexMapConfig 的值改变时，会自动触发 ApplyConfigToGrid()
            config.OnConfigChanged += ApplyConfigToGridDelayed;
            ApplyConfigToGridDelayed();

#else
            // 初始化时立刻延迟应用一次配置，保证 Grid 和配置同步
            config.OnConfigChanged += ApplyConfigToGrid;
            ApplyConfigToGrid();
#endif
        }
    }

    /// <summary>
    /// 当脚本被禁用时调用（比如对象被销毁、切换场景等）。
    /// 取消事件订阅，避免内存泄漏或引用错误。
    /// </summary>
    private void OnDisable()
    {
        if (config != null)
#if UNITY_EDITOR
            config.OnConfigChanged -= ApplyConfigToGridDelayed;
#else
            config.OnConfigChanged -= ApplyConfigToGrid;
#endif
    }

    /// <summary>
    /// 将配置文件里的参数应用到 Grid 组件上。
    /// 这是核心函数，保证 Grid 的参数与 HexMapConfig 保持一致。
    /// </summary>
    public void ApplyConfigToGrid()
    {
        if (config != null && grid != null)
        {
            // 设置 Grid 的单元格大小（x=高度，y=宽度，z=一般为0）
            grid.cellSize = new Vector3(config.tileHeight, config.tileWidth, 0f);

            // 设置 Grid 的布局类型为 Hexagon（平顶/尖顶由 Tilemap 决定）
            grid.cellLayout = GridLayout.CellLayout.Hexagon;

            // 设置轴顺序为 YXZ
            // 作用：决定 Grid 坐标系在 Unity 世界坐标里的映射方式
            grid.cellSwizzle = GridLayout.CellSwizzle.YXZ;
        }
    }

#if UNITY_EDITOR
    /// <summary>
    /// 编辑器模式下延迟应用配置，避免 SendMessage 警告
    /// </summary>
    private void ApplyConfigToGridDelayed()
    {
        // 如果在运行时，直接调用
        if (Application.isPlaying)
        {
            ApplyConfigToGrid();
        }
        else
        {
            // 编辑器模式下延迟调用，避免在 Awake/OnValidate/CheckConsistency 期间修改 Grid
            EditorApplication.delayCall += () =>
            {
                if (this != null) // 防止对象被销毁
                    ApplyConfigToGrid();
            };
        }
    }

    /// <summary>
    /// 编辑器模式下创建默认 HexMapConfig.asset 并关联到本脚本
    /// </summary>
    [ContextMenu("Create Default HexMapConfig")]
    private void CreateDefaultConfig()
    {
        // 创建默认 asset
        HexMapConfig newConfig = ScriptableObject.CreateInstance<HexMapConfig>();

        // 确保保存路径存在
        string folderPath = "Assets/Configs";
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets", "Configs");
        }

        string assetPath = folderPath + "/HexMapConfig.asset";

        // 避免覆盖已有 asset
        if (AssetDatabase.LoadAssetAtPath<HexMapConfig>(assetPath) != null)
        {
            Debug.LogWarning("HexMapConfig.asset already exists.");
            newConfig = AssetDatabase.LoadAssetAtPath<HexMapConfig>(assetPath);
        }
        else
        {
            AssetDatabase.CreateAsset(newConfig, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("Created new HexMapConfig.asset at " + assetPath);
        }

        // 自动关联到 GridController
        config = newConfig;
        EditorUtility.SetDirty(this); // 标记本脚本为修改过，需要保存
    }
#endif
}
