using System.Collections.Generic;

namespace SoulRunProject.Common
{
    public interface ILevelUpEventGroup
    {
        public List<ILevelUpEvent> LevelUpType { get; }
    }
}