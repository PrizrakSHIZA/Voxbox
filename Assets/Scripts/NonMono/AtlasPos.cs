using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtlasPos
{
    private Vector2[] UVs;
    private float PixelSize = 16f;

    public AtlasPos(Vector2 pos)
    {
        float size = 1f / PixelSize;
        UVs = new Vector2[]
        {          
            new Vector2(pos.x * size - size, pos.y * size - size),
            new Vector2(pos.x * size - size, pos.y * size),
            new Vector2(pos.x * size, pos.y * size),
            new Vector2(pos.x * size, pos.y * size - size),         
        };
    }

    public Vector2[] GetUVs()
    {
        return UVs;
    }
}
