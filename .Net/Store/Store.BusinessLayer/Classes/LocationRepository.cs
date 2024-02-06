using Dapper;
using Microsoft.Extensions.Logging;
using Store.BusinessLayer.Interfaces;
using Store.DataModel;
using Store.Repository;
using Store.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using static Store.BusinessLayer.Response;

namespace Store.BusinessLayer.Classes
{
    public class LocationRepository : BaseRespository<Location>, ILocationRepository
    {
        public LocationRepository(IDbConnection connection, ILogger<Location> logger) : base(connection, logger)
        {

        }

        public async Task<IResult<List<LocationViewModel>>> GetAll()
        {
            var response = new Result<List<LocationViewModel>>();

            try
            {
                string query = "sp_StoreLocation";
                var locations = await repository.GetAllAsync(query, null, commandType: CommandType.StoredProcedure);
                var data = MapperConfig.Instance.Map<List<LocationViewModel>>(locations);
                response.Data = data.Count > 0 ? data : null;
                response.Message = "Result";
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Message = exception.Message;
            }

            return response;
        }

        public async Task<IResult<int>> Insert(LocationViewModel locationViewModel)
        {
            var response = new Result<int>();

            try
            {
                var model = MapperConfig.Instance.Map<Location>(locationViewModel);
                response.Data = await repository.InsertAsync(model);
                response.Message = "Successfully added new record";

            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Message = exception.Message;
            }

            return response;
        }


        public async Task<IResult<List<LocationViewModel>>> GetAll(LocationViewModel model)
        {
            var response = new Result<List<LocationViewModel>>();

            try
            {
                string query = "sp_GetLocationByTime";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("fromTime", model.LocationStartTime);
                parameters.Add("endTime", model.LocationEndTime);
                var locations = await repository.GetAllAsync(query, parameters, commandType: CommandType.StoredProcedure);
                var data = MapperConfig.Instance.Map<List<LocationViewModel>>(locations);
                response.Data = data.Count > 0 ? data : null;
                response.Message = "Result";
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Message = exception.Message;
            }

            return response;
        }
    }
}