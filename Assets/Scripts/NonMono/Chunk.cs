using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Block;

public class Chunk
{
    static public int chunksize = 16, chunkheight = 256;
    public int x;
    public int z;

    //public List<Block> blocks = new List<Block>();
    public BlockType[,,] blocks = new BlockType[chunksize + 2, chunkheight, chunksize + 2];
    private float scaler = .03f;
    private float hightmap, hightmap2;
    private static float offsetx = Random.Range(0f, 9999f);
    private static float offsetz = Random.Range(0f, 9999f);

    public Chunk(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public void GenerateChunk()
    {
        hightmap2 = Mathf.PerlinNoise((this.x  * .005f + offsetx), (this.z * .005f + offsetz));
        Debug.Log(hightmap2);
        for (int x = 1 + this.x; x < chunksize + this.x + 1; x++)
        {
            for (int z = 1 + this.z; z < chunksize + this.z + 1; z++)
            {
                hightmap = Mathf.RoundToInt(Mathf.PerlinNoise(x * scaler + offsetx, z * scaler + offsetz) + Mathf.PerlinNoise(x * 0.005f + offsetx, z * 0.005f + offsetz) * 30 /*(100 * hightmap2)*/ + 64);
                //Debug.Log(hightmap);
                for (int y = 0; y < chunkheight; y++)
                {
                    if (y == hightmap)
                    {
                        blocks[x - this.x, y, z - this.z] = BlockType.Grass;
                    }
                    else if (y < hightmap && y > hightmap - 6)
                    {
                        blocks[x - this.x, y, z - this.z] = BlockType.Dirt;
                    }
                    else if (y <= hightmap - 6)
                    {
                        blocks[x - this.x, y, z - this.z] = BlockType.Stone;
                    }
                    else
                    {
                        blocks[x - this.x, y, z - this.z] = BlockType.Air;
                    }
                }
            }
        }
    }
}
