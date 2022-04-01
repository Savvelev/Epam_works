using M09_Intoduction_To_Language_Integrated_Query.CriteriaContent;
using System;
using System.Collections.Generic;
using System.Linq;

namespace M09_Intoduction_To_Language_Integrated_Query
{
    public class Filter
    {
        public IEnumerable<Student> FilterByCriterias(IReadOnlyCollection<Student> students, Criterias criterias)
        {
            IEnumerable<Student> studentQuery = new List<Student>(students);
         
            studentQuery = studentQuery.Where(x => criterias.Validator(x));         

            if (!criterias.ToDoSort) return studentQuery;
            {
                Func<IEnumerable<Student>, Func<Student, object>, IOrderedEnumerable<Student>> sort;
                sort = criterias.Ascending ? Enumerable.OrderBy : Enumerable.OrderByDescending;
         
                studentQuery = criterias.SortName switch
                {
                    AppConstants.Name => sort(studentQuery, student => student.Name),
                    AppConstants.Test => sort(studentQuery, student => student.Test),
                    AppConstants.Mark => sort(studentQuery, student => student.Mark),
                    AppConstants.Date => sort(studentQuery, student => student.Date),
                    _ => studentQuery
                };
            }
            return studentQuery;
        }
    }
}