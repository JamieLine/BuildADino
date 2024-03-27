using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class Miner : Tile, UpgradableTile {

    // How much wood the player should receive per Global Tile Update.
    // This might be edited by upgrades etc
    private int StoneGain;
    private int IronGain;

    private int UpgradeCooldown = 0;
    private int MaxUpgradeCooldown = 250;
    private int UpgradeCost = 10;
    public int UpgradeCount { get; private set;}

    private Texture2D CurrentTexture;


    public Miner(ContentManager Content) {
        StoneGain = 1;
        IronGain = 0;

        CurrentTexture = Content.Load<Texture2D>("MinerTile");
    }

    public void OnGlobalTileUpdate(IUpdateableResourceCounts CurrentSession)
    {
        CurrentSession.AddStone(StoneGain);
        CurrentSession.AddIron(IronGain);
    }

    public void UpgradeTile()
    {
        StoneGain += 1;
        IronGain += 1;
    }

    public void UpgradeTile(IUpdateableResourceCounts CurrentSession)
    {
        if (UpgradeCooldown == 0 && CurrentSession.CanPayStone(UpgradeCost)) {
            CurrentSession.RemoveStone(UpgradeCost);
            UpgradeCooldown = MaxUpgradeCooldown;
            StoneGain += 1;
            IronGain += 1;
            UpgradeCount += 1;
        }
    }

    public void Tick(int DeltaTime)
    {
        UpgradeCooldown -= DeltaTime;
        if (UpgradeCooldown < 0) { UpgradeCooldown = 0;}
    }

    public Texture2D GetTexture()
    {
        return CurrentTexture;
    }
}