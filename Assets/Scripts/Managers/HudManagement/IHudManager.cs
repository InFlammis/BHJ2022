namespace InFlammis.Victoria.Assets.Scripts.Managers.HudManagement
{
    /// <summary>
    /// Manager for the Head-up display. Manages the display of information on the screen.
    /// </summary>
    public interface IHudManager
    {
        /// <summary>
        /// Update the Hi-Score value
        /// </summary>
        /// <param name="value">Hi-score value</param>
        //void SetHiScore(int value);

        void SetHiScore(object publisher, string target, int value);

        /// <summary>
        /// Set the score value
        /// </summary>
        /// <param name="value">The score value</param>
        //void SetScore(int value);

        void SetScore(object publisher, string target, int value);

        /// <summary>
        /// Update the multiplier value
        /// </summary>
        /// <param name="value"></param>
        //void SetMultiplier(int value);
        void SetMultiplier(object publisher, string target, int value);

        /// <summary>
        /// Update the health value
        /// </summary>
        /// <param name="value">The health value</param>
        /// <param name="maxValue">The max health value</param>
        //void SetHealth(int value, int maxValue);
        void SetHealth(object publisher, string target, int value, int maxValue);

        /// <summary>
        /// Set the text in the central area of the screen. Used for Game Over, You won type of message.
        /// </summary>
        /// <param name="text"></param>
        //void SetCentralText(string text);
        void SetCentralText(object publisher, string target, string text);

    }
}
