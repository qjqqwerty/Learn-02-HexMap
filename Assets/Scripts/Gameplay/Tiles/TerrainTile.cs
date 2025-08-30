using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Terrain Tile", menuName = "Tiles/Terrain Tile")]
public class TerrainTile : Tile
{
    public TerrainMoveType moveType;
    public TerrainExplorationType explorationType;
    public TerrainSightType sightType;

    // 你也可以在 Inspector 中添加自定义颜色、图标等属性
}
