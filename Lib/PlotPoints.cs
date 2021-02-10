using System;
using System.Collections.Generic;
using System.Text;

namespace Lib
{
    public static class PlotPoints
    {
        public delegate float Function(float x);
        public static float[] CreateDots(float start, float end, Function func, int q = 500)
        {
            float step = (end - start) / q;
            float curr = start;
            float[] res = new float[q];
            for (int i = 0; i < q; i++)
            {
                res[i] = func(curr);
                curr += step;
            }
            return res;
        }
    }
}
