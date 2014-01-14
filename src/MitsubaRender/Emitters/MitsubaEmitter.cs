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

namespace MitsubaRender.Emitters
{
    /// <summary>
    /// TODO summary emitter
    /// </summary>
    public abstract class MitsubaEmitter
    {
        /// <summary>
        ///   The internal ID of the current emitter.
        /// </summary>
        protected string EmitterId;

        /// <summary>
        /// This method has to be implemented in each emitter.
        /// </summary>
        /// <returns></returns>
        public abstract string GetEmitterId();

        /// <summary>
        /// This method reads the values introduced by the user and established class properties with them.
        /// </summary>
        protected abstract void ReadDataFromUI();
    }
}