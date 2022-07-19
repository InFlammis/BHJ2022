using System;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Layout.Sectors
{
    public class NeighbourActivationArea : MonoBehaviour
    {
        public Sector neighbour;

        [SerializeField] Color color;

        private Collider2D _collider;

        private Sector parent;

        public bool IsActive => _collider.enabled;

        public void SetActive(bool isActive)
        {
            if (_collider == null)
            {
                return;
            }

            _collider.enabled = isActive;
        }

        private void Awake()
        {
            parent = GetComponentInParent<Sector>();
            _collider = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (neighbour == null)
            {
                return;
            }

            neighbour.NeighbourActivation(parent);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (neighbour == null)
            {
                return;
            }

            neighbour.NeighbourDeactivation(parent);
        }
        private void OnDrawGizmos()
        {
            if (_collider == null || !IsActive)
            {
                return;
            }

            var rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);

            Gizmos.matrix = rotationMatrix;
            Gizmos.color = color;
            Gizmos.DrawCube(Vector3.zero, Vector3.one);
        }
    }
}
