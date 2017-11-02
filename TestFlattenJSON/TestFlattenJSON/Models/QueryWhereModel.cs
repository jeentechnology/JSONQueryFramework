using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFlattenJSON.Models
{
    public class QueryWhereModel
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public enum MatchOperator { equal, startsWith, endsWith, contains }

    }
}
