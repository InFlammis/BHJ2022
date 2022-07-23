using System;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public event Action<Vector2, RectTransform> CollisionDetectedEvent;

    /// <summary>
    /// Background Texture
    /// </summary>
    [SerializeField] private Texture bgTexture;

    /// <summary>
    /// Foreground texture
    /// </summary>
    [SerializeField] private Texture stainTexture;

    /// <summary>
    /// Shader to merge the foreground texture onto the background texture
    /// </summary>
    [SerializeField] private ComputeShader bgTextureComputeShader;

    private RawImage rawImage;
    private RenderTexture background;
    private Camera mainCamera;

    /// <summary>
    /// Apply the foreground to the current tile.
    /// Invoke the Shader passing the textures and the position.
    /// </summary>
    /// <param name="offset">The position on the background where to apply the foreground</param>
    public void ApplyForeground(Vector2 offset)
    {
        offset = new Vector2((stainTexture.width * offset.x * 4) - stainTexture.width / 2, (stainTexture.height * offset.y * 4) - stainTexture.height / 2);

        bgTextureComputeShader.SetTexture(0, "Result", background);
        bgTextureComputeShader.SetTexture(0, "InputTxt", bgTexture);
        bgTextureComputeShader.SetTexture(0, "SpotTxt", stainTexture);
        bgTextureComputeShader.SetInts("offset", new int[] { (int)(offset.x), (int)(offset.y) });

        var dispatchSize = new Vector3Int(Mathf.CeilToInt((stainTexture.width) / 8f), Mathf.CeilToInt((stainTexture.height) / 8f), 1);

        bgTextureComputeShader.Dispatch(0, dispatchSize.x, dispatchSize.y, dispatchSize.z);
    }

    private void Awake()
    {
        mainCamera = Camera.main;
        rawImage = GetComponent<RawImage>();

        background = new RenderTexture(bgTexture.width, bgTexture.height, 0);
        background.enableRandomWrite = true;
        background.Create();

        rawImage.texture = background;
    }

    void Start()
    {
        Graphics.CopyTexture(bgTexture, background);
    }
}