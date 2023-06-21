using System.Text.RegularExpressions;

namespace RegularExpression
{
    public static class RegularExpressionStore
    {
        // should return a bool indicating whether the input string is
        // a valid team international email address: firstName.lastName@domain (serhii.mykhailov@teaminternational.com etc.)
        // address cannot contain numbers
        // address cannot contain spaces inside, but can contain spaces at the beginning and end of the string
        public static bool Method1(string input)
        {
            string anyword = "[a-zA-Z]+";
            string spaces = @"\s*";
            string dot = @"\.";
            string at = "@";
            string com = @"com\b";

            string pattern = $"{spaces}{anyword}{dot}{anyword}{at}{anyword}{dot}{com}{spaces}";


            Regex rg = new Regex(pattern);
            return rg.IsMatch(input);
        }

        // the method should return a collection of field names from the json input
        public static IEnumerable<string> Method2(string inputJson)
        {
            Regex rg = new Regex(@"(?<="")(\w+)(?="":)");

          
            

            var matchCollection = rg.Matches(inputJson);
          

            return matchCollection.Select((i) => i.ToString()) ;
        }

        // the method should return a collection of field values from the json input
        public static IEnumerable<string> Method3(string inputJson)
        {
            Regex rg = new Regex(@"(?<=(:|:""))(\w+)(?=("",|,|}))");

            //{"FieldString1":"field1","FieldString2":null,"_fieldString3":"_field3","FieldBool":true,"FieldInt":42}

            var matchCollection = rg.Matches(inputJson);


            return matchCollection.Select((i) => i.ToString());
        }

        // the method should return a collection of field names from the xml input
        public static IEnumerable<string> Method4(string inputXml)
        {
            Regex rg = new Regex(@"(?<=<)(\w+)(?=(>| xsi))");
            

            var matchCollection = rg.Matches(inputXml);


            return matchCollection.Select((i) => i.ToString());
        }

        // the method should return a collection of field values from the input xml
        // omit null values
        public static IEnumerable<string> Method5(string inputXml)
        {
            Regex rg = new Regex(@"(?<=>)(\w+)(?=<)");

            
            var matchCollection = rg.Matches(inputXml);

           
            return matchCollection.Select((i) => $"{i}");
        }

        // read from the input string and return Ukrainian phone numbers written in the formats of 0671234567 | +380671234567 | (067)1234567 | (067) - 123 - 45 - 67
        // +38 - optional Ukrainian country code
        // (067)-123-45-67 | 067-123-45-67 | 38 067 123 45 67 | 067.123.45.67 etc.
        // make a decision for operators 067, 068, 095 and any subscriber part.
        // numbers can be separated by symbols , | ; /
        public static IEnumerable<string> Method6(string input)
        {

            
            Regex subscriber = new Regex(@"\+38|\+38 |38 ");
            Regex notSinglePlus = new Regex(@"(?<!\+\d{1})");
            Regex start = new Regex(@"(095|067|068|\(095\)|\(067\)|\(068\))");
            Regex charsAndDigitsAfter = new Regex(@"( - | |-|\.)(?!095|067|068)((\d){3}(( - | |-|\.)(\d){2}){2}|(\d){4}(( - | |-|\.)(\d){3}))");
            Regex onlyDigitsAfter = new Regex(@"(?:\d{7})");

            
            Regex rg = new Regex(@$"({subscriber}|{notSinglePlus}){start}({onlyDigitsAfter}|{charsAndDigitsAfter})\b");

            var matchCollection = rg.Matches(input);

            return matchCollection.Select((i) => {
                var number = i.ToString();
                if (subscriber.IsMatch(i.ToString()) && !i.ToString().StartsWith("+"))                
                {
                    number = "+" + i.ToString();
                }
            
                return number;
            }
            
            );
        }
    }
}