using System;
using System.Reflection;

namespace ShopDapperData.Extensions
{
    internal static class ObjectFieldExtensions
    {

        /// <summary>
        /// Sets the value of a Private Field
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fieldName">Field Name</param>
        /// <param name="value">Value to Set</param>
        internal static void SetField<T>(this Object obj, string fieldName, T value) where T : new()
        {
            obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance).SetValue(obj, value);
        }

        /// <summary>
        /// Gets the value of a Private Field
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="fieldName">Field Name</param>
        /// <returns></returns>
        internal static T GetField<T>(this Object obj, string fieldName) where T: new()
        {
            return (T) obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(obj);
        }
    }
}
