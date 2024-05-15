using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ManualRenderer : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask0;
    
    [SerializeField]
    private RenderTexture renderTexture1;

    [SerializeField]
    private LayerMask layerMask1;
    
    [SerializeField]
    private RenderTexture renderTexture2;

    [SerializeField]
    private LayerMask layerMask2;
    
    [SerializeField]
    private RenderTexture renderTexture3;

    [SerializeField]
    private LayerMask layerMask3;
    
    [SerializeField]
    private RenderTexture renderTexture4;

    [SerializeField]
    private LayerMask layerMask4;

    [SerializeField]
    private Camera camera;

    // Start is called before the first frame update
    private void Update()
    {
        camera.targetTexture = renderTexture1;
        camera.cullingMask = layerMask1;
        camera.Render();
        
        camera.targetTexture = renderTexture2;
        camera.cullingMask = layerMask2;
        camera.Render();
        
        camera.targetTexture = renderTexture3;
        camera.cullingMask = layerMask3;
        camera.Render();
        
        camera.targetTexture = renderTexture4;
        camera.cullingMask = layerMask4;
        camera.Render();
        
        camera.targetTexture = null;
        camera.cullingMask = layerMask0;
    }
}
