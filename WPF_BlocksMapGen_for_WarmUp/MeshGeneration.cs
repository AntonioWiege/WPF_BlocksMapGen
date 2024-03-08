using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace WPFGettingInto
{
    internal static class MeshGeneration
    {
            public static void GenerateMesh(MeshGeometry3D mesh, int mapSize = 8, int steps = 8)
            {
                MeshGeometry3D cube = new MeshGeometry3D();
                cube.Positions = new Point3DCollection(new Point3D[]{
             new Point3D(0, 1, 0), new Point3D(0, 1, 1), new Point3D(1, 1, 1), new Point3D(1, 1, 0)
                                  , new Point3D(0, 0, 0)   , new Point3D(0, 1, 0)   , new Point3D(1, 1, 0)   , new Point3D(1, 0, 0)
                                  , new Point3D(1, 0, 0)   , new Point3D(1, 1, 0)   , new Point3D(1, 1, 1)   , new Point3D(1, 0, 1)
                                  , new Point3D(0, 0, 0)   , new Point3D(0, 0, 1)   , new Point3D(0, 1, 1)   , new Point3D(0, 1, 0)
                                  , new Point3D(1, 0, 1)   , new Point3D(1, 1, 1)   , new Point3D(0, 1, 1)   , new Point3D(0, 0, 1)
                                  , new Point3D(1, 0, 0)   , new Point3D(1, 0, 1)   , new Point3D(0, 0, 1)   , new Point3D(0, 0, 0)
            });
                cube.Normals = new Vector3DCollection(new[] {
          new Vector3D( 0, 1, 0),  new Vector3D( 0 ,1 ,0),new Vector3D(   0 ,1 ,0),  new Vector3D(0, 1, 0)
                                  ,  new Vector3D(0, 0 ,- 1)   ,  new Vector3D(0, 0, - 1)  ,  new Vector3D(0, 0, - 1)  ,  new Vector3D(0, 0, - 1)
                                  ,  new Vector3D(1, 0 ,0) ,  new Vector3D(1, 0, 0) , new Vector3D(1 ,0 ,0 ) ,  new Vector3D(1 ,0, 0)
                                  ,  new Vector3D(-1 ,0, 0) ,  new Vector3D(-1 ,0 ,0) ,  new Vector3D(-1 ,0 ,0)  ,  new Vector3D(-1, 0, 0)
                                  ,  new Vector3D(0, 0 ,1) ,  new Vector3D(0, 0 ,1) ,  new Vector3D(0 ,0, 1) ,  new Vector3D(0, 0, 1)
                                 ,  new Vector3D(0 ,- 1 ,0) ,  new Vector3D(0, - 1, 0) ,  new Vector3D(0 ,- 1, 0) ,  new Vector3D(0 ,- 1, 0)
            });
                cube.TriangleIndices = new Int32Collection(new int[] {
            0 ,1 ,2  , 2, 3 ,0,  4 ,5, 6,   6, 7 ,4  , 8, 9 ,10 ,  10, 11, 8,
                                          12 ,13, 14 ,  14, 15, 12 ,  16, 17, 18,   18, 19 ,16,   20, 21, 22 ,  22, 23, 20
            });

                int triangleOffset = 0;

                List<Point3D> positions = new();
                List<Vector3D> normals = new();
                List<int> indices = new();
                bool[,,] cubeworld = NoiseMain.GetCubeWorld(mapSize, steps);

                for (int z = 1; z < mapSize * steps - 1; z++)
                {
                    for (int y = 1; y < mapSize * steps - 1; y++)
                    {
                        for (int x = 1; x < mapSize * steps - 1; x++)
                        {
                            Point3D o = new Point3D(x, y, z);
                            if (!cubeworld[x, y, z])
                            {
                                continue;
                            }
                            if (!cubeworld[x + 1, y, z])
                            {
                                positions.Add(new Point3D(1, 0, 0).Add(o));
                                positions.Add(new Point3D(1, 1, 0).Add(o));
                                positions.Add(new Point3D(1, 1, 1).Add(o));
                                positions.Add(new Point3D(1, 0, 1).Add(o));
                                AddRegularQuadNormals(new Vector3D(1, 0, 0), normals);
                                AddRegularQuadIndices(triangleOffset, indices, ref triangleOffset);
                            }
                            if (!cubeworld[x - 1, y, z])
                            {
                                positions.Add(new Point3D(0, 0, 0).Add(o));
                                positions.Add(new Point3D(0, 0, 1).Add(o));
                                positions.Add(new Point3D(0, 1, 1).Add(o));
                                positions.Add(new Point3D(0, 1, 0).Add(o));
                                AddRegularQuadNormals(new Vector3D(-1, 0, 0), normals);
                                AddRegularQuadIndices(triangleOffset, indices, ref triangleOffset);
                            }
                            if (!cubeworld[x, y + 1, z])
                            {
                                positions.Add(new Point3D(0, 1, 0).Add(o));
                                positions.Add(new Point3D(0, 1, 1).Add(o));
                                positions.Add(new Point3D(1, 1, 1).Add(o));
                                positions.Add(new Point3D(1, 1, 0).Add(o));
                                AddRegularQuadNormals(new Vector3D(0, 1, 0), normals);
                                AddRegularQuadIndices(triangleOffset, indices, ref triangleOffset);
                            }
                            if (!cubeworld[x, y - 1, z])
                            {
                                positions.Add(new Point3D(1, 0, 0).Add(o));
                                positions.Add(new Point3D(1, 0, 1).Add(o));
                                positions.Add(new Point3D(0, 0, 1).Add(o));
                                positions.Add(new Point3D(0, 0, 0).Add(o));
                                AddRegularQuadNormals(new Vector3D(0, -1, 0), normals);
                                AddRegularQuadIndices(triangleOffset, indices, ref triangleOffset);
                            }
                            if (!cubeworld[x, y, z + 1])
                            {
                                positions.Add(new Point3D(1, 0, 1).Add(o));
                                positions.Add(new Point3D(1, 1, 1).Add(o));
                                positions.Add(new Point3D(0, 1, 1).Add(o));
                                positions.Add(new Point3D(0, 0, 1).Add(o));
                                AddRegularQuadNormals(new Vector3D(0, 0, 1), normals);
                                AddRegularQuadIndices(triangleOffset, indices, ref triangleOffset);
                            }
                            if (!cubeworld[x, y, z - 1])
                            {
                                positions.Add(new Point3D(0, 0, 0).Add(o));
                                positions.Add(new Point3D(0, 1, 0).Add(o));
                                positions.Add(new Point3D(1, 1, 0).Add(o));
                                positions.Add(new Point3D(1, 0, 0).Add(o));
                                AddRegularQuadNormals(new Vector3D(0, 0, -1), normals);
                                AddRegularQuadIndices(triangleOffset, indices, ref triangleOffset);
                            }

                        }
                    }
                }

                double rescale_mesh = 0.1;
                for (int i = 0; i < positions.Count; i++)
                {
                    Point3D point = positions[i];
                    point.X *= rescale_mesh;
                    point.Y *= rescale_mesh;
                    point.Z *= rescale_mesh;
                    positions[i] = point;
                }
                mesh.Positions = new Point3DCollection(positions);
                mesh.Normals = new Vector3DCollection(normals);
                mesh.TriangleIndices = new Int32Collection(indices);
            }

        public static void AddRegularQuadIndices(int triangleOffset, List<int> indices, ref int counter)
            {
                indices.Add(triangleOffset + 0);
                indices.Add(triangleOffset + 1);
                indices.Add(triangleOffset + 2);
                indices.Add(triangleOffset + 2);
                indices.Add(triangleOffset + 3);
                indices.Add(triangleOffset + 0);
                counter += 4;
            }

        public static void AddRegularQuadNormals(Vector3D normal, List<Vector3D> normals)
            {
                for (int n = 0; n < 4; n++)
                {
                    normals.Add(normal);
                }
            }
    }
}
