using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework.Graphics;

public interface Tile{
    public void OnGlobalTileUpdate(IUpdateableResourceCounts CurrentSession);
    public void Tick(int DeltaTime);
    public Texture2D GetTexture();
}