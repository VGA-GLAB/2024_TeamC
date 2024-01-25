using UnityEngine;

namespace SoulRunProject.SoulMixScene
{
    /// <summary> Traitを実行するためのインターフェース
    /// インターフェースはSerializeできないので、ラッパークラスを作成してSerializeする</summary>
    [CreateAssetMenu(fileName = "TraitWrapper", menuName = "SoulRunProject/TraitWrapper")]
    public class TraitWrapper : ScriptableObject
    {
        public ITraitInterface Trait { get; set; }

        public TraitWrapper(ITraitInterface trait)
        {
            Trait = trait;
        }
    }
}