
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class Forest : Tile, UpgradableTile
{

    // How much wood the player should receive per Global Tile Update.
    // This might be edited by upgrades etc
    private int WoodGain;
    private int UpgradeCooldown = 0;
    private int MaxUpgradeCooldown = 250;
    private int UpgradeCost = 10;
    public int UpgradeCount { get; private set;}

    private Texture2D CurrentTexture;

    public Forest(ContentManager Content) {
        CurrentTexture = Content.Load<Texture2D>("ForestTile");
        WoodGain = 1;
    }

    public void OnGlobalTileUpdate(IUpdateableResourceCounts CurrentSession)
    {
        CurrentSession.AddWood(WoodGain);
    }

    public void UpgradeTile(IUpdateableResourceCounts CurrentSession)
    {
        if (UpgradeCooldown == 0 && CurrentSession.CanPayWood(UpgradeCost)) {
            CurrentSession.RemoveWood(UpgradeCost);
            UpgradeCooldown = MaxUpgradeCooldown;
            WoodGain += 1;
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