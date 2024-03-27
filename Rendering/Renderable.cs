
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface Renderable {
    public Texture2D GetTexture();
    public Vector2 GetLocation();
    public float GetDepth();
}