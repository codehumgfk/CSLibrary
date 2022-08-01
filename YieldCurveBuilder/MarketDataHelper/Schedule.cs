using System;
using System.Collections.Generic;
using System.Text;

namespace MarketDataHelper
{
    public class Schedule
    {
        public DateTime StartDate;
        public DateTime EndDate;
        public DateTime PaymentDate;
        public double DCF;

        public Schedule(DateTime startDate, DateTime endDate, DateTime paymentDate, double dcf)
        {
            StartDate = startDate;
            EndDate = endDate;
            PaymentDate = paymentDate;
            DCF = dcf;
        }

        public Schedule Copy()
        {
            return new Schedule(StartDate, EndDate, PaymentDate, DCF);
        }
    }
}
