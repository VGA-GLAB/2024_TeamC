namespace SoulRunProject.SoulMixScene
{
    /// <summary> Traitを実行するためのインターフェース
    /// インターフェースはSerializeできないので、ラッパークラスを作成してSerializeする</summary>
    [System.Serializable]
    public class TraitWrapper
    {
        public ITraitInterface Trait { get; set; }

        public TraitWrapper(ITraitInterface trait)
        {
            Trait = trait;
        }
    }
}