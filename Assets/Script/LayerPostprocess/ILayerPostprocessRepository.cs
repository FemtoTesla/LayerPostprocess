using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace FemtoChanTools.LayerPostprocess
{
    /// <summary>
    /// LayerPostprocessリポジトリ
    /// </summary>
    public interface ILayerPostprocessRepository : IRTHandleRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        RenderTextureDescriptor GetCameraTargetDescriptor();

        /// <summary>
        /// ポストプロセス済みのRenderTextureのリストを得る
        /// 黒背景、depthなし、ポストプロセス済み
        /// </summary>
        /// <returns></returns>
        IEnumerable<RTHandle> GetPostprocessRTHandles();
    }

}
