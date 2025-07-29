using System;

public static class RandomHelper
{
    private static readonly Random _random = new Random();

    /// ������� 0 �� 1��
    public static int GetRandomZeroOrOne()
    {
        return _random.Next(2);
    }

    // ������� 0~1 ��С�� [0.0, 1.0)
    public static double GetRandomDouble() {
        return _random.NextDouble();
    }


    /// ����ָ����Χ�ڵ�������� [min, max] (����min��max)��
    public static int Range(int min, int max)
    {
        // Next(min, max + 1) ���Ͻ��ǡ����������ģ�����Ҫ+1
        return _random.Next(min, max + 1);
    }

    /// ����ָ����Χ�ڵ���������� [min, max]��
    public static float Range(float min, float max)
    {
        return (float)(_random.NextDouble() * (max - min) + min);
    }
}