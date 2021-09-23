using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpecFlowTraining;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;


namespace SpecFlowProjectTest.Steps
{
    [Binding]
    public class CCPropertiesSteps
    {
        private CreditCard _cc;
        [Given(@"I have a credit card")]
        public void GivenIHaveANewCreditCard()
        {
            _cc = new CreditCard();
        }

        [Given(@"the credit card limit is (.*) USD")]
        public void GivenTheCreditCardLimitIsUSD(float limit)
        {
            _cc.AddLimit(limit);
        }

        [Then(@"credit card should be active")]
        public void ThenCreditCardShouldBeActive()
        {
            Assert.IsTrue(_cc.IsActive);
        }

        [Then(@"balance should be (.*)")]
        public void ThenBalanceShouldBe(float balanceAmt)
        {
            Assert.AreEqual(balanceAmt, _cc.Balance);
        }

        [Then(@"outstanding amount should be (.*)")]
        public void ThenOutstandingAmountShouldBe(float outstandingAmt)
        {
            Assert.AreEqual(outstandingAmt, _cc.OutstandingAmount);
        }

        [Then(@"total limit should be (.*)")]
        public void ThenTotalLimitShouldBe(float expectedLimit)
        {
            Assert.AreEqual(expectedLimit, _cc.Limit);
        }

        [Given(@"extra offer on limit is (.*)%")]
        public void GivenExtraOfferOnLimitIs(int offerPercentage)
        {
            _cc.ApplyOffer(offerPercentage);
        }

        [Given(@"limit and offer is as follows")]
        public void GivenLimitAndOfferIsAsFollows(Table table)
        {
            //---------Weekly Typed Conversion---------------
            //var limit = table.Rows.First(row => row["attribute"] == "limit")["value"];
            //var offer = table.Rows.First(row => row["attribute"] == "offer")["value"];

            //_cc.AddLimit(Convert.ToDouble(limit));
            //_cc.ApplyOffer(Convert.ToDouble(offer));

            //------------Strongly typed conversion----------
            //var limitAndOffer = table.CreateInstance<CCLimitAndOffer>();
            //_cc.AddLimit(limitAndOffer.Limit);
            //_cc.ApplyOffer(limitAndOffer.Offer);

            //------------Dynamic instance------------------
            dynamic attributes = table.CreateDynamicInstance();
            _cc.AddLimit(attributes.limit);
            _cc.ApplyOffer(attributes.offer);
        }


        [When(@"following extra offers are applied")]
        public void WhenFollowingExtraOffersAreApplied(Table table)
        {
            //---------Weekly Typed Conversion---------------
            var category = table.Rows.First(row => row["attribute"] == "category")["value"];
            var offer = table.Rows.First(row => row["attribute"] == "offer")["value"];

            _cc.SetCategory((CardCategory)Enum.Parse(typeof(CardCategory), category));
            _cc.ApplyOffer(Convert.ToDouble(offer));



        }
        [When(@"credit card is blocked")]
        public void WhenCreditCardIsBlocked()
        {
            _cc.Block();
        }

        [Then(@"credit card's IsActive flag should be (.*)")]
        public void ThenCreditCardSIsActiveFlagShouldBe(bool isActive)
        {
            Assert.AreEqual(isActive, _cc.IsActive);
        }

        [Given(@"the card category is (.*)")]
        public void GivenTheCardCategoryIsGold(CardCategory category)
        {
            _cc.SetCategory(category);
        }

        [When(@"a transaction with below attributes is billed")]
        public void WhenATransactionWithBelowAttributesIsBilled(Table table)
        {
            //-------------------Weakly typed-------------------
            // foreach (var row in table.Rows)
            // {
            //     double itemPrice = Convert.ToDouble(row["ItemPrice"]);
            //     double cbPercent = Convert.ToDouble(row["CashBackPercentage"]);
            //     double maxCb = Convert.ToDouble(row["MaxCashBack"]);
            //     Transaction tx = new Transaction(itemPrice, cbPercent, maxCb);
            //     _cc.BillTransaction(tx);
            //}

            //-----------------Strongly types create set-------------
            //IEnumerable<Transaction> transactions = table.CreateSet<Transaction>();
            //foreach (var tx in transactions)
            //    _cc.BillTransaction(tx);

            //-----------------Create dynamic sets---------------------
            IEnumerable<dynamic> transactions = table.CreateDynamicSet();
            foreach (var tx in transactions)
                _cc.BillTransaction(new Transaction(tx.ItemPrice, tx.CashBackPercentage, tx.MaxCashBack));
        }

        [When(@"the credit card due date has passed (.*)")]
        public void WhenTheCreditCardDueDateHasPassedDaysAgo(DateTime date)
        {
            _cc.SetBillDueDate(date);
        }

        [Then(@"credit card's status should be past due")]
        public void ThenCreditCardSStatusShouldBePastDue()
        {
            Assert.IsTrue(_cc.PastDue);
        }

        [When(@"a transaction with below attributes is billed - use automatic custom conversion")]
        public void WhenATransactionWithBelowAttributesIsBilled_UseAutomaticCustomConversion(IEnumerable<Transaction> txObjectSet)
        {
            foreach (var tx in txObjectSet)
                _cc.BillTransaction(tx);
        }

    }
}
