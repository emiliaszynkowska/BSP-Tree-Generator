using System.CodeDom.Compiler;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BSPGenerator : MonoBehaviour
{
    //Tiles
    public Tile groundTile;
    public Tile wallTile;
    public Tile edgeTile;
    public Tile leftEdgeTile;
    public Tile rightEdgeTile;
    public Tile leftCornerTile;
    public Tile rightCornerTile;
    public Tile leftPointTile;
    public Tile rightPointTile;
    public Tile leftWallTile;
    public Tile rightWallTile;
    //Tilemaps
    public Tilemap groundMap;
    public Tilemap outerWallMap;
    public Tilemap innerWallMap;
    public Tilemap edgeMap;
    //Variables
    public int width;
    public int height;
    public int minRoomSize;
    public int maxRoomSize;
    
    // Start is called before the first frame update
    void Start()
    {
        Dungeon root = new Dungeon(new Rect(0,0,width,height));
        Generate(root);
        root.CreateRoom();
        FillRooms(root);
    }

    void Generate(Dungeon dungeon)
    {
        if (dungeon.IsLeaf())
        {
            if (dungeon.rect.width > maxRoomSize || dungeon.rect.height > maxRoomSize || Random.Range(0, 1) > 0.25)
            {
                if (dungeon.Split(minRoomSize, maxRoomSize))
                {
                    Generate(dungeon.left);
                    Generate(dungeon.right);
                }
            }
        }
    }

    void FillRooms(Dungeon dungeon)
    {
        if (dungeon == null)
            return;
        if (dungeon.IsLeaf())
        {
            for (int x = (int) dungeon.room.x; x < dungeon.room.xMax; x++) {
                for (int y = (int) dungeon.room.y; y < dungeon.room.yMax; y++) {
                    groundMap.SetTile(new Vector3Int(x,y,0), groundTile);
                }
            }
        } else {
            FillRooms(dungeon.left);
            FillRooms(dungeon.right);
        }
    }

}
