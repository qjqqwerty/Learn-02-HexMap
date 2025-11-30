using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CellsSlot", menuName = "Inventory/CellsSlot")]
public class CellsSlot : ScriptableObject
{
    public int width, height;
    public List<CellData> cell = new();
}
