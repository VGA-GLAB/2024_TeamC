using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using SoulRunProject.SoulMixScene;

namespace SoulRun.InGame
{
    public class SoulCard
    {
        // 固有番号
        public int CardID { get; set; }

        // カードの画像
        public Image Image { get; set; }

        // 名前
        public string SoulName { get; set; }
        // レベル
        public int SoulLevel { get; set; }
        // 
        public string Ability { get; set; }
        public List<StatusDefine> Status { get; set; }
        public List<TraitDefine> TraitList { get; set; }

        // コンストラクタでSoulの属性を設定
        public SoulCard(string soulName, int soulLevel, string ability, List<StatusDefine> status,
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