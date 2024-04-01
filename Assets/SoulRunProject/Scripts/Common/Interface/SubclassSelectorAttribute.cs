using System;
using UnityEngine;

namespace SoulRunProject.Common
{
    public class NameAttribute : Attribute
    {
        public string GetName { get; }
        public NameAttribute(string name) => GetName = name;
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class SubclassSelectorAttribute : PropertyAttribute
    {
        private readonly bool _mIncludeMono;

        public SubclassSelectorAttribute(bool includeMono = false)
        {
            _mIncludeMono = includeMono;
        }

        public bool IsIncludeMono()
        {
            return _mIncludeMono;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class HideInEditorAttribute : Attribute
    {
        // 特に中身を定義する必要はありません
    }
}