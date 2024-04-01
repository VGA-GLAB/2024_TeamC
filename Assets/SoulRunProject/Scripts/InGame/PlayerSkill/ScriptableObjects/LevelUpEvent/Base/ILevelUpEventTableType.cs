using System.Collections.Generic;

namespace SoulRunProject.Common
{
    public interface ILevelUpEventTableType
    {
        public List<ILevelUpEventGroup> LevelUpTable { get; }
    }
}