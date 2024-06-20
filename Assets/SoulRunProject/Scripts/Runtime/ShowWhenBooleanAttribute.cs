using UnityEngine;
namespace SoulRunProject.Runtime
{
    public class ShowWhenBooleanAttribute : PropertyAttribute
    {
        public readonly string BoolVariableName;
        public readonly bool TrueThenDisable;

        public ShowWhenBooleanAttribute(string boolVariableName,
            bool trueThenDisable = false)
        {
            BoolVariableName = boolVariableName;
            TrueThenDisable = trueThenDisable;
        }
    }
}