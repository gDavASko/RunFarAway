using System.Collections.Generic;
using UnityEngine;

namespace KBP.CORE
{

    public static class Extensions
    {
        public static List<T> ToList<T>(this T[] array)
        {
            return new List<T>(array);
        }
        
        public static string Color(this string need, Color color)
        {
            return "<color=" + color.ToHex() + ">" + need + "</color>";
        }
        
        public static string ToHex(this Color color)
        {
            string rtn = "#" + ((int)(color.r * 255)).ToString("X2") + ((int)(color.g * 255)).ToString("X2") + ((int)(color.b * 255)).ToString("X2");
            return rtn;
        }
    }
}