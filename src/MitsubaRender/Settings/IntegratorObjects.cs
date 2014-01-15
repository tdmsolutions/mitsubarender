namespace MitsubaRender.Settings
{
    internal static class IntegratorObjectInstances
    {
        public static AmbientOclusion AmbientOclusion;
        public static DirectIlumination DirectIlumination;
        public static PathTracer PathTracer;
        public static VolumetricPathTracerSimple VolumetricPathTracerSimple;
        public static VolumetricPathTracerExtended VolumetricPathTracerExtended;
        public static AdjointParticleTracer AdjointParticleTracer;
        public static VirtualPointLightRenderer VirtualPointLightRenderer;
        public static PhotonMapper PhotonMapper;
        public static ProgressivePhotonMapper ProgressivePhotonMapper;
        public static StochasticProgressivePhotonMapper StochasticProgressivePhotonMapper ;
        public static BidirectionalPathTracer BidirectionalPathTracer;
        public static PrimarySampleSpaceMLT PrimarySampleSpaceMLT;
        public static SampleSpaceMLT SampleSpaceMLT;
        public static EnergyRedisributionPathTracing EnergyRedisributionPathTracing;
    }

    class AmbientOclusion
    {
        public int ShadingSamples;
        public float OcclusionRayLength;
    }

    class DirectIlumination
    {
        public int EmitterSmaples;
        public int BSDFSamples;
        public bool StrictSurfaceNormals;
        public bool HideDirectlyVisibleEmitters;
    }

    class PathTracer
    {
        public int MaximumDepth;
        public int RussianRouletteSartingDepth;
        public bool StrictSurfaceNormals;
        public bool HideDirectlyVisibleEmitters;
    }
    class VolumetricPathTracerSimple
    {
        public int MaximumDepth;
        public int RussianRouletteSartingDepth;
        public bool StrictSurfaceNormals;
        public bool HideDirectlyVisibleEmitters;
    }
    public class VolumetricPathTracerExtended
    {
 public int MaximumDepth;
        public int RussianRouletteSartingDepth;
        public bool StrictSurfaceNormals;
        public bool HideDirectlyVisibleEmitters;
    }
    class AdjointParticleTracer
    {
        public int WorkUnitGranularity;
        public int RussianRouletteSartingDepth;
        public int MaximumDepth;
        public bool BruteForce;
    }
    class VirtualPointLightRenderer
    {
        public int Shadow;
        public int MaximumDepth;
        public float ClampingFactor;
    }
    class PhotonMapper
    {
        public int DirectSamples;
        public int GlossySamples;
        public int MaximumDepth;
        public int GlobalPhotons;
        public int VolumePhotons;
        public float LookUpradiusGlobal;
        public float LookUpradiusCaustic;
        public int WorkUnitGranularity;
        public int RussianRouletteSartingDepth;
        public bool HideDirectlyVisibleEmitters;

    }
    class ProgressivePhotonMapper
    {
        public int SamplesPerIteration;
        public int WorkUnitGranularity;
        public int RussianRouletteSartingDepth;
        public float SizeReductionParameter;
    }
    class StochasticProgressivePhotonMapper
    {
        public int PhotonsPerIteration;
        public int WorkUnitGranularity;
        public int RussianRouletteSartingDepth;
        public float SizeReductionParameter;
    }
    class BidirectionalPathTracer
    {
        public int MaximumDepth;
        public bool CreateLightImage;
        public bool UseDirectSamplingMethods;
        public int RussianRouletteSartingDepth;
    }
    class PrimarySampleSpaceMLT
    {
        public bool Bidirectional;
        public int MaximumDepth;
        public int DirectSamples;
        public bool TwoStageMLT;
        public int LuminanceSamples;
        public float LargeStepProbability;
        public int RussianRouletteSartingDepth;
    }
    class SampleSpaceMLT
    {
        public int MaximumDepth;
        public int DirectSamples;
        public bool TwoStageMLT;
        public int LuminanceSamples;
        public bool BidirectionalMutation;
        public bool LensPerturbation;
        public bool CausticPerturbation;
        public bool MultiChainPerturbation;
        public bool ManifoldPerturbation;
    }
    class EnergyRedisributionPathTracing
    {
        public int MaximumDepth;
        public int AverageNumberOfChains;
        public int MutationsPerChain;
        public int DirectSamples;
        public int LuminanceSamples;
        public bool BidirectionalMutation;
        public bool LensPerturbation;
        public bool CausticPerturbation;
        public bool MultiChainPerturbation;
        public bool ManifoldPerturbation;
        public int ProbabilityFactor;
        public int RussianRouletteSartingDepth;
    }
}
