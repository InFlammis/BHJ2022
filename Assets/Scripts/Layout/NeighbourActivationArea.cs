using System;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Layout
{
    public class NeighbourActivationArea : MonoBehaviour
    {
        [SerializeField] Sector neighbour;

        [SerializeField] Color color;

        private Collider2D _collider;

        private Sector parent;

        public bool IsActive => _collider.enabled;

        public void SetActive(bool isActive)
        {
            _collider.enabled = isActive;
        }

        private void Awake()
        {
            parent = this.GetComponentInParent<Sector>();
            _collider = this.GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(neighbour == null)
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
            if (this._collider == null || !IsActive)
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
