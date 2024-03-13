using System;
using UnityEngine;

namespace SoulRunProject.Common
{
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
}