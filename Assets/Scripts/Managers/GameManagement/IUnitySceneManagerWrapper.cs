using UnityEngine;
using UnityEngine.SceneManagement;

namespace BulletHellJam2022.Assets.Scripts.Managers.GameManagement
{
    public delegate void SceneLoadedEventArgs(Scene scene, LoadSceneMode loadSceneMode);
    public delegate void SceneUnloadedEventArgs(Scene scene);

    /// <summary>
    /// Wrapper manager for Game scenes. A scene can be a Menu scene or a Level scene.
    /// </summary>
    public interface IUnitySceneManagerWrapper
    {
        /// <summary>
        /// Event raised when a new scene is loaded
        /// </summary>
        public event SceneLoadedEventArgs SceneLoaded;

        /// <summary>
        /// Event raised when a scene is unloaded
        /// </summary>
        public event SceneUnloadedEventArgs SceneUnloaded;

        /// <summary>
        /// Load a scene asynchronously
        /// </summary>
        /// <param name="sceneName">The name of the scene to load</param>
        /// <param name="loadSceneMode">The load mode</param>
        /// <returns></returns>
        AsyncOperation LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode);

        /// <summary>
        /// Unload a scene asynchronously
        /// </summary>
        /// <param name="sceneName">The name of the scene to unload</param>
        /// <returns></returns>
        AsyncOperation UnloadSceneAsync(string sceneName);
    }
}