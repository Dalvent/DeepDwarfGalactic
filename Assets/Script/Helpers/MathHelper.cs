using UnityEngine;

namespace Script.Helpers
{
    public static class MathHelper
    {
        public static Vector3 ToVector3(this Vector2 vector2)
        {
            return vector2;
        }
        
        public static Vector3 NegX(this Vector3 vector3)
        {
            return new Vector3(-Mathf.Abs(vector3.x), vector3.y, vector3.z);
        }
        
        public static Vector3 PosX(this Vector3 vector3)
        {
            return new Vector3(Mathf.Abs(vector3.x), vector3.y, vector3.z);
        }    
        public static Vector3 SetY(this Vector3 vector3, float y)
        {
            return new Vector3(vector3.x, y, vector3.z);
        }
    }
}