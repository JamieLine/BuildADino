using System;

public interface IUpdateableResourceCounts {
    public void AddWood(int W);
    public void AddCoal(int C);
    public void AddIron(int I);
    public void AddStone(int S);
    public bool CanPayWood(int W);
    public bool CanPayCoal(int C);
    public bool CanPayIron(int I); 
    public bool CanPayStone(int S);
    public bool RemoveWood(int W);
    public bool RemoveCoal(int C);
    public bool RemoveIron(int I);
    public bool RemoveStone(int S);
}