using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;

public class InputHandler {
    private readonly Renderer CurrentRenderer;
    private readonly GameSession CurrentSession;

    private int TEMP_SelectX = 0;

    public InputHandler(Renderer CurrentRenderer, GameSession CurrentSession) {
        this.CurrentRenderer = CurrentRenderer;
        this.CurrentSession = CurrentSession;
    }

    public void HandleAllInputs(GameTime gameTime) {
        CheckAndMoveCamera(gameTime);
        CheckAndSelectTile(gameTime);

        // TEMP
        /*if (Keyboard.GetState().IsKeyDown(Keys.Space)) {
            CurrentSession.GetTileGrid().SelectTile(TEMP_SelectX, 0);
            if (TEMP_SelectX == 0) {TEMP_SelectX = 1;}
            else {TEMP_SelectX = 0;}
        }*/

        if (Keyboard.GetState().IsKeyDown(Keys.Enter)) {
            if (CurrentSession.GetTileGrid().IsATileSelected()) {
                CurrentSession.GetTileGrid().UpgradeSelectedTile(CurrentSession);                
            }
        }
    }

    public void CheckAndMoveCamera(GameTime gameTime) {
        if (Keyboard.GetState().IsKeyDown(Keys.Up)) {
            CurrentRenderer.MoveCamera(new Vector2(0, -5*gameTime.ElapsedGameTime.Milliseconds / 1000f));
        }

        if (Keyboard.GetState().IsKeyDown(Keys.Down)) {
            CurrentRenderer.MoveCamera(new Vector2(0, 5*gameTime.ElapsedGameTime.Milliseconds / 1000f));
        }

        if (Keyboard.GetState().IsKeyDown(Keys.Left)) {
            CurrentRenderer.MoveCamera(new Vector2(-5*gameTime.ElapsedGameTime.Milliseconds / 1000f, 0));
        }

        if (Keyboard.GetState().IsKeyDown(Keys.Right)) {
            CurrentRenderer.MoveCamera(new Vector2(5*gameTime.ElapsedGameTime.Milliseconds / 1000f, 0));
        }
    }

    public void CheckAndSelectTile(GameTime gameTime) {
        if (Mouse.GetState().LeftButton == ButtonState.Pressed) {
            Vector2 MousePositionInScreenSpace = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            Vector2 MousePositionInWorldSpace = CurrentRenderer.ScreenSpaceToWorldSpace(MousePositionInScreenSpace);
            Tile? PotentialTile = CurrentSession.GetTileGrid().GetTileAtPosition(MousePositionInWorldSpace);

            // If there is no tile here, we deselect a tile if one is already selected.
            if (PotentialTile is null) { CurrentSession.GetTileGrid().DeselectTile(); }

            // If there is a space for a tile, but no tile, we deselect a tile if one is already selected.
            if (PotentialTile is EmptyTile) { CurrentSession.GetTileGrid().DeselectTile(); }

            // If the tile is upgradable, then select it.
            if (PotentialTile is UpgradableTile) {
                Point Index = CurrentSession.GetTileGrid().WorldPositionToTileIndex(MousePositionInWorldSpace);
                CurrentSession.GetTileGrid().SelectTile(Index);
            }
        }
    }
}