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

    /// <summary>
    /// The background texture where the forground texture is applied.
    /// </summary>
    [SerializeField] private Texture2D background;

    /// <summary>
    /// The texture to apply to the background.
    /// </summary>
    [SerializeField] private Texture2D foreground;

    /// <summary>
    /// The prefab for the tile.
    /// </summary>
    [SerializeField] private GameObject tile;

    /// <summary>
    /// Size of the canvas containing the grid of tiles.
    /// </summary>
    private Vector2 tileManagerSize = new Vector2(80, 120);

    /// <summary>
    /// A dictionary containing all the tiles indexed by their position.
    /// </summary>
    private Dictionary<Vector2, Tile> tileDictionary = new ();

    /// <summary>
    /// The relative shifts to find the neighbours of a tile.
    /// </summary>
    private readonly Vector2 leftShift = new Vector2(-1, 0);
    private readonly Vector2 topShift = new Vector2(0, 1);
    private readonly Vector2 topLeftShift = new Vector2(-1, 1);
    private readonly Vector2 bottomShift = new Vector2(0, -1);
    private readonly Vector2 bottomLeftShift = new Vector2(-1, -1);
    private readonly Vector2 rightShift = new Vector2(1, 0);
    private readonly Vector2 topRightShift = new Vector2(1, 1);
    private readonly Vector2 bottomRightShift = new Vector2(1, -1);

    /// <summary>
    /// Half size of the ground canvas
    /// </summary>
    private Vector2 groundHalfSize;
    
    /// <summary>
    /// The tile size.
    /// </summary>
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

        // Build the dictionary
        for(var x = groundRect.xMin; x < groundRect.xMax; x += tileRect.width)
        {
            for(var y = groundRect.yMin; y < groundRect.yMax; y += tileRect.height)
            {
                // The value
                var newTile = GameObject.Instantiate(tile, new Vector3(x, y, 0), Quaternion.identity);
                newTile.transform.SetParent(this.gameObject.transform);

                // The key
                var key = new Vector2(x, y);

                if(tileDictionary.ContainsKey(key)){
                    Debug.LogError($"Key {key} already found in the tile dictionary");
                }

                tileDictionary.Add(new Vector2(x, y), newTile.GetComponent<Tile>());
            }
        }

        SubscribeToSpitEvents();
    }

    private void SubscribeToSpitEvents()
    {
        var messenger = (_staticObjects.Messenger as ISpitEventsMessenger);

        messenger.HasDied.AddListener(Spit_HasDied);
    }

    private void Spit_HasDied(object publisher, string target)
    {
        var publisherGo = (GameObject)publisher;

        Tile tileToProcess = FindTile(publisherGo.transform.position);

        if (tileToProcess != null)
        {
            ApplySpitOnTile(publisherGo.transform.position, tileToProcess);
        }
    }

    /// <summary>
    /// Find the tile containing the position.
    /// 1. Translate the tile ground so that the origin is in (0, 0).
    /// 2. Translate the position coordinates to the new coordinate system.
    /// 3. Normalize the position to the closest previous start of a tile.
    /// 4. Translate the normalized position of the tile start back to the original coordinate system.
    /// 5. Look up in the dictionary and find the tile.
    /// </summary>
    /// <param name="position">The position</param>
    /// <returns>The tile containing the position.</returns>
    private Tile FindTile(Vector3 position)
    {
        var tileRectTransform = tile.transform as RectTransform;

        // Translate the position coordinates to the new coordinate system.
        var positionNormalized = new Vector2(position.x + groundHalfSize.x, position.y + groundHalfSize.y);

        // Normalize the position to the closest previous start of a tile.
        var tileKeyNormalized = new Vector2(
            positionNormalized.x - (positionNormalized.x % tileRectTransform.rect.width),
            positionNormalized.y - (positionNormalized.y % tileRectTransform.rect.height)
            );

        // Translate the normalized position of the tile start back to the original coordinate system.
        var tileDictKey = tileKeyNormalized - groundHalfSize;

        // Look up in the dictionary and find the tile.
        if (!tileDictionary.TryGetValue(tileDictKey, out var tileFound))
        {
            Debug.LogError($"Could not find tile {tileDictKey} in dictionary.");
            return null;
        }

        return tileFound;
    }

    /// <summary>
    /// 1. Calculate the position relative to the tile.
    /// 2. Apply the foreground to the tile at the relative position.
    /// 3. Check whether the sprite overflows in some of the eight neigbour tiles.
    /// 3.1 If it does, calculate the position of the sprite relative to the neigbour tile
    /// 3.2 Apply the foreground to the neighbour tile at the relative position.
    /// </summary>
    /// <param name="position">The position where to apply the stain.</param>
    /// <param name="tile">The tile containing the position.</param>
    private void ApplySpitOnTile(Vector2 position, Tile tile)
    {
        var rectTransform = tile.transform as RectTransform;

        // Calculate the position relative to the tile size [x, y} -> [0, 1}
        var relativePosition = new Vector2(
            (position.x - rectTransform.position.x) / tileSize.x,
            (position.y - rectTransform.position.y) / tileSize.x
            );

        // Apply the foreground to the tile at the relative position
        tile.ApplyForeground(relativePosition);

        // Check whether the sprite overflows to the neigbour tiles.
        var notifyLeft = background.width * relativePosition.x - foreground.width / 2 < 0;
        var notifyRight = background.width * relativePosition.x + foreground.width / 2 > background.width;
        var notifyBottom = background.height * relativePosition.y - foreground.height / 2 < 0;
        var notifyTop = background.height * relativePosition.y + foreground.height / 2 > background.height;

        // If it overflows to the left neighbour
        if (notifyLeft )
        {
            // Calculate the relative position respect to the neighbour
            var leftTilePosition = rectTransform.offsetMin + leftShift * tileSize;

            // Find the neighbour tile and apply the sprite to the relative position
            if (tileDictionary.ContainsKey(leftTilePosition))
            {
                var newRelativePosition = relativePosition - leftShift;
                tileDictionary[leftTilePosition].ApplyForeground(newRelativePosition);
            }

            // If it overflows to the left-top neighbour
            if (notifyTop)
            {
                // Calculate the relative position respect to the neighbour
                var topTilePosition = rectTransform.offsetMin + topShift * tileSize;

                // Find the neighbour tile and apply the sprite to the relative position
                if (tileDictionary.ContainsKey(topTilePosition))
                {
                    var newRelativePosition = relativePosition - topShift;
                    tileDictionary[topTilePosition].ApplyForeground(newRelativePosition);
                }

                // Calculate the relative position respect to the neighbour
                var topLeftTilePosition = rectTransform.offsetMin + topLeftShift * tileSize;

                // Find the neighbour tile and apply the sprite to the relative position
                if (tileDictionary.ContainsKey(topLeftTilePosition))
                {
                    var newRelativePosition = relativePosition - topLeftShift;
                    tileDictionary[topLeftTilePosition].ApplyForeground(newRelativePosition);
                }
            }

            // If it overflows to the left-bottom neighbour
            if (notifyBottom)
            {
                // Calculate the relative position respect to the neighbour
                var bottomTilePosition = rectTransform.offsetMin + bottomShift * tileSize;

                // Find the neighbour tile and apply the sprite to the relative position
                if (tileDictionary.ContainsKey(bottomTilePosition))
                {
                    var newRelativePosition = relativePosition - bottomShift;
                    tileDictionary[bottomTilePosition].ApplyForeground(newRelativePosition);
                }

                // Calculate the relative position respect to the neighbour
                var bottomLeftTilePosition = rectTransform.offsetMin + bottomLeftShift * tileSize;

                // Find the neighbour tile and apply the sprite to the relative position
                if (tileDictionary.ContainsKey(bottomLeftTilePosition))
                {
                    var newRelativePosition = relativePosition - bottomLeftShift;
                    tileDictionary[bottomLeftTilePosition].ApplyForeground(newRelativePosition);
                }
            }
        }

        // If it overflows to the right neighbour
        else if (notifyRight)
        {
            // Calculate the relative position respect to the neighbour
            var rightTilePosition = rectTransform.offsetMin + rightShift * tileSize;

            // Find the neighbour tile and apply the sprite to the relative position
            if (tileDictionary.ContainsKey(rightTilePosition))
            {
                var newRelativePosition = relativePosition - rightShift;
                tileDictionary[rightTilePosition].ApplyForeground(newRelativePosition);
            }

            // If it overflows to the right-top neighbour
            if (notifyTop)
            {
                // Calculate the relative position respect to the neighbour
                var topTilePosition = rectTransform.offsetMin + topShift * tileSize;

                // Find the neighbour tile and apply the sprite to the relative position
                if (tileDictionary.ContainsKey(topTilePosition))
                {
                    var newRelativePosition = relativePosition - topShift;
                    tileDictionary[topTilePosition].ApplyForeground(newRelativePosition);
                }

                // Calculate the relative position respect to the neighbour
                var topRightTilePosition = rectTransform.offsetMin + topRightShift * tileSize;

                // Find the neighbour tile and apply the sprite to the relative position
                if (tileDictionary.ContainsKey(topRightTilePosition))
                {
                    var newRelativePosition = relativePosition - topRightShift;
                    tileDictionary[topRightTilePosition].ApplyForeground(newRelativePosition);
                }
            }

            // If it overflows to the right-bottom neighbour
            if (notifyBottom)
            {
                // Calculate the relative position respect to the neighbour
                var bottomTilePosition = rectTransform.offsetMin + bottomShift * tileSize;

                // Find the neighbour tile and apply the sprite to the relative position
                if (tileDictionary.ContainsKey(bottomTilePosition))
                {
                    var newRelativePosition = relativePosition - bottomShift;
                    tileDictionary[bottomTilePosition].ApplyForeground(newRelativePosition);
                }

                // Calculate the relative position respect to the neighbour
                var bottomRightTilePosition = rectTransform.offsetMin + bottomRightShift * tileSize;

                // Find the neighbour tile and apply the sprite to the relative position
                if (tileDictionary.ContainsKey(bottomRightTilePosition))
                {
                    var newRelativePosition = relativePosition - bottomRightShift;
                    tileDictionary[bottomRightTilePosition].ApplyForeground(newRelativePosition);
                }
            }
        }
        else
        {
            // If it overflows to the top neighbour
            if (notifyTop)
            {
                // Calculate the relative position respect to the neighbour
                var topTilePosition = rectTransform.offsetMin + topShift * tileSize;

                // Find the neighbour tile and apply the sprite to the relative position
                if (tileDictionary.ContainsKey(topTilePosition))
                {
                    var newRelativePosition = relativePosition - topShift;
                    tileDictionary[topTilePosition].ApplyForeground(newRelativePosition);
                }
            }

            // If it overflows to the bottom neighbour
            else if (notifyBottom)
            {
                // Calculate the relative position respect to the neighbour
                var bottomTilePosition = rectTransform.offsetMin + bottomShift * tileSize;

                // Find the neighbour tile and apply the sprite to the relative position
                if (tileDictionary.ContainsKey(bottomTilePosition))
                {
                    var newRelativePosition = relativePosition - bottomShift;
                    tileDictionary[bottomTilePosition].ApplyForeground(newRelativePosition);
                }
            }
        }
    }
}