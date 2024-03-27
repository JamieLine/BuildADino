
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class TileGrid {
    private List<Tile> Tiles;
    // The width and height of the grid in "Tile Units"
    private readonly int Width;
    private readonly int Height;

    private Point? SelectedTileAddress;

    public void DeselectTile() {
        SelectedTileAddress = null;
    }

    public TileGrid(int W, int H) {
        Width = W;
        Height = H;

        Tiles = new List<Tile>();
        for (int i = 0; i < W*H; i++) {
            Tiles.Add(new EmptyTile());
        }
    }

    public Point WorldPositionToTileIndex(Vector2 Position) {
        int TilePositionX = (int) Math.Floor(Position.X / Renderer.TileWidth);
        int TilePositionY = (int) Math.Floor(Position.Y / Renderer.TileHeight);

        return new Point(TilePositionX, TilePositionY);
    }

    // World space position
    public Tile? GetTileAtPosition(Vector2 Position) {
        Point Index = WorldPositionToTileIndex(Position);

        if (!IsLegalTile(Index.X, Index.Y)) {
            return null;
        }

        return GetTile(Index);
    }

    private bool IsLegalTile(int X, int Y) {
        return (X >= 0 && X < Width && Y >= 0 && Y < Height);
    }

    public bool IsATileSelected() {
        return SelectedTileAddress.HasValue;
    }

    public Point? GetSelectedTileAddress() {
        return SelectedTileAddress;
    }

    public Tile? GetSelectedTile() {
        if (!SelectedTileAddress.HasValue) { return null; }
        return GetTile(SelectedTileAddress.Value);
    }

    public void SelectTile(Point P) {
        SelectedTileAddress = P;
    }

    public void SelectTile(int X, int Y) {
        SelectedTileAddress = new Point(X, Y);
    }

    public void UpgradeSelectedTile(GameSession CurrentSession) {
        if (SelectedTileAddress.HasValue) {
            if (GetTile(SelectedTileAddress.Value) is UpgradableTile) {
                UpgradableTile UTile = GetTile(SelectedTileAddress.Value) as UpgradableTile;
                UTile.UpgradeTile(CurrentSession);
            }
        }
    }

    public Tile GetTile(int X, int Y) {
        if (!IsLegalTile(X, Y)) {
            throw new System.ArgumentOutOfRangeException("X or Y out of range. X = " + X.ToString() + ", Y = " + Y.ToString() + ", Width = " + Width.ToString() + ", Height = " + Height.ToString());
        }
        return Tiles[X*Width + Y];
    }

    public Tile GetTile(Point P) {
        return GetTile(P.X, P.Y);
    }

    public List<Tile> GetListOfAllTiles() {
        return Tiles;
    }

    public void SetTile(int X, int Y, Tile T) {
        if (!IsLegalTile(X, Y)) {
            throw new System.ArgumentOutOfRangeException("X or Y out of range. X = " + X.ToString() + ", Y = " + Y.ToString() + ", Width = " + Width.ToString() + ", Height = " + Height.ToString());
        }

        Tiles[X*Width + Y] = T;
    }

    public List<PositionTilePair> GetAllTilesAndPositions() {
        List<PositionTilePair> Pairs = new List<PositionTilePair>();
        for (int X = 0; X < Width; X++) {
            for (int Y = 0; Y < Height; Y++) {
                Pairs.Add(new PositionTilePair(new Point(X, Y), GetTile(X, Y)));
            }
        }

        return Pairs;
    }
}