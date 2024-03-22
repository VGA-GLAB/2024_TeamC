namespace SoulRunProject.InGame
{
    /// <summary>
    /// ゲーム内時間で動いているクラス
    /// </summary>
    public interface IInGameTime
    {
        /// <summary>
        /// Trueでとめる、Falseでうごかす
        /// </summary>
        /// <param name="toPause"></param>
        void SwitchPause(bool toPause);
    }
}
