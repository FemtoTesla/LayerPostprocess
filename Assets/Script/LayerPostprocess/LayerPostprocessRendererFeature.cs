using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace FemtoChanTools.LayerPostprocess
{
    /// <summary>
    /// レイヤー別ポストプロセスのRendererFeature
    /// </summary>
    public class LayerPostprocessRendererFeature : ScriptableRendererFeature
    {
        [SerializeField] 
        private RenderPassEvent renderPassEvent;
        
        private LayerPostprocessRenderPass layerPostprocessRenderPass;
        
        public override void Create()
        {
            layerPostprocessRenderPass = new LayerPostprocessRenderPass(LayerPostprocessRepository.Instance);
            layerPostprocessRenderPass.renderPassEvent = renderPassEvent;
        }

        public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
        {
            base.SetupRenderPasses(renderer, in renderingData);

            layerPostprocessRenderPass.Setup(renderer, in renderingData);
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            renderer.EnqueuePass(layerPostprocessRenderPass);
        }
    }

}
