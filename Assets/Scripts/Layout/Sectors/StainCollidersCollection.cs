using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Layout.Sectors
{
    public class StainCollidersCollection : MonoBehaviour
    {

        public void SetActive(bool isActive)
        {
            var colliders = GetComponentsInChildren<Collider2D>();

            foreach(var collider in colliders)
            {
                collider.enabled = isActive;
            }
        }
    }
}
