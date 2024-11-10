using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Windows.Reports.Services
{
    public class ReportParameterService
    {
        public ReportParameterService()
        {
            Monts = getLastSixMonth();
        }

        public List<string> Monts { get; set; }
        private List<string> getLastSixMonth()
        {
            List<string> lastSixMonths = new List<string>();
            DateTime currentDate = DateTime.Now;

            for (int i = 0; i < 6; i++)
            {
                DateTime month = currentDate.AddMonths(-i);
                string monthYear = month.ToString("MM.yyyy");
                lastSixMonths.Add(monthYear);
            }

            return lastSixMonths;
        }

    }
}
