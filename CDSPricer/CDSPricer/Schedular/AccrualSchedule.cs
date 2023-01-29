using System;
using System.Collections.Generic;
using System.Text;

namespace CDSPricer.Schedular
{
    public class AccrualSchedule
    {
        public DateTime StartDate;
        public DateTime EndDate;
        public DateTime PaymentDate;
        public double Dcf;

        public AccrualSchedule(DateTime startDate, DateTime endDate, DateTime paymentDate, double dcf)
        {
            StartDate = startDate;
            EndDate = endDate;
            PaymentDate = paymentDate;
            Dcf = dcf;
        }
    }
}
