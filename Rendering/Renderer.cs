using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class Renderer {

    private GraphicsDevice GD;
    private SpriteBatch SB;
    private ContentManager Content;
    private SpriteFont DefaultFont;

    private Texture2D SelectedTileTexture;

    private Vector2 Camera;

    private UpgradePanel CurrentUpgradePanel;

    public Renderer(GraphicsDevice InGraphicsDevice, SpriteBatch InSpriteBatch, ContentManager InContent) {
        GD = InGraphicsDevice;
        SB = InSpriteBatch;
        Content = InContent;

        DefaultFont = Content.Load<SpriteFont>("DefaultFont");
        SelectedTileTexture = Content.Load<Texture2D>("SelectionTile");

        Camera = new Vector2(0, 0);

        CurrentUpgradePanel = new UpgradePanel(Content, new Rectangle(500, 240, 300, 240));
    }

    // The width and height of a tile in "pixels?" in World Space
    public static readonly int TileWidth = 128;
    public static readonly int TileHeight = 128;

    private Vector2 AsVector2(Point P) {
        return new Vector2(P.X, P.Y);
    }

    public Vector2 ScreenSpaceToWorldSpace(Vector2 V) {
        return V + Camera;
    }

    public void Render(GameSession CurrentSession) {
        // Start by working out where to draw every tile
        List<PositionTilePair> TilesAndPositions = CurrentSession.GetTileGrid().GetAllTilesAndPositions();
        foreach (PositionTilePair Pair in TilesAndPositions) {
            // The returned coordinates are "Tile-Grid Theoretic" we want them in "World Space"
            Pair.P.X *= TileWidth;
            Pair.P.Y *= TileHeight;
        }

        GD.Clear(Color.CornflowerBlue);

        SB.Begin();

        foreach (PositionTilePair Pair in TilesAndPositions) {
            // If the Pair describes a non-empty Tile, we draw it.
            // Otherwise, the Pair has no texture and has no need to be drawn.

            // When we draw the tiles, we get their "World Space" coordinates, so we translate them to make them "Screen Space"
            if (Pair.T.GetType() != typeof(EmptyTile)) {
                SB.Draw(Pair.T.GetTexture(), AsVector2(Pair.P) - Camera, Color.White);
            }
        }

        // Now we draw the selected tile texture on top of a selected tile
        if (CurrentSession.GetTileGrid().IsATileSelected()) {
            // We need to find where this particular tile is in World Space
            Point SelectedTileLocationInWorldSpace = CurrentSession.GetTileGrid().GetSelectedTileAddress().Value;
            SelectedTileLocationInWorldSpace.X *= TileWidth;
            SelectedTileLocationInWorldSpace.Y *= TileHeight;

            Vector2 SelectedTileLocationInScreenSpace = AsVector2(SelectedTileLocationInWorldSpace) - Camera;
            
            SB.Draw(SelectedTileTexture, SelectedTileLocationInScreenSpace, Color.White);
        }

        // START TEMP
        //SB.DrawString(DefaultFont, "Current Wood: " + CurrentSession.GetWood().ToString(), new Vector2(100, 100), Color.Black);
        //SB.DrawString(DefaultFont, "Current Wood Upgrade Count: " + ((Forest)(CurrentSession.GetTileGrid().GetTile(0, 0))).UpgradeCount, new Vector2(100, 200), Color.Black);
        //SB.DrawString(DefaultFont, "Current Stone: " + CurrentSession.GetStone().ToString(), new Vector2(100, 300), Color.Black);
        // END TEMP

        // Now that the game is drawn, we draw the UI
        // We draw the upgrade panel only if a tile is selected, and it is non-empty.
        // The upgrade panel then handles it regardless of whether or not that tile is upgradeable.
        if (CurrentSession.GetTileGrid().IsATileSelected()) {
            if (!(CurrentSession.GetTileGrid().GetSelectedTile() is EmptyTile)) {
                CurrentUpgradePanel.SelectNewTile(CurrentSession.GetTileGrid().GetSelectedTile());
                CurrentUpgradePanel.Render(SB);
            }
        }

        SB.End();
    }

    public void MoveCamera(Vector2 Movement) {
        Camera += Movement;
    }
    
}