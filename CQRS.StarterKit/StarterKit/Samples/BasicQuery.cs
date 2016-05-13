using System.Collections.Generic;
using System.Linq;
using StarterKit.Queries;

namespace StarterKit.Samples
{
    public class GetProductsQuery : IQuery<IEnumerable<Product>>
    {
        public GetProductsQuery(bool includeInactive = false)
        {
            IncludeInactive = includeInactive;
        }

        public bool IncludeInactive { get; set; }
    }

    public class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly Database database;

        public GetProductsQueryHandler(Database database)
        {
            this.database = database;
        }

        public IEnumerable<Product> Handle(GetProductsQuery query)
        {
            if (query.IncludeInactive)
            {
                return database.Products.ToList();
            }

            return database.Products.Where(p => p.IsActive).ToList();
        }
    }
}
