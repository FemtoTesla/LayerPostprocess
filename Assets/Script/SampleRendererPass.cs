using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SampleRendererPass : ScriptableRenderPass
{
    public SampleRendererPass(RenderPassEvent renderPassEvent)
    {
        this.renderPassEvent = renderPassEvent;
    }
    
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        //Debug.Log("Execute SampleRendererPass");
    }
}