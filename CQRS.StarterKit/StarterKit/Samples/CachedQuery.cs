using System;
using System.Collections.Generic;
using System.Threading;
using StarterKit.Queries;

namespace StarterKit.Samples
{
    public class GetAllProductsQuery : IQuery<IEnumerable<Product>>, ICachedQuery
    {
        public string CacheKey => "GetAllProducts";
        public TimeSpan CacheDuration => TimeSpan.FromHours(1);
    }

    public class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly Database database;

        public GetAllProductsQueryHandler(Database database)
        {
            this.database = database;
        }

        public IEnumerable<Product> Handle(GetAllProductsQuery query)
        {
            var result = new List<Product>();

            foreach (var product in database.Products)
            {
                Thread.Sleep(result.Count * 1000); // very long operation! N+1 issue here
                result.Add(product);
            }

            return result;
        }
    }
}
