using BulletHellJam2022.Assets.Scripts.Managers.SceneManagement;
using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.Menus
{
    /// <summary>
    /// Manages the actions related to a MenuButton
    /// </summary>
    public class MenuButtonManager : MyMonoBehaviour
    {
        /// <summary>
        /// Sound to play when the button is hovered
        /// </summary>
        [SerializeField]
        private Sound HoverSound;

        /// <summary>
        /// Sound to play when the button is clicked
        /// </summary>
        [SerializeField]
        private Sound ClickSound;

        /// <summary>
        /// Reference to the SceneManager instance
        /// </summary>
        [SerializeField]
        private SceneManager SceneManager;

        /// <summary>
        /// Play a sound when the button is hovered
        /// </summary>
        public void PlayHoverSound()
        {
            SceneManager.PlaySound(HoverSound);
        }

        /// <summary>
        /// Play a sound when the button is clicked
        /// </summary>
        public void PlayClickSound()
        {
            SceneManager.PlaySound(ClickSound);
        }

    }
}
