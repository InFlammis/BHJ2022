using System;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public event Action<Vector2, RectTransform> CollisionDetectedEvent;

    [SerializeField] private Texture bgTexture;
    [SerializeField] private Texture stainTexture;
    [SerializeField] private ComputeShader bgTextureComputeShader;

    private RawImage rawImage;
    private RenderTexture background;
    private Camera mainCamera;

    public void OnSelect(Vector3 contactPoint)
    {
        //TODO: REVIEW THIS SCRIPT
        var rectTransform = this.gameObject.transform as RectTransform;
        var xMin = rectTransform.offsetMin.x;
        var yMin = rectTransform.offsetMin.y;

        var mouseX = (contactPoint.x - xMin)/ rectTransform.rect.width;
        var mouseY = (contactPoint.y - yMin)/rectTransform.rect.height;

        var mouseRelPos = new Vector2(mouseX, mouseY);

        CollisionDetectedEvent?.Invoke(mouseRelPos, rectTransform);

        ApplyForeground(mouseRelPos);
    }

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