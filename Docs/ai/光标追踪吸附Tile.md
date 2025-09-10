## 光标追踪吸附Tile
## 询问文本
询问的 AI
``` text
如何让一组图片 
也间接跟随鼠标光标 
但是的中心点始终是离光标最近的平顶六边形tile的中心 
其中在中心瓦片（角色所在的瓦片）和周围6格范围外时为图片1（不可选的移动点） 
在中心瓦片地图的周围6格范围内（角色所在周围六格）为图片2（可选的移动点） 
判断
依据是之前的脚本 

Vector2Int WorldToAxialRound(Vector2 world)
{
    // 归一化 y，再用平顶标准公式 
    float x = world.x;
    float y = world.y; // 如果 H 不等于标准高度，先归一化 y 
    float yPrime = y * scaleY; // scaleY = (sqrt(3)*s)/H 
    float qf = (2f / 3f) * x / s;
    float rf = (-1f / 3f) * x / s - (1f / Mathf.Sqrt(3f)) * yPrime / s;
    Debug.Log($"Fractional q, r: {qf}, {rf}");
    return CubeRound_Axial(qf, rf);
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


```

---

## 回答
明白了，你希望在 Unity 中实现一种效果：鼠标移动时，有两组图片跟随鼠标，但是它们的位置要“吸附”到离鼠标最近的平顶六边形瓦片的中心点，同时根据与角色中心瓦片的距离显示不同的图片。我们可以在你的现有 WorldToAxialRound 与 CubeRound_Axial 基础上实现。

下面我整理一个完整思路，并给出示例代码：

---

### 一、基本思路

1. **获取鼠标在世界坐标的位置**

``` csharp
Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
mouseWorldPos.z = 0; // 保证在2D平面

```

2. **将鼠标位置转换为最近六边形格子坐标 (q, r)**
你已有函数：
``` csharp
Vector2Int tileCoord = WorldToAxialRound(mouseWorldPos);

```

3. **获取该瓦片的世界中心坐标**
公式（平顶六边形，边长 s = 1.06，高度 h = 0.39）：
``` csharp
Vector2 HexAxialToWorld(int q, int r)
{
    float x = s * (3f/2f * q);
    float y = s * Mathf.Sqrt(3)/2 * q + s * Mathf.Sqrt(3) * r;
    return new Vector2(x, y);
}

```

4. **计算与角色瓦片的距离**（六边形距离，六方向网格距离）

``` csharp
int HexDistance(Vector2Int a, Vector2Int b)
{
    int dq = a.x - b.x;
    int dr = a.y - b.y;
    int ds = -dq - dr;
    return Mathf.Max(Mathf.Abs(dq), Mathf.Abs(dr), Mathf.Abs(ds));
}

```

5. **判断使用图片1还是图片2**

``` csharp
int dist = HexDistance(tileCoord, playerTileCoord);
if (dist == 0 || dist > 6)
    image.sprite = sprite1; // 不可移动点
else
    image.sprite = sprite2; // 可移动点

```

6. **让图片跟随瓦片中心**

``` csharp
image.transform.position = HexAxialToWorld(tileCoord.x, tileCoord.y);

```

---

### 二、示例完整代码

``` csharp
public class HexCursorFollow : MonoBehaviour
{
    public float s = 1.06f; // 六边形边长
    public Sprite sprite1;   // 不可移动点
    public Sprite sprite2;   // 可移动点
    public Transform player; // 玩家Transform
    public Image cursorImage;

    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        Vector2Int tileCoord = WorldToAxialRound(mouseWorldPos);
        Vector2Int playerTileCoord = WorldToAxialRound(player.position);

        int dist = HexDistance(tileCoord, playerTileCoord);

        if (dist == 0 || dist > 6)
            cursorImage.sprite = sprite1;
        else
            cursorImage.sprite = sprite2;

        cursorImage.transform.position = HexAxialToWorld(tileCoord.x, tileCoord.y);
    }

    Vector2 HexAxialToWorld(int q, int r)
    {
        float x = s * 3f/2f * q;
        float y = s * Mathf.Sqrt(3)/2 * q + s * Mathf.Sqrt(3) * r;
        return new Vector2(x, y);
    }

    int HexDistance(Vector2Int a, Vector2Int b)
    {
        int dq = a.x - b.x;
        int dr = a.y - b.y;
        int ds = -dq - dr;
        return Mathf.Max(Mathf.Abs(dq), Mathf.Abs(dr), Mathf.Abs(ds));
    }

    Vector2Int WorldToAxialRound(Vector2 world)
    {
        float x = world.x;
        float y = world.y;
        float scaleY = Mathf.Sqrt(3) * s / 0.39f; // 归一化
        float yPrime = y * scaleY;
        float qf = (2f / 3f) * x / s;
        float rf = (-1f / 3f) * x / s - (1f / Mathf.Sqrt(3f)) * yPrime / s;
        return CubeRound_Axial(qf, rf);
    }

    Vector2Int CubeRound_Axial(float qf, float rf)
    {
        float xf = qf, zf = rf, yf = -xf - zf;
        int xr = Mathf.RoundToInt(xf);
        int yr = Mathf.RoundToInt(yf);
        int zr = Mathf.RoundToInt(zf);
        float xDiff = Mathf.Abs(xr - xf);
        float yDiff = Mathf.Abs(yr - yf);
        float zDiff = Mathf.Abs(zr - zf);
        if (xDiff > yDiff && xDiff > zDiff) xr = -yr - zr;
        else if (yDiff > zDiff) yr = -xr - zr;
        else zr = -xr - yr;
        return new Vector2Int(xr, zr);
    }
}

```

---

### 三、效果：

- 鼠标移动时，图片自动吸附到最近瓦片中心

- 图片类型根据距离角色中心瓦片的范围显示不同

- 可支持多组图片，只需重复调用 cursorImage.sprite = ... 和 transform.position = ...
