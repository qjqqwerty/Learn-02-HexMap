/// <summary>
/// 地形通行类型
/// </summary>
public enum TerrainMoveType
{
    /// <summary>
    /// 完全阻挡，无法通过
    /// </summary>
    Blocked,
    /// <summary>
    /// 只能船只通行
    /// </summary>
    ShipOnly,
    /// <summary>
    /// 可以通行，但代价高
    /// </summary> 
    HighCost,
    /// <summary>
    /// 正常通行
    /// </summary>
    Normal
}

/// <summary>
/// 地形视野修正
/// </summary>
public enum TerrainSightType
{
    /// <summary>
    /// 不影响视野
    /// </summary>
    Normal,
    /// <summary>
    /// 遮蔽视野（如森林）
    /// </summary>
    BlockVision,
    /// <summary>
    /// 提升视野（如丘陵）
    /// </summary>
    ExtendVision
}

/// <summary>
/// 地形探索类型
/// </summary>
public enum TerrainExplorationType
{
    /// <summary>
    /// 无内容（如纯地形）
    /// </summary>
    None,
    /// <summary>
    /// 可搜索区域
    /// Hex can be scovenged.
    /// 如小镇、废弃大楼、废弃城市
    /// </summary>
    CanBeScovenged,
    /// <summary>
    /// 有物品区域
    /// has items in it.
    /// 自然类地形，如森林、湖泊、山丘
    /// </summary>
    HasItemsInIt,
    /// <summary>
    /// 剧情类地形，触发特殊事件，之后恢复成普通探索
    /// </summary>
    Special
}
