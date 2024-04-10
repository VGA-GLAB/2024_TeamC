using System.Collections.Generic;

namespace SoulRunProject.Common
{
    public interface ILevelUpEventList
    {
        public List<ILevelUpEvent> LevelUpEventList { get; }
    }
}