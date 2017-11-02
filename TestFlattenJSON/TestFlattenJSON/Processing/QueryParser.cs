using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFlattenJSON.Models;

namespace TestFlattenJSON.Processing
{
    public class QueryParser : IQueryParser
    {
        /// <summary>
        /// Parse out a query to convert into a query model
        /// </summary>
        public QueryModel ParseQuery(string query)
        {
            var parsedQuery = new Dictionary<int, string>();
            
            foreach (var item in AllQueryParts)
            {
                var part = ParseQueryPart(query, item);
                parsedQuery.Add(item.Key, part);
            }

            return BuildQueryModel(parsedQuery);
        }


        /// <summary>
        /// Buiild out all the parts of the query model
        /// </summary>
        private QueryModel BuildQueryModel(Dictionary<int, string> parsedQuery)
        {
            var ret = new QueryModel();

            ParseSelect(ret, parsedQuery[0]);
            ParseFrom(ret, parsedQuery[1]);
            ParseWhere(ret, parsedQuery[2]);

            return ret;
        }

        /// <summary>
        /// Take the select statemnt and convert it into the relevent parts of the query model
        /// </summary>
        private void ParseSelect(QueryModel model, string queryPart)
        {
            model.QueryFields = queryPart.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(i => i.Trim())
                .ToList();
        }

        /// <summary>
        /// Take the from statement and convert it into the relevent parts of the query model.
        /// </summary>
        private void ParseFrom(QueryModel model, string queryPart)
        {

            var index = 0;
            var items = new Dictionary<int, string>();
            foreach (var item in queryPart.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries))
            {
                items.Add(index++, item.Trim());
            }

            if (items.Keys.Count == 0)
                throw new Exception("unable to parse the FROM part of query, nothing was parsed.");

            var maxKey = items.Keys.Max();
            model.BaseQueryTerm = items[0];

            if (items.Keys.Count == 1)
                return;

            if (items.Values.Any(v => v == "[]"))
            {
                model.CheckArrayItems = true;
            }

            model.ChildQueryTerm = items[maxKey];

            if (items.Keys.Count <= 2)
                return;

            foreach (var item in items)
            {
                if(item.Key != 0 && item.Key != maxKey)
                {
                    model.AllChildTerms.Add(item.Value);
                }
            }
            
        }

        private void ParseWhere(QueryModel model, string queryPart)
        {

        }



        private string ParseQueryPart(string query, KeyValuePair<int,string> queryPart)
        {
            if (!AllQueryParts.ContainsKey(queryPart.Key))
                throw new IndexOutOfRangeException("The query part that was requested doesn't exist.");

            var startIndex = query.IndexOf(queryPart.Value);
            var isEnd = AllQueryParts.Keys.Max() == queryPart.Key;

            if (isEnd)
                return query.Substring(startIndex).Trim();

            var nextKeyWord = AllQueryParts[queryPart.Key + 1];
            var totalLength = (query.IndexOf(nextKeyWord) - startIndex);
            return query.Substring(startIndex, totalLength).Trim();
        }



        private Dictionary<int, string> AllQueryParts = new Dictionary<int, string>()
        {
            { 0, "SELECT"  },
            { 1, "FROM" },
            { 2, "WHERE" }

        };
    }
}
