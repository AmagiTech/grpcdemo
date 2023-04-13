using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GRpcServer.Services
{
    public class CountriesService : Countries.CountriesBase
    {
        private readonly ILogger<CustomersService> _logger;
        public CountriesService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        private static IEnumerable<Country> _countries = new List<Country>()
        {
            new Country()
            {
                Id = 1,
                Name = "Turkey",
                Continent = Continents.Europe,
                IsActive = true,
                Established = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(new DateTime(1923,10,23).ToUniversalTime())
            },
            new Country()
            {
                Id = 2,
                Name = "China",
                Continent = Continents.Asia,
                IsActive = false,
                Established = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(new DateTime(1949,10,23).ToUniversalTime())
            },
             new Country()
            {
                Id = 3,
                Name = "Argentina",
                Continent = Continents.SouthAmerica,
                Established = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(new DateTimeOffset(1816,7,9,1,1,1,TimeSpan.Zero))
            },
        };

        public override Task<CountriesReply> GetCountries(Empty request, ServerCallContext context)
        {
            var response = new CountriesReply();
            response.Countries.AddRange(_countries);
            return Task.FromResult(response);
        }

    }
}
