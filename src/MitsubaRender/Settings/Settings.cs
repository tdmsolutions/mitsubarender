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

using System;
using System.IO;
using MitsubaRender.Integrators;
using MitsubaRender.RenderSettings;

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
    }
    enum ReconstructionFilterType
    {
        BoxFilter,
        TentFilter,
        GaussianFilter,
        MitchellNetravaliFilter,
        CatmullRomFilter,
        LanczosSincFilter
    }

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
        
        public static object Integrator;
        public static object Sampler;
        public static object ReconstructionFilter;
        
        public static string FolderIntegratorsName = "Integrators";
        public static string FolderSamplersName = "Samplers";
        public static string FolderReconstructionFiltersName = "Reconstruction Filters";
        public static string FolderRenderSettingsPresetsName = "Render Settings Presets";

        public static string FolderUserFolder = Path.Combine(new []{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"TDM Solutions","Mitsuba Render"});
        public static string FolderIntegratorsFolder = Path.Combine(new[] { FolderUserFolder, FolderIntegratorsName });
        public static string FolderSamplersFolder = Path.Combine(new[] { FolderUserFolder, FolderSamplersName });
        public static string FolderReconstructionFiltersFolder = Path.Combine(new[] { FolderUserFolder, FolderReconstructionFiltersName });
        public static string FolderRenderSettingsPresetsFolder = Path.Combine(new[] { FolderUserFolder, FolderRenderSettingsPresetsName });
       
        public static bool GenerateDefaultIntegrators()
        {
            Tools.FileTools.CheckOrCreateFolder(Path.GetDirectoryName(FolderIntegratorsFolder));
            var success = true;

            foreach (var integrator in  IntegratorObjectInstances.GetIntegratorDefaultInstances())
            {
                if (!integrator.Save())
                    success = false;
            }
            return success;
        }
        public static bool GenerateDefaultSamplers()
        {
            Tools.FileTools.CheckOrCreateFolder(Path.GetDirectoryName(FolderSamplersFolder));
            var success = true;

            foreach (var sampler in SamplerObjectInstances.GetSamplersDefaultInstances())
            {
                if (!sampler.Save())
                    success = false;
            }
            return success;
        }
        public static bool GenerateDefaultReconstructionFilters()
        {
            Tools.FileTools.CheckOrCreateFolder(Path.GetDirectoryName(FolderReconstructionFiltersFolder));
            var success = true;

            foreach (var sampler in ReconstructionFilterObjectInstances.GetReconstructionFilterDefaultInstances())
            {
                if (!sampler.Save())
                    success = false;
            }
            return success;
        }
    }
}