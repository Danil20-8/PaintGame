using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class VectorHelpers
{
    public static Vector4 Abs(this Vector4 vector4)
    {
        return new Vector4(Mathf.Abs(vector4.x), Mathf.Abs(vector4.y), Mathf.Abs(vector4.z), Mathf.Abs(vector4.w));
    }
    public static Vector3 Abs(this Vector3 vector3)
    {
        return new Vector3(Mathf.Abs(vector3.x), Mathf.Abs(vector3.y), Mathf.Abs(vector3.z));
    }
    public static Vector2 Abs(this Vector2 vector2)
    {
        return new Vector2(Mathf.Abs(vector2.x), Mathf.Abs(vector2.y));
    }
    public static Vector2 YX(this Vector2 vector2)
    {
        return new Vector2(vector2.y, vector2.x);
    }
    public static Vector2 YX(this Vector3 vector3)
    {
        return new Vector2(vector3.y, vector3.x);
    }
    public static Vector2 XZ2XY(this Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.z);
    }
    public static Vector3 XZ(this Vector2 vector2)
    {
        return new Vector3(vector2.x, 0, vector2.y);
    }
    public static Vector3 X0Z(this Vector3 vector3)
    {
        return new Vector3(vector3.x, 0, vector3.z);
    }
    public static Vector3 V0YZ(this Vector3 vector3)
    {
        return new Vector3(0, vector3.y, vector3.z);
    }
    public static Vector3 SetXZ(this Vector3 self, Vector2 vector2)
    {
        return new Vector3(vector2.x, self.y, vector2.y);
    }
}

