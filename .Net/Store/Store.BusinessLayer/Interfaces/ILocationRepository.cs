using Store.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Store.BusinessLayer.Response;

namespace Store.BusinessLayer.Interfaces
{
    public interface ILocationRepository
    {
        Task<IResult<List<LocationViewModel>>> GetAll();
        Task<IResult<List<LocationViewModel>>> GetAll(LocationViewModel locationViewModel);
        Task<IResult<int>> Insert(LocationViewModel locationViewModel);
    }
}
