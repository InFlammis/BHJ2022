using System;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Layout
{
    public class Sector : MonoBehaviour
    {
        public enum SectorState
        {
            // All NAA and AA are inactive
            Inactive,

            // AA are active, NAA are inactive
            Awaken,

            //
            Active
        }

        public SectorState State = SectorState.Inactive;

        private ActivationArea northAa;
        private ActivationArea southAa;

        private NeighbourActivationArea northNaa;
        private NeighbourActivationArea southNaa;

        [SerializeField]private Color activeColor;
        [SerializeField]private Color inactiveColor;
        [SerializeField]private Color awakenColor;


        public void NeighbourActivation(Sector neighbour)
        {
            SetState(SectorState.Awaken);
        }
        public void NeighbourDeactivation(Sector neighbour)
        {
            SetState(SectorState.Inactive);
        }

        private void Awake()
        {
            northAa = GameObject.FindGameObjectWithTag("NorthAA").GetComponent<ActivationArea>();
            southAa = GameObject.FindGameObjectWithTag("SouthAA").GetComponent<ActivationArea>();

            northNaa = GameObject.FindGameObjectWithTag("NorthNAA").GetComponent<NeighbourActivationArea>();
            southNaa = GameObject.FindGameObjectWithTag("SouthNAA").GetComponent<NeighbourActivationArea>();

            SetState(State);

        }

        private void Start()
        {
            northAa.ActivationEvent += ActivationEvent;
            southAa.ActivationEvent += ActivationEvent;

        }

        private void ActivationEvent(ActivationArea aa)
        {
            SetState(SectorState.Active);
        }

        private void SetState(SectorState state)
        {
            switch (state)
            {
                case SectorState.Inactive:
                    {
                        northAa.SetActive(false);
                        southAa.SetActive(false);
                        northNaa.SetActive(false);
                        southNaa.SetActive(false);
                        ToggleStainColliderState(false);
                    }
                    break;
                case SectorState.Awaken:
                    {
                        northAa.SetActive(true);
                        southAa.SetActive(true);
                        northNaa.SetActive(false);
                        southNaa.SetActive(false);
                        ToggleStainColliderState(false);
                    }
                    break;
                case SectorState.Active:
                    {
                        northAa.SetActive(true);
                        southAa.SetActive(true);
                        northNaa.SetActive(true);
                        southNaa.SetActive(true);
                        ToggleStainColliderState(true);
                    }
                    break;
                default:
                    throw new Exception("Unmanaged state");
            };
        }

        private void ToggleStainColliderState(bool enabled)
        {

        }
        private void DisableColliders()
        {
            Debug.Log($"Disabling colliders for {gameObject.name}");
        }

        private void EnableColliders()
        {
            Debug.Log($"Enabling colliders for {gameObject.name}");
        }

        private void OnDrawGizmos()
        {
            var rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

            Gizmos.matrix = rotationMatrix;
            Gizmos.color = State switch
            {
                SectorState.Active => activeColor,
                SectorState.Awaken => awakenColor,
                _ => inactiveColor
            };

            Gizmos.DrawCube(Vector3.zero, transform.localScale);
        }
    }
}
