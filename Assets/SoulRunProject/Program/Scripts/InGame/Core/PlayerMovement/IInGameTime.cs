namespace SoulRunProject.InGame
{
    /// <summary>
    /// ゲーム内時間で動いているクラス
    /// </summary>
    public interface IInGameTime
    {
        void UpdateAction();
        void FixedUpdateAction();
        void SwitchPause(bool toPause);
    }
}
