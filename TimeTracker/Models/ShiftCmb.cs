using TimeTracker.BE.DB.Models;

namespace TimeTracker.Models
{
    public class ShiftCmb : Shift
    {
        public string StartDateStr
        {
            get
            {
                var result = "";
                if (StartDate != DateTime.MaxValue)
                    result = StartDate.ToString("dd.MM.yy");

                return result;
            }
        }

        public ShiftCmb()
        {
            StartDate = DateTime.MaxValue;
        }

        public ShiftCmb(Shift shift) : this()
        {
            GuidId = shift.GuidId;
            Description = shift.Description;
            StartDate = shift.StartDate;
        }
    }
}