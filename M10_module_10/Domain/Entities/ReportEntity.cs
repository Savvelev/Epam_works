using System;
using System.Xml.Serialization;

namespace Domain.Entities
{
    [Serializable]
    [XmlRoot]
    public class ReportEntity
    {
        [XmlElement]
        public string Lecture { get; set; }
        [XmlElement]
        public string StudentName { get; set; }
        [XmlElement]
        public bool AttendingLecture { get; set; }
    }
}
