using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGettingInto
{
    internal static class NoiseMain
    {

        public static bool[,,] GetCubeWorld(int mapSize, int steps)
        {
            Random rng = new Random();
            double planarize = 0.3;
            double[,,] map = new double[mapSize+1, mapSize+1, mapSize+1];
            bool[,,] cubeworld = new bool[mapSize * steps, mapSize * steps, mapSize * steps];

            for (int z = 0; z <= mapSize; z++)
            {
                for (int y = 0; y <= mapSize; y++)
                {
                    for (int x = 0; x <= mapSize; x++)
                    {
                        map[x, y, z] = rng.NextDouble() - (mapSize / 2 - y) * planarize;
                    }
                }
            }

            for (int z = 0; z < mapSize; z++)
            {
                for (int y = 0; y < mapSize; y++)
                {
                    for (int x = 0; x < mapSize; x++)
                    {
                        for (int i = 0; i < steps; i++)
                        {
                            for (int p = 0; p < steps; p++)
                            {
                                for (int u = 0; u < steps; u++)
                                {
                                    double v = 1.0 / steps;
                                    double v1 = CosineInterpolate(map[x, y, z] ,map[x + 1, y, z] , (u * v));
                                    double v2 = CosineInterpolate(map[x, y, z + 1] , map[x + 1, y, z + 1] , (u * v));
                                    v1 = CosineInterpolate(v1 , v2 , (i * v));

                                    double v3 = CosineInterpolate(map[x, y + 1, z], map[x + 1, y + 1, z], (u * v));
                                    double v4 = CosineInterpolate(map[x, y + 1, z + 1], map[x + 1, y + 1, z + 1], (u * v));
                                    v3 = CosineInterpolate(v3, v4, (i * v));

                                    cubeworld[x * steps + u, y * steps + p, z * steps + i] = (CosineInterpolate(v1, v3, (p * v)) < .5);

                                }
                            }
                        }
                    }
                }
            }
            return cubeworld;
        }

        //based on paul bourks method online : http://www.paulbourke.net/miscellaneous/interpolation/ 
        public static double CosineInterpolate(
   double y1, double y2,
   double mu)
        {
            mu = (1 - Math.Cos(mu * Math.PI)) *.25+mu*.5;
            return (y1 * (1 - mu) + y2 * mu);
        }

    }
}
