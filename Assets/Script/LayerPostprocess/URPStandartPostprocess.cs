using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace FemtoChanTools.LayerPostprocess
{
    public static class LayerPostprocessUtils
    {
        /// <summary>
        /// VolumeComponent名のEnum
        /// </summary>
        public enum URPStandartPostprocessType
        {
            ToneMapping,
            Bloom,
            Vignette,
            LensDistortion,
            DepthOfField,
            ChannelMixer,
            ChromaticAberration,
            ColorAdjustments,
            ColorCurves,
            ColorLookup,
            FilmGrain,
            LiftGammaGain,
            MotionBlur,
            PaniniProjection,
            ShadowsMidtonesHighlight,
            SplitToning,
            WhiteBalance,
        }

        /// <summary>
        /// VolumeComponentのTypeを得る
        /// </summary>
        /// <param name="postprocessType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static Type GetVolumeComponent(URPStandartPostprocessType postprocessType)
        {
            switch (postprocessType)
            {
                case URPStandartPostprocessType.ToneMapping:
                    return typeof(Tonemapping);
                case URPStandartPostprocessType.Bloom:
                    return typeof(Bloom);
                case URPStandartPostprocessType.Vignette:
                    return typeof(Vignette);
                case URPStandartPostprocessType.LensDistortion:
                    return typeof(LensDistortion);
                case URPStandartPostprocessType.DepthOfField:
                    return typeof(DepthOfField);
                case URPStandartPostprocessType.ChannelMixer:
                    return typeof(ChannelMixer);
                case URPStandartPostprocessType.ChromaticAberration:
                    return typeof(ChromaticAberration);
                case URPStandartPostprocessType.ColorAdjustments:
                    return typeof(ColorAdjustments);
                case URPStandartPostprocessType.ColorCurves:
                    return typeof(ColorCurves);
                case URPStandartPostprocessType.ColorLookup:
                    return typeof(ColorLookup);
                case URPStandartPostprocessType.FilmGrain:
                    return typeof(FilmGrain);
                case URPStandartPostprocessType.LiftGammaGain:
                    return typeof(LiftGammaGain);
                case URPStandartPostprocessType.MotionBlur:
                    return typeof(MotionBlur);
                case URPStandartPostprocessType.PaniniProjection:
                    return typeof(PaniniProjection);
                case URPStandartPostprocessType.ShadowsMidtonesHighlight:
                    return typeof(ShadowsMidtonesHighlights);
                case URPStandartPostprocessType.SplitToning:
                    return typeof(SplitToning);
                case URPStandartPostprocessType.WhiteBalance:
                    return typeof(WhiteBalance);
                default:
                    throw new ArgumentOutOfRangeException(nameof(postprocessType), postprocessType, null);
            }
        }
    }

}