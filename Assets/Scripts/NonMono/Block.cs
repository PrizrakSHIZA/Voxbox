using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    public int id;
    public string name;
    public string displayedname;
    public BlockType type;

    private Vector2[] Tiles = new Vector2[6];

    public Block(int id, string name, string displayedname, BlockType type, Vector2 TopUV)
    {
        this.id = id;
        this.name = name;
        this.displayedname = displayedname;
        this.type = type;
        for (int i = 0; i < Tiles.Length; i++)
        {
            Tiles[i] = TopUV;
        }
    }

    public Block(int id, string name, string displayedname, BlockType type, Vector2 TopUV, Vector2 SideUV, Vector2 BottomUV)
    {
        this.id = id;
        this.name = name;
        this.displayedname = displayedname;
        this.type = type;
        Tiles[0] = TopUV;
        Tiles[5] = BottomUV;
        for (int i = 1; i < Tiles.Length -1; i++)
        {
            Tiles[i] = SideUV;
        }
    }

    public Block(int id, string name, string displayedname, BlockType type, Vector2 TopUV, Vector2 LeftUV, Vector2 FrontUV, Vector2 BottomUV)
    {
        this.id = id;
        this.name = name;
        this.displayedname = displayedname;
        this.type = type;
        Tiles[0] = TopUV;
        Tiles[1] = Tiles[2] = LeftUV;
        Tiles[3] = Tiles[4] = FrontUV;
        Tiles[5] = BottomUV;
    }

    public Block(int id, string name, string displayedname, BlockType type, Vector2 TopUV, Vector2 LeftUV, Vector2 RightUV, Vector2 FrontUV, Vector2 BackUV, Vector2 BottomUV)
    {
        this.id = id;
        this.name = name;
        this.displayedname = displayedname;
        this.type = type;
        Tiles[0] = TopUV;
        Tiles[1] = LeftUV;
        Tiles[2] = RightUV;
        Tiles[3] = FrontUV;
        Tiles[4] = BackUV;
        Tiles[5] = BottomUV;
    }

    public Vector2 GetTiles(TilePos tilename)
    {
        return Tiles[(int)tilename];
    }

    public static Dictionary<BlockType, Block> AllBlocks = new Dictionary<BlockType, Block>()
    {
        {BlockType.Grass, new Block(1, "Grass", "Grass", BlockType.Grass, new Vector2(1,1), new Vector2(2,1), new Vector2(3,1))},
        {BlockType.Dirt, new Block(2, "Dirt", "Dirt", BlockType.Dirt, new Vector2(3,1))},
        {BlockType.Stone, new Block(3, "Stone", "Stone", BlockType.Stone, new Vector2(4,1))},
    };

    public enum TilePos : int 
    { 
        Top = 0, 
        Left = 1,
        Right = 2,
        Front = 3,
        Back = 4,
        Bottom = 5
    }

    public enum BlockType { Air, Grass, Dirt, Stone }
}
