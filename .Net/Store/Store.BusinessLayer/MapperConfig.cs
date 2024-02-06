using AutoMapper;
using Store.DataModel;
using Store.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessLayer
{
    internal class MapperConfig
    {
        internal static IMapper Instance { get; private set; }

        static MapperConfig() => CreateMappings();

        static void CreateMappings()
        {
            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<Location, LocationViewModel>()
                .ForMember(d => d.LocationName, o => o.MapFrom(e => e.Name))
                .ForMember(d => d.LocationStartTime, o => o.MapFrom(e => e.StartTime))
                .ForMember(d => d.LocationEndTime, o => o.MapFrom(e => e.EndTime)).ReverseMap();
            });

            config.AssertConfigurationIsValid();
            Instance = config.CreateMapper();
        }
    }
}
