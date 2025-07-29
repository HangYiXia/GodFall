using System;

public static class RandomHelper
{
    private static readonly Random _random = new Random();

    /// 随机返回 0 或 1。
    public static int GetRandomZeroOrOne()
    {
        return _random.Next(2);
    }

    // 随机返回 0~1 的小数 [0.0, 1.0)
    public static double GetRandomDouble() {
        return _random.NextDouble();
    }


    /// 返回指定范围内的随机整数 [min, max] (包含min和max)。
    public static int Range(int min, int max)
    {
        // Next(min, max + 1) 的上界是“不包含”的，所以要+1
        return _random.Next(min, max + 1);
    }

    /// 返回指定范围内的随机浮点数 [min, max]。
    public static float Range(float min, float max)
    {
        return (float)(_random.NextDouble() * (max - min) + min);
    }
}