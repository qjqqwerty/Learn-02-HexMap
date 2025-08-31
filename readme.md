## 欢迎来的这个开源项目

作者也很羞涩不知道说什么

---

### 目录机构
<!-- #region 目录机构 -->
ProjectRoot/
│
<!-- #region Assets (根目录) -->
├── Assets/                             # Unity 主目录
<!-- #region Data (数据) -->
│   ├── Data/                           # 数据
│   │   ├── Audio/
│   │   │   ├── BGM/                    # 背景音乐
│   │   │   │   ├── …….mp3
│   │   │   │   └── …….mp3
│   │   │   ├── SFX/                    # 音效
│   │   │   │   ├── …….wav
│   │   │
│   │   ├── Configs/                    # 配置文件
│   │   │   ├── HexMapConfig.asset      # 六边形参数配置文件
│   │   │
│   │   ├── Palettes/                   # 平铺调色板 (Tile Palette)
│   │   │   ├── Hex Palette             # 调色板 (预制件资产)
│   │   │
│   │   ├── Sprites/                    # 精灵（2D 图片）
│   │   │   ├── Cursor/                 # 光标相关
│   │   │   │   ├── HexHilight.png      # 鼠标位置是 可选瓦片 时的图片
│   │   │   │   ├── HexHilightInvalid.png # 鼠标位置是 不可选瓦片 时的图片
│   │   │   │   ├── x2_CurDefault.png   # 游戏地图页面 鼠标光标图片
│   │   │   │   ├── x2…….png            # 其他鼠标光标图片
│   │   │   │
<!-- #region Iteams (游戏内物料) -->
│   │   │   ├── Iteams/                 # 游戏内物料
│   │   │   │   ├── CharLook /            # 人物服饰外观 (Character Costume Look)
│   │   │   │   │   ├── CreItm_0_Hat/         # 头部装备 (Costume Item Hat)
│   │   │   │   │   │   ├── Hat/              # 头部
│   │   │   │   │   │   ├── Face/             # 面部
│   │   │   │   │   │   └── Eye/              # 眼部
│   │   │   │   │   ├── CreItm_1_UpperBody/   # 上身装备 (Costume Item UpperBody)
│   │   │   │   │   ├── CreItm_2_LowerBody/   # 下身装备 (Costume Item LowerBody)
│   │   │   │   │   ├── CreItm_3_Overcoat/    # 外套装备 (Costume Item Overcoat)
│   │   │   │   │   ├── CreItm_4_Backpacks/   # 包类装备 (Costume Item BackpacksLike)
│   │   │   │   │   ├── CreItm_5_Sash/        # 腰部装备 (Costume Item Sash)
│   │   │   │   │   ├── CreItm_6_Held/        # 持有装备 (Costume Item Held)
│   │   │   │   │   │   ├── CanShoulder/      # 可肩背
│   │   │   │   │   │   └── Handheld/         # 手持装备
│   │   │   │   │   ├── CreItm_7_Foot/        # 脚本装备 (Costume Item Foot)
<!-- #endregion -->
│   │   │
│   │   │── Tiles/                      # 瓦片
│   │   │   ├── HexDay_01_Ocean         # 深海瓦片
│   │   │   ├── HexDay_02……36           # 其他地形瓦片
<!-- #endregion -->
│   │
<!-- #region Scenes (场景) -->
│   ├── Scenes/                         # Unity 场景
│   │   ├── SampleScene.unity           # 示例场景 (TODO)
<!-- #endregion -->
│   │
<!-- #region Scripts (脚本) -->
│   ├── Scripts/                        # 所有 C# 脚本
│   │   ├── Configs/                    # 配置相关
│   │   │   ├── HexMapConfig.cs         # ScriptableObject 类 生成 六边形参数的配置文件
│   │   │   ├── GridController.cs       # MonoBehaviour 类 定义 根据配置文件控制 Grid 对象
│   │   │
│   │   ├── Data/                       # 数据相关 (枚举、结构体、常量)
│   │   │   └── Enums/                  # 枚举相关
│   │   │       └── TerrainEnums.cs     # 地形枚举 (TODO)
│   │   │   
│   │   ├── Gameplay/                   # 逻辑相关 (鼠标、玩家、地图)
│   │   │   ├── Cursor/                 # 光标相关
│   │   │   │   ├── CursorhexFollower.cs # MonoBehaviour 类 定义 跟随当前鼠标位置的六边形边框 以当前鼠标光标所在 瓦片(Tile) 中心为中心
│   │   │   │   ├── CustomCursor.cs     # MonoBehaviour 类 定义 修改鼠标光标的图案
│   │   │   │
│   │   │   ├── Player/                 # Player逻辑脚本
│   │   │   │   ├── PlayerMovement.cs   # 玩家移动逻辑
│   │   │   │
│   │   │   ├── Tiles/                  # 瓦片脚本
│   │   │   │   ├── TerrainTile.cs      # 瓦片地形类 (TODO)
│   │   │
│   │   ├── Utils/                      # 工具类
│   │   │   ├── HexUtils.cs         # 六边形工具箱函数 (计算距离、计算位置)
<!-- #endregion -->
│   │
│   └── Prefabs/                 # 预制件
│       ├── Player.prefab
│       ├── TileCursor.prefab
<!-- #endregion -->
│
└── Extras/                      # 额外素材（Unity 未加载 && 通过 .gitignore 忽略 不会上传）
    ├── Img/                     # 参考图片/美术资源
    ├── Audio/                   # 原始音效素材
<!-- #endregion -->

---

### 图层排序
<!-- #region Sorting Layer (排序图层) -->
| Sorting | Layer Name | 描述 |
| ---- | ---- | ---- |
| 00 | Default | 默认 |
| 01 | TileMap | 瓦片地图 |
| 02 | Null | 待定 |
| 03 | TileScavHint | 拾荒信息: <br> 1. 是否可探索 <br>&emsp; 是否存在物品 <br>&emsp; 行踪标记 (NPCMark) <br>&emsp; 地块高亮追踪器 (HexTileHilightFollowerCursor) |
| 04 | Null | 待定 |
| 05 | CharLookModel | 人物外观模型:<br> 1. 人物右手持物外观(CharLook_HandheldR) <br> 2. 人物肩部外观 (CharLook_Shoulder) <br>&emsp; 人物背包外观 (CharLook_Backpacks) <br> 3. 人物模型 (CharModel) <br> 4. 人物脚部外观 (CharLook_Foot) <br>&emsp; 人物脸部外观 (CharLook_Face) <br> 5. 人物下身外观 (CharLook_LowerBody) <br>&emsp; 人物眼部外观 (CharLook_Eye) <br> 6. 人物上身外观 (CharLook_UpperBody) <br>&emsp; 人物头盔外观 (CharLook_Hat) <br> 7. 人物肩部外观 (CharLook_Sash) <br> 8. 人物腰部外观 (CharLook_Overcoat) <br> 9. 人物左手持物外观 (CharLook_HandheldL) |
| 06 | Null | 待定 |
| 07 | TileMapHint | 地块: <br> 1. 地标标记 (TileTags) |
| 09 | Null | 待定 |
| 11 | UI | UI界面 <br> 1. 天气状态 (UIWeatherStatus) <br> 2. 鼠标悬停处瓦片提示信息 (UITileHint) |
| 13 | Null | 待定 |
| 15 | Cursor | 鼠标光标: <br> 1. 鼠标光标皮肤 (CursorSkin) <br> 2. 光标悬停处详情 (CursorDetail)|
<!-- #endregion -->