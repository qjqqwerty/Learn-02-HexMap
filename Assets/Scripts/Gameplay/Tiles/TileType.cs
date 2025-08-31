using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileType : MonoBehaviour
{
    [Header("瓦片属性")]
    // 地形通行类型
    public TerrainMoveType moveType = TerrainMoveType.ShipOnly;
    // 地形探索类型
    public TerrainExplorationType explorationType = TerrainExplorationType.None;
    // 地形视野修正
    public TerrainSightType sightType = TerrainSightType.Normal;

    [Tooltip("是否可以通行 (由 moveType 决定)")]
    public bool canPass ;

    [Tooltip("移动消耗 (由 moveType 决定)")]
    public float cost ;

    private void OnValidate()
    {
        // 编辑器里自动同步 cost / canPass
        UpdateTileProperties();
    }

    /// <summary>
    /// 根据地形类型更新瓦片属性
    /// </summary>
    public void UpdateTileProperties()
    {
        switch (moveType)
        {
            case TerrainMoveType.Blocked:
                canPass = false;
                cost = Mathf.Infinity; // 永远无法通过
                break;

            case TerrainMoveType.ShipOnly:
                canPass = false; // 默认角色不能通过（后续可扩展船只单位例外）
                cost = Mathf.Infinity;
                break;

            case TerrainMoveType.HighCost:
                canPass = true;
                cost = 2f; // 自定义高代价
                break;

            case TerrainMoveType.Normal:
                canPass = true;
                cost = 1f;
                break;
        }
    }
}
