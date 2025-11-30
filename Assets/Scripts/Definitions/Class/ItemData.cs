
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "gameData/新建物品")]
public class ItemData : ScriptableObject
{
    // ───────────────────────────────────────────────
    [Header("【 基础信息 】")]
    // ───────────────────────────────────────────────
    [Tooltip("物品ID")]
    public int id = 0;                 // 物品唯一ID

    [Tooltip("物品组ID")]
    public int groupId = 0;            // 物品组ID

    [Tooltip("物品子组ID")]
    public int subGroupId = 0;         // 物品子组ID

    [Tooltip("物品分类，例如 \"Seed/Vegetable/Carrot\"")]
    public string category = "";   // 分类路径，例如 "Seed/Vegetable/Carrot"

    [Tooltip("物品预制体")]
    public GameObject prefab = null;

    [Tooltip("物品描述")]
    [TextArea(1, 5)]
    public string description = "";

    [Tooltip("鉴定描述（strDescAlt）")]
    [TextArea(1, 5)]
    public string descriptionAlt = "";

    [Tooltip("开关映射列表(aSwitchIDs)")]
    public List<SwitchValue> switchIDs = new();



    // ───────────────────────────────────────────────
    [Header("【 图像 / 图标 】")]
    // ───────────────────────────────────────────────
    [Tooltip("物品图标")]
    public Sprite icon = null;

    [Tooltip("图标高度")]
    public int iconHeight = 1;

    [Tooltip("图标宽度")]
    public int iconWidth = 1;



    // ───────────────────────────────────────────────
    [Header("【 数值属性 】")]
    // ───────────────────────────────────────────────
    [Tooltip("物品最大堆叠数量(nStackLimit)")]
    public int maxStack = 1;

    [Tooltip("价格(fMonetaryValue)")]
    public float price = 0f;

    [Tooltip("鉴定价格(fMonetaryValueAlt)")]
    public float appraisalPrice = 0f;

    [Tooltip("物品重量(fWeight)")]
    public int weight = 0;

    [Tooltip("物品耐久度(fDurability)")]
    public float durability = 1;

    [Tooltip("每小时损耗(fDegradePerHour)")]
    public float degradePerHour = 0f;

    [Tooltip("装备状态下每小时损耗(fEquipDegradePerHour)")]
    public float equipDegradePerHour = 0f;

    [Tooltip("每次使用的损耗(fDegradePerUse)")]
    public float degradePerUse = 0f;

    [Tooltip("攻击模式列表(aAttackModes)")]
    public List<AttackMode> attackModes = new();



    // ───────────────────────────────────────────────
    [Header("【 条件信息 】")]
    // ───────────────────────────────────────────────
    [Tooltip("装备条件")]
    public List<intDict> equipConditions = new();

    [Tooltip("持有条件")]
    public List<intDict> possessConditions = new();

    [Tooltip("使用条件")]
    public List<intDict> useConditions = new();



    // ───────────────────────────────────────────────
    [Header("【 属性 】")]
    // ───────────────────────────────────────────────
    [Tooltip("属性(vProperties)")]
    public List<int> properties = new();

    [Tooltip("空间大小(aCapacities)")]
    public intDict capacities = new();

    [Tooltip("内容物ID(aContentIDs)")]
    public List<int> contentIDs = new();



    // ───────────────────────────────────────────────
    [Header("【 装备 / 使用槽位 】")]
    // ───────────────────────────────────────────────
    [Tooltip("装备槽位列表，例如 22=0=0")]
    public List<EquipSlot> equipSlots = new();

    [Tooltip("可使用槽位(vUseSlots)")]
    public List<int> useSlots = new();

    [Tooltip("插槽是否锁定(bSocketLocked)")]
    public bool socketLocked = false;



    // ───────────────────────────────────────────────
    [Header("【 其他 】")]
    // ───────────────────────────────────────────────
    [Tooltip("格式 ID(nFormatID)")]
    public int formatId = 0;

    [Tooltip("掉落 ID(nTreasureID)")]
    public int treasureID = 0;

    [Tooltip("组件 ID(nComponentID)")]
    public int componentID = 0;

    [Tooltip("是否镜像(bMirrored)")]
    public bool mirrored = false;

    [Tooltip("装备深度(nSlotDepth)")]
    public int slotDepth = 0;

    [Tooltip("充能配置(strChargeProfiles)")]
    public int chargeProfiles = 0;



    // ───────────────────────────────────────────────
    [Header("【 音效 】")]
    // ───────────────────────────────────────────────
    [Tooltip("音效(aSounds)，如 cuePickup, cuePutdown")]
    public List<string> sounds = new();



}

[System.Serializable]
public class intDict
{
    public int key;    // 左边数字，例如 2、3、211
    public int value;  // 右边数字，例如 21、-210、64
}

[System.Serializable]
public class AttackMode
{
    public int modeId;
    public int value;
}

[System.Serializable]
public class SwitchValue
{
    public bool key;
    public int groupId;
    public int subGroupId;
}

[System.Serializable]
public class EquipSlot
{
    public int slotId;
    public bool param1;
    public bool param2;
}