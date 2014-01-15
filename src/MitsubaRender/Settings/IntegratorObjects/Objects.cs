using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MitsubaRender.Settings.IntegratorObjects
{
     class AmbientOclusion
    {
        public  int ShadingSamples;
        public  float OcclusionRayLength;
    }

    class DirectIlumination
    {
        public  int EmitterSmaples;
        public  int BSDFSamples;
        public  bool StrictSurfaceNormals;
        public  bool HideDirectlyVisibleEmitters;
    }

    class PathTracer
    {
        public int MaximumDepth;
        public int RussianRouletteSartingDepth;
        public bool StrictSurfaceNormals;
        public bool HideDirectlyVisibleEmitters;
    }
    class VolumetricPathTracer
    {
        public int MaximumDepth;
        public int RussianRouletteSartingDepth;
        public bool StrictSurfaceNormals;
        public bool HideDirectlyVisibleEmitters;
    }
    class VolumetricPathTracerExtended
    {
       
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

    }
    class ProgressivePhotonMapper
    {
    }
    class StochasticProgressivePhotonMapper
    {
    }
    class BidirectionalPathTracer
    {
    }
    class PrimarySampleSpaceMLT
    {
    }
    class SampleSpaceMLT
    {
    }
    class EnergyRedisributionPathTracing
    {
    }
}
