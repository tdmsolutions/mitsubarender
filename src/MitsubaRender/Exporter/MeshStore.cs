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
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ionic.Zlib;
using Rhino;
using Rhino.Geometry;

//TODO: esta clase ha sido escrita integramente por Wenzel, revisar tema licencia !!!

namespace MitsubaRender.Exporter
{
    /// <summary>
    ///   This class serializes the Rhino geometry for Mitsuba understanding.
    /// </summary>
    public class MeshStore
    {
        private const ushort MTS_FILEFORMAT_HEADER = 0x041C;
        private const ushort MTS_FILEFORMAT_VERSION_V4 = 0x04;
        private const uint MTS_HAS_NORMALS = 0x0001;
        private const uint MTS_HAS_TEXCOORDS = 0x0002;
        //private const uint MTS_HAS_COLORS = 0x0008;
        //private const uint MTS_FACE_NORMALS = 0x0010;
        private const uint MTS_SINGLE_PRECISION = 0x0100;
        //private const uint MTS_DOUBLE_PRECISION = 0x0200;

        private readonly string _basePath;
        private string _filename;
        private int _meshCount;
        private List<ulong> _meshDict;
        private FileStream _output;

        public MeshStore(string basePath)
        {
            _basePath = basePath;
        }

        public string Filename
        {
            get { return _filename; }
        }

        public void Create(string filename)
        {
            _output = new FileStream(Path.Combine(_basePath, filename), FileMode.Create);
            _filename = filename;
            _meshDict = new List<ulong>();
            _meshCount = 0;
        }

        public int Store(Mesh mesh, string name)
        {
            var faces = mesh.Faces;
            var vertices = mesh.Vertices;
            var normals = mesh.Normals;
            var texCoords = mesh.TextureCoordinates;
            faces.ConvertQuadsToTriangles();
            var vertexCount = vertices.Count;
            var triangleCount = faces.TriangleCount;

            // Commented, it takes a lot of time
            Log("Creating render...");
            //Log("MeshStore[" + _meshCount + "]: adding mesh with " + vertexCount + " vertices, " + triangleCount +
             //   " triangles" + (name.Length > 0 ? (" (\"" + name + "\")") : ""));
            _meshDict.Add((ulong) _output.Position);
            Serialize(_output, MTS_FILEFORMAT_HEADER);
            Serialize(_output, MTS_FILEFORMAT_VERSION_V4);
            Stream zStream = new ZlibStream(_output, CompressionMode.Compress, CompressionLevel.BestCompression, true);
            var flags = MTS_SINGLE_PRECISION;
            if (texCoords.Count > 0) flags |= MTS_HAS_TEXCOORDS;
            if (normals.Count > 0) flags |= MTS_HAS_NORMALS;
            Serialize(zStream, flags);
            Serialize(zStream, name);
            Serialize(zStream, (ulong) vertexCount);
            Serialize(zStream, (ulong) triangleCount);
            for (var i = 0; i < vertexCount; ++i)
            {
                var p = vertices[i];
                Serialize(zStream, p.X);
                Serialize(zStream, p.Y);
                Serialize(zStream, p.Z);
            }
            if (normals.Count > 0)
            {
                for (var i = 0; i < vertexCount; ++i)
                {
                    var n = normals[i];
                    Serialize(zStream, n.X);
                    Serialize(zStream, n.Y);
                    Serialize(zStream, n.Z);
                }
            }
            if (texCoords.Count > 0)
            {
                for (var i = 0; i < vertexCount; ++i)
                {
                    var uv = texCoords[i];
                    Serialize(zStream, uv.X);
                    Serialize(zStream, uv.Y);
                }
            }
            for (var i = 0; i < triangleCount; ++i)
            {
                var face = faces[i];
                if (!face.IsTriangle) throw new Exception("Internal error: expected a triangle face!");
                Serialize(zStream, face.A);
                Serialize(zStream, face.B);
                Serialize(zStream, face.C);
            }
            zStream.Close();
            return _meshCount++;
        }

        public void Close()
        {
            // Write the dictionary
            foreach (var entry in _meshDict) Serialize(_output, entry);
            Serialize(_output, (uint) _meshDict.Count);
            _output.Close();
            _output = null;
        }

        public void Serialize(Stream stream, object value)
        {
            byte[] result;
            var type = value.GetType();
            if (type == typeof (string))
            {
                /* Strings are handled differently */
                var encodedBytes = Encoding.UTF8.GetBytes((string) value);
                result = new[] {(byte) 0};
                stream.Write(encodedBytes, 0, encodedBytes.Length);
                stream.Write(result, 0, result.Length);
                return;
            }
            if (type == typeof (byte)) result = new[] {(byte) value};
            else if (type == typeof (ushort)) result = BitConverter.GetBytes((ushort) value);
            else if (type == typeof (int)) result = BitConverter.GetBytes((int) value);
            else if (type == typeof (uint)) result = BitConverter.GetBytes((uint) value);
            else if (type == typeof (ulong)) result = BitConverter.GetBytes((ulong) value);
            else if (type == typeof (float)) result = BitConverter.GetBytes((float) value);
            else throw new Exception("MeshStore::serialize(): unsupported data type!");
            if (!BitConverter.IsLittleEndian) Array.Reverse(result);
            stream.Write(result, 0, result.Length);
        }

        private void Log(string text)
        {
            RhinoApp.WriteLine(text);
        }
    }
}