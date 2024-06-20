using UnityEngine;

namespace SoulRunProject.Runtime
{
    public class CustomElementLabelAttribute : PropertyAttribute
    {
        public readonly string Name;
        public readonly int AddNumber;
        
        public CustomElementLabelAttribute(string name, int addNumber)
        {
            Name = name;
            AddNumber = addNumber;
        }
    }
}