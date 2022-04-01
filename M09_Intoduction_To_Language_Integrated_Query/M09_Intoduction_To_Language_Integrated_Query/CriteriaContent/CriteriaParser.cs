using M09_Intoduction_To_Language_Integrated_Query.CriteriaContent;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace M09_Intoduction_To_Language_Integrated_Query
{
    public class CriteriaParser
    {
        Criterias criterias;
        public CriteriaParser(Criterias criterias)
        {
            this.criterias = criterias;
        }
        public Criterias Parse(string input, Regex regex)
        {
            var validator = new StudentValidator(criterias);
            var output = regex.Matches(input)
                .OfType<Match>()
                .Select(m => m.Value.Substring(1))
                .ToArray();        

            foreach (var item in output)
            {                            
                var splited = item.Split(" ");
                var criteria = splited[0];
                var value =splited[1];              

                if (splited[0] == AppConstants.Sort)
                {
                    criterias.ToDoSort = true;
                    criteria = splited[1];
                    value = splited[3];
                    criterias.SortName = splited[1];
                    criterias.Ascending = splited[2] == AppConstants.Asc;
                }
                FillCriteria(criterias, criteria, value, validator);
            }
            criterias.Validator = validator.Validate;

            return criterias;
        }
        private static void FillCriteria(Criterias criterias, string criteria, string value, StudentValidator validator)
        {
            switch (criteria)
            {
                case AppConstants.Name:
                    validator.AddFilterOption(StudentValidator.ValidationOptins.Name);
                    criterias.Name = value;
                    break;
                case AppConstants.MinMark:
                    validator.AddFilterOption(StudentValidator.ValidationOptins.MinMark);
                    criterias.MinMark = int.Parse(value);
                    break;
                case AppConstants.MaxMark:
                    validator.AddFilterOption(StudentValidator.ValidationOptins.MaxMark);
                    criterias.MaxMark = int.Parse(value);
                    break;
                case AppConstants.DateFrom:
                    validator.AddFilterOption(StudentValidator.ValidationOptins.DateFrom);
                    criterias.DateFrom = DateTime.Parse(value);
                    break;
                case AppConstants.DateTo:
                    validator.AddFilterOption(StudentValidator.ValidationOptins.DateTo);
                    criterias.DateTo = DateTime.Parse(value);
                    break;
                case AppConstants.Test:
                    validator.AddFilterOption(StudentValidator.ValidationOptins.Test);
                    criterias.Test = value;
                    break;
                default:
                    throw new ArgumentException("Unknown case");
            }
        }
    }
}
