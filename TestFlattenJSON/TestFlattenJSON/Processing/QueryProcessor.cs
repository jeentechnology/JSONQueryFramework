using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFlattenJSON.Processing
{
    public class QueryProcessor
    {

        public QueryProcessor(IQueryParser query)
        {
            this.query = query;
        }


        private readonly IQueryParser query;

    }
}
