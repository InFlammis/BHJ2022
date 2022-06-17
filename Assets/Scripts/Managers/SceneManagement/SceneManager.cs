using InFlammis.Victoria.Assets.Scripts.Managers.SoundManagement;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Managers.SceneManagement
{
    /// <summary>
    /// Base class for a scene manager. A scene can be a level scene or a menu scene.
    /// Managers for levels and menus inherit from here.
    /// </summary>
    public class SceneManager : MyMonoBehaviour, IMyMonoBehaviour
    {
        [SerializeField] protected StaticObjectsSO _staticObjects;

        public StaticObjectsSO StaticObjects => _staticObjects;

        void Start()
        {
        }

        /// <summary>
        /// Method invoked to request to play a sound
        /// </summary>
        /// <param name="sound"></param>
        public virtual void PlaySound(Sound sound) { }
    }
}
