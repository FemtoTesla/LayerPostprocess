using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace FemtoChanTools.LayerPostprocess
{
    /// <summary>
    /// Layer別にPostProcessをかけたRTHandleを生成する
    /// </summary>
    [ExecuteAlways]
    public class LayerPostprocessSettings : MonoBehaviour
    {
        private ILayerPostprocessRepository layerPostprocessRepository;
        private Camera layerPostprocessCamera;
        private UniversalAdditionalCameraData universalAdditionalCameraData;

        [SerializeField]
        private VolumeProfile useVolumeProfile;
        
        [SerializeField] 
        private List<LayerPostprocessData> layerPostprocessDataList;

        private const int NothingCullingMask = 0;
        private RenderTextureDescriptor renderTextureDescriptor;

        private void Awake()
        {
            GetNecessaryComponents();
            layerPostprocessCamera.cullingMask = NothingCullingMask;
        }

        private void Update()
        {
            // ProcessFrame
            layerPostprocessRepository = LayerPostprocessRepository.Instance;
            layerPostprocessRepository.ReleaseRTHandleAll();
            
            
            // check Enable Postprocessing
            bool isEnablePostprocess = layerPostprocessDataList.Any();
            if (!isEnablePostprocess) return;

            // check Component
            bool isNeedGetComponents = layerPostprocessCamera == null || universalAdditionalCameraData == null;
            if (isNeedGetComponents)
            {
                bool isSuccessGetComponents = GetNecessaryComponents();
                if (!isSuccessGetComponents)
                {
                    Debug.LogWarning("GetComponent Failed.");
                    return;
                }
            }
            
            // Render
            foreach (LayerPostprocessData layerPostprocessData in layerPostprocessDataList)
            {
                RenderByPostprocess(layerPostprocessData);
            }
            layerPostprocessCamera.cullingMask = NothingCullingMask;
            useVolumeProfile.components.ForEach(component => component.active = true);
        }

        /// <summary>
        /// LayerPostprocessに必要なコンポーネントを取得する
        /// </summary>
        private bool GetNecessaryComponents()
        {
            bool isGetLayerPostProcessCamera = TryGetComponent<Camera>(out layerPostprocessCamera);
            bool isGetUniversalAdditionalCameraData = TryGetComponent<UniversalAdditionalCameraData>(out universalAdditionalCameraData);
            return isGetLayerPostProcessCamera && isGetUniversalAdditionalCameraData;
        }
        
        private void RenderByPostprocess(LayerPostprocessData layerPostprocessData)
        {
            // LayerMask設定
            layerPostprocessCamera.cullingMask = layerPostprocessData.LayerMask;
            
            // VolumeComponent設定
            foreach (VolumeComponent component in useVolumeProfile.components)
            {
                bool isTargetVolumeComponent = component.GetType() == LayerPostprocessUtils.GetVolumeComponent(layerPostprocessData.URPStandartPostprocessType);
                component.active = isTargetVolumeComponent;
            }
            
            // 白背景
            RenderToRenderTexture(Color.white, layerPostprocessData.WhiteBGKey);
            
            // 黒背景
            RenderToRenderTexture(Color.black, layerPostprocessData.BlackBGKey);
            
            // 
            layerPostprocessCamera.targetTexture = null;
        }

        private void RenderToRenderTexture(Color backGroundColor, string key)
        {
            layerPostprocessCamera.backgroundColor = backGroundColor;
            
            // CameraTarget設定
            // Alloc
            renderTextureDescriptor = layerPostprocessRepository.GetCameraTargetDescriptor();
            bool isSuccessAllocate = layerPostprocessRepository.AllocateRTHandle(renderTextureDescriptor, key, name: key);
            if (!isSuccessAllocate)
            {
                Debug.LogError("RTHandle Allocate is Failed.");
                return;
            }
            // Get
            RTHandle renderTarget = layerPostprocessRepository.GetRTHandleByKey(key);
            layerPostprocessCamera.targetTexture = renderTarget;
            
            // Render
            layerPostprocessCamera.Render();
        }
    }

}
