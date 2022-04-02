using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BulletHellJam2022.Assets.Scripts.Managers.GameManagement
{
    //This class is non-testable because SceneManager is not mockable
    /// <summary>
    /// Wrapper that manages load/unload operations on a scene.
    /// Singleton
    /// </summary>
    public sealed class UnitySceneManagerWrapper : IUnitySceneManagerWrapper
    {
        /// <inheritdoc/>
        public event SceneLoadedEventArgs SceneLoaded;
        /// <inheritdoc/>
        public event SceneUnloadedEventArgs SceneUnloaded;

        private static IUnitySceneManagerWrapper _instance;
        
        /// <summary>
        /// Instance of IUnitySceneManagerWrapper
        /// </summary>
        public static IUnitySceneManagerWrapper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UnitySceneManagerWrapper();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Private constructor as this is a singleton
        /// </summary>
        private UnitySceneManagerWrapper()
        {
            SceneManager.sceneLoaded += (scene, mode) => SceneLoaded?.Invoke(scene, mode);
            SceneManager.sceneUnloaded += scene => SceneUnloaded?.Invoke(scene);
        }

        /// <inheritdoc/>
        public AsyncOperation LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode)
        {
            return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        /// <inheritdoc/>
        public AsyncOperation UnloadSceneAsync(string sceneName)
        {
            return SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
