// Decompiled with JetBrains decompiler
// Type: ArrayExtensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8E6852E2-1B68-460A-A222-84B5FB6C5C57
// Assembly location: C:\Users\m\Downloads\Solgryns-Pokemon-On-Twitch-App-V2.2.6\Solgryns Pokemon On Twitch_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

public static class ArrayExtensions
{
    public static void AddTo2DStringArray(ref string[,] array, params string[] elements)
    {
        string[,] strArray = new string[array.GetLength(0) + 1, array.GetLength(1)];
        for (int index1 = 0; index1 < array.GetLength(0); ++index1)
        {
            for (int index2 = 0; index2 < array.GetLength(1); ++index2)
                strArray[index1, index2] = array[index1, index2];
        }
        for (int index = 0; index < array.GetLength(1); ++index)
            strArray[strArray.GetLength(0) - 1, index] = elements[index];
        array = strArray;
    }

    public static void AddNonDuplicateEntryTo2DStringArray(
      ref string[,] array,
      params string[] elements)
    {
        for (int index1 = 0; index1 < array.GetLength(0); ++index1)
        {
            if (elements[0].ToLower() == array[index1, 0].ToLower())
            {
                for (int index2 = 0; index2 < elements.Length; ++index2)
                    array[index1, index2] = elements[index2];
                return;
            }
        }
        ArrayExtensions.AddTo2DStringArray(ref array, elements);
    }

    public static void Add<T>(ref T[] array, T t)
    {
        int newSize = array.Length + 1;
        Array.Resize<T>(ref array, newSize);
        array[newSize - 1] = t;
    }

    public static bool AddNonpulicate<T>(ref T[] array, T t)
    {
        if (array.Has<T>(t))
            return false;
        ArrayExtensions.Add<T>(ref array, t);
        return true;
    }

    public static bool Has<T>(this T[] array, T t)
    {
        for (int index = 0; index < array.Length; ++index)
        {
            if (array[index].Equals((object)t))
                return true;
        }
        return false;
    }

    public static List<string> ToStringList<T>(this List<T> list)
    {
        List<string> stringList = new List<string>(list.Count);
        foreach (T obj in list)
            stringList.Add(obj.ToString());
        return stringList;
    }

    public static List<string> ToStringList<T>(this T[] list)
    {
        List<string> stringList = new List<string>(list.Length);
        foreach (T obj in list)
            stringList.Add(obj.ToString());
        return stringList;
    }
}
