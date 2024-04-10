namespace SoulRunProject.Common
{
    public interface ILevelUpEvent
    {
        public void LevelUp(in ISkillParameter skillParameter);
    }
}