using System;

namespace TowerDefence
{
    [Serializable]
    public struct GameCurrency
    {
        public Currency currency;
        public int count;

        public GameCurrency(Currency currency, int count)
        {
            this.currency = currency;
            this.count = count;
        }
    }
}
