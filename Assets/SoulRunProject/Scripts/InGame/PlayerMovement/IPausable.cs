namespace SoulRunProject.InGame
{
    /// <summary>
    /// ゲーム内時間で動いているクラス
    /// </summary>
    public interface IPausable
    {
        /// <summary>
        /// PauseManagerに自身を登録する
        /// </summary>
        void Register();
        /// <summary>
        /// PauseManagerから自身を登録解除する
        /// </summary>
        void UnRegister();

        /// <summary>
        /// Trueでとめる、Falseでうごかす
        /// </summary>
        void Pause(bool isPause);
    }
}
