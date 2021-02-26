using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Profile
    {
        public double StartMoney { get; }
        public double CurrentMoney { get; set; }
        public double CurrentCurrency { get; set; }

        public Profile(double currentMoney, double currentCurrency)
        {
            CurrentMoney = currentMoney;
            CurrentCurrency = currentCurrency;
            StartMoney = currentMoney;
        }
    }
}
