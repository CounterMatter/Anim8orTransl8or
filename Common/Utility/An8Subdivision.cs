// Copyright © 2025 Contingent Games.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.

using Anim8orTransl8or.An8.V100;
using System;

namespace Anim8orTransl8or.Utility
{
   static class An8Subdivision
   {
      /// <summary>
      /// This will produce the same points, texcoords, and vertices as if the
      /// user had clicked Build->Convert to Mesh in Anim8or v1.00.
      /// </summary>
      /// <param name="s">the subdivision</param>
      /// <param name="callback">the callback for warnings</param>
      /// <returns>the calculated mesh</returns>
      internal static mesh Calculate(
         subdivision s,
         Action<String> callback = null)
      {
         mesh m = new mesh();
         m.name = s?.name;
         m.@base = s?.@base;
         m.pivot = s?.pivot;
         m.material = s?.material;
         m.smoothangle = s?.smoothangle;
         m.materiallist = s?.materiallist;
         m.points = s?.points;
         m.normals = s?.normals;
         m.edges = s?.edges;
         m.texcoords = s?.texcoords;
         m.faces = s?.faces;
         m.morphoffsets = s?.morphoffsets;

         if ( s?.working?.value is Int64 working && working > 0 )
         {
            // TODO: Subdivide 'working' number of times. How do we do it?
            callback?.Invoke($"The \"{s?.name?.value}\" subdivision will be exported without performing a subdivision. Please convert it to a mesh in Anim8or if subdivisions are desired.");
         }

         return m;
      }
   }
}
