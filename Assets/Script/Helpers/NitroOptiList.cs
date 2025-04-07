using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NitroInfo
{
    public float WorldX;
    public float WorldY;
    public int Value;
    public Sprite Sprite;
}

[Serializable]
public class NitroByPositionList
{
    [SerializeField] private List<NitroInfo> sortedList = new();

    public IReadOnlyList<NitroInfo> List => sortedList;
    
    // Добавление с поддержанием порядка
    public void Add(NitroInfo item)
    {
        int index = sortedList.BinarySearch(item, Comparer<NitroInfo>.Create((a, b) => b.WorldY.CompareTo(a.WorldY)));
        if (index < 0) index = ~index;
        sortedList.Insert(index, item);
    }

    public void Remove(NitroInfo item)
    {
        int index = sortedList.BinarySearch(item, Comparer<NitroInfo>.Create((a, b) => b.WorldY.CompareTo(a.WorldY)));
        sortedList.RemoveAt(index);
    }

    private int LowerBound(float value)
    {
        int left = 0, right = sortedList.Count;
        while (left < right)
        {
            int mid = (left + right) / 2;
            if (sortedList[mid].WorldY > value) // Больше — выше, идем вправо
                left = mid + 1;
            else
                right = mid;
        }
        return left;
    }

    private int UpperBound(float value)
    {
        int left = 0, right = sortedList.Count;
        while (left < right)
        {
            int mid = (left + right) / 2;
            if (sortedList[mid].WorldY >= value) // >= → тоже идем вправо
                left = mid + 1;
            else
                right = mid;
        }
        return left;
    }

    // Получение всех элементов в диапазоне [min..max]
    public (int from, int to) FindRange(float min, float max)
    {
        int to = LowerBound(min);
        int from = UpperBound(max);
        return (from, to);
    }
}