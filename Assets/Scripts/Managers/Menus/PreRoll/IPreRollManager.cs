namespace BulletHellJam2022.Assets.Scripts.Managers.Menus.PreRoll
{
    public interface IPreRollManager
    {
        public StaticObjectsSO StaticObjects { get; }

        /// <summary>
        /// Invoked on Start
        /// </summary>
        void OnStart();

        /// <summary>
        /// Invoked on Awake
        /// </summary>
        void OnAwake();


    }

}
