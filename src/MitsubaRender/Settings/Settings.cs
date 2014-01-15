﻿// This file is part of MitsubaRenderPlugin project.
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
    public static class MitsubaSettings
    {



        //TODO settings
        public static string WorkingDirectory;
        public static string MitsubaPath;
        public static string ApplicationPath;
    }
}