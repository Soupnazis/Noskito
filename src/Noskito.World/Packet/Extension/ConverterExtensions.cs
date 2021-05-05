using System;
using System.Collections.Generic;
using System.Text;

namespace Noskito.World.Packet.Extension
{
    /// <summary>
    ///     Contains some extension method used for easily convert string to selected value
    /// </summary>
    public static class ConverterExtensions
    {
        /// <summary>
        ///     Convert a string to long
        /// </summary>
        /// <param name="value">string value to convert</param>
        /// <returns>parsed long value</returns>
        public static long ToLong(this string value)
        {
            return long.Parse(value);
        }

        /// <summary>
        ///     Convert a string to a nullable long
        /// </summary>
        /// <param name="value">string value to convert</param>
        /// <returns>parsed long value or null</returns>
        public static long? ToNullableLong(this string value)
        {
            return value == "-1" || value == "-" ? (long?) null : long.Parse(value);
        }

        /// <summary>
        ///     Convert a string to a nullable int
        /// </summary>
        /// <param name="value">string value to convert</param>
        /// <returns>parsed int value or null</returns>
        public static int? ToNullableInt(this string value)
        {
            return value == "-1" || value == "-" ? (int?) null : int.Parse(value);
        }

        /// <summary>
        ///     Convert a string to a nullable byte
        /// </summary>
        /// <param name="value">string value to convert</param>
        /// <returns>parsed byte value or null</returns>
        public static byte? ToNullableByte(this string value)
        {
            return value == "-1" || value == "-" ? (byte?) null : byte.Parse(value);
        }

        /// <summary>
        ///     Convert a string to a nullable short
        /// </summary>
        /// <param name="value">string value to convert</param>
        /// <returns>parsed short value or null</returns>
        public static short? ToNullableShort(this string value)
        {
            return value == "-1" || value == "-" ? (short?) null : short.Parse(value);
        }

        /// <summary>
        ///     Convert a string to short
        /// </summary>
        /// <param name="value">string value to convert</param>
        /// <returns>parsed short value</returns>
        public static short ToShort(this string value)
        {
            return short.Parse(value);
        }

        /// <summary>
        ///     Convert a string to byte
        /// </summary>
        /// <param name="value">string value to convert</param>
        /// <returns>parsed byte value</returns>
        public static byte ToByte(this string value)
        {
            return byte.Parse(value);
        }

        /// <summary>
        ///     Convert a string to int
        /// </summary>
        /// <param name="value">string value to convert</param>
        /// <returns>parsed int value</returns>
        public static int ToInt(this string value)
        {
            return int.Parse(value);
        }

        /// <summary>
        ///     Convert a string to bool
        /// </summary>
        /// <param name="value">string value to convert</param>
        /// <returns>parsed bool value</returns>
        public static bool ToBool(this string value)
        {
            return value == "1";
        }

        /// <summary>
        ///     Convert a string to a nullable short array
        /// </summary>
        /// <param name="value">string value to split and convert</param>
        /// <param name="separator">separator used to split the string value</param>
        /// <returns>parsed array value</returns>
        public static short?[] ToNullableShortArray(this string value, char separator = '.')
        {
            string[] split = value.Split(separator);
            var array = new short?[split.Length];

            for (int i = 0; i < split.Length; i++)
            {
                array[i] = split[i].ToNullableShort();
            }

            return array;
        }

        /// <summary>
        ///     Convert a string to a bool array
        /// </summary>
        /// <param name="value">string value to split and convert</param>
        /// <param name="separator">separator used to split the string value</param>
        /// <returns>parsed array value</returns>
        public static bool[] ToBoolArray(this string value, char separator = '.')
        {
            string[] split = value.Split(separator);
            var array = new bool[split.Length];
            for (int i = 0; i < split.Length; i++)
            {
                array[i] = split[i].ToBool();
            }

            return array;
        }

        /// <summary>
        ///     Convert as string to it's enum value
        /// </summary>
        /// <param name="value">string value to convert</param>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns>parsed value as selected enum type</returns>
        public static T ToEnum<T>(this string value) where T : System.Enum
        {
            return (T) System.Enum.Parse(typeof(T), value);
        }

        public static Guid ToGuid(this string value)
        {
            return Guid.Parse(value);
        }

        /// <summary>
        ///     Convert a nullable long to string
        /// </summary>
        /// <param name="value">nullable long to convert</param>
        /// <returns>converted nullable long as string</returns>
        public static string Format(this long? value)
        {
            return value == null ? "-1" : value.ToString();
        }

        /// <summary>
        ///     Convert an enum to int string
        /// </summary>
        /// <param name="value">Enum value to convert</param>
        /// <typeparam name="T">Type of the enum</typeparam>
        /// <returns>converter enum as string</returns>
        public static string Format<T>(this T value) where T : System.Enum
        {
            return ((int) (object) value).ToString();
        }

        public static string Format(this bool boolean)
        {
            return boolean ? "1" : "0";
        }

        public static string Format(this short? value)
        {
            return value == null ? "-1" : value.ToString();
        }

        public static string Format(this int? value)
        {
            return value == null ? "-1" : value.ToString();
        }

        public static string Format(this IEnumerable<short?> values, char separator = '.')
        {
            var sb = new StringBuilder();
            foreach (var value in values)
            {
                sb.Append(value.Format()).Append(separator);
            }

            return sb.ToString();
        }

        public static string Format(this IEnumerable<bool> values, char separator = '.')
        {
            var sb = new StringBuilder();
            foreach (bool value in values)
            {
                sb.Append(value.Format()).Append(separator);
            }

            return sb.ToString();
        }

        public static string Format(this string value)
        {
            return value ?? "-";
        }
    }
}