using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace FemtoChanTools.LayerPostprocess
{
    /// <summary>
    /// RTHandle管理リポジトリ
    /// </summary>
    public interface IRTHandleRepository
    {
        /// <summary>
        /// keyと紐づけたRTHandleをAllocあるいはReallocateする
        /// </summary>
        /// <param name="descriptor"></param>
        /// <param name="key"></param>
        /// <param name="filterMode"></param>
        /// <param name="wrapMode"></param>
        /// <param name="isShadowMap"></param>
        /// <param name="anisoLevel"></param>
        /// <param name="mipMapBias"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        bool AllocateRTHandle(in RenderTextureDescriptor descriptor, 
            string key,
            FilterMode filterMode = FilterMode.Point,
            TextureWrapMode wrapMode = TextureWrapMode.Repeat,
            bool isShadowMap = false,
            int anisoLevel = 1,
            float mipMapBias = 0,
            string name = "");

        /// <summary>
        /// keyに紐づいたRTHandleを取得する
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        RTHandle GetRTHandleByKey(string key);

        /// <summary>
        /// 確保していたRTHandleを解放する
        /// </summary>
        /// <returns></returns>
        bool ReleaseRTHandle(string key);
        
        /// <summary>
        /// 確保していたRTHandleを全て解放する
        /// </summary>
        /// <returns></returns>
        bool ReleaseRTHandleAll();
    }

}
