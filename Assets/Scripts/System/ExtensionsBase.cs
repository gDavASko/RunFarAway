using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RFW
{
    public static class ExtensionsBase
    {
        private static DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        private static float lastTimeGet;
        private static float lastTimeGetTimeStamp;
        private static double lastTime;
        private static double lastTimeTimestamp;

        public static System.Random rnd = null;

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static double ExtractNumber(this Dictionary<string, object> dictionary, string key)
        {
            if (dictionary == null)
            {
                Debug.LogErrorFormat("Dictionary is null! '{0}'", key);
                return 0;
            }

            object obj = null;
            if (!dictionary.TryGetValue(key, out obj))
            {
                Debug.LogWarningFormat("Key was not found '{0}'", key);
                return 0;
            }

            if (obj.GetType() == typeof(string))
            {
                double result;
                if (double.TryParse(obj.ToString(), out result))
                {
                    return result;
                }

                return 0;
            }

            if (obj.GetType() == typeof(int))
            {
                return (int)obj;
            }

            if (obj.GetType() == typeof(long))
            {
                return (long)obj;
            }

            return (double)obj;
        }

        public static T Extract<T>(this Dictionary<string, object> dictionary, string key, T defaultValue = default(T))
        {
            if (dictionary == null)
            {
                Debug.LogWarningFormat("Dictionary is null! '{0}'", key);
                return defaultValue;
            }

            object obj = null;
            if (!dictionary.TryGetValue(key, out obj))
            {
                Debug.LogWarningFormat("Key was not found '{0}'", key);
                return defaultValue;
            }

            T result = defaultValue;
            if (typeof(T).IsEnum)
            {
                result = (T)System.Enum.Parse(typeof(T), (string)obj, true);
            }
            else
            {
                try
                {
                    result = (T)obj;
                }
                catch
                {
                    Debug.LogWarningFormat("Can't convert '{0}' to format '{1}', it '{2}'", key, typeof(T),
                        obj.GetType());
                    return result;
                }
            }

            return result;
        }

        public static List<T> ToList<T>(this T[] array)
        {
            return new List<T>(array);
        }

        public static int Round(this int value, int power)
        {
            int pow = (int)Mathf.Pow(10, power);
            return (int)Mathf.Round(value / pow) * pow;
        }

        public static float Round(this float value, int power)
        {
            float pow = Mathf.Pow(10, power);
            return Mathf.RoundToInt(value * pow) / pow;
        }

        public static double Round(this double value, int power)
        {
            double pow = System.Math.Pow(10, power);
            return System.Math.Round(value * pow) / pow;
        }

        public static T GetOrAdd<T>(this GameObject gameObject) where T : Component
        {
            T comp = gameObject.GetComponent<T>();
            if (comp == null)
            {
                comp = gameObject.AddComponent<T>();
            }

            return comp;
        }

        public static Color SetAlpha(this Color color, float a)
        {
            return new Color(color.r, color.g, color.b, a);
        }

        public static void SetLayer(this GameObject gameObject, int layer, bool withChild = true)
        {
            if (gameObject == null)
            {
                return;
            }

            gameObject.layer = layer;

            if (withChild)
            {
                foreach (Transform child in gameObject.transform)
                {
                    child.gameObject.SetLayer(layer);
                }
            }
        }

        public static T Get<T>(this object[] parameters, int index = 0)
        {
            T result = default(T);
            int currentIndex = -1;
            System.Type targetType = typeof(T);
#if NETFX_CORE
            TypeInfo targetTypeInfo = targetType.GetTypeInfo();
#else
            System.Type targetTypeInfo = targetType;
#endif
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i] == null)
                {
                    continue;
                }
#if NETFX_CORE
                TypeInfo type = parameters[i].GetType().GetTypeInfo();
#else
                System.Type type = parameters[i].GetType();
#endif
                if (targetTypeInfo.IsAssignableFrom(type) ||
                    type.IsAssignableFrom(targetTypeInfo) ||
                    type.IsSubclassOf(targetType))
                {
                    currentIndex++;
                    if (currentIndex == index)
                    {
                        result = (T)parameters[i];
                        break;
                    }
                }
            }

            return result;
        }

        public static void SetActive(this List<GameObject> list, bool isActive)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != null)
                {
                    list[i].SetActive(isActive);
                }
            }
        }


        #region RND

        public static T RandomElement<T>(this IEnumerator<T> collection)
        {
            if (collection == null)
            {
                return default(T);
            }

            int count = 0;
            while (collection.MoveNext())
            {
                count++;
            }

            if (count == 0)
            {
                return default(T);
            }

            collection.Reset();
            int randomElement = Random.Range(0, count + 1);
            while (randomElement > 0 && collection.MoveNext())
            {
                randomElement--;
            }

            return (T)collection.Current;
        }

        public static T RandomWeightElement<T>(this List<T> parameters, List<int> weights = null)
        {
            if (parameters == null || parameters.Count == 0)
            {
                return default(T);
            }

            if (weights == null)
            {
                weights = new List<int>() { 200, 100, 50, };
            }

            int needCount = parameters.Count - weights.Count;
            if (needCount > 0)
            {
                for (int i = 0; i < needCount; i++)
                {
                    weights.Add(10);
                }
            }
            else
            {
                for (int i = 0; i < needCount; i++)
                {
                    weights.RemoveAt(weights.Count - 1);
                }
            }

            int summ = 0;
            for (int i = 0; i < weights.Count; i++)
            {
                summ += weights[i];
            }

            int random = Random.Range(0, summ);
            for (int i = 0; i < weights.Count; i++)
            {
                summ -= weights[i];
                if (summ <= random)
                {
                    return parameters[i];
                }
            }

            return parameters[Random.Range(0, parameters.Count)];
        }

        public static T RandomElement<T>(this List<T> parameters, int startIndex = 0)
        {
            int count = parameters.Count;
            if (parameters == null || count == 0)
            {
                return default;
            }

            return parameters[Random.Range(Mathf.Min(count - 1, startIndex), count)];
        }

        public static T RandomElement<T>(this List<T> parameters, int min, int max)
        {
            if (parameters == null || parameters.Count == 0)
            {
                return default(T);
            }

            return parameters[Random.Range(Mathf.Max(0, min), Mathf.Min(parameters.Count, max))];
        }

        public static T RandomElement<T>(this T[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                return default(T);
            }

            return (T)parameters[Random.Range(0, parameters.Length)];
        }

        public static int GetRandom(int val, bool bothSignes = false)
        {
            InitRandom();
            return rnd.Next(val) * (bothSignes ? (rnd.Next(2) == 0 ? -1 : 1) : 1);
        }

        public static T GetAtRandom<T>(this List<T> list, int minIndex = 0, int maxIndex = int.MaxValue,
            bool remove = false)
        {
            if (list.Count == 0)
                return default(T);
            if (minIndex > list.Count || maxIndex < 0)
                return default(T);
            int realMax = Mathf.Min(maxIndex, list.Count);
            int realMin = Mathf.Max(minIndex, 0);
            realMax = realMax - realMin;
            int rnd = GetRandom(realMax);
            T val = list[realMin + rnd];
            if (remove)
                list.Remove(val);
            return val;
        }

        public static List<T> GetRandomElements<T>(this List<T> list, int count)
        {
            if (list.Count == 0)
            {
                return new List<T>();
            }

            List<int> indexes = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                indexes.Add(i);
            }

            List<T> result = new List<T>();
            for (int i = 0; i < count; i++)
            {
                int cur = indexes.GetAtRandom(0, int.MaxValue, true);
                result.Add(list[cur]);
            }

            return result;
        }

        public static void InitRandom()
        {
            if (rnd == null)
                rnd = new System.Random(System.DateTime.Now.Millisecond);
        }

        #endregion

        #region TIMESTRING

        public static string GetTimeString(this System.DateTime dateTime)
        {
            string time = (dateTime.Hour < 10 ? "0" : "") + dateTime.Hour + ":" + (dateTime.Minute < 10 ? "0" : "") +
                          dateTime.Minute + ":" + (dateTime.Second < 10 ? "0" : "") + dateTime.Second;
            return time;
        }

        public static string ToTime(this int value)
        {
            int hours = value / 3600;
            int minutes = (value - hours * 3600) / 60;
            int seconds = value % 60;

            return (hours > 0 ? (hours.TimeFormat() + ":") : "") + minutes.TimeFormat() + ":" + seconds.TimeFormat();
        }

        public static string TimeFormat(this int value)
        {
            return value >= 10 ? value.ToString() : "0" + value;
        }

        public static string ToTime(this float value)
        {
            return ((int)value).ToTime();
        }

        public static string ToTime(this double value)
        {
            return ((int)value).ToTime();
        }

        public static DateTime ToDateTime(this double seconds)
        {
            DateTime result = epochStart;
            result = result.AddSeconds(seconds).ToLocalTime();
            return result;
        }

        public static double GetTimestamp(this DateTime date)
        {
            float time = Time.time;
            if (lastTimeGetTimeStamp == time)
            {
                return lastTimeTimestamp;
            }

            lastTimeGetTimeStamp = time;
            lastTimeTimestamp = (date - epochStart).TotalSeconds;

            return lastTimeTimestamp;
        }

        public static double CurrentTime()
        {
            float time = Time.time;
            lastTimeGet = time;
            lastTime = (DateTime.UtcNow - epochStart).TotalSeconds;
            return lastTime;
        }

        public static string GetDigitTimeString(int value, int periodsCount = 4, bool showZeros = false,
            bool alwaysTwoDigits = true)
        {
            if (value < 0)
                return "<0";

            int rem = 0;
            int days = value / 86400;
            rem = value - days * 86400;
            int hours = rem / 3600;
            rem = rem - hours * 3600;
            int minutes = rem / 60;
            rem = rem - minutes * 60;
            int seconds = rem % 60;

            string sDays = ":";
            string sHrs = ":";
            string sMin = ":";
            string sSec = ":";

            sDays = days.ToString() + sDays;
            sHrs = (hours > 9 || !alwaysTwoDigits ? hours.ToString() : "0" + hours) + sHrs;
            sMin = (minutes > 9 || !alwaysTwoDigits ? minutes.ToString() : "0" + minutes) + sMin;
            sSec = (seconds > 9 || !alwaysTwoDigits ? seconds.ToString() : "0" + seconds);

            bool bDays = false;
            bool bHrs = false;
            bool bMin = false;
            bool bSec = false;

            switch (periodsCount)
            {
                case 1:
                    bDays = days > 0;
                    bHrs = !bDays && hours > 0;
                    bMin = !bDays && !bHrs && minutes > 0;
                    bSec = !bDays && !bHrs && !bMin && seconds > 0;
                    break;

                case 2:
                    bDays = days > 0;
                    bHrs = bDays || hours > 0;
                    bMin = (bHrs && !bDays) || (!bHrs && minutes > 0) || (!bHrs && showZeros);
                    bSec = !bHrs;
                    break;

                case 3:
                    bDays = days > 0;
                    bHrs = bDays || (hours > 0 || showZeros);
                    bMin = bDays || bHrs || minutes > 0 || showZeros;
                    bSec = !bDays;
                    break;

                case 4:
                    bDays = days > 0 || showZeros;
                    bHrs = bDays || hours > 0 || showZeros;
                    bMin = bDays || bHrs || minutes > 0 || showZeros;
                    bSec = true;
                    break;
            }

            string result = "";
            if (bDays)
                result += sDays;
            if (bHrs)
                result += sHrs;
            if (bMin)
                result += sMin;
            if (bSec)
                result += sSec;

            return result;
        }

        #endregion
    }
}