## Unity 六边形地图鼠标控制移动
## 询问文本
询问的 AI
``` text
现在我有一个Unity 平顶的六边形地图 

游戏角色初始点在地图中心 即坐标（0，0） 左侧上侧是负数
宽1高0.4 
如何设计用鼠标移动 保证点击时 能判定准确是周围六个格子或自身格子 
以此为基础 点击周6格时 移动到 对应格中心点 并打印log 
log格式 Mouse Position: (0.61,0.13), Player Position: (0.00, 0.00), qr: (1, -1) 
并希望玩家移动到右上角
给我详细说明平顶六边形 如何算是判断一个格子的数学公式 
尽可能多结合坐标系图 
说明什么是q，r 点击自身格时 额外打印 这里是当前
```

---

## 回答
下面给你一套“平顶六边形（flat-topped）”在 Unity 中用鼠标点击→判定本格/六邻格→移动到格中心并打印日志的完整思路与代码。示例默认格子宽度 W = 1、高度 H = 0.4，角色初始在(0,0) 且左/上为负。我同时给出数学公式、坐标系图示与可直接用的 C# 脚本。

---

### 一、坐标系与 q、r 的含义（轴坐标 Axial）

平顶六边形常用 axial（轴坐标） 表示一个格子：

- q 轴：水平方向（→），每 +1 往正右走一格。

- r 轴：右下方向（↘），每 +1 往右下走一格。

- 还有一个隐含轴 s = -q - r（↖），用于计算距离与舍入，但你通常不直接存它。

六个邻居（按顺时针）：

```scss
E(右)      : (q+1, r+0)
NE(右上)   : (q+1, r-1)
NW(左上)   : (q+0, r-1)
W(左)      : (q-1, r+0)
SW(左下)   : (q-1, r+1)
SE(右下)   : (q+0, r+1)

```

> “右上角”= (1, -1) —— 你希望玩家移动到右上，就意味着点击选中的轴坐标是 (q, r) = (1, -1)。

简单示意（中心为 0,0）：
``` markdown
        (0,-1)      (1,-1)
            \      /
(-1,0)  ——> (0,0) ——> (1,0)
            /      \
       (-1,1)      (0,1)

```

---

### 二、从世界坐标 (x,y) ⇄ 轴坐标 (q,r) 的通用公式（平顶）

>关键点：你的 W=1, H=0.4 不是正六边形的标准比例（标准应为 H = √3/2 * W ≈ 0.866 * W）。
因此需要先对 y 轴做一次“归一化缩放”，才能用标准公式做格子判定，保证点击稳定、边界不“漏判”。

设：

- W = 六边形宽（两对平边之间的距离）

- H = 六边形高

- s = W / 2（等价于六边形的“外接半径”）

- scaleY = (√3 * s) / H = (√3 * W / 2) / H（把你实际的纵向比例拉回到正六边形）

对鼠标/物体的世界坐标 (x, y)：

1. 先把 y 归一化：
y' = y * scaleY

2. 用平顶的标准“世界→轴坐标”公式（浮点值）：
``` ini
x_center = 0.75 * W * q
y_center = H * ( r + q/2 )
```

3. **立方体坐标舍**入得到整数 (q, r)（用 s = -q-r 作误差最大维的回调修正）。
反过来，若已知整数 (q, r)，要得到该格中心点的世界坐标（直接是你当前的非标准比例，不需要再缩放）：
``` ini
x_center = 0.75 * W * q
y_center = H * ( r + q/2 )

```
>这两个式子非常好记：横向步长始终是 0.75 * W；纵向是按 H 叠加且列与列之间偏移 q/2 个 H。

---

### 三、只允许“自身 + 六邻格”的判定
把点击得到的 (q, r) 与玩家当前 (q0, r0) 比较，计算“六边形距离”：
``` ini
dx = q - q0
dz = r - r0
dy = -dx - dz
dist = max(|dx|, |dy|, |dz|)

```
仅当 dist <= 1 时才认为本次点击合法（是自身或六邻格）；否则忽略。
>如果点击的是自身 (q, r) == (q0, r0)，除了打印日志，再额外打印一句：“这里是当前”。

---

### Unity 代码（可直接挂到一个空物体上）

- 把玩家 Transform 拖到 player 字段。

- 设定 W=1, H=0.4（或你的项目实际值）。

- 角色初始在 (0,0)，则初始轴坐标为 q0=0, r0=0。

- 左键点地：会自动判定是否为自身/邻格；若是，就把玩家移动到对应格中心并打印你需要的日志格式。

``` csharp
using UnityEngine;

public class FlatTopHexClickMover : MonoBehaviour
{
    [Header("Hex metrics (flat-topped)")]
    public float W = 1f;   // hex width
    public float H = 0.4f; // hex height (can be non-regular)
    public Transform player;

    // 当前玩家所在格（轴坐标）
    public Vector2Int playerQR = new Vector2Int(0, 0);

    // 预计算
    float s;        // = W/2
    float scaleY;   // = (sqrt(3)*s)/H
    Camera cam;

    void Awake()
    {
        cam = Camera.main;
        s = W * 0.5f;
        scaleY = (Mathf.Sqrt(3f) * s) / H;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 1) 屏幕→世界
            Vector3 mws = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mouseWorld = new Vector2(mws.x, mws.y);

            // 2) 世界→轴坐标(整数)
            Vector2Int clickQR = WorldToAxialRound(mouseWorld);

            // 3) 仅允许 自身/六邻格
            if (HexDistance(playerQR, clickQR) <= 1)
            {
                // 4) 目标格中心点（世界坐标）
                Vector2 target = AxialToWorldCenter(clickQR);

                // 打印日志（按你的格式）
                Debug.Log(
                    $"Mouse Position: ({mouseWorld.x:F2},{mouseWorld.y:F2}), " +
                    $"Player Position: ({player.position.x:F2}, {player.position.y:F2}), " +
                    $"qr: ({clickQR.x}, {clickQR.y})"
                );

                // 自身格点击：额外提示
                if (clickQR == playerQR)
                {
                    Debug.Log("这里是当前");
                }

                // 5) 移动（示例：瞬移；你也可以改成协程/Lerp）
                if (player != null)
                {
                    player.position = new Vector3(target.x, target.y, player.position.z);
                }

                // 6) 更新玩家当前格
                playerQR = clickQR;
            }
            else
            {
                // 非自身/邻格：忽略或提示
                // Debug.Log("超出可点击范围（仅自身+六邻格）");
            }
        }
    }

    // 世界→轴坐标(四舍五入到最近格)
    Vector2Int WorldToAxialRound(Vector2 world)
    {
        // 归一化 y，再用平顶标准公式
        float x = world.x;
        float yPrime = world.y * scaleY;

        float qf = (2f / 3f) * x / s;
        float rf = (-1f / 3f) * x / s + (1f / Mathf.Sqrt(3f)) * yPrime / s; // 此处 ± 可以修改轴方向

        return CubeRound_Axial(qf, rf);
    }

    // 轴坐标→该格中心（世界坐标）
    Vector2 AxialToWorldCenter(Vector2Int qr)
    {
        int q = qr.x;
        int r = qr.y;

        float x = 0.75f * W * q;
        float y = H * (r + 0.5f * q);
        return new Vector2(x, y);
    }

    // 立方体舍入：把(qf, rf)转为最邻近的整数(q, r)
    Vector2Int CubeRound_Axial(float qf, float rf)
    {
        float xf = qf;
        float zf = rf;
        float yf = -xf - zf;

        int xr = Mathf.RoundToInt(xf);
        int yr = Mathf.RoundToInt(yf);
        int zr = Mathf.RoundToInt(zf);

        float xDiff = Mathf.Abs(xr - xf);
        float yDiff = Mathf.Abs(yr - yf);
        float zDiff = Mathf.Abs(zr - zf);

        if (xDiff > yDiff && xDiff > zDiff)
        {
            xr = -yr - zr;
        }
        else if (yDiff > zDiff)
        {
            yr = -xr - zr;
        }
        else
        {
            zr = -xr - yr;
        }

        return new Vector2Int(xr, zr); // (q, r)
    }

    // 六边形距离（仅用于限制“自身+六邻格”）
    int HexDistance(Vector2Int a, Vector2Int b)
    {
        int dx = a.x - b.x;
        int dz = a.y - b.y;
        int dy = -dx - dz;
        return Mathf.Max(Mathf.Abs(dx), Mathf.Abs(dy), Mathf.Abs(dz));
    }

    // 后面的没有用到 
    // 后面的没有用到 
    // 后面的没有用到 
    // （可选）在场景里可视化当前邻居 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        // 画出玩家所在格中心
        Vector2 c0 = AxialToWorldCenter(playerQR);
        Gizmos.DrawWireSphere(new Vector3(c0.x, c0.y, 0), 0.03f);

        // 六邻格 
        Vector2Int[] dirs = {
            new Vector2Int( 1,  0), // 右
            new Vector2Int( 1, -1), // 右上
            new Vector2Int( 0, -1), // 左上
            new Vector2Int(-1,  0), // 左
            new Vector2Int(-1,  1), // 左下
            new Vector2Int( 0,  1), // 右下
        };
        Gizmos.color = Color.yellow;
        foreach (var d in dirs)
        {
            Vector2 c = AxialToWorldCenter(playerQR + d);
            Gizmos.DrawWireSphere(new Vector3(c.x, c.y, 0), 0.025f);
        }
    }
}

```

---

### 五、关于你给的日志示例与“右上角”

- 右上角邻居的轴坐标固定为 (1, -1)。

- 当你点击落在这个邻居所属的区域，日志就会打印：
qr: (1, -1)，并且玩家会移动到 AxialToWorldCenter(1,-1) 对应的中心点。

- 若你点击的是玩家当前所在格（比如初始 (0,0)），除正常日志外，会额外打印：“这里是当前”。

---

### 六、踩坑与校准

1. 因为你的 H=0.4 比标准比例扁很多，如果不做 scaleY 归一化，点击边缘处容易出错/抖动。

2. 如果你换了格子尺寸，只需改 W/H，其它公式与代码不用变。

3. 若你使用了相机缩放或不同世界缩放，也不受影响，因为我们直接用世界坐标与相机 ScreenToWorldPoint。