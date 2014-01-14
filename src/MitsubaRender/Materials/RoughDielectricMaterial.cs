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
using System.Runtime.InteropServices;
using MitsubaRender.Materials.Interfaces;
using MitsubaRender.Materials.Wrappers;
using Rhino.Display;
using Rhino.DocObjects;

namespace MitsubaRender.Materials
{
    /// <summary>
    /// TODO summary
    /// </summary>
    [Guid("4F1D6722-307B-41bf-ADC1-BE53C241331A")]
    public class RoughDielectricMaterial : MitsubaMaterial, IDielectric, IRough<float>
    {
        /// <summary>
        /// 
        /// </summary>
        private static int _count;

        #region Material Parameters

        /// <summary>
        /// Specifies the type of microfacet normal distribution used to model the surface roughness.
        /// </summary>
        public string Distribution { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MitsubaType<float, string> Alpha { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MitsubaType<Color4f, string> AlphaU { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MitsubaType<Color4f, string> AlphaV { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MitsubaType<float, string> IntIOR { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MitsubaType<float, string> ExtIOR { get; set; }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public override string TypeName
        {
            get { return "Mitsuba Rough Dielectric material"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string TypeDescription
        {
            get { return "TODO not working ! ---- Mitsuba Rough Dielectric material"; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string GetMaterialId()
        {
            if (string.IsNullOrEmpty(MaterialId)) MaterialId = "__roughdielectric" + _count++;
            return MaterialId;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void CreateUserInterface()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method reads the values introduced by the user and established class properties with them.
        /// </summary>
        protected override void ReadDataFromUI()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="simulation"></param>
        /// <param name="isForDataOnly"></param>
        public override void SimulateMaterial(ref Material simulation, bool isForDataOnly)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Main ctor.
        /// </summary>
        public RoughDielectricMaterial()
        {
            Alpha = new MitsubaType<float, string>();
            //CreateUserInterface();
        }
    }
}