using System.Collections;
using UnityEngine;
using UnityEngine.Internal;

namespace BulletHellJam2022.Assets.Scripts
{
    /// <summary>
    /// Interface introduce to improve testability of classes with references to MonoBehaviours.
    /// It is actually a Facade towards the corresponding methods of a MonoBehaviour.
    /// </summary>
    public interface IMyMonoBehaviour
    {
        /// <summary>
        /// The GameObject this MonoBehaviour belongs to
        /// </summary>
        GameObject GameObject { get; }
        
        /// <summary>
        /// Start a Coroutine. 
        /// </summary>
        /// <param name="routine">Coroutine to start</param>
        /// <returns>Reference to the new Coroutine</returns>
        Coroutine StartCoroutine(IEnumerator routine);
        /// <summary>
        /// Start a Coroutine. 
        /// </summary>
        /// <param name="methodName">Name of the method to start as a coroutine</param>
        /// <returns>Reference to the new Coroutine</returns>
        Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value);

        /// <summary>
        /// Stop a Coroutine. 
        /// </summary>
        /// <param name="routine">Coroutine to stop</param>
        void StopCoroutine(IEnumerator routine);

        /// <summary>
        /// Stop a Coroutine. 
        /// </summary>
        /// <param name="routine">Coroutine to stop</param>
        void StopCoroutine(Coroutine routine);
    }
}
