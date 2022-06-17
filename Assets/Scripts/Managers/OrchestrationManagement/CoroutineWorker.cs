using System.Collections;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Managers.OrchestrationManagement
{
    /// <summary>
    /// Provides support for MonoBehaviour features for those classes that are not.
    /// Used to start Coroutines in the Orchestration flow.
    /// </summary>
    public class CoroutineWorker : MonoBehaviour
    {
        /// <summary>
        /// Start a coroutine
        /// </summary>
        /// <param name="_coroutine">Coroutine to start</param>
        public void Work(IEnumerator _coroutine)
        {
            StartCoroutine(WorkCoroutine(_coroutine));
        }

        private IEnumerator WorkCoroutine(IEnumerator _coroutine)
        {
            yield return StartCoroutine(_coroutine);
            Destroy(this.gameObject);
        }
    }
}
