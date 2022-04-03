using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts
{
    /// <summary>
    /// Implementation of a IMyMonoBehaviour. MonoBehaviour object implementing the IMyMonoBehaviour interface
    /// </summary>
    public class MyMonoBehaviour : MonoBehaviour, IMyMonoBehaviour
    {
        /// <inheritdoc/>
        public virtual GameObject GameObject => base.gameObject;
    }
}
