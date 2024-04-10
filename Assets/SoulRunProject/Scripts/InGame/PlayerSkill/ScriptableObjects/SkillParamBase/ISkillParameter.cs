namespace SoulRunProject.Common
{
    /// <summary>
    /// スキルのパラメーター
    /// </summary>
    [Name("基底クラス")]
    public interface ISkillParameter
    {
        /// <summary>
        /// シーンロード時にこのパラメータを初期化するように登録する。
        /// </summary>
        void InitializeParamOnSceneLoaded();
    }
}
