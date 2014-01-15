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
   public class IntegratorGaussianFilter
    {
        public IntegratorGaussianFilter()
        {
            StandardDeviation = 0.5;
        }

        [DisplayName(@"Standard deviation")]
        [Description("Standard deviation of the Gaussian")]
        public double StandardDeviation { get; set; }
    }

    class IntegratorMitchellNetravaliFilter
    {
        public IntegratorMitchellNetravaliFilter()
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

    }

    class IntegratorLanczosSincFilter
    {
        public IntegratorLanczosSincFilter()
        {
            NumberOfLobes = 3;
        }

        [DisplayName(@"Number of lobes")]
        [Description("Number of filter lobes")]
        public int NumberOfLobes { get; set; }
    }




}
