using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Terrain Tile", menuName = "Tiles/Terrain Tile")]
public class TerrainTile : Tile
{
    // 你可以在这里添加更多自定义属性
    public TerrainMoveType moveType;
    // 地形探索类型
    public TerrainExplorationType explorationType;
    // 地形视野修正
    public TerrainSightType sightType;

    // 你也可以在 Inspector 中添加自定义颜色、图标等属性
}
