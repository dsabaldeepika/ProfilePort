using System;

namespace ProfilePort.Controllers
{
    internal class ApplicationDbContext
    {
        public System.Linq.IQueryable<Favorite> Favorites { get; internal set; }

        internal void Dispose()
        {
            throw new NotImplementedException();
        }

        internal object Entry(Favorite favorite)
        {
            throw new NotImplementedException();
        }
    }
}