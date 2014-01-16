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

namespace MitsubaRender.Settings
{
    enum IntegratorType
    {
        Ambientoclusion,
        DirectIlumination,
        PathTracer,
        VolumetricPathTracerSimple,
        VolumetricPathTracerExtended,
        AdjointParticleTracer,
        VirtualPointLightRenderer,
        PhotonMapper,
        ProgressivePhotonMapper,
        StochasticProgressivePhotonMapper,
        BidirectionalPathTracer,
        PrimarySampleSpaceMLT,
        SampleSpaceMLT,
        EnergyRedisributionPathTracing
    }
    enum SamplerType
    {
        IndependentSampler,
        StraitfieldSampler,
        LowDiscrepancySampler,
        HammersleyQMCSampler,
        HaltonQMCSampler,
        SobolQMCSampler
    };

    internal static class IntegratorsDataSource
    {
        static internal readonly string[] IntegratorData = new[] {
            "Ambient oclusion",
            "Direct ilumination", 
            "Path tracer", 
            "Volumetric path tracer (Simple)",
            "Volumetric path tracer (Extended)",
            "Adjoint particle tracer", 
            "Virtual point light renderer", 
            "Photon mapper",
            "Progressive photon mapper", 
            "Stochastic progressive photon mapper", 
            "Bidirectional path tracer", 
            "Primary Sample Space MLT", 
            "Sample Space MLT",
            "Energy redisribution path tracing" };

        static internal readonly string[] SamplerData = new[]
            {
                "Independent sampler",
                "Straitfield sampler",
                "Low discrepancy sampler",
                "Hammersley QMC sampler",
                "Halton QMC sampler",
                "Sobol QMC sampler"

            };

        static internal readonly string[] ReconstructionData = new[]
            {
                "Box filter",
                "Tent filter",
                "Gaussian filter",
                "Mitchell-Netravali filter",
                "Catmull-Rom filter",
                "Lanczos Sinc filter"
            };
    }
    public static class MitsubaSettings
    {



        //TODO settings
        public static string WorkingDirectory;
        public static string MitsubaPath;
        public static string ApplicationPath;
    }
}