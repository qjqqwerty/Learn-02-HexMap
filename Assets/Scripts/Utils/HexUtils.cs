using UnityEngine;

/// <summary>
/// 平顶六边形工具类，提供坐标转换方法
/// </summary>
public static class HexUtils
{
    // 六边形尺寸
    private static HexMapConfig config;

    public static void Init(HexMapConfig cfg)
    {
        config = cfg;
    }

    /// <summary>
    /// 世界坐标 → axial 坐标并四舍五入到最近瓦片
    /// </summary>
    public static Vector2Int WorldToAxialRound(Vector2 world)
    {
        float s = config.S;
        float scaleY = config.ScaleY;

        float x = world.x;
        float y = world.y;
        float yPrime = y * scaleY;

        float qf = (2f / 3f) * x / s;
        float rf = (-1f / 3f) * x / s - (1f / Mathf.Sqrt(3f)) * yPrime / s;
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log($"Fractional q, r: {qf}, {rf}");
        }

        return CubeRound_Axial(qf, rf);
    }

    /// <summary>
    /// cube 坐标四舍五入到最近整数 axial 坐标
    /// </summary>
    private static Vector2Int CubeRound_Axial(float qf, float rf)
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
            xr = -yr - zr;
        else if (yDiff > zDiff)
            yr = -xr - zr;
        else
            zr = -xr - yr;

        return new Vector2Int(xr, zr);
    }

    /// <summary>
    /// axial 坐标 → 世界坐标
    /// </summary>
    public static Vector2 AxialToWorld(Vector2Int hex)
    {
        float s = config.S;
        float tileHeight = config.tileHeight;

        int q = hex.x;
        int r = hex.y;

        float x = s * 3f / 2f * q;
        float y = -tileHeight / (Mathf.Sqrt(3f) * s) * Mathf.Sqrt(3f) * s * (r + q / 2f);
        return new Vector2(x, y);
    }
}
