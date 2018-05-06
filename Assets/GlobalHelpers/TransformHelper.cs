using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class TransformHelper
{
    public static bool IsOne(this Transform transform, Transform parent)
    {
        while(transform != null)
        {
            if (transform == parent)
                return true;
            transform = transform.parent;
        }
        return false;
    }
}
