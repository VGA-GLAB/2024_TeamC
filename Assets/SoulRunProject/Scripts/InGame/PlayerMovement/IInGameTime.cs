namespace SoulRunProject.InGame
{
    /// <summary>
    /// ゲーム内時間で動いているクラス
    /// </summary>
    public interface IInGameTime
    {
        void SwitchPause(bool toPause);
    }
}
