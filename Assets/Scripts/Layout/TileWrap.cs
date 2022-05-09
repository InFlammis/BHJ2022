using UnityEngine;

public class TileWrap
{
    public Tile tile { get; private set; }
    public Vector2 offsetMin { get; private set; }
    public Vector2 offsetMax { get; private set; }

    public TileWrap(Tile tile)
    {
        this.tile = tile;
        var rectTransform = tile.transform as RectTransform;
        offsetMin = rectTransform.offsetMin;
        offsetMax = rectTransform.offsetMax;
    }
}
