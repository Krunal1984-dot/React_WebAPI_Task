
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Store.BusinessLayer.Interfaces;
using Store.Controllers;
using Store.DataModel;
using Store.ViewModel;
using static Store.BusinessLayer.Response;

namespace StoreUnitTest
{
    public class StoreTest
    {
        private readonly Mock<ILocationRepository> mock;
        public StoreTest()
        {
            mock = new Mock<ILocationRepository>();
        }

        [Fact]
        public async void GetLocationData()
        {
            mock.Setup(p => p.GetAll()).Returns(GetLocations);
            var controller = new LocationController(mock.Object);
            // Act
            var result = await controller.GetAllLocations();
            // Assert
            var viewResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<Result<List<LocationViewModel>>>(
                viewResult.Value);
            var dataModel = model.Data.FirstOrDefault(p=>p.LocationEndTime == "15:00" && p.LocationStartTime == "5:00");
            Assert.NotNull(dataModel);
        }
        [Fact]
        public async void GetLocationByTime()
        {
            var location = new LocationViewModel()
            {
                LocationStartTime = "12:00",
                LocationEndTime = "21:00"
            };
            mock.Setup(p => p.GetAll(location)).Returns(GetLocationsbyTime);
            var controller = new LocationController(mock.Object);
            // Act
            var result = await controller.GetAllLocations(location);
            // Assert
            var viewResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<Result<List<LocationViewModel>>>(
                viewResult.Value);
            Assert.NotNull(model);
        }
        [Fact]
        public async void InsertLocation()
        {
            var location = new LocationViewModel()
            {
                Id = 7,
                LocationName = "Pharmacy-1",
                LocationStartTime = "7:00",
                LocationEndTime = "17:00"
            };
            mock.Setup(p => p.Insert(location)).Returns(GetLocationsAdd);
            var ltime = new LocationController(mock.Object);
            var result = await ltime.Insert(location);
            var viewResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<Result<int>>(
                viewResult.Value);
            Assert.True(model.Data.Equals(location.Id));
        }
        [Fact]
        public async void UploadFileLocation()
        {
            string validCsvFilePath = "../../../Upload/LocationData.csv";
            mock.Setup(p => p.GetAll()).Returns(GetLocations);
            var fileUploader = new LocationController(mock.Object);
            IFormFile csvFile = ConvertFilePathToIFormFile(validCsvFilePath);
            // Act
            var result = fileUploader.UploadCSV(csvFile);
            var viewResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<Result<string>>(
                viewResult.Value);
            // Assert
            Assert.True(model.Success);
        }

        private async Task<IResult<List<LocationViewModel>>> GetLocations()
        {
            var response = new Result<List<LocationViewModel>>();
            response.Data = new List<LocationViewModel>()
            {

                new LocationViewModel()
                {
                    Id = 1,
                    LocationName = "Pharmacy",
                    LocationStartTime="5:00",
                    LocationEndTime= "15:00"
                },
                new LocationViewModel()
                {
                    Id = 2,
                    LocationName = "Bakery",
                    LocationStartTime="9:00",
                    LocationEndTime= "18:00"
                },
                new LocationViewModel()
                {
                    Id = 3,
                    LocationName = "barber",
                    LocationStartTime="12:00",
                    LocationEndTime= "21:00"
                },
                new LocationViewModel()
                {
                    Id = 4,
                    LocationName = "supermarket",
                    LocationStartTime="5:00",
                    LocationEndTime= "15:00"
                }
            }.ToList();
            return response;
        }

        private async Task<IResult<List<LocationViewModel>>> GetLocationsbyTime(LocationViewModel model)
        {
            var response = new Result<List<LocationViewModel>>();
            response.Data = new List<LocationViewModel>()
            {

                new LocationViewModel()
                {
                    Id = 1,
                    LocationName = "Pharmacy",
                    LocationStartTime="5:00",
                    LocationEndTime= "15:00"
                },
                new LocationViewModel()
                {
                    Id = 2,
                    LocationName = "Bakery",
                    LocationStartTime="9:00",
                    LocationEndTime= "18:00"
                },
                new LocationViewModel()
                {
                    Id = 3,
                    LocationName = "barber",
                    LocationStartTime="12:00",
                    LocationEndTime= "21:00"
                },
                new LocationViewModel()
                {
                    Id = 4,
                    LocationName = "supermarket",
                    LocationStartTime="5:00",
                    LocationEndTime= "15:00"
                }
            }.Where(p => p.LocationStartTime == model.LocationStartTime && p.LocationEndTime == model.LocationEndTime).ToList();
            return response;
        }

        private async Task<IResult<int>> GetLocationsAdd()
        {
            var response = new Result<int>();
            response.Data = 7;
            return response;
        }
        private async Task<IResult<int>> GetBadLocationsAdd()
        {
            var response = new Result<int>();
            response.Data = 8;
            return response;
        }

        public IFormFile ConvertFilePathToIFormFile(string filePath)
        {
            // Open the file stream
            FileStream stream = new FileStream(filePath, FileMode.Open);

            // Extract the file name from the path
            string fileName = Path.GetFileName(filePath);

            // Create an IFormFile instance using the file stream
            return new FormFile(stream, 0, stream.Length, null, fileName);
        }
    }
}