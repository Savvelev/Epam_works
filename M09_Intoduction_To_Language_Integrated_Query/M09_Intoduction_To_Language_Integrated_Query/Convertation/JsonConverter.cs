using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;

namespace M09_Intoduction_To_Language_Integrated_Query
{
    class JsonConverter<T> : IConverter<T> 
    {       
        public IReadOnlyCollection<T> Convert()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Convertation/Results_of_students_tests.json");
            var json = File.ReadAllText(path);

            var format = "dd/MM/yyyy";
            var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };

            var students = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(json, dateTimeConverter);
            return students;
        }
    }
}
