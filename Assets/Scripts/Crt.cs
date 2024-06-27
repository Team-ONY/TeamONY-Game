using UnityEngine;

[ExecuteAlways]
public class Crt: MonoBehaviour
{
    [SerializeField]private Material m_Material;

    private void OnRenderImage(RenderTexture src,RenderTexture dest){
        Graphics.Blit(src,dest,m_Material);
    }
}
