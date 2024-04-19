namespace SoulRunProject.InGame
{
    /// <summary>
    /// ゲーム内時間で動いているクラス
    /// </summary>
    public interface IPausable
    {
        /// <summary>
        /// Trueでとめる、Falseでうごかす
        /// </summary>
        /// <param name="isPause"></param>
        void Pause(bool isPause);
    }
}
