using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class GameEngine
    {
        private Profile _currentUser = null;
        private Random _random = new Random();
        private double _currencyExchangeRate = 0;
        private double k = 0.2;
        public GameEngine(double currentMoney, double currentCurrency, double currencyExchangeRate)
        {
            _currentUser = new Profile(currentMoney, currentCurrency);
            _currencyExchangeRate = currencyExchangeRate;
        }

        public double GetNewCurrencyExchangeRate()
        {
         //   k = _random.NextDouble();
            _currencyExchangeRate = _currencyExchangeRate * (1 + k * (_random.NextDouble() - 0.5));
            _currencyExchangeRate = _currencyExchangeRate < 1 ? 1 : _currencyExchangeRate;
            return _currencyExchangeRate;
        }

        public bool IsBoughtCurrency(double money)
        {
            if (_currentUser.CurrentMoney < money)
            {
                return false;
            }
            BoughtCurrency(money);
            return true;
        }

        private void BoughtCurrency(double money)
        {
            _currentUser.CurrentMoney -= money;
            _currentUser.CurrentCurrency += (double)(money / _currencyExchangeRate);
        }

        public bool IsSoldCurrency(double currency)
        {
            if (_currentUser.CurrentCurrency < currency)
            {
                return false;
            }
            SoldCurrency(currency);
            return true;
        }

        private void SoldCurrency(double currency)
        {
            _currentUser.CurrentCurrency -= currency;
            _currentUser.CurrentMoney += currency * _currencyExchangeRate;
        }

        public double GetResultTheCalculateProfit()
        {
            return _currentUser.CurrentMoney + _currentUser.CurrentCurrency * _currencyExchangeRate - _currentUser.StartMoney;
        }

        public double GetMoney()
        {
            return _currentUser.CurrentMoney;
        }

        public double GetCurrency()
        {
            return _currentUser.CurrentCurrency;
        }
    }
}
