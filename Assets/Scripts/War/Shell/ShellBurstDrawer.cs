using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(MeshCollider))]
public class ShellBurstDrawer : MonoBehaviour, IShallInteractable
{
    [SerializeField] private Texture2D _defaultTexture;
    [SerializeField] private Texture2D _burstTexture;
    [SerializeField] private float _burstSize = 1f;

    private RenderTexture _renderTexture;
    private MeshCollider _colliderMesh;
    private Renderer _rend;

    private void OnEnable()
    {
        _rend = GetComponent<Renderer>();
        _colliderMesh = GetComponent<MeshCollider>();
        
        _renderTexture = new RenderTexture(512, 512, 32);
        _rend.material.SetTexture("_MainTex", _renderTexture);
        
        Graphics.Blit(_defaultTexture, _renderTexture);
    }

    public void ShellTouch(RaycastHit hit)
    {
        var pixelUV = new Vector2(hit.textureCoord.x * _renderTexture.width, hit.textureCoord.y * _renderTexture.height);
        var burstUVSize = new Vector2(_burstSize / _colliderMesh.bounds.size.x, _burstSize / _colliderMesh.bounds.size.y) * _renderTexture.width;

        RenderTexture.active = _renderTexture;
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, _renderTexture.width, _renderTexture.height, 0);

        var rect = new Rect(
            pixelUV.x - burstUVSize.x / 2,
            _renderTexture.height - pixelUV.y - burstUVSize.y / 2,
            burstUVSize.x,
            burstUVSize.y
        );

        Graphics.DrawTexture(rect, _burstTexture);

        GL.PopMatrix();
        RenderTexture.active = null;
    }
}