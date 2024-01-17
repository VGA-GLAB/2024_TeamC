namespace SoulRun.InGame
{
    /// <summary>
    /// インゲームのPlayerへの入力のインターフェース
    /// 左右移動、ジャンプ、必殺技、オプションへのアクセスのインターフェースを持つ
    /// </summary>
    public interface ISoulRunInGameInput
    {
        /// <summary>
        /// Playerを左右に移動させる
        /// </summary>
        /// <param name="rightInput">入力値</param>
        public void HorizontalMove(float rightInput);

        /// <summary>
        /// Playerをジャンプさせる
        /// </summary>
        public void Jump();

        /// <summary>
        /// 必殺技を発動する
        /// </summary>
        public void ActiveUniqueSkill();

        /// <summary>
        /// オプションパネルを開く、既に開いているなら閉じる
        /// </summary>
        public void ActiveOptionPannel();
    }
}
