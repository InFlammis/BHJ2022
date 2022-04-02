using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.Menus
{
    public class PauseMenuManagerCore : IPauseMenuManager
    {
        /// <inheritdoc/>
        public event EventHandler ResumeGameEvent;

        /// <inheritdoc/>
        public event EventHandler QuitCurrentGameEvent;

        /// <inheritdoc/>
        public event EventHandler<Sound> PlaySoundEvent;

        public readonly IMyMonoBehaviour Parent;

        public PauseMenuManagerCore(IMyMonoBehaviour parent)
        {
            Parent = parent;
        }

        /// <inheritdoc/>
        public void OnStart()
        {
            //Debug.Log($"Pause menu opened");
        }

        /// <inheritdoc/>
        public void OnAwake() { }

        /// <inheritdoc/>
        public void ResumeGame()
        {
            ResumeGameEvent?.Invoke(this, new EventArgs());
        }

        /// <inheritdoc/>
        public void QuitCurrentGame()
        {
            QuitCurrentGameEvent?.Invoke(this, new EventArgs());
        }
    }
}
