using System.Collections.Generic;
using UnityEngine;

namespace ChatFight
{
    public static class List
    {
        public static T GetRandom<T>(this List<T> list)
        {
            Debug.Assert(list.IsNullOrEmpty() == false, "List can't be null or empty");
            return list[Random.Range(0, list.Count)];
        }

        public static T ExtractRandom<T>(this List<T> list)
        {
            Debug.Assert(list.IsNullOrEmpty() == false, "List can't be null or empty");
            int index = Random.Range(0, list.Count);
            T item = list[index];
            list.RemoveAt(index);

            return item;
        }

        public static List<T> ExtractRandoms<T>(this List<T> list, int amount)
        {
            List<T> newList = new List<T>();
            while (newList.Count < amount && list.IsNullOrEmpty() == false)
            {
                newList.Add(list.ExtractRandom());
            }
            return newList;
        }

        public static List<T> GetDistinctRandoms<T>(this List<T> list, int amount)
        {
            Debug.Assert(list.IsNullOrEmpty() == false, "List can't be null or empty");
            Debug.Assert(amount >= 0, "Can't request a negative amount: " + amount);

            List<T> newList = new List<T>(amount);
            List<T> tempList = new List<T>(list);

            // Try and get random numbers
            for (int i = 0; i < amount; ++i)
            {
                if (tempList.IsNullOrEmpty())
                {
                    break;
                }
                // Extract the random item
                var element = ExtractRandom(tempList);
                newList.Add(element);
            }
            return newList;
        }

        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            return (list == null) || (list.Count == 0);
        }

        public static void RemoveIfContained<T>(this List<T> list, T item)
        {
            Debug.Assert(list != null, "List can't be null");
            if (list.Contains(item))
            {
                list.Remove(item);
            }
        }

        public static void RemoveIfContained<T>(this List<T> list, List<T> items)
        {
            Debug.Assert(list != null, "List can't be null");
            foreach (var item in items)
            {
                list.RemoveIfContained(item);
            }
        }

        public static List<T> RemoveDuplicates<T>(this List<T> list)
        {
            Debug.Assert(list.IsNullOrEmpty() == false, "List can't be null or empty");
            List<T> newList = new List<T>(list.Count);

            foreach (T item in list)
            {
                if (newList.Contains(item) == false)
                {
                    newList.Add(item);
                }
            }
            return newList;
        }

        /// Fisher-Yates Shuffle algorithm adapted from:
        /// http://www.dotnetperls.com/fisher-yates-shuffle
        /// 
        public static void Shuffle<T>(this List<T> list)
        {
            Debug.Assert(list != null, "List can't be null.");

            int n = list.Count;
            if (n > 0)
            {
                for (int i = 0; i < n; ++i)
                {
                    // value returns a random number between 0 and 1.
                    // ... It is equivalent to Math.random() in Java.
                    int r = i + (int)(Random.value * (n - i));
                    T t = list[r];
                    list[r] = list[i];
                    list[i] = t;
                }
            }
        }
    }
}
