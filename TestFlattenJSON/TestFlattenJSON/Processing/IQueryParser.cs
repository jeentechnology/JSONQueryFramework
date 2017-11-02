using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFlattenJSON.Models;

namespace TestFlattenJSON.Processing
{
    public interface IQueryParser
    {

        QueryModel ParseQuery(string query);

    }
}
