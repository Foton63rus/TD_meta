using System;

namespace TowerDefence
{
    public abstract class Currency
    {
        public int Amount;
    }

    [Serializable]
    public class Energy : Currency
    {
        public Energy(int amount = 0) { Amount = amount; }
    
        public Energy() { Amount = 0; }
    }
    
    [Serializable]
    public class Coin : Currency
    {
        public Coin(int amount = 0) { Amount = amount; }

        public Coin() { Amount = 0; }
    }

    [Serializable]
    public class Diamond : Currency
    {
        public Diamond(int amount = 0) { Amount = amount; }
    
        public Diamond() { Amount = 0; }
    }

    [Serializable]
    public class Star : Currency
    {
        public Star(int amount = 0) { Amount = amount; }
    
        public Star() { Amount = 0; }
    }
}