using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InFlammis.Victoria.Assets.Scripts.Managers.GameManagement
{
    public sealed class UnitySceneManagerWrapper : IUnitySceneManagerWrapper
    {
        public event SceneLoadedEventArgs SceneLoaded;
        public event SceneUnloadedEventArgs SceneUnloaded;

        private static IUnitySceneManagerWrapper _instance;
        
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

        private UnitySceneManagerWrapper()
        {
            SceneManager.sceneLoaded += (scene, mode) => SceneLoaded?.Invoke(scene, mode);
            SceneManager.sceneUnloaded += scene => SceneUnloaded?.Invoke(scene);
        }

        public AsyncOperation LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode)
        {
            return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        public AsyncOperation UnloadSceneAsync(string sceneName)
        {
            return SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
