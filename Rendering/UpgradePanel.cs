using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class UpgradePanel : UIPanel {
    private Tile RelevantTile;
    private Texture2D BackgroundTexture;
    private SpriteFont Font;
    private Rectangle BoundingRect;

    public UpgradePanel(ContentManager Content, Rectangle BoundingRect) {
        this.RelevantTile = new EmptyTile();
        this.BoundingRect = BoundingRect;
        BackgroundTexture = Content.Load<Texture2D>("UpgradeBackgroundTexture");
        Font = Content.Load<SpriteFont>("DefaultFont");
    }

    public void Render(SpriteBatch SB) {
        SB.Draw(BackgroundTexture, BoundingRect, Color.White);
        if (RelevantTile is UpgradableTile) {
            UpgradableTile UTile = RelevantTile as UpgradableTile;
            SB.DrawString(Font, "UCount: " + UTile.UpgradeCount.ToString(), new Vector2(BoundingRect.Left + 50f, BoundingRect.Top + 50f), Color.Black);
        }
        
        else {
            SB.DrawString(Font, "Not Upgradable", new Vector2(BoundingRect.Top + 50f, BoundingRect.Left + 50f), Color.Black);
        }
    }

    public void SelectNewTile(Tile NewTile) {
        RelevantTile = NewTile;
    }
}