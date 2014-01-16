// This file is part of MitsubaRenderPlugin project.
//  
// This program is free software; you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by the
// Free Software Foundation; either version 3 of the License, or (at your
// option) any later version. This program is distributed in the hope that
// it will be useful, but WITHOUT ANY WARRANTY; without even the implied
// warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details. 
// 
// You should have received a copy of the GNU General Public License
// along with MitsubaRenderPlugin.  If not, see <http://www.gnu.org/licenses/>.
// 
// Copyright 2014 TDM Solutions SL

using System.ComponentModel;

namespace MitsubaRender.Integrators
{
    internal static class IntegratorObjectInstances
    {
        public static IntegratorAmbientOclusion AmbientOclusion;
        public static IntegratorDirectIlumination DirectIlumination;
        public static IntegratorPathTracer PathTracer;
        public static IntegratorVolumetricPathTracerSimple VolumetricPathTracerSimple;
        public static IntegratorVolumetricPathTracerExtended VolumetricPathTracerExtended;
        public static IntegratorAdjointParticleTracer AdjointParticleTracer;
        public static IntegratorVirtualPointLightRenderer VirtualPointLightRenderer;
        public static IntegratorPhotonMapper PhotonMapper;
        public static IntegratorProgressivePhotonMapper ProgressivePhotonMapper;
        public static IntegratorStochasticProgressivePhotonMapper StochasticProgressivePhotonMapper ;
        public static IntegratorBidirectionalPathTracer BidirectionalPathTracer;
        public static IntegratorPrimarySampleSpaceMLT PrimarySampleSpaceMLT;
        public static IntegratorSampleSpaceMLT SampleSpaceMLT;
        public static EnergyRedisributionPathTracing EnergyRedisributionPathTracing;
    }
    public class IntegratorAmbientOclusion
    {
        public IntegratorAmbientOclusion()
        {
            ShadingSamples = 1;
            OcclusionRayLength = -1.0f;
        }

        [DisplayName(@"Shading samples")]
        [Description("Specifies the number of shading samples that should be computed per primary ray")]
        public int ShadingSamples { get; set; }

        [DisplayName(@"Occlusion ray length")]
        [Description("Specifies the world-space length of the ambient occlusion rays that will be cast. Default: -1, i.e. automatic")]
        public float OcclusionRayLength { get; set; }
    }
    public class IntegratorDirectIlumination
    {
        public IntegratorDirectIlumination()
        {
            EmitterSmaples = 1;
            BSDFSamples = 1;
            StrictSurfaceNormals = false;
            HideDirectlyVisibleEmitters = false;
        }

        [DisplayName(@"Emitter samples")]
        [Description("Number of samples to take using the emitter sampling technique")]
        public int EmitterSmaples { get; set; }

        [DisplayName(@"BSDF samples")]
        [Description("Number of samples to take using the BSDF sampling technique")]
        public int BSDFSamples { get; set; }

        [DisplayName(@"Strict surface normals")]
        [Description("<p>Be strict about potential inconsistencies involving shading normals?</p><p>Triangle meshes often rely on interpolated shading normals to suppress the inherently faceted appearance of the underlying geometry. These fake normals are not without problems, however. They can lead to paradoxical situations where a light ray impinges on an object from a direction that is classified as outside according to the shading normal, and inside according to the true geometric normal.</p> <p>The <tt>strictNormals</tt> parameter specifies the intended behavior when such cases arise. The default (<tt>false</tt>, i.e. carry on) gives precedence to information given by the shading normal and considers such light paths to be valid. This can theoretically cause light leaks through boundaries, but it is not much of a problem in practice.</p> <p>When set to <tt>true</tt>, the path tracer detects inconsistencies and ignores these paths. When objects are poorly tesselated, this latter option may cause them to lose a significant amount of the incident radiation (or, in other words, they will look dark). The bidirectional integrators in Mitsuba (BDPT, PSSMLT, MLT, ..) implicitly have <tt>strictNormals</tt> activated. Hence, another use of this parameter is to match renderings created by these methods.</p>")]
        public bool StrictSurfaceNormals { get; set; }

        [DisplayName(@"Hide directly visible emitters")]
        [Description("Hide light sources (e.g. area or environment light sources) that are directly visible to the camera? Reflections of light sources remain unaffected.")]
        public bool HideDirectlyVisibleEmitters { get; set; } 
    }
    public class IntegratorPathTracer
    {
        public IntegratorPathTracer()
        {
            MaximumDepth = -1;
            RussianRouletteStartingDepth = 5;
            StrictSurfaceNormals = false;
            HideDirectlyVisibleEmitters = false;
        }

        [DisplayName(@"Maximum Depth")]
        [Description("Specifies the longest path depth in the generated output image (where <tt>-1</tt> corresponds to ∞). A value of 1 will only render directly visible light sources. 2 will lead to single-bounce (direct-only) illumination, and so on.")]
        public int MaximumDepth { get; set; }

        [DisplayName(@"Russian roulette starting depth")]
        [Description("Specifies the minimum path depth, after which the implementation will start to use the russian roulette path termination criterion (set to <tt>-1</tt> to disable).")]
        public int RussianRouletteStartingDepth { get; set; } 

        [DisplayName(@"Strict surface normals")]
        [Description("<p>Be strict about potential inconsistencies involving shading normals?</p><p>Triangle meshes often rely on interpolated shading normals to suppress the inherently faceted appearance of the underlying geometry. These fake normals are not without problems, however. They can lead to paradoxical situations where a light ray impinges on an object from a direction that is classified as outside according to the shading normal, and inside according to the true geometric normal.</p> <p>The <tt>strictNormals</tt> parameter specifies the intended behavior when such cases arise. The default (<tt>false</tt>, i.e. carry on) gives precedence to information given by the shading normal and considers such light paths to be valid. This can theoretically cause light leaks through boundaries, but it is not much of a problem in practice.</p> <p>When set to <tt>true</tt>, the path tracer detects inconsistencies and ignores these paths. When objects are poorly tesselated, this latter option may cause them to lose a significant amount of the incident radiation (or, in other words, they will look dark). The bidirectional integrators in Mitsuba (BDPT, PSSMLT, MLT, ..) implicitly have <tt>strictNormals</tt> activated. Hence, another use of this parameter is to match renderings created by these methods.</p>")]
        public bool StrictSurfaceNormals { get; set; } 

        [DisplayName(@"Hide directly visible emitters")]
        [Description("Hide light sources (e.g. area or environment light sources) that are directly visible to the camera? Reflections of light sources remain unaffected.")]
        public bool HideDirectlyVisibleEmitters { get; set; } 
    }
    public class IntegratorVolumetricPathTracerSimple
    {
        public IntegratorVolumetricPathTracerSimple()
        {
            MaximumDepth = -1;
            RussianRouletteStartingDepth = 5;
            StrictSurfaceNormals = false;
            HideDirectlyVisibleEmitters = false;
        }

        [DisplayName(@"Maximum Depth")]
        [Description("Specifies the longest path depth in the generated output image (where <tt>-1</tt> corresponds to ∞). A value of 1 will only render directly visible light sources. 2 will lead to single-bounce (direct-only) illumination, and so on.")]
        public int MaximumDepth { get; set; }

        [DisplayName(@"Russian roulette starting depth")]
        [Description("Specifies the minimum path depth, after which the implementation will start to use the russian roulette path termination criterion (set to <tt>-1</tt> to disable).")]
        public int RussianRouletteStartingDepth { get; set; } 

        [DisplayName(@"Strict surface normals")]
        [Description("<p>Be strict about potential inconsistencies involving shading normals?</p><p>Triangle meshes often rely on interpolated shading normals to suppress the inherently faceted appearance of the underlying geometry. These fake normals are not without problems, however. They can lead to paradoxical situations where a light ray impinges on an object from a direction that is classified as outside according to the shading normal, and inside according to the true geometric normal.</p> <p>The <tt>strictNormals</tt> parameter specifies the intended behavior when such cases arise. The default (<tt>false</tt>, i.e. carry on) gives precedence to information given by the shading normal and considers such light paths to be valid. This can theoretically cause light leaks through boundaries, but it is not much of a problem in practice.</p> <p>When set to <tt>true</tt>, the path tracer detects inconsistencies and ignores these paths. When objects are poorly tesselated, this latter option may cause them to lose a significant amount of the incident radiation (or, in other words, they will look dark). The bidirectional integrators in Mitsuba (BDPT, PSSMLT, MLT, ..) implicitly have <tt>strictNormals</tt> activated. Hence, another use of this parameter is to match renderings created by these methods.</p>")]
        public bool StrictSurfaceNormals { get; set; } 

        [DisplayName(@"Hide directly visible emitters")]
        [Description("Hide light sources (e.g. area or environment light sources) that are directly visible to the camera? Reflections of light sources remain unaffected.")]
        public bool HideDirectlyVisibleEmitters { get; set; } 
    }
    public class IntegratorVolumetricPathTracerExtended
    {
        public IntegratorVolumetricPathTracerExtended()
        {
            MaximumDepth = -1;
            RussianRouletteStartingDepth = 5;
            StrictSurfaceNormals = false;
            HideDirectlyVisibleEmitters = false;
        }

        [DisplayName(@"Maximum Depth")]
        [Description("Specifies the longest path depth in the generated output image (where <tt>-1</tt> corresponds to ∞). A value of 1 will only render directly visible light sources. 2 will lead to single-bounce (direct-only) illumination, and so on.")]
        public int MaximumDepth { get; set; }

        [DisplayName(@"Russian roulette starting depth")]
        [Description("Specifies the minimum path depth, after which the implementation will start to use the russian roulette path termination criterion (set to <tt>-1</tt> to disable).")]
        public int RussianRouletteStartingDepth { get; set; } 

        [DisplayName(@"Strict surface normals")]
        [Description("<p>Be strict about potential inconsistencies involving shading normals?</p><p>Triangle meshes often rely on interpolated shading normals to suppress the inherently faceted appearance of the underlying geometry. These fake normals are not without problems, however. They can lead to paradoxical situations where a light ray impinges on an object from a direction that is classified as outside according to the shading normal, and inside according to the true geometric normal.</p> <p>The <tt>strictNormals</tt> parameter specifies the intended behavior when such cases arise. The default (<tt>false</tt>, i.e. carry on) gives precedence to information given by the shading normal and considers such light paths to be valid. This can theoretically cause light leaks through boundaries, but it is not much of a problem in practice.</p> <p>When set to <tt>true</tt>, the path tracer detects inconsistencies and ignores these paths. When objects are poorly tesselated, this latter option may cause them to lose a significant amount of the incident radiation (or, in other words, they will look dark). The bidirectional integrators in Mitsuba (BDPT, PSSMLT, MLT, ..) implicitly have <tt>strictNormals</tt> activated. Hence, another use of this parameter is to match renderings created by these methods.</p>")]
        public bool StrictSurfaceNormals { get; set; } 

        [DisplayName(@"Hide directly visible emitters")]
        [Description("Hide light sources (e.g. area or environment light sources) that are directly visible to the camera? Reflections of light sources remain unaffected.")]
        public bool HideDirectlyVisibleEmitters { get; set; } 

    }
    public class IntegratorAdjointParticleTracer
    {
        public IntegratorAdjointParticleTracer()
        {
            WorkUnitGranularity = 200000;
            RussianRouletteStartingDepth = 5;
            MaximumDepth = -1;
            BruteForce = false;
        }

        [DisplayName(@"Work unit granularity")]
        [Description("	Specifies the work unit granularity used to parallize the particle tracing task (default: 200K samples per work unit). This should be high enough so that accumulating partially exposed images (and potentially sending them over the network) is not the bottleneck.")]
        public int WorkUnitGranularity { get; set; }

        [DisplayName(@"Russian roulette starting depth")]
        [Description("Specifies the minimum path depth, after which the implementation will start to use the russian roulette path termination criterion (set to <tt>-1</tt> to disable).")]
        public int RussianRouletteStartingDepth { get; set; }

        [DisplayName(@"Maximum depth")]
        [Description("Specifies the longest path depth in the generated output image (where <tt>-1</tt> corresponds to ∞). A value of 1 will only render directly visible light sources. 2 will lead to single-bounce (direct-only) illumination, and so on.")]
        public int MaximumDepth { get; set; }

        [DisplayName(@"Brute force")]
        [Description("If set to <tt>true</tt> the integrator does not attempt to create connections to the sensor and purely relies on hitting it via ray tracing. This is mainly intended for debugging purposes.")]
        public bool BruteForce { get; set; } 
    }
    public class IntegratorVirtualPointLightRenderer
    {
        public IntegratorVirtualPointLightRenderer()
        {
            ShadowMapResolution = 512;
            MaximumDepth = -1;
            ClampingFactor = 0.1f;
        }

        [DisplayName(@"Shadow map resolution")]
        [Description("If")]
        public int ShadowMapResolution { get; set; }

        [DisplayName(@"Maximum depth")]
        [Description("Specifies the longest path depth in the generated output image (where <tt>-1</tt> corresponds to ∞). A value of 1 will only render directly visible light sources. 2 will lead to single-bounce (direct-only) illumination, and so on.")]
        public int MaximumDepth { get; set; } 

        [DisplayName(@"Clamping factor")]
        [Description("Relative clamping factor (0=no clamping, 1=full clamping)")]
        public float ClampingFactor { get; set; } 
    }
    public class IntegratorPhotonMapper
    {
        public IntegratorPhotonMapper()
        {
            DirectSamples = 16;
            GlossySamples = 32;
            MaximumDepth = -1;
            GlobalPhotons = 250000;
            CausticPhotons = 250000;
            VolumePhotons = 250000;
            LookupRadiusGlobal = 0.05f;
            LookupRadiusCaustic = 0.0125f;
            CausticPhotonMapLookupSize = 120;
            WorkUnitGranularity = 0;
            RussianRouletteSartingDepth = 5;
            HideDirectlyVisibleEmitters = false;
        }

        [DisplayName(@"Direct samples")]
        [Description("Number of luminaire samples for direct illumination")]
        public int DirectSamples { get; set; }

        [DisplayName(@"Glossy samples")]
        [Description("Number of glossy samples for direct illumination")]
        public int GlossySamples { get; set; }

        [DisplayName(@"Maximum depth")]
        [Description("Specifies the longest path depth in the generated output image (where <tt>-1</tt> corresponds to ∞). A value of 1 will only render directly visible light sources. 2 will lead to single-bounce (direct-only) illumination, and so on.")]
        public int MaximumDepth { get; set; }

        [DisplayName(@"Global photons")]
        [Description("Number of photons to collect for the global photon map (if applicable)")]
        public int GlobalPhotons { get; set; }

        [DisplayName(@"Caustic photons")]
        [Description("Number of photons to collect for the caustic photon map (if applicable)")]
        public int CausticPhotons { get; set; }

        [DisplayName(@"Volume photons")]
        [Description("Number of photons to collect for the volume photon map (if applicable)")]
        public int VolumePhotons { get; set; }

        [DisplayName(@"Lookup radius (global)")]
        [Description("Radius of lookups in the global photon map (relative to the scene size)")]
        public float LookupRadiusGlobal { get; set; }

        [DisplayName(@"Lookup radius (caustic)")]
        [Description("Radius of lookups in the caustic photon map (relative to the scene size)")]
        public float LookupRadiusCaustic { get; set; }

        [DisplayName(@"Caustic photon map lookup size")]
        [Description("Number of photons that should be fetched in photon map queries")]
        public int CausticPhotonMapLookupSize { get; set; }

        [DisplayName(@"Work unit granularity")]
        [Description("Granularity of photon tracing work units (in shot particles, 0 => decide automatically)")]
        public int WorkUnitGranularity { get; set; }

        [DisplayName(@"Russian roulette starting depth")]
        [Description("Specifies the minimum path depth, after which the implementation will start to use the russian roulette path termination criterion (set to <tt>-1</tt> to disable).")]
        public int RussianRouletteSartingDepth { get; set; }

        [DisplayName(@"Hide directly visible emitters")]
        [Description("Hide light sources (e.g. area or environment light sources) that are directly visible to the camera? Reflections of light sources remain unaffected.")]
        public bool HideDirectlyVisibleEmitters { get; set; } 

    }
    public class IntegratorProgressivePhotonMapper
    {
        public IntegratorProgressivePhotonMapper()
        {
            MaximumDepth = -1;
            InitialRadius = 0;
            PhotonsPerIteration = 250000;
            WorkUnitGranularity = 0;
            RussianRouletteSartingDepth = 5;
            SizeReductionParameter = 0.7f;
        }
       
        [DisplayName(@"Maximum depth")]
        [Description("Specifies the longest path depth in the generated output image (where <tt>-1</tt> corresponds to ∞). A value of 1 will only render directly visible light sources. 2 will lead to single-bounce (direct-only) illumination, and so on.")]
        public int MaximumDepth { get; set; }
        
        [DisplayName(@"Initial radius")]
        [Description("Initial photon query radius (<tt>0</tt> = infer based on scene size and camera resolution)")]
        public float InitialRadius { get; set; }

        [DisplayName(@"Photons per iteration")]
        [Description("Number of photons to shoot in each iteration")]
        public int PhotonsPerIteration { get; set; }

        [DisplayName(@"Work unit granularity")]
        [Description("Granularity of photon tracing work units (in shot particles, 0 => decide automatically)")]
        public int WorkUnitGranularity { get; set; }

        [DisplayName(@"Russian roulette starting depth")]
        [Description("Specifies the minimum path depth, after which the implementation will start to use the russian roulette path termination criterion (set to <tt>-1</tt> to disable).")]
        public int RussianRouletteSartingDepth { get; set; }

        [DisplayName(@"Size reduction parameter")]
        [Description("Alpha parameter from the paper (influences the speed, at which the photon radius is reduced)")]
        public float SizeReductionParameter { get; set; } 
    }
    public class IntegratorStochasticProgressivePhotonMapper
    {
        public IntegratorStochasticProgressivePhotonMapper()
        {
            MaximumDepth = -1;
            InitialRadius = 0;
            PhotonsPerIteration = 250000;
            WorkUnitGranularity = 0;
            RussianRouletteSartingDepth = 5;
            SizeReductionParameter = 0.7f;
        }
       
        [DisplayName(@"Maximum depth")]
        [Description("Specifies the longest path depth in the generated output image (where <tt>-1</tt> corresponds to ∞). A value of 1 will only render directly visible light sources. 2 will lead to single-bounce (direct-only) illumination, and so on.")]
        public int MaximumDepth { get; set; }
        
        [DisplayName(@"Initial radius")]
        [Description("Initial photon query radius (<tt>0</tt> = infer based on scene size and camera resolution)")]
        public float InitialRadius { get; set; }

        [DisplayName(@"Photons per iteration")]
        [Description("Number of photons to shoot in each iteration")]
        public int PhotonsPerIteration { get; set; }

        [DisplayName(@"Work unit granularity")]
        [Description("Granularity of photon tracing work units (in shot particles, 0 => decide automatically)")]
        public int WorkUnitGranularity { get; set; }

        [DisplayName(@"Russian roulette starting depth")]
        [Description("Specifies the minimum path depth, after which the implementation will start to use the russian roulette path termination criterion (set to <tt>-1</tt> to disable).")]
        public int RussianRouletteSartingDepth { get; set; }

        [DisplayName(@"Size reduction parameter")]
        [Description("Alpha parameter from the paper (influences the speed, at which the photon radius is reduced)")]
        public float SizeReductionParameter { get; set; } 
    }
    public class IntegratorBidirectionalPathTracer
    {
        public IntegratorBidirectionalPathTracer()
        {
            MaximumDepth = -1;
            CreateLightImage = true;
            UseDirectSamplingMethods = true;
            RussianRouletteStartingDepth = 5;
        }

        [DisplayName(@"Maximum depth")]
        [Description("Specifies the longest path depth in the generated output image (where <tt>-1</tt> corresponds to ∞). A value of 1 will only render directly visible light sources. 2 will lead to single-bounce (direct-only) illumination, and so on.")]
        public int MaximumDepth { get; set; }
       
        [DisplayName(@"Create light image")]
        [Description("Include sampling strategies that connect paths traced from emitters directly to the camera? (i.e. what the adjoint particle tracer does) This improves the effectiveness of bidirectional path tracing but severely increases the local and remote communication overhead, since large <em>light images</em> must be transferred between threads or over the network. See the main documentation for a more detailed explanation.")]
        public bool CreateLightImage { get; set; }

        [DisplayName(@"Use direct sampling methods depth")]
        [Description("Enable direct sampling strategies? This is a generalization of direct illumination sampling that works with both emitters and sensors. Usually a good idea.")]
        public bool UseDirectSamplingMethods { get; set; } 

        [DisplayName(@"Russian roulette starting depth")]
        [Description("Specifies the minimum path depth, after which the implementation will start to use the russian roulette path termination criterion (set to <tt>-1</tt> to disable).")]
        public int RussianRouletteStartingDepth { get; set; } 
    }
    public class IntegratorPrimarySampleSpaceMLT
    {
        public IntegratorPrimarySampleSpaceMLT()
        {
            Bidirectional = true;
            MaximumDepth = -1;
            DirectSamples = 16;
            TwoStageMLT = false;
            LuminanceSamples = 100000;
            LargeStepProbability = 0.3f;
            RussianRouletteSartingDepth = 5;
        }

        [DisplayName(@"Bidirectional")]
        [Description("If set to <tt>true</tt>, the MLT algorithm runs on top of a bidirectional path tracer with multiple importance sampling. Otherwise, the implementation reverts to a unidirectional path tracer.")]
        public bool Bidirectional { get; set; }

        [DisplayName(@"Maximum depth")]
        [Description("Specifies the longest path depth in the generated output image (where <tt>-1</tt> corresponds to ∞). A value of 1 will only render directly visible light sources. 2 will lead to single-bounce (direct-only) illumination, and so on.")]
        public int MaximumDepth { get; set; }

        [DisplayName(@"Direct samples")]
        [Description(" By default, this plugin renders the direct illumination component separately using an optimized direct illumination sampling strategy that uses low-discrepancy number sequences for superior performance (in other words, it is <em>not</em> rendered by PSSMLT). This parameter specifies the number of samples allocated to that method. To force PSSMLT to be responsible for the direct illumination component as well, set this parameter to <tt>-1</tt>.")]
        public int DirectSamples { get; set; }

        [DisplayName(@"Two-stage MLT")]
        [Description(" Use two-stage MLT? Please see the documentation for details.")]
        public bool TwoStageMLT { get; set; }

        [DisplayName(@"Luminance Samples")]
        [Description("MLT-type algorithms create output images that are only <em>relative</em> The algorithm can e.g. determine that a certain pixel is approximately twice as bright as another one, but the absolute scale is unknown. To recover it, this plugin computes the average luminance arriving at the sensor by generating a number of samples.")]
        public int LuminanceSamples { get; set; }

        [DisplayName(@"Large step probability")]
        [Description("Rate at which the implementation tries to replace the current path with a completely new one. Usually, there is little need to change this.")]
        public float LargeStepProbability { get; set; }

        [DisplayName(@"Russian roulette starting depth")]
        [Description("Specifies the minimum path depth, after which the implementation will start to use the russian roulette path termination criterion (set to <tt>-1</tt> to disable).")]
        public int RussianRouletteSartingDepth { get; set; }  
    }
    public class IntegratorSampleSpaceMLT
    {
        [DisplayName(@"Maximum depth")]
        [Description("Specifies the longest path depth in the generated output image (where <tt>-1</tt> corresponds to ∞). A value of 1 will only render directly visible light sources. 2 will lead to single-bounce (direct-only) illumination, and so on.")]
        public int MaximumDepth { get; set; }  

        [DisplayName(@"Direct samples")]
        [Description("By default, this plugin renders the direct illumination component separately using an optimized direct illumination sampling strategy that uses low-discrepancy number sequences for superior performance (in other words, it is <em>not</em> rendered by PSSMLT). This parameter specifies the number of samples allocated to that method. To force PSSMLT to be responsible for the direct illumination component as well, set this parameter to <tt>-1</tt>")]
        public int DirectSamples { get; set; }  

        [DisplayName(@"Two-stage MLT")]
        [Description(" Use two-stage MLT? Please see the documentation for details.")]
        public bool TwoStageMLT { get; set; }  
       
        [DisplayName(@"Luminance Samples")]
        [Description("MLT-type algorithms create output images that are only <em>relative</em> The algorithm can e.g. determine that a certain pixel is approximately twice as bright as another one, but the absolute scale is unknown. To recover it, this plugin computes the average luminance arriving at the sensor by generating a number of samples.")]
        public int LuminanceSamples { get; set; }  

        [DisplayName(@"Bidirectional mutation")]
        [Description("Selectively enable/disable the bidirectional mutation")]
        public bool BidirectionalMutation { get; set; }  

        [DisplayName(@"Lens pertubation")]
        [Description("Selectively enable/disable the lens perturbation")]
        public bool LensPerturbation { get; set; }  

        [DisplayName(@"Caustic perturbation")]
        [Description("Selectively enable/disable the caustic perturbation")]
        public bool CausticPerturbation { get; set; }  

        [DisplayName(@"Multi chain pertubation")]
        [Description("Selectively enable/disable the multi-chain perturbation")]
        public bool MultiChainPerturbation { get; set; }  

        [DisplayName(@"Manifold pertubation")]
        [Description("Selectively enable/disable the manifold perturbation")]
        public bool ManifoldPerturbation { get; set; }

        [DisplayName(@"Probability factor")]
        [Description("Probability factor (lambda) of the manifold perturbation")]
        public int ProbabilityFactor { get; set; }  

    }
    public class EnergyRedisributionPathTracing
    {
        public EnergyRedisributionPathTracing()
        {
            MaximumDepth = -1;
            AverageNumberOfChains = 1;
            MaxNumberOfChains = 0;
            MutationsPerChain = 100;
            DirectSamples = 16;
            LuminanceSamples = 15000;
            BidirectionalMutation = false;
            LensPerturbation = true;
            CausticPerturbation = true;
            MultiChainPerturbation = true;
            ManifoldPerturbation = false;
            ProbabilityFactor = 50;
            RussianRouletteStartingDepth = 5;

        }
        [DisplayName(@"Maximum depth")]
        [Description("Specifies the longest path depth in the generated output image (where <tt>-1</tt> corresponds to ∞). A value of 1 will only render directly visible light sources. 2 will lead to single-bounce (direct-only) illumination, and so on.")]
        public int MaximumDepth { get; set; }

        [DisplayName(@"Average number of chains")]
        [Description("Specifies the number of Markov Chains that, on average, are started per pixel")]
        public int AverageNumberOfChains { get; set; }

        [DisplayName(@"Max. number of chains")]
        [Description("pecifies a limit for the number of chains that will be started at a pixel. '0' disables this option.")]
        public int MaxNumberOfChains { get; set; }

        [DisplayName(@"Mutations per chain")]
        [Description("Specifies the number of mutations to be performed in each Markov Chain")]
        public int MutationsPerChain { get; set; }

        [DisplayName(@"Direct samples")]
        [Description("By default, this plugin renders the direct illumination component separately using an optimized direct illumination sampling strategy that uses low-discrepancy number sequences for superior performance (in other words, it is <em>not</em> rendered by PSSMLT). This parameter specifies the number of samples allocated to that method. To force PSSMLT to be responsible for the direct illumination component as well, set this parameter to <tt>-1</tt>")]
        public int DirectSamples { get; set; }

        [DisplayName(@"Luminance Samples")]
        [Description("MLT-type algorithms create output images that are only <em>relative</em> The algorithm can e.g. determine that a certain pixel is approximately twice as bright as another one, but the absolute scale is unknown. To recover it, this plugin computes the average luminance arriving at the sensor by generating a number of samples.")]
        public int LuminanceSamples { get; set; }

        [DisplayName(@"Bidirectional mutation")]
        [Description("Selectively enable/disable the bidirectional mutation")]
        public bool BidirectionalMutation { get; set; }

        [DisplayName(@"Lens pertubation")]
        [Description("Selectively enable/disable the lens perturbation")]
        public bool LensPerturbation { get; set; }

        [DisplayName(@"Caustic perturbation")]
        [Description("Selectively enable/disable the caustic perturbation")]
        public bool CausticPerturbation { get; set; }

        [DisplayName(@"Multi chain pertubation")]
        [Description("Selectively enable/disable the multi-chain perturbation")]
        public bool MultiChainPerturbation { get; set; }

        [DisplayName(@"Manifold pertubation")]
        [Description("Selectively enable/disable the manifold perturbation")]
        public bool ManifoldPerturbation { get; set; }

        [DisplayName(@"Probability factor")]
        [Description("Probability factor (lambda) of the manifold perturbation")]
        public int ProbabilityFactor { get; set; }

        [DisplayName(@"Russian roulette starting depth")]
        [Description("Specifies the minimum path depth, after which the implementation will start to use the russian roulette path termination criterion (set to <tt>-1</tt> to disable).")]
        public int RussianRouletteStartingDepth { get; set; }  
    }
}
