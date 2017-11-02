using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFlattenJSON.Models
{
    public class QueryModel
    {

        /// <summary>
        /// The root query item to be searched
        /// </summary>
        public string BaseQueryTerm { get; set; }

        /// <summary>
        /// Defines the bottom child term to match
        /// </summary>
        public string ChildQueryTerm { get; set; }

        /// <summary>
        /// Whether or not to search through array items
        /// </summary>
        public bool CheckArrayItems { get; set; }

        /// <summary>
        /// The stack of terms between Base and child
        /// </summary>
        public List<string> AllChildTerms { get; set; }

        /// <summary>
        /// The where terms from the query
        /// </summary>
        public List<QueryWhereModel> WhereTerms { get; set; }

        /// <summary>
        /// The fields that they want returned from the queried object
        /// </summary>
        public List<string> QueryFields { get; set; }

    }
}
