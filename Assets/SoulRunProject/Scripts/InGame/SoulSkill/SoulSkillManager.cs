using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// ソウル技を管理するクラス
    /// </summary>
    public class SoulSkillManager : MonoBehaviour
    {
        [SerializeField] SoulSkillBase _currentSoulSkill;
        
        public void SetSoulSkill(SoulSkillBase soulSkill)
        {
            _currentSoulSkill = soulSkill;
        }
        
        public void AddSoul(float soul)
        {
            _currentSoulSkill.AddSoul(soul);
        }
        
        public void UseSoulSkill()
        {
            _currentSoulSkill.UseSoulSkill();
        }
    }
}
