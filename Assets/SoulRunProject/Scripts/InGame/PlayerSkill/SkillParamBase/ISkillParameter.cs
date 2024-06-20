using SoulRunProject.SoulMixScene;

namespace SoulRunProject.Common
{
    public interface ISkillParameter
    {
        void SetPlayerStatus(in PlayerStatus status);
        ISkillParameter Clone();
    }
}