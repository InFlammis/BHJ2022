using System;

namespace BulletHellJam2022.Assets.Scripts.Managers.Menus.Pause
{
    public class PauseMenuManagerCore : IPauseMenuManager
    {
        public readonly IPauseMenuManager Parent;

        public StaticObjectsSO StaticObjects => Parent.StaticObjects;

        public PauseMenuManagerCore(IPauseMenuManager parent)
        {
            Parent = parent;
        }

        public void OnStart()
        {
        }

        public void OnAwake() { }

        public void ResumeGame()
        {
        }

        public void QuitCurrentGame()
        {
        }
    }
}
