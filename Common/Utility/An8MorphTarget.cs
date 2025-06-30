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

using Anim8orTransl8or.An8;
using Anim8orTransl8or.An8.V100;
using System;
using System.Collections.Generic;

namespace Anim8orTransl8or.Utility
{
   static class An8MorphTarget
   {
      /// <summary>
      /// This will produce an object with the selected morph target applied.
      /// </summary>
      /// <param name="s">the subdivision</param>
      /// <param name="callback">the callback for warnings</param>
      /// <returns>the calculated mesh</returns>
      internal static @object Calculate(
         @object o,
         morphtarget mt,
         Action<String> callback = null)
      {
         @object o2 = new @object();
         o2.name = $"{o?.name}_Morph_{mt?.name.value}";
         o2.layers = o?.layers;
         o2.material = o?.@material;

         if ( o?.mesh != null && o.mesh.Length > 0 )
         {
            List<mesh> meshes = new List<mesh>();

            foreach ( mesh mesh in o.mesh )
            {
               meshes.Add(Calculate(mesh, mt, callback));
            }

            o2.mesh = meshes.ToArray();
         }

         // Note: These aren't affected by morph targets
         o2.sphere = o?.sphere;
         o2.cylinder = o?.cylinder;
         o2.cube = o?.cube;

         if ( o?.subdivision != null && o.subdivision.Length > 0 )
         {
            List<subdivision> subdivisions = new List<subdivision>();

            foreach ( subdivision subdivision in o.subdivision )
            {
               subdivisions.Add(Calculate(subdivision, mt, callback));
            }

            o2.subdivision = subdivisions.ToArray();
         }

         // Note: These aren't affected by morph targets
         o2.pathcom = o?.pathcom;
         o2.textcom = o?.textcom;
         o2.modifier = o?.modifier;
         o2.image = o?.image;

         if ( o?.group != null && o.group.Length > 0 )
         {
            List<group1> groups = new List<group1>();

            foreach ( group1 group in o.group )
            {
               groups.Add(Calculate(group, mt, callback));
            }

            o2.group = groups.ToArray();
         }

         // Note: This doesn't need to be copied, since we are handling it now
         //o2.morphtarget = o?.morphtarget;

         return o2;
      }

      static mesh Calculate(
         mesh m,
         morphtarget mt,
         Action<String> callback = null)
      {
         morphoffsetdata[] morphoffsetdatas = null;

         foreach ( morphoffsets morphoffsets in m?.morphoffsets ??
            new morphoffsets[0] )
         {
            if ( morphoffsets.name == mt.name.value &&
               morphoffsets.morphoffsetdata != null &&
               morphoffsets.morphoffsetdata.Length > 0 &&
               m.points?.point != null &&
               m.points?.point?.Length > 0 )
            {
               morphoffsetdatas = morphoffsets.morphoffsetdata;
               break;
            }
         }

         if ( morphoffsetdatas != null )
         {
            mesh m2 = new mesh();
            m2.name = m.name;
            m2.@base = m.@base;
            m2.pivot = m.pivot;
            m2.material = m.material;
            m2.layer = m.layer;
            m2.smoothangle = m.smoothangle;
            m2.materiallist = m.materiallist;

            // Morph the points
            List<point> points = new List<point>();

            for ( Int32 i = 0; i < m.points.point.Length; i++ )
            {
               morphoffsetdata morphoffsetdata = null;

               foreach ( morphoffsetdata mod in morphoffsetdatas )
               {
                  if ( mod.pointindex == i )
                  {
                     morphoffsetdata = mod;
                     break;
                  }
               }

               point originalPoint = m.points.point[i];

               if ( morphoffsetdata != null )
               {
                  // Morph the point
                  point morphedPoint = new point(
                     originalPoint.x + morphoffsetdata.point.x,
                     originalPoint.y + morphoffsetdata.point.y,
                     originalPoint.z + morphoffsetdata.point.z);

                  points.Add(morphedPoint);
               }
               else
               {
                  // Use the original point
                  points.Add(originalPoint);
               }
            }

            m2.points = new points() { point = points.ToArray() };

            // Note: The normals need to be recalculated
            m2.normals = null;

            m2.edges = m.edges;
            m2.texcoords = m.texcoords;
            m2.faces = m.faces;

            // Note: This doesn't need to be copied, since we are handling it
            // now
            //m2.morphoffsets = o?.morphoffsets;

            return m2;
         }
         else
         {
            return m;
         }
      }

      static subdivision Calculate(
         subdivision s,
         morphtarget mt,
         Action<String> callback = null)
      {
         morphoffsetdata[] morphoffsetdatas = null;

         foreach ( morphoffsets morphoffsets in s?.morphoffsets ??
            new morphoffsets[0] )
         {
            if ( morphoffsets.name == mt.name.value &&
               morphoffsets.morphoffsetdata != null &&
               morphoffsets.morphoffsetdata.Length > 0 &&
               s.points?.point != null &&
               s.points?.point?.Length > 0 )
            {
               morphoffsetdatas = morphoffsets.morphoffsetdata;
               break;
            }
         }

         if ( morphoffsetdatas != null )
         {
            subdivision s2 = new subdivision();
            s2.name = s.name;
            s2.@base = s.@base;
            s2.pivot = s.pivot;
            s2.material = s.material;
            s2.layer = s.layer;
            s2.smoothangle = s.smoothangle;
            s2.working = s.working;
            s2.divisions = s.divisions;
            s2.materiallist = s.materiallist;

            // Morph the points
            List<point> points = new List<point>();

            for ( Int32 i = 0; i < s.points.point.Length; i++ )
            {
               morphoffsetdata morphoffsetdata = null;

               foreach ( morphoffsetdata mod in morphoffsetdatas )
               {
                  if ( mod.pointindex == i )
                  {
                     morphoffsetdata = mod;
                     break;
                  }
               }

               point originalPoint = s.points.point[i];

               if ( morphoffsetdata != null )
               {
                  // Morph the point
                  point morphedPoint = new point(
                     originalPoint.x + morphoffsetdata.point.x,
                     originalPoint.y + morphoffsetdata.point.y,
                     originalPoint.z + morphoffsetdata.point.z);

                  points.Add(morphedPoint);
               }
               else
               {
                  // Use the original point
                  points.Add(originalPoint);
               }
            }

            s2.points = new points() { point = points.ToArray() };

            // Note: The normals need to be recalculated
            s2.normals = null;

            s2.edges = s.edges;
            s2.texcoords = s.texcoords;
            s2.faces = s.faces;

            // Note: This doesn't need to be copied, since we are handling it
            // now
            //m2.morphoffsets = o?.morphoffsets;

            return s2;
         }
         else
         {
            return s;
         }
      }

      static group1 Calculate(
         group1 g,
         morphtarget mt,
         Action<String> callback = null)
      {
         group1 g2 = new group1();
         g2.name = g?.name;
         g2.@base = g?.@base;
         g2.layer = g?.layer;
         g2.pivot = g?.pivot;

         if ( g?.mesh != null && g.mesh.Length > 0 )
         {
            List<mesh> meshes = new List<mesh>();

            foreach ( mesh mesh in g.mesh )
            {
               meshes.Add(Calculate(mesh, mt, callback));
            }

            g2.mesh = meshes.ToArray();
         }

         // Note: These aren't affected by morph targets
         g2.sphere = g?.sphere;
         g2.cylinder = g?.cylinder;
         g2.cube = g?.cube;

         if ( g?.subdivision != null && g.subdivision.Length > 0 )
         {
            List<subdivision> subdivisions = new List<subdivision>();

            foreach ( subdivision subdivision in g.subdivision )
            {
               subdivisions.Add(Calculate(subdivision, mt, callback));
            }

            g2.subdivision = subdivisions.ToArray();
         }

         // Note: These aren't affected by morph targets
         g2.pathcom = g?.pathcom;
         g2.textcom = g?.textcom;
         g2.modifier = g?.modifier;
         g2.image = g?.image;

         if ( g?.group != null && g.group.Length > 0 )
         {
            List<group1> groups = new List<group1>();

            foreach ( group1 group in g.group )
            {
               groups.Add(Calculate(group, mt, callback));
            }

            g2.group = groups.ToArray();
         }

         return g2;
      }
   }
}
