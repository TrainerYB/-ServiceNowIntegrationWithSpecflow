using SpecFlowProjectTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using SpecFlowTraining;


namespace SpecFlowProjectTest.Drivers
{
    [Binding]
    class CustomConversion
    {
        [StepArgumentTransformation("(.*) days ago")]
        public DateTime DaysAgoConversion(int daysAgo)
        {
            return DateTime.Today.Subtract(TimeSpan.FromDays(daysAgo));
        }

        [StepArgumentTransformation]
        public IEnumerable<Transaction> TransactionObjectConversion(Table table)
        {
            return table.CreateSet<Transaction>();
        }
    }
}
