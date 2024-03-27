using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

public class GameSession : IFetchableResourceCounts, IUpdateableResourceCounts{
    private int Wood;
    private int Coal;
    private int Iron;

    private int Stone;

    private TileGrid ActiveTiles;

    private ContentManager Content;

    public GameSession(ContentManager InContent) {
        Content = InContent;
        Wood = 0;
        Coal = 0;
        Iron = 0;
        Stone = 0;

        ActiveTiles = new TileGrid(10, 10);
        ActiveTiles.SetTile(0, 0, new Forest(Content));
        ActiveTiles.SetTile(1, 0, new Miner(Content));
        ActiveTiles.SetTile(2, 2, new Forest(Content));
        //ActiveTiles = new List<Tile>
        //{
        //    // The player gets these tiles for free
        //    new Forest(Content, new Vector2(50, 50), 1), new Miner(Content, new Vector2(250, 250), 1),
        //};
    }

    public TileGrid GetTileGrid() {
        return ActiveTiles;
    }

    public void TickTiles(int DeltaTime) {
        foreach (Tile T in ActiveTiles.GetListOfAllTiles()) {
            T.Tick(DeltaTime);
        }
    }

    public void UpdateActiveTiles() {
        foreach (Tile T in ActiveTiles.GetListOfAllTiles()) {
            T.OnGlobalTileUpdate(this);
        }
    }

    public void AddCoal(int C)
    {
        Coal += C;
    }

    public void AddIron(int I)
    {
        Iron += I;
    }

    public void AddWood(int W)
    {
        Wood += W;
    }

    public void AddStone(int S) {
        Stone += S;
    }

    public int GetCoal()
    {
        return Coal;
    }

    public int GetIron()
    {
        return Iron;
    }

    public int GetWood()
    {
        return Wood;
    }

    public int GetStone() {
        return Stone;
    }

    public bool CanPayWood(int W)
    {
        return Wood >= W;
    }

    public bool CanPayCoal(int C)
    {
        return Coal >= C;
    }

    public bool CanPayIron(int I)
    {
        return Iron >= I;
    }

    public bool CanPayStone(int S)
    {
        return Stone >= S;
    }

    public bool RemoveWood(int W)
    {
        if (CanPayWood(W)) {
            Wood -= W;
            return true;
        }

        return false;
    }

    public bool RemoveCoal(int C)
    {
        if (CanPayCoal(C)) {
            Coal -= C;
            return true;
        }

        return false;
    }

    public bool RemoveIron(int I)
    {
        if (CanPayIron(I)) {
            Iron -= I;
            return true;
        }

        return false;
    }

    public bool RemoveStone(int S)
    {
        if (CanPayStone(S)) {
            Stone -= S;
            return true;
        }

        return false;

    }
}