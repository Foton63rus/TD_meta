using System;
using UnityEngine;

namespace TowerDefence
{
    [Serializable]
    public class Deposit
    {
        [SerializeField] private int _energy;
        [SerializeField] private int _coin;
        [SerializeField] private int _diamond;
        [SerializeField] private int _star;

        public int Energy => _energy;

        public int Coin => _coin;

        public int Diamond => _diamond;

        public int Star => _star;

        public Deposit( int energy = 0, int coin = 0, int diamond = 0, int star = 0)
        {
            _energy = energy;
            _coin = coin;
            _diamond = diamond;
            _star = star;
        }
        
        public Deposit( Energy energy = null, Coin coin = null, Diamond diamond = null, Star star = null)
        {
            _energy = energy != null ? energy.Amount : 0;
            _coin = coin != null ? coin.Amount : 0;
            _diamond = diamond != null ? diamond.Amount : 0;
            _star = star != null ? star.Amount : 0;
        }

        public bool canSubtract( Deposit subtrahend )
        {
            if (_energy >= subtrahend._energy &&
                _coin >= subtrahend._coin &&
                _diamond >= subtrahend._diamond &&
                _star >= subtrahend._star)
            {
                return true;
            }
            
            return false;
        }
        
        public bool canSubtract( Energy energy = null, Coin coin = null, Diamond diamond = null, Star star = null )
        {
            Deposit subtrahend = new Deposit(energy, coin, diamond, star);
            
            if (_energy >= subtrahend._energy &&
                _coin >= subtrahend._coin &&
                _diamond >= subtrahend._diamond &&
                _star >= subtrahend._star)
            {
                return true;
            }
            
            return false;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else {
                Deposit secObj = (Deposit) obj;
                return (_energy == secObj._energy) && 
                       (_coin == secObj._coin) &&
                       (_diamond == secObj._diamond) &&
                       (_star == secObj._star);
            }
        }

        public override string ToString()
        {
            return $"currency[ energy: {_energy}, coin: {_coin}, diamond: {_diamond}, star: {_star}]";
        }
    }

    public abstract class BaseCurrency
    {
        public int Amount;
    }

    [Serializable]
    public class Energy : BaseCurrency
    {
        public Energy(int amount)
        {
            Amount = amount;
        }
    }
    
    [Serializable]
    public class Coin : BaseCurrency
    {
        public Coin(int amount)
        {
            Amount = amount;
        }
    }

    [Serializable]
    public class Diamond : BaseCurrency
    {
        public Diamond(int amount)
        {
            Amount = amount;
        }
    }

    [Serializable]
    public class Star : BaseCurrency
    {
        public Star(int amount)
        {
            Amount = amount;
        }
    }

    [Serializable]
    public enum Currency
    {
        Free = 0,
        Ads = 1,
        GameMoney = 2,
        RealMoney = 3
    }
    
}