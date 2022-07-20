using InFlammis.Victoria.Assets.Scripts.Managers;
using System;
using UnityEngine;
using System.Linq;
using InFlammis.Victoria.Assets.Scripts.Layout.Sectors.StateMachine;

namespace InFlammis.Victoria.Assets.Scripts.Layout.Sectors
{
    public partial class Sector : MonoBehaviour, IEquatable<Sector>
    {
        public event Action<Sector> OnSectorActivated;
        public event Action<Sector> OnSectorDectivated;

        [SerializeField] private StaticObjectsSO _staticObjects;

        [SerializeField] private Color activeColor;
        [SerializeField] private Color inactiveColor;
        [SerializeField] private Color awakenColor;

        private IStateFactory _stateFactory;

        private State CurrentState;

        [HideInInspector]public Areas areas;

        private bool playerInSector = false;
        private bool playerInNaa = false;
        private bool playerInAa = false;

        public bool PlayerInSector
        {
            get => playerInSector;
            set
            {
                playerInSector = value;
                SetState();
            }
        }
        public bool PlayerInNaa
        {
            get => playerInNaa;
            set
            {
                playerInNaa = value;
                SetState();
            }
        }
        public bool PlayerInAa
        {
            get => playerInAa;
            set
            {
                playerInAa = value;
                SetState();
            }
        }

        private void Awake()
        {
            var sectorCollider = GetComponent<Collider2D>();
            var aa = gameObject.GetComponentsInChildren<ActivationArea>(true);
            var naa = gameObject.GetComponentsInChildren<NeighbourActivationArea>(true);
            var stainCollidersCollection = gameObject.GetComponentInChildren<StainCollidersCollection>();

            areas = new Areas(aa, naa, sectorCollider, stainCollidersCollection);

            _stateFactory = new StateFactory(this);
        }

        private void Start()
        {
            foreach(var aa in areas.ActivationAreas)
            {
                aa.ActivationEvent += ActivationEvent;
                aa.DeactivationEvent += DeactivationEvent;
            }

            var playerTransform = _staticObjects.Messenger.RequestForPlayerTransform(this, null);

            playerInSector = areas.SectorCollider.OverlapPoint(playerTransform.position);
            SetState();
        }

        public void NeighbourActivation(Sector neighbour)
        {
            PlayerInNaa = true;
        }
        public void NeighbourDeactivation(Sector neighbour)
        {
            PlayerInNaa = false;
        }

        private void ActivationEvent(ActivationArea aa)
        {
            PlayerInAa = true;
        }
        private void DeactivationEvent(ActivationArea aa)
        {
            PlayerInAa = false;
        }

        private void SetState()
        {
            if (PlayerInSector || PlayerInAa)
            {
                if (SetCurrentState(_stateFactory.Active))
                {
                    OnSectorActivated?.Invoke(this);
                }
            }
            else if (!PlayerInSector && !PlayerInAa && !PlayerInNaa)
            { 
                if (SetCurrentState(_stateFactory.Inactive))
                {
                    OnSectorDectivated?.Invoke(this);
                }
            }
            else if (!PlayerInSector && !PlayerInAa && PlayerInNaa)
            { 
                SetCurrentState(_stateFactory.Awaken); 
            }
        }

        private bool SetCurrentState(State state)
        {
            if(state == null)
            {
                return false;
            }
            if(state == CurrentState)
            {
                return false;
            }

            if(CurrentState != null)
            {
                CurrentState.OnExit();
            }

            CurrentState = state;

            CurrentState.OnEnter();

            return true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag != "Player")
            {
                return;
            }
            PlayerInSector = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag != "Player")
            {
                return;
            }
            PlayerInSector = false;
        }

        private void OnDrawGizmos()
        {
            if(this.areas == null)
            {
                return;
            }

            var rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);


            GizmoDrawBoxCollider(this.areas.SectorCollider);

            //var polygonCollider = this.areas.SectorCollider as PolygonCollider2D;
            //GizmoDrawPolygonCollider(polygonCollider);

            //if (polygonCollider != null)
            //{
            //    GizmoDrawPolygonCollider(polygonCollider);
            //}
            //else
            //{
            //    GizmoDrawBoxCollider(this.areas.SectorCollider);
            //}
        }

        private void GizmoDrawPolygonCollider(PolygonCollider2D collider)
        {
            var rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);

            Gizmos.matrix = rotationMatrix;
            Gizmos.color = CurrentState switch
            {
                AwakenState => awakenColor,
                ActiveState => activeColor,
                _ => inactiveColor
            };

            var mesh = new Mesh();
            mesh.SetVertices(collider.points.Select(x => new Vector3(x.x, x.y, 0)).ToArray());
            mesh.RecalculateNormals();
            Gizmos.DrawMesh(mesh);
        }

        private void GizmoDrawBoxCollider(Collider2D collider)
        {
            var rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

            Gizmos.matrix = rotationMatrix;
            Gizmos.color = CurrentState switch
            {
                AwakenState => awakenColor,
                ActiveState => activeColor,
                _ => inactiveColor
            };

            Gizmos.DrawCube(Vector3.zero, transform.localScale);
        }

        public bool Equals(Sector other)
        {
            if(other == null)
            {
                return false;
            }

            return this.GetInstanceID() == other.GetInstanceID();
        }

        public class Areas
        {
            public Areas(ActivationArea[] activationAreas, NeighbourActivationArea[] neighbourActivationAreas, Collider2D sectorCollider, StainCollidersCollection stainColliders)
            {
                this.ActivationAreas = activationAreas;
                this.NeighbourActivationAreas = neighbourActivationAreas;
                this.SectorCollider = sectorCollider;
                this.stainCollidersCollection = stainColliders;
            }

            public void SetActive(bool isActive)
            {
                this.SetActivationAreasActive(isActive);
                this.SetNeighbourActivationAreasActive(isActive);
                this.SetSectorColliderActive(isActive);
                this.SetStainCollidersActive(isActive);
            }

            public void SetActivationAreasActive(bool isActive)
            {
                foreach (var naa in this.NeighbourActivationAreas)
                {
                    naa.SetActive(isActive);
                }
            }

            public void SetNeighbourActivationAreasActive(bool isActive)
            {
                foreach(var aa in this.ActivationAreas)
                {
                    aa.SetActive(isActive);
                }
            }

            public void SetSectorColliderActive(bool isActive)
            {
                this.SectorCollider.enabled = isActive;
            }

            public void SetStainCollidersActive(bool isActive)
            {
                this.stainCollidersCollection?.SetActive(isActive);
            }

            public ActivationArea[] ActivationAreas { get;}
            public NeighbourActivationArea[] NeighbourActivationAreas { get; }

            public Collider2D SectorCollider { get; set; }
            public StainCollidersCollection stainCollidersCollection { get; set; }
        }
    }
}
