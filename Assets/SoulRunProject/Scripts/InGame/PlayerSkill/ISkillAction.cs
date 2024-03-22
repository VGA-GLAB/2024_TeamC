namespace SoulRunProject.InGame
{
    public interface ISkillAction
    {
        /// <summary>
        /// スキルの起動、再開時に呼ばれる
        /// </summary>
        public void Start();
        
        /// <summary>
        /// スキルの更新処理
        /// </summary>
        public void Update();
        
        /// <summary>
        /// ポーズ時やスキルの終了時に呼ばれる
        /// </summary>
        public void Stop();
    }
}
