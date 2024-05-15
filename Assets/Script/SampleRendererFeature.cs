using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SampleRendererFeature : ScriptableRendererFeature
{
    [SerializeField]
    private RenderPassEvent renderPassEvent;
    
    private SampleRendererPass sampleRendererPass;
    
    public override void Create()
    {
        sampleRendererPass = new SampleRendererPass(renderPassEvent);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(sampleRendererPass);
    }
}
