using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public static class VectorListHelper
{
    public static Vector3 Average(this IEnumerable<Vector3> list)
    {
        Vector3 sum = new Vector3();
        int count = 0;

        foreach (var v in list)
        {
            sum += v;
            ++count;
        }

        return sum / count;
    }

    public static Vector3 Center(this IEnumerable<Vector3> list)
    {
        IEnumerator<Vector3> cur = list.GetEnumerator();
        cur.MoveNext();
        Vector3 curValue = cur.Current;

        Vector3 min = curValue;
        Vector3 max = curValue;

        while(cur.MoveNext() != false)
        {
            curValue = cur.Current;

            min = new Vector3(Mathf.Min(min.x, curValue.x), Mathf.Min(min.y, curValue.y), Mathf.Min(min.z, curValue.z));
            max = new Vector3(Mathf.Max(max.x, curValue.x), Mathf.Max(max.y, curValue.y), Mathf.Max(max.z, curValue.z));
        }

        return (max + min) / 2;
    }
}

