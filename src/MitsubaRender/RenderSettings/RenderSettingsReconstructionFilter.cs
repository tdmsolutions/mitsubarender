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
using System.ComponentModel;
using System.IO;
using MitsubaRender.Integrators;
using MitsubaRender.Settings;

namespace MitsubaRender.RenderSettings
{
    static class ReconstructionFilterObjectInstances
    {
        public static ISave[] GetReconstructionFilterDefaultInstances()
        {
            return new ISave[] { 
                new ReconstructionFilterGaussianFilter(), 
                new ReconstructionFilterMitchellNetravaliFilter(), 
                new ReconstructionFilterLanczosSincFilter()
            };
        }
    }
    [Serializable]
    class ReconstructionFilterGaussianFilter : ISave
    {
        public ReconstructionFilterGaussianFilter()
        {
            StandardDeviation = 0.5;
        }

        [DisplayName(@"Standard deviation")]
        [Description("Standard deviation of the Gaussian")]
        public double StandardDeviation { get; set; }

        public bool Save(string name)
        {
            if (String.IsNullOrEmpty(name) || String.IsNullOrWhiteSpace(name))
                name = GetType().Name;

            if (!Directory.Exists(MitsubaSettings.FolderReconstructionFiltersFolder))
                return false;

            var filePath = Path.Combine(MitsubaSettings.FolderReconstructionFiltersFolder, name) + LibraryReconstructionFilters.Extension;
            return Tools.FileTools.SaveObject(filePath, this);
        }

        public bool Save()
        {
            return Save(null);
        }
    }
    [Serializable]
    class ReconstructionFilterMitchellNetravaliFilter : ISave
    {
        public ReconstructionFilterMitchellNetravaliFilter()
        {
            BParameter = 0.33333;
            CParameter = 0.33333;
        }

        [DisplayName(@"B parameter")]
        [Description("B parameter from the paper")]
        public double BParameter { get; set; }

        [DisplayName(@"C parameter")]
        [Description("C parameter from the paper")]
        public double CParameter { get; set; }

        public bool Save(string name)
        {
            if (String.IsNullOrEmpty(name) || String.IsNullOrWhiteSpace(name))
                name = GetType().Name;

            if (!Directory.Exists(MitsubaSettings.FolderReconstructionFiltersFolder))
                return false;

            var filePath = Path.Combine(MitsubaSettings.FolderReconstructionFiltersFolder, name) + LibraryReconstructionFilters.Extension;
            return Tools.FileTools.SaveObject(filePath, this);
        }

        public bool Save()
        {
            return Save(null);
        }

    }
    [Serializable]
    class ReconstructionFilterLanczosSincFilter : ISave
    {
        public ReconstructionFilterLanczosSincFilter()
        {
            NumberOfLobes = 3;
        }

        [DisplayName(@"Number of lobes")]
        [Description("Number of filter lobes")]
        public int NumberOfLobes { get; set; }

        public bool Save(string name)
        {
            if (String.IsNullOrEmpty(name) || String.IsNullOrWhiteSpace(name))
                name = GetType().Name;

            if (!Directory.Exists(MitsubaSettings.FolderReconstructionFiltersFolder))
                return false;

            var filePath = Path.Combine(MitsubaSettings.FolderReconstructionFiltersFolder, name) + LibraryReconstructionFilters.Extension;
            return Tools.FileTools.SaveObject(filePath, this);
        }

        public bool Save()
        {
            return Save(null);
        }
    }
}
