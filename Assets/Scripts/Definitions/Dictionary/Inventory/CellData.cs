using UnityEngine;

[System.Serializable]
public class CellData : MonoBehaviour
{
    public ItemData item;
    public int count;
    
    [Tooltip("图标位置")]
    public iconLoc iconLoc = new();
}

[System.Serializable]
public class iconLoc
{
    public int x, y;        // 位置
    public int rotate = 0;  // 旋转
}