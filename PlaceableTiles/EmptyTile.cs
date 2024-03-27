using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class EmptyTile : Tile
{
    public float GetDepth()
    {
        throw new System.NotImplementedException();
    }

    public Vector2 GetLocation()
    {
        throw new System.NotImplementedException();
    }

    public Texture2D GetTexture()
    {
        throw new System.NotImplementedException();
    }

    public void OnGlobalTileUpdate(IUpdateableResourceCounts CurrentSession)
    {
    }

    public void Tick(int DeltaTime)
    {
    }
}