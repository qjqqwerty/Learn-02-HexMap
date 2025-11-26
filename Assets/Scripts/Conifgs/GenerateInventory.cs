using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class GenerateInventory : MonoBehaviour
{
    public GameObject cellPrefab;  // 格子预制体
    GameObject[] items;      // 存储格子信息的数组
    public int slotCount = 4;        // 格子数量
    private void Awake()
    {
        items = new GameObject[slotCount];
        for (int i = 0; i < slotCount; i++)
        {
            items[i] = transform.GetChild(i).gameObject;
            items[i].name = "Cell " + (i + 1);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GenerateInventory))]
public class GenerateInventoryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GenerateInventory inventory = target as GenerateInventory;
        if (GUILayout.Button("生成仓库"))
        {
            var chileNum = Selection.activeTransform.childCount;
            // 删除现有的子对象
            for (int i = 0; i < chileNum; i++)
            {
                DestroyImmediate(Selection.activeTransform.GetChild(0).gameObject);
            }

            // 生成新的格子
            for (int i = 0; i < inventory.slotCount; i++)
            {
                GameObject go = Instantiate(inventory.cellPrefab, Selection.activeTransform);
            }
        }
        base.OnInspectorGUI();
    }
}

#endif
