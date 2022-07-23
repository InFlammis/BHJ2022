using InFlammis.Victoria.Assets.Scripts.Layout.Sectors;
using InFlammis.Victoria.Assets.Scripts.MessageBroker.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Managers.StainsManagement
{
    public class StainsManager : MonoBehaviour
    {
        [SerializeField] private StaticObjectsSO _staticObjects;

        [SerializeField] private StainsManagerSettings _initSettings;

        private List<Sector> sectors = new List<Sector>();
        private List<Sector> activeSectors = new List<Sector>();

        private void Awake()
        {
            SubscribeToSpitEvents();
        }

        private void Start()
        {
            sectors.AddRange(gameObject.GetComponentsInChildren<Sector>());

            foreach(var sector in sectors)
            {
                sector.OnSectorActivated += Sector_OnSectorActivated;
                sector.OnSectorDectivated += Sector_OnSectorDectivated;
            }
        }

        private void Sector_OnSectorDectivated(Sector sector)
        {
            if (!activeSectors.Contains(sector))
            {
                return;
            }

            activeSectors.Remove(sector);
        }

        private void Sector_OnSectorActivated(Sector sector)
        {
            if (activeSectors.Contains(sector))
            {
                return;
            }

            activeSectors.Add(sector);
        }

        public virtual void SubscribeToSpitEvents()
        {
            var messenger = (_staticObjects.Messenger as ISpitEventsMessenger);

            messenger.HasDied.AddListener(Spit_HasDied);
        }

        private void Spit_HasDied(object publisher, string target)
        {
            var publisherGo = (GameObject)publisher;

            var parentSector = this.FindParentInActiveSectors(publisherGo.transform.position);
            
            if(parentSector == null)
            {
                return;
            }

            var parentGo = parentSector.GetComponentInChildren<StainCollidersCollection>().gameObject;

            var newGo = new GameObject("Stain");
            newGo.layer = LayerMask.NameToLayer("Background");

            newGo.transform.parent = parentGo.transform;
            newGo.transform.position = publisherGo.transform.position;
            newGo.transform.rotation = publisherGo.transform.rotation;

            var collider = newGo.AddComponent<CircleCollider2D>();
            collider.radius = 0.5f;
            collider.isTrigger = true;
        }

        private Sector FindParentInActiveSectors(Vector3 position)
        {
            var analysedSectors = new List<Sector>();
            var sectorsQueue = new Queue<Sector>(activeSectors);
            while(sectorsQueue.Count > 0)
            {
                var sector = sectorsQueue.Dequeue();
                if (sector.areas.SectorCollider.OverlapPoint(position))
                {
                    return sector;
                }
                else
                {
                    analysedSectors.Add(sector);
                    foreach(var naa in sector.areas.NeighbourActivationAreas)
                    {
                        this.CheckEnqueueSector(naa.neighbour, analysedSectors, sectorsQueue);
                    }
                }
            }

            return null;

        }

        private void CheckEnqueueSector(Sector sector, List<Sector> analysedSectors, Queue<Sector> sectorsQueue)
        {
            if (sector == null)
            {
                return;
            }

            if (!analysedSectors.Contains(sector) && !sectorsQueue.Contains(sector))
            {
                sectorsQueue.Enqueue(sector);
            }
        }
    }
}
