using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowTraining
{
    public class Transaction
    {
        private double itemPrice;
        private double cashBackPercentage;
        private double maxCashBack;

        public Transaction(double ItemPrice, double CashBackPercentage, double MaxCashBack)
        {
            this.ItemPrice = ItemPrice;
            this.CashBackPercentage = CashBackPercentage;
            this.MaxCashBack = MaxCashBack;
        }
        public double ItemPrice { get => itemPrice; set => itemPrice = value; }
        public double CashBackPercentage { get => cashBackPercentage; set => cashBackPercentage = value; }
        public double MaxCashBack { get => maxCashBack; set => maxCashBack = value; }

        
    }

}
