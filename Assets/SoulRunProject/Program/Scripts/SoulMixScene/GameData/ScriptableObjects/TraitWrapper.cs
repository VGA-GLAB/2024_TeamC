using UnityEngine;

namespace SoulRunProject.SoulMixScene
{
    /// <summary> Traitを実行するためのインターフェース
    /// インターフェースはSerializeできないので、ラッパークラスを作成してSerializeする</summary>
    [CreateAssetMenu(fileName = "TraitWrapper", menuName = "SoulRunProject/TraitWrapper")]
    public class TraitWrapper : ScriptableObject
    {
        [SerializeReference, SubclassSelector] private ITraitInterface _trait;

        public TraitWrapper(ITraitInterface trait)
        {
            _trait = trait;
        }
    }
}