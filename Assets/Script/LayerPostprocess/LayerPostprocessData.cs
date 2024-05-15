using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FemtoChanTools.LayerPostprocess
{
    /// <summary>
    /// LayerMaskとVolumeComponentのペア
    /// </summary>
    [Serializable]
    public class LayerPostprocessData
    {
        [SerializeField]
        private string whiteBGKey;
        
        [SerializeField]
        private string blackBGKey;
        
        [SerializeField] 
        private LayerMask layerMask;

        [SerializeField]
        private LayerPostprocessUtils.URPStandartPostprocessType urpStandartPostprocessType;

        public string WhiteBGKey => whiteBGKey;
        public string BlackBGKey => blackBGKey;
        public LayerMask LayerMask => layerMask;
        public LayerPostprocessUtils.URPStandartPostprocessType URPStandartPostprocessType => urpStandartPostprocessType;
    }

}
