using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class InventoryManager : MonoBehaviour
{
    public GridLayoutGroup grid;
    public CellsSlot slot;   // 你的数据类

    void OnValidate()
    {
        Debug.Log("Editor 正在执行 InventoryManager");
        InitBagSize();
    }

    void InitBagSize()
    {
        int totalCells = grid.transform.childCount;
        int width = 0, height = 0;

        switch (grid.constraint)
        {
            case GridLayoutGroup.Constraint.FixedColumnCount:
                width = grid.constraintCount;
                height = totalCells / width;
                break;

            case GridLayoutGroup.Constraint.FixedRowCount:
                height = grid.constraintCount;
                width = totalCells / height;
                break;

            default:
                Debug.LogError("GridLayoutGroup 必须设置 Constraint 为固定列数或行数！");
                return;
        }

        slot.width = width;
        slot.height = height;

        Debug.Log($"背包自动初始化完成：{width} x {height}（总格子：{totalCells}）");
    }
}
