using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(MeshCollider))]
public class ShellBurstDrawer : MonoBehaviour, IShallInteractable
{
    [SerializeField] private Texture2D _defaultTexture;
    [SerializeField] private Texture2D _burstTexture;
    [SerializeField] private float burstSize = 1f;

    private RenderTexture _renderTexture;
    private Renderer _renderer;
    private MeshCollider _meshCollider;

    private void OnEnable()
    {
        _renderTexture = new RenderTexture(512, 512, 32);
        _renderer = GetComponent<Renderer>();
        _meshCollider = GetComponent<MeshCollider>();
        _renderer.material.SetTexture("_MainTex", _renderTexture);
        Graphics.Blit(_defaultTexture, _renderTexture);
    }

    public void ShellTouch(RaycastHit hit)
    {
        Vector2 pixelUV = hit.textureCoord;

        float burstUVSizeX = burstSize / _meshCollider.bounds.size.x;
        float burstUVSizeY = burstSize / _meshCollider.bounds.size.y;

        pixelUV.x *= _renderTexture.width;
        pixelUV.y *= _renderTexture.height;

        RenderTexture.active = _renderTexture;

        GL.PushMatrix();
        GL.LoadPixelMatrix(0, _renderTexture.width, _renderTexture.height, 0);

        Rect drawRect = new Rect
        (
            pixelUV.x - (_renderTexture.width * burstUVSizeX) / 2,
            (_renderTexture.height - pixelUV.y) - (_renderTexture.height * burstUVSizeY) / 2,
            _renderTexture.width * burstUVSizeX,
            _renderTexture.height * burstUVSizeY
        );

        Graphics.DrawTexture(drawRect, _burstTexture);

        GL.PopMatrix();
        RenderTexture.active = null;
    }
}
