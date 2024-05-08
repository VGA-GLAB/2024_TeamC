using UnityEngine;

namespace SoulRunProject.Runtime
{
    public class ShowWhenEnumAttribute : PropertyAttribute
    {
        public readonly string EnumVariableName;
        public readonly object EnumValue;

        public ShowWhenEnumAttribute(string enumVariableName,
            object enumValue)
        {
            EnumVariableName = enumVariableName;
            EnumValue = enumValue;
        }
    }
}