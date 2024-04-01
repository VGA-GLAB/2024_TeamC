namespace SoulRunProject.Common
{
    /// <summary>
    /// プレイヤーがInGameで所持しているリソースを保持するクラス
    /// </summary>
    public class PlayerResourceContainer
    {
        public int Coin;

        public void Initialize()
        {
            Coin = 0;
        }
    }
}
