using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("按键控制的 界面 UI 列表")]
    [Tooltip("列表长度应与可被按键控制的界面数量一致")]
    public ScreenUIKeyItem[] items;      // 在 Inspector 里填入 1 对 1 映射

    private void Update()
    {
        // 没有按键按下 → 不遍历（性能极好）
        if (!Input.anyKeyDown)
            return;

        foreach (var item in items)
        {
            if (Input.GetKeyDown(item.key))
            {
                ToggleUI(item.ui);
                break;  // 只处理一个按键
            }
        }
    }

    /// <summary>
    /// 打开/关闭一个 UI
    /// </summary>
    private void ToggleUI(GameObject ui)
    {
        // 如果当前打开的就是这个 → 则关闭它
        if (ui == null) return;

        bool active = !ui.activeSelf;
        ui.SetActive(active);
    }
}
