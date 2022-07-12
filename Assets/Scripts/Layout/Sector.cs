using System;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Layout
{
    public class Sector : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            EnableColliders();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            DisableColliders();
        }

        private void DisableColliders()
        {
            Debug.Log($"Disabling colliders for {gameObject.name}");
        }

        private void EnableColliders()
        {
            Debug.Log($"Enabling colliders for {gameObject.name}");
        }
    }
}
