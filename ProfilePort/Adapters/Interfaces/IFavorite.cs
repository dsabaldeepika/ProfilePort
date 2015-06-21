using ProfilePort.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfilePort.Adapters.Interfaces
{
    public interface IFavorite
    {
        List<Favorite> GetFavorite(String UserId);
        Favorite GetFavorite(int id);
        Favorite PostNewFavorite(string UserID, Favorite newFavorite);
        Favorite PutFavorite(int id, Favorite newFavorite);
        Favorite DeleteFavorite(int id);
    }
}
