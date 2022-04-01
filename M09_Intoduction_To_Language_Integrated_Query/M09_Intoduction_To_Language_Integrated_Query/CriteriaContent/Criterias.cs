using M09_Intoduction_To_Language_Integrated_Query.CriteriaContent;
using System;

namespace M09_Intoduction_To_Language_Integrated_Query
{
    public class Criterias
    {
        public Func<Student, bool> Validator { get; set; }
        public string Name { get; set; }
        public int MinMark { get; set; }
        public int MaxMark { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Test { get; set; }    

        private string sortName;
        public string SortName
        {
            get
            {
                return sortName;
            }
            set
            {
                if (value== AppConstants.MinMark || value == AppConstants.MaxMark)               
                    sortName = AppConstants.Mark;
                
                else if (value == AppConstants.DateFrom || value == AppConstants.DateTo)              
                    sortName = AppConstants.Date;
                
                else              
                    sortName = value;                              
            }
        }
        public bool Ascending { get; set; } = true;
        public bool ToDoSort { get; set; }
    }
}

