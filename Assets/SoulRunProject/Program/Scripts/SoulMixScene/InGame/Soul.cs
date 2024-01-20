using System.Collections.Generic;

namespace SoulRun.InGame
{
    public class Soul
    {
        public string SoulName { get; set; }
        public int SoulLevel { get; set; }
        public string Ability { get; set; }
        public List<StatusDefine> Status { get; set; }
        public List<TraitDefine> TraitList { get; set; }

        // コンストラクタでSoulの属性を設定
        public Soul(string soulName, int soulLevel, string ability, List<StatusDefine> status,
            List<TraitDefine> traitList)
        {
            SoulName = soulName;
            SoulLevel = soulLevel;
            Ability = ability;
            Status = status;
            TraitList = traitList;
        }
    }


}