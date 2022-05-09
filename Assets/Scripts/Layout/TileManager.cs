using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Texture2D background;
    [SerializeField] private Texture2D foreground;

    private IDictionary<Vector2, TileWrap> tileWrapsDictionary;

    private readonly Vector2 leftShift = new Vector2(-1, 0);
    private readonly Vector2 topShift = new Vector2(0, 1);
    private readonly Vector2 topLeftShift = new Vector2(-1, 1);
    private readonly Vector2 bottomShift = new Vector2(0, -1);
    private readonly Vector2 bottomLeftShift = new Vector2(-1, -1);
    private readonly Vector2 rightShift = new Vector2(1, 0);
    private readonly Vector2 topRightShift = new Vector2(1, 1);
    private readonly Vector2 bottomRightShift = new Vector2(1, -1);

    // Start is called before the first frame update
    void Start()
    {
        tileWrapsDictionary = FindObjectsOfType<Tile>().Select(
            x =>
            {
                var tileWrap = new TileWrap(x);
                x.CollisionDetectedEvent += X_CollisionDetectedEvent;
                return new KeyValuePair<Vector2, TileWrap>(tileWrap.offsetMin, tileWrap);
            }).ToDictionary(x => x.Key, x => x.Value);
    }

    private void X_CollisionDetectedEvent(Vector2 mouseRel, RectTransform rectTransform)
    {
        var notifyLeft = background.width * mouseRel.x - foreground.width / 2 < 0;
        var notifyRight = background.width * mouseRel.x + foreground.width / 2 > background.width;
        var notifyBottom = background.height * mouseRel.y - foreground.height / 2 < 0;
        var notifyTop = background.height * mouseRel.y + foreground.height / 2 > background.height;
        if (notifyLeft )
        {
            //notifyLeft
            var leftTilePosition = rectTransform.offsetMin + leftShift;
            if (tileWrapsDictionary.ContainsKey(leftTilePosition))
            {
                var newMouseRel = mouseRel - leftShift;
                tileWrapsDictionary[leftTilePosition].tile.ApplyForeground(newMouseRel);
            }

            if (notifyTop)
            {
                //notify top and top-left
                var topTilePosition = rectTransform.offsetMin + topShift;
                if (tileWrapsDictionary.ContainsKey(topTilePosition))
                {
                    var newMouseRel = mouseRel - topShift;
                    tileWrapsDictionary[topTilePosition].tile.ApplyForeground(newMouseRel);
                }

                var topLeftTilePosition = rectTransform.offsetMin + topLeftShift;
                if (tileWrapsDictionary.ContainsKey(topLeftTilePosition))
                {
                    var newMouseRel = mouseRel - topLeftShift;
                    tileWrapsDictionary[topLeftTilePosition].tile.ApplyForeground(newMouseRel);
                }
            }
            if (notifyBottom)
            {
                //notify bottom and bottom-left
                var bottomTilePosition = rectTransform.offsetMin + bottomShift;
                if (tileWrapsDictionary.ContainsKey(bottomTilePosition))
                {
                    var newMouseRel = mouseRel - bottomShift;
                    tileWrapsDictionary[bottomTilePosition].tile.ApplyForeground(newMouseRel);
                }

                var bottomLeftTilePosition = rectTransform.offsetMin + bottomLeftShift;
                if (tileWrapsDictionary.ContainsKey(bottomLeftTilePosition))
                {
                    var newMouseRel = mouseRel - bottomLeftShift;
                    tileWrapsDictionary[bottomLeftTilePosition].tile.ApplyForeground(newMouseRel);
                }
            }
        }
        else if (notifyRight)
        {
            //notify right
            var rightTilePosition = rectTransform.offsetMin + rightShift;
            if (tileWrapsDictionary.ContainsKey(rightTilePosition))
            {
                var newMouseRel = mouseRel - rightShift;
                tileWrapsDictionary[rightTilePosition].tile.ApplyForeground(newMouseRel);
            }

            if (notifyTop)
            {
                //notify top and top-right
                var topTilePosition = rectTransform.offsetMin + topShift;
                if (tileWrapsDictionary.ContainsKey(topTilePosition))
                {
                    var newMouseRel = mouseRel - topShift;
                    tileWrapsDictionary[topTilePosition].tile.ApplyForeground(newMouseRel);
                }

                var topRightTilePosition = rectTransform.offsetMin + topRightShift;
                if (tileWrapsDictionary.ContainsKey(topRightTilePosition))
                {
                    var newMouseRel = mouseRel - topRightShift;
                    tileWrapsDictionary[topRightTilePosition].tile.ApplyForeground(newMouseRel);
                }
            }
            if (notifyBottom)
            {
                //notify bottom and bottom-right
                var bottomTilePosition = rectTransform.offsetMin + bottomShift;
                if (tileWrapsDictionary.ContainsKey(bottomTilePosition))
                {
                    var newMouseRel = mouseRel - bottomShift;
                    tileWrapsDictionary[bottomTilePosition].tile.ApplyForeground(newMouseRel);
                }

                var bottomRightTilePosition = rectTransform.offsetMin + bottomRightShift;
                if (tileWrapsDictionary.ContainsKey(bottomRightTilePosition))
                {
                    var newMouseRel = mouseRel - bottomRightShift;
                    tileWrapsDictionary[bottomRightTilePosition].tile.ApplyForeground(newMouseRel);
                }
            }
        }
        else
        {
            if (notifyTop)
            {
                //notify top
                var topTilePosition = rectTransform.offsetMin + topShift;
                if (tileWrapsDictionary.ContainsKey(topTilePosition))
                {
                    var newMouseRel = mouseRel - topShift;
                    tileWrapsDictionary[topTilePosition].tile.ApplyForeground(newMouseRel);
                }
            }
            else if (notifyBottom)
            {
                //notify bottom
                var bottomTilePosition = rectTransform.offsetMin + bottomShift;
                if (tileWrapsDictionary.ContainsKey(bottomTilePosition))
                {
                    var newMouseRel = mouseRel - bottomShift;
                    tileWrapsDictionary[bottomTilePosition].tile.ApplyForeground(newMouseRel);
                }
            }
        }
    }
}