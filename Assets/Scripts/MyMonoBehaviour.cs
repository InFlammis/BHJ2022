using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulletHellJam2022.Assets.Scripts.Player;
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
