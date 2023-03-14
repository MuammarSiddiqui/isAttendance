using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isAttendance
{
    public class AttendanceLog
    {
        public int Id { get; set; }
        public string UserId { get; set; } = "";
        public string signIn { get; set; } = null;
        public string signOut { get; set; } = null;
        public string Date { get; set; } = null;
    }
}
