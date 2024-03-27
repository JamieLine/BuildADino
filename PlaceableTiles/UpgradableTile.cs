public interface UpgradableTile {
    public void UpgradeTile(IUpdateableResourceCounts CurrentSession);
    public int UpgradeCount { get;}
}