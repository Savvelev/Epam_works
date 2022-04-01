using Domain.Entities;
using System.Collections.Generic;

namespace Module_10.UnitTests
{
    class ReportSetUp
    {
        public List<ReportEntity> TestReport { get; } = new()
        {
            new ReportEntity { Lecture = "Math", StudentName = "Mikhail Savelev", AttendingLecture = true },
            new ReportEntity { Lecture = "Math", StudentName = "Mikhail Savelev", AttendingLecture = false }
        };
    }
}
