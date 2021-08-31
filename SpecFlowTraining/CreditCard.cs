using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowTraining
{
    public class CreditCard
    {
        private double balance;
        private bool isActive;
        private double approvedLimit;
        private double limit;
        private double interestRate;
        private double outstandingAmount;
        private bool isMaxedOut;
        private CardCategory category;
        private DateTime billDueDate;
        private bool pastDue;

        public CreditCard(double approvedLimit = 0)
        {
            ApprovedLimit = approvedLimit;
            Limit = approvedLimit;
            Balance = approvedLimit;
            OutstandingAmount = 0;
            IsActive = true;
            IsMaxedOut = false;
        }

        public double Balance
        {
            get { return balance; }
            set { balance = value; }
        }
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
        public double Limit
        {
            get { return limit; }
            set { limit = value; }
        }
        public double InterestRate
        {
            get { return interestRate; }
            set { interestRate = value; }
        }
        public double OutstandingAmount
        {
            get { return outstandingAmount; }
            set { outstandingAmount = value; }
        }

        public bool IsMaxedOut { get => isMaxedOut; set => isMaxedOut = value; }
        public CardCategory Category { get => category; set => category = value; }
        public double ApprovedLimit { get => approvedLimit; set => approvedLimit = value; }
        public DateTime ExpiryDate { get => BillDueDate; set => BillDueDate = value; }
        public bool PastDue { get => pastDue; set => pastDue = value; }
        public DateTime BillDueDate { get => billDueDate; set => billDueDate = value; }

        public void AddLimit(double limit)
        {
            ApprovedLimit = limit;
            Limit = limit;
            Balance = limit;
            OutstandingAmount = 0;
        }

        public void SetCategory(CardCategory category)
        {
            Category = category;
            double extraLimit;
            switch (category)
            {
                case CardCategory.Silver:
                    extraLimit = ApprovedLimit * 2.50 / 100;
                    Limit += extraLimit;
                    break;
                case CardCategory.Gold:
                    extraLimit = ApprovedLimit * 5 / 100;
                    Limit += extraLimit;
                    break;
                case CardCategory.Diamond:
                    extraLimit = ApprovedLimit * 7.5 / 100;
                    Limit += extraLimit;
                    break;
                case CardCategory.Platinum:
                    extraLimit = ApprovedLimit * 10 / 100;
                    Limit += extraLimit;
                    break;
                default:
                    break;
            }
        }

        public void ApplyOffer(double offerPercentage)
        {
            double extraLimit = ApprovedLimit * offerPercentage / 100;
            Limit += extraLimit;
        }
        public void Block()
        {
            IsActive = false;
        }
        public void BillTransaction(Transaction tx)
        {
            double txAmount;
            double cbAmount = 0;
            if (tx.CashBackPercentage != 0)
            {
                cbAmount = tx.ItemPrice * (tx.CashBackPercentage / 100);
                if (cbAmount >= tx.MaxCashBack)
                    cbAmount = tx.MaxCashBack;
            }
            txAmount = tx.ItemPrice - cbAmount;
            Balance -= txAmount;
            OutstandingAmount += txAmount;

            if (OutstandingAmount >= Limit)
                IsMaxedOut = true;
        }

        public void SetBillDueDate(DateTime date)
        {
            BillDueDate = date;
            if (DateTime.Today > date)
                PastDue = true;
        }
    }
}
