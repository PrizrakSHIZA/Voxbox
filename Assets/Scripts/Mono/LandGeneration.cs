using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Block;

public class LandGeneration : MonoBehaviour
{
    public Material material;
    public GameObject chunk, cube, player;
    public int renderchunks = 3;

    List<Chunk> Chunks = new List<Chunk>();
    List<Chunk> loadedChunks = new List<Chunk>();
    List<Chunk> todeleteChunks = new List<Chunk>();
    private int currentchunkX = 0, currentchunkZ = 0, lastchunkX = 0, lastchunkZ = 0;
    private GameObject temp;
    private Chunk tempchunk;

    void Start()
    {
        Check();
    }

    void Update()
    {
        currentchunkX = Mathf.FloorToInt(player.transform.position.x / Chunk.chunksize) * Chunk.chunksize;
        currentchunkZ = Mathf.FloorToInt(player.transform.position.z / Chunk.chunksize) * Chunk.chunksize;
        if (lastchunkX != currentchunkX || lastchunkZ != currentchunkZ)
        {
            Check();
        }
    }

    public void Check()
    {
        todeleteChunks = loadedChunks;
        loadedChunks.Clear();
        for (int x = currentchunkX - Chunk.chunksize * renderchunks; x <= currentchunkX + Chunk.chunksize * renderchunks; x += Chunk.chunksize)
        {
            for(int z = currentchunkZ - Chunk.chunksize * renderchunks; z <= currentchunkZ + Chunk.chunksize * renderchunks; z += Chunk.chunksize)
            {
                if (loadedChunks.Find(c => (c.x == x) && (c.z == z)) != null)
                {
                    //Debug.Log($"I already have chunk on {x}:{z}");
                }
                else
                {
                    if (Chunks.Find(c => (c.x == x) && (c.z == z)) != null)
                    {
                        GameObject createdchunk = Instantiate(chunk, new Vector3(x, 0, z), Quaternion.identity);
                        tempchunk = Chunks.Find(c => (c.x == x) && (c.z == z));
                        loadedChunks.Add(tempchunk);
                        BuildMesh(tempchunk, createdchunk);
                    }
                    else
                    {
                        GameObject createdchunk = Instantiate(chunk, new Vector3(x, 0, z), Quaternion.identity);
                        tempchunk = new Chunk(x, z);
                        tempchunk.GenerateChunk();
                        Chunks.Add(tempchunk);
                        BuildMesh(tempchunk, createdchunk);
                    }
                }
            }
        }
        lastchunkX = currentchunkX;
        lastchunkZ = currentchunkZ;
    }
    
    public void BuildMesh(Chunk chunk, GameObject chunkmesh)
    {
        Mesh mesh = new Mesh();

        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int x = 1; x < Chunk.chunksize + 1; x++)
            for (int z = 1; z < Chunk.chunksize + 1; z++)
                for (int y = 0; y < Chunk.chunkheight; y++)
                {
                    if (chunk.blocks[x, y, z] != 0)
                    {
                        Vector3 blockPos = new Vector3(x - 1, y, z - 1);
                        int numFaces = 0;
                        //Top face
                        if (y < Chunk.chunkheight - 1 && chunk.blocks[x, y + 1, z] == 0)
                        {
                            verts.Add(blockPos + new Vector3(0, 1, 0));
                            verts.Add(blockPos + new Vector3(0, 1, 1));
                            verts.Add(blockPos + new Vector3(1, 1, 1));
                            verts.Add(blockPos + new Vector3(1, 1, 0));
                            numFaces++;

                            uvs.AddRange(new AtlasPos(AllBlocks[chunk.blocks[x, y, z]].GetTiles(TilePos.Top)).GetUVs());
                        }

                        //Bottom face
                        if (y > 0 && chunk.blocks[x, y - 1, z] == 0)
                        {
                            verts.Add(blockPos + new Vector3(0, 0, 0));
                            verts.Add(blockPos + new Vector3(1, 0, 0));
                            verts.Add(blockPos + new Vector3(1, 0, 1));
                            verts.Add(blockPos + new Vector3(0, 0, 1));
                            numFaces++;

                            uvs.AddRange(new AtlasPos(AllBlocks[chunk.blocks[x, y, z]].GetTiles(TilePos.Bottom)).GetUVs());
                        }

                        //Front face
                        if (chunk.blocks[x, y, z - 1] == 0)
                        {
                            verts.Add(blockPos + new Vector3(0, 0, 0));
                            verts.Add(blockPos + new Vector3(0, 1, 0));
                            verts.Add(blockPos + new Vector3(1, 1, 0));
                            verts.Add(blockPos + new Vector3(1, 0, 0));
                            numFaces++;

                            uvs.AddRange(new AtlasPos(AllBlocks[chunk.blocks[x, y, z]].GetTiles(TilePos.Front)).GetUVs());
                        }

                        //Right face
                        if (chunk.blocks[x + 1, y, z] == 0)
                        {
                            verts.Add(blockPos + new Vector3(1, 0, 0));
                            verts.Add(blockPos + new Vector3(1, 1, 0));
                            verts.Add(blockPos + new Vector3(1, 1, 1));
                            verts.Add(blockPos + new Vector3(1, 0, 1));
                            numFaces++;

                            uvs.AddRange(new AtlasPos(AllBlocks[chunk.blocks[x, y, z]].GetTiles(TilePos.Right)).GetUVs());
                        }

                        //Back face
                        if (chunk.blocks[x, y, z + 1] == 0)
                        {
                            verts.Add(blockPos + new Vector3(1, 0, 1));
                            verts.Add(blockPos + new Vector3(1, 1, 1));
                            verts.Add(blockPos + new Vector3(0, 1, 1));
                            verts.Add(blockPos + new Vector3(0, 0, 1));
                            numFaces++;

                            uvs.AddRange(new AtlasPos(AllBlocks[chunk.blocks[x, y, z]].GetTiles(TilePos.Back)).GetUVs());
                        }

                        //Left face
                        if (chunk.blocks[x - 1, y, z] == 0)
                        {
                            verts.Add(blockPos + new Vector3(0, 0, 1));
                            verts.Add(blockPos + new Vector3(0, 1, 1));
                            verts.Add(blockPos + new Vector3(0, 1, 0));
                            verts.Add(blockPos + new Vector3(0, 0, 0));
                            numFaces++;

                            uvs.AddRange(new AtlasPos(AllBlocks[chunk.blocks[x, y, z]].GetTiles(TilePos.Left)).GetUVs());
                        }

                        int tl = verts.Count - 4 * numFaces;
                        for (int i = 0; i < numFaces; i++)
                        {
                            tris.AddRange(new int[] { tl + i * 4, tl + i * 4 + 1, tl + i * 4 + 2, tl + i * 4, tl + i * 4 + 2, tl + i * 4 + 3 });
                            //uvs.AddRange(new AtlasPos(AllBlocks[chunk.blocks[x, y, z]].GetTiles(TilePos.Top)).GetUVs());
                        }
                    }
                }
        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uvs.ToArray();

        mesh.RecalculateNormals();

        chunkmesh.GetComponent<MeshFilter>().mesh = mesh;
        chunkmesh.GetComponent<MeshCollider>().sharedMesh = mesh;
        chunkmesh.GetComponent<MeshRenderer>().material = material;
    }
}
