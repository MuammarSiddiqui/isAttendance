using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isAttendance
{
    public class AttendanceLog
    {
        public Guid? Id { get; set; }
        public Guid? AttendanceStatusId { get; set; }
        public string EmployeeId { get; set; } = null;
        public string SignIn { get; set; } = null;
        public string Name { get; set; } = null;
        public bool IsAdmin { get; set; }
        public string SignOut { get; set; } = null;
        public Guid? CampusId { get; set; } = StaticValues.GetCampusId();
        public string NotificationStatus { get; set; } = null;
        public Guid? AcademicSessionId { get; set; }
        public string Comment { get; set; } = null;
        public DateTime AttendanceDate { get; set; }
      
    }
    public class AttendanceHistory
    {
        public Guid? Id { get; set; }
        public string UserId { get; set; } = "";
        public string Time { get; set; } = null; 
        public string SignIn { get; set; } = null;
        public string SignOut { get; set; } = null;
        public string Status { get; set; } = null;
        public string Date { get; set; } = null;
        public Guid? CampusId { get; set; } = StaticValues.GetCampusId();
    }

}
