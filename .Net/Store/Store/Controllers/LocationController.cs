using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using Store.BusinessLayer.Interfaces;
using Store.Extentions;
using Store.ViewModel;
using System.Data;
using System.Reflection;
using static Store.BusinessLayer.Response;

namespace Store.Controllers
{
    [Route("api/store/locations")]
    public class LocationController : Controller
    {
        private readonly ILocationRepository locationRepository;

        public LocationController(ILocationRepository locationRepository)
        {
            this.locationRepository = locationRepository ?? throw new ArgumentNullException(nameof(locationRepository));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLocations()
        {
            var locations = await locationRepository.GetAll();
            return locations.ToHttpResult();
        }

        [HttpPost("getLocationByTime")]
        public async Task<IActionResult> GetAllLocations([FromBody] LocationViewModel model)
        {
            var locations = await locationRepository.GetAll(model);
            return locations.ToHttpResult();
        }

        [HttpPost("insert")]
        public async Task<IActionResult> Insert([FromBody] LocationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var location = await locationRepository.Insert(model);
            return location.ToHttpResult(StatusCodes.Status201Created);
        }

        [HttpPost("uploadCSV")]
        public async Task<IActionResult> UploadCSV(IFormFile file)
        {
            var result = new Result<string>();
            try
            {
                DataSet dsexcelRecords = new DataSet();

                IExcelDataReader reader = null;
                string fileName = string.Empty;
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    fileName = Convert.ToString(file.FileName).ToLower();

                    if (fileName.EndsWith(".csv"))
                    {
                        reader = ExcelReaderFactory.CreateCsvReader(stream);
                        dsexcelRecords = reader.AsDataSet();
                    }
                    if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
                    {
                        DataTable LocationWorkSheet = dsexcelRecords.Tables[0];


                        LocationWorkSheet.Rows.RemoveAt(0);

                        for (int i = 0; i < LocationWorkSheet.Rows.Count; i++)
                        {
                            if (Convert.ToString(LocationWorkSheet.Rows[i][0]) == "")
                            {
                                result.Message = "Location Name should not be blank";
                                result.Success = false;
                                result.Data = "Continue";
                                return result.ToHttpResult(StatusCodes.Status500InternalServerError);
                            }
                            if (Convert.ToString(LocationWorkSheet.Rows[i][1]) == "")
                            {
                                result.Message = "Location start time should not be blank";
                                result.Success = false;
                                result.Data = "Continue";
                                return result.ToHttpResult(StatusCodes.Status500InternalServerError);
                            }
                            if (Convert.ToString(LocationWorkSheet.Rows[i][2]) == "")
                            {
                                result.Message = "Location end time should not be blank";
                                result.Success = false;
                                result.Data = "Continue";
                                return result.ToHttpResult(StatusCodes.Status500InternalServerError);
                            }

                            var model = new LocationViewModel
                            {
                                LocationName = Convert.ToString(LocationWorkSheet.Rows[i][0]),
                                LocationStartTime = Convert.ToString(LocationWorkSheet.Rows[i][1]),
                                LocationEndTime = Convert.ToString(LocationWorkSheet.Rows[i][2])
                            };

                            await locationRepository.Insert(model);
                        }
                    }
                }

                result.Message = "Location data is imported successfully";
                result.Success = true;
                result.Data = "Continue";
                return result.ToHttpResult(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                result.Data = "Error";
                return result.ToHttpResult(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
