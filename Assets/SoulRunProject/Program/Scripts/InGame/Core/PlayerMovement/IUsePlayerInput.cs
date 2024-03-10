namespace SoulRunProject.InGame
{
    /// <summary>
    /// PlayerInputを受け取るinterface
    /// </summary>
    public interface IUsePlayerInput
    {
        /// <summary>
        /// HorizontalAxisを受け付ける
        /// </summary>
        void InputHorizontal(float horizontal);
        
        void Jump();
    }
}
