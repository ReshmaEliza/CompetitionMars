using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TestMarsCompetition.ModelEducation;
using Newtonsoft.Json;
using System.IO;


namespace TestMarsCompetition.Utilities
{
    public class JsonReaderEdu
    {

        public static TestData ReadTestData(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<TestData>(json);
        }
    }
}