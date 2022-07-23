using InFlammis.Victoria.Assets.Scripts.Managers;
using InFlammis.Victoria.Assets.Scripts.MessageBroker.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Canvas), typeof(RectTransform))]
public class TileManager : MonoBehaviour
{
    [SerializeField] private StaticObjectsSO _staticObjects;

    [SerializeField] private Texture2D background;
    [SerializeField] private Texture2D foreground;

    [SerializeField] private GameObject tile;

    private Vector2 tileManagerSize = new Vector2(80, 120);

    private IDictionary<Vector2, TileWrap> tileWrapsDictionary;
    private Dictionary<Vector2, Tile> tileDictionary = new ();

    private readonly Vector2 leftShift = new Vector2(-1, 0);
    private readonly Vector2 topShift = new Vector2(0, 1);
    private readonly Vector2 topLeftShift = new Vector2(-1, 1);
    private readonly Vector2 bottomShift = new Vector2(0, -1);
    private readonly Vector2 bottomLeftShift = new Vector2(-1, -1);
    private readonly Vector2 rightShift = new Vector2(1, 0);
    private readonly Vector2 topRightShift = new Vector2(1, 1);
    private readonly Vector2 bottomRightShift = new Vector2(1, -1);

    private Vector2 groundHalfSize;
    private Vector2 tileSize;

    void Awake()
    {
        var groundRectTransform = gameObject.transform as RectTransform;

        // Position the TilesGrid
        groundRectTransform.localScale = Vector3.one;
        groundRectTransform.SetPositionAndRotation( Vector3.zero, Quaternion.identity);
        groundRectTransform.pivot = new Vector2(0.5f, 0.5f);
        groundRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tileManagerSize.x);
        groundRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, tileManagerSize.y);

        var groundRect = groundRectTransform.rect;
        var tileRectTransform = tile.transform as RectTransform;
        var tileRect = tileRectTransform.rect;

        groundHalfSize = new Vector2(groundRect.width / 2, groundRect.height / 2);
        tileSize = tileRect.size;

        for(var x = groundRect.xMin; x < groundRect.xMax; x += tileRect.width)
        {
            for(var y = groundRect.yMin; y < groundRect.yMax; y += tileRect.height)
            {
                var newTile = GameObject.Instantiate(tile, new Vector3(x, y, 0), Quaternion.identity);
                newTile.transform.SetParent(this.gameObject.transform);
                var key = new Vector2(x, y);
                if(tileDictionary.ContainsKey(key)){
                    Debug.Log("key found");
                }
                tileDictionary.Add(new Vector2(x, y), newTile.GetComponent<Tile>());
            }
        }

        SubscribeToSpitEvents();
    }

    // Start is called before the first frame update
    void Start()
    {
        //tileWrapsDictionary = FindObjectsOfType<Tile>().Select(
        //    x =>
        //    {
        //        var tileWrap = new TileWrap(x);
        //        //x.CollisionDetectedEvent += X_CollisionDetectedEvent;
        //        return new KeyValuePair<Vector2, TileWrap>(tileWrap.offsetMin, tileWrap);
        //    }).ToDictionary(x => x.Key, x => x.Value);
    }

    public virtual void SubscribeToSpitEvents()
    {
        var messenger = (_staticObjects.Messenger as ISpitEventsMessenger);

        messenger.HasDied.AddListener(Spit_HasDied);
    }

    private void Spit_HasDied(object publisher, string target)
    {
        var publisherGo = (GameObject)publisher;

        //Find RectTransform of the Tile
        Tile tileToProcess = FindTile(publisherGo.transform.position);

        ApplySpitOnTile(publisherGo.transform.position, tileToProcess);
    }

    private Tile FindTile(Vector3 position)
    {
        var tileRectTransform = tile.transform as RectTransform;

        var positionNormalized = new Vector2(position.x + groundHalfSize.x, position.y + groundHalfSize.y);

        var tileKeyNormalized = new Vector2(
            positionNormalized.x - (positionNormalized.x % tileRectTransform.rect.width),
            positionNormalized.y - (positionNormalized.y % tileRectTransform.rect.height)
            );

        var tileDictKey = tileKeyNormalized - groundHalfSize;

        if (!tileDictionary.TryGetValue(tileDictKey, out var tileFound))
        {
            Debug.LogError($"Could not find tile {tileDictKey} in dictionary.");
            return null;
        }

        return tileFound;
    }

    private void ApplySpitOnTile(Vector2 position, Tile tile)
    {
        var rectTransform = tile.transform as RectTransform;

        var relativePosition = new Vector2(
            (position.x - rectTransform.position.x) / tileSize.x,
            (position.y - rectTransform.position.y) / tileSize.x
            );

        tile.ApplyForeground(relativePosition);


        var notifyLeft = background.width * relativePosition.x - foreground.width / 2 < 0;
        var notifyRight = background.width * relativePosition.x + foreground.width / 2 > background.width;
        var notifyBottom = background.height * relativePosition.y - foreground.height / 2 < 0;
        var notifyTop = background.height * relativePosition.y + foreground.height / 2 > background.height;
        if (notifyLeft )
        {
            //notifyLeft
            var leftTilePosition = rectTransform.offsetMin + leftShift * tileSize;
            if (tileDictionary.ContainsKey(leftTilePosition))
            {
                var newRelativePosition = relativePosition - leftShift;
                tileDictionary[leftTilePosition].ApplyForeground(newRelativePosition);
            }

            if (notifyTop)
            {
                //notify top and top-left
                var topTilePosition = rectTransform.offsetMin + topShift * tileSize;
                if (tileDictionary.ContainsKey(topTilePosition))
                {
                    var newRelativePosition = relativePosition - topShift;
                    tileDictionary[topTilePosition].ApplyForeground(newRelativePosition);
                }

                var topLeftTilePosition = rectTransform.offsetMin + topLeftShift * tileSize;
                if (tileDictionary.ContainsKey(topLeftTilePosition))
                {
                    var newRelativePosition = relativePosition - topLeftShift;
                    tileDictionary[topLeftTilePosition].ApplyForeground(newRelativePosition);
                }
            }
            if (notifyBottom)
            {
                //notify bottom and bottom-left
                var bottomTilePosition = rectTransform.offsetMin + bottomShift * tileSize;
                if (tileDictionary.ContainsKey(bottomTilePosition))
                {
                    var newRelativePosition = relativePosition - bottomShift;
                    tileDictionary[bottomTilePosition].ApplyForeground(newRelativePosition);
                }

                var bottomLeftTilePosition = rectTransform.offsetMin + bottomLeftShift * tileSize;
                if (tileDictionary.ContainsKey(bottomLeftTilePosition))
                {
                    var newRelativePosition = relativePosition - bottomLeftShift;
                    tileDictionary[bottomLeftTilePosition].ApplyForeground(newRelativePosition);
                }
            }
        }
        else if (notifyRight)
        {
            //notify right
            var rightTilePosition = rectTransform.offsetMin + rightShift * tileSize;
            if (tileDictionary.ContainsKey(rightTilePosition))
            {
                var newRelativePosition = relativePosition - rightShift;
                tileDictionary[rightTilePosition].ApplyForeground(newRelativePosition);
            }

            if (notifyTop)
            {
                //notify top and top-right
                var topTilePosition = rectTransform.offsetMin + topShift * tileSize;
                if (tileDictionary.ContainsKey(topTilePosition))
                {
                    var newRelativePosition = relativePosition - topShift;
                    tileDictionary[topTilePosition].ApplyForeground(newRelativePosition);
                }

                var topRightTilePosition = rectTransform.offsetMin + topRightShift * tileSize;
                if (tileDictionary.ContainsKey(topRightTilePosition))
                {
                    var newRelativePosition = relativePosition - topRightShift;
                    tileDictionary[topRightTilePosition].ApplyForeground(newRelativePosition);
                }
            }
            if (notifyBottom)
            {
                //notify bottom and bottom-right
                var bottomTilePosition = rectTransform.offsetMin + bottomShift * tileSize;
                if (tileDictionary.ContainsKey(bottomTilePosition))
                {
                    var newRelativePosition = relativePosition - bottomShift;
                    tileDictionary[bottomTilePosition].ApplyForeground(newRelativePosition);
                }

                var bottomRightTilePosition = rectTransform.offsetMin + bottomRightShift * tileSize;
                if (tileDictionary.ContainsKey(bottomRightTilePosition))
                {
                    var newRelativePosition = relativePosition - bottomRightShift;
                    tileDictionary[bottomRightTilePosition].ApplyForeground(newRelativePosition);
                }
            }
        }
        else
        {
            if (notifyTop)
            {
                //notify top
                var topTilePosition = rectTransform.offsetMin + topShift * tileSize;
                if (tileDictionary.ContainsKey(topTilePosition))
                {
                    var newRelativePosition = relativePosition - topShift;
                    tileDictionary[topTilePosition].ApplyForeground(newRelativePosition);
                }
            }
            else if (notifyBottom)
            {
                //notify bottom
                var bottomTilePosition = rectTransform.offsetMin + bottomShift * tileSize;
                if (tileDictionary.ContainsKey(bottomTilePosition))
                {
                    var newRelativePosition = relativePosition - bottomShift;
                    tileDictionary[bottomTilePosition].ApplyForeground(newRelativePosition);
                }
            }
        }
    }
}