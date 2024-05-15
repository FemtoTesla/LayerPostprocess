using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace FemtoChanTools.LayerPostprocess
{
    /// <summary>
    /// レイヤー別ポストプロセスのRenderPass
    /// </summary>
    public class LayerPostprocessRenderPass : ScriptableRenderPass
    {
        private const string CmdNameCameraTargetBlit = "CMD_CameraTargetBlit";
        
        private readonly ILayerPostprocessRepository layerPostprocessRepository;
        
        private ScriptableRenderer scriptableRenderer;
        private RenderTextureDescriptor duplicateCameraTargetDescriptor;
        
        private RTHandle beforeRenderLayerPostprocessRTHandle;
        
        public LayerPostprocessRenderPass(ILayerPostprocessRepository layerPostprocessRepository)
        {
            this.layerPostprocessRepository = layerPostprocessRepository;
        }

        /// <summary>
        /// Setup
        /// </summary>
        /// <param name="renderer"></param>
        /// <param name="renderingData"></param>
        public void Setup(ScriptableRenderer renderer, in RenderingData renderingData)
        {
            scriptableRenderer = renderer;
            
            duplicateCameraTargetDescriptor = new RenderTextureDescriptor
            {
                width = Screen.width,
                height = Screen.height,
                msaaSamples = 4,
                graphicsFormat = GraphicsFormat.R8G8B8A8_SRGB,
                dimension = TextureDimension.Tex2D
            };
        }
        
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            // カメラのCullingMaskがNothingになっているので本来のレンダリングは行われない
            IEnumerable<RTHandle> postprocessRTHandles = layerPostprocessRepository.GetPostprocessRTHandles();

            // Layer別ポストプロセス前の描画内容をRTHandleに用意する
            PrepareBeforeRenderLayerPostprocessRTHandle(context, ref renderingData);
        }

        /// <summary>
        /// Layer別ポストプロセス前の描画内容をRTHandleに用意する
        /// </summary>
        /// <param name="context"></param>
        /// <param name="renderingData"></param>
        private void PrepareBeforeRenderLayerPostprocessRTHandle(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            // Alloc
            if (beforeRenderLayerPostprocessRTHandle == null)
            {
                beforeRenderLayerPostprocessRTHandle = RTHandles.Alloc(duplicateCameraTargetDescriptor);
            }
            else
            {
                RenderingUtils.ReAllocateIfNeeded(ref beforeRenderLayerPostprocessRTHandle, duplicateCameraTargetDescriptor);
            }
            beforeRenderLayerPostprocessRTHandle.rt.name = "beforeRenderLayerPostprocessRTHandle";
            
            // cameraTargetをBlit
            CommandBuffer cmd = CommandBufferPool.Get(CmdNameCameraTargetBlit);
            cmd.BeginSample(CmdNameCameraTargetBlit);
            cmd.Blit(scriptableRenderer.cameraColorTargetHandle, beforeRenderLayerPostprocessRTHandle); //  レイヤー別ポストプロセスの処理前のテクスチャ複製
            cmd.EndSample(CmdNameCameraTargetBlit);
            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();
            CommandBufferPool.Release(cmd);
        }
    }

}
