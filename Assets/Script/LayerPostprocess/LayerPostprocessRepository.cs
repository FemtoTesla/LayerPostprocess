using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace FemtoChanTools.LayerPostprocess
{
    /// <summary>
    /// RTHandle管理リポジトリ
    /// </summary>
    public class LayerPostprocessRepository : ILayerPostprocessRepository
    {
        // singleton
        private static LayerPostprocessRepository __instance;
        public static LayerPostprocessRepository Instance
        {
            get
            {
                if (__instance == null)
                {
                    __instance = new LayerPostprocessRepository();
                }
                return __instance;
            }
        }

        private Dictionary<string, RTHandle> rtHandleDic;
        private RenderTextureDescriptor cameraTargetDescriptorCache;

        private LayerPostprocessRepository()
        {
            rtHandleDic = new Dictionary<string, RTHandle>();
            cameraTargetDescriptorCache = new RenderTextureDescriptor
            {
                width = Screen.width,
                height = Screen.height,
                msaaSamples = 1,
                volumeDepth = 0,
                mipCount = 0,
                graphicsFormat = GraphicsFormat.R8G8B8A8_UNorm,
                stencilFormat = GraphicsFormat.None,
                depthStencilFormat = GraphicsFormat.None,
                colorFormat = RenderTextureFormat.ARGB32,
                sRGB = false,
                depthBufferBits = 0,
                dimension = TextureDimension.Tex2D,
                shadowSamplingMode = ShadowSamplingMode.CompareDepths,
                vrUsage = VRTextureUsage.None,
                memoryless = RenderTextureMemoryless.None,
                useMipMap = false,
                autoGenerateMips = false,
                enableRandomWrite = false,
                bindMS = false,
                useDynamicScale = false
            };
        }

        public RenderTextureDescriptor GetCameraTargetDescriptor()
        {
            return cameraTargetDescriptorCache;
        }

        /// <inheritdoc/>
        public IEnumerable<RTHandle> GetPostprocessRTHandles()
        {
            return rtHandleDic.Values;
        }

        /// <inheritdoc/>
        public bool AllocateRTHandle(in RenderTextureDescriptor descriptor, 
            string key,
            FilterMode filterMode = FilterMode.Point,
            TextureWrapMode wrapMode = TextureWrapMode.Repeat,
            bool isShadowMap = false,
            int anisoLevel = 1,
            float mipMapBias = 0,
            string name = "")
        {
            bool isExist = rtHandleDic.ContainsKey(key);
            if (isExist)
            {
                Debug.LogWarning($"RTHandle is Exist. key:{key}");
                return false;
            }

            RTHandle rtHandle = RTHandles.Alloc(descriptor, filterMode, wrapMode, isShadowMap, anisoLevel, mipMapBias, name);
            bool isAllocateSuccess = rtHandle != null;
            if (!isAllocateSuccess)
            {
                Debug.LogError($"RTHandle allocate is failed.");
                return false;
            }
            rtHandleDic.Add(key, rtHandle);
            return true;
        }

        /// <inheritdoc/>
        public RTHandle GetRTHandleByKey(string key)
        {
            if (!rtHandleDic.TryGetValue(key, out RTHandle rtHandle))
            {
                Debug.LogError($"RTHandle is Not Exist. key:{key}");
                return null;
            }
            return rtHandle;
        }

        /// <inheritdoc/>
        public bool ReleaseRTHandle(string key)
        {
            if (!rtHandleDic.TryGetValue(key, out RTHandle rtHandle))
            {
                Debug.LogError($"RTHandle is Not Exist. key:{key}");
                return false;
            }
            
            rtHandleDic[key].Release();
            RTHandles.Release(rtHandleDic[key]);
            rtHandleDic.Remove(key);
            return true;
        }

        /// <inheritdoc/>
        public bool ReleaseRTHandleAll()
        {
            foreach (KeyValuePair<string, RTHandle> kvp in rtHandleDic)
            {
                kvp.Value.rt.Release();
                RTHandles.Release(kvp.Value);
            }
            rtHandleDic.Clear();
            return true;
        }
    }

}
