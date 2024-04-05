using Microsoft.AspNetCore.Mvc.Testing;
using CIoTD.Presentation;
using CIoTD.Security;
using CIoTD.Infrastructure;
using CIoTD.Domain;
using CIoTD.Application;

namespace TestCIoTD
{
    public class DeviceControllerFixture
    {
        public DeviceService DeviceService { get; }
        public DeviceControllerFixture()
        {
            DeviceService = new DeviceService(new DeviceRepository());
        }
    }

    public class DeviceControllerIntegrationTests: IClassFixture<WebApplicationFactory<Startup>>, IClassFixture<DeviceControllerFixture>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly DeviceControllerFixture _fixtureDevice;

        public DeviceControllerIntegrationTests(WebApplicationFactory<Startup> factory, DeviceControllerFixture fixtureDevice)
        {
            _factory = factory;
            _fixtureDevice = fixtureDevice;
        }

        private async Task<string> AuthenticateAsync(HttpClient client)
        {
            var user = UserRepository.Get("username", "password");
            var token = JwtAuthenticationManager.GenerateToken(user);
            return token;
        }

        [Fact]
        public async Task Create_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var token = await AuthenticateAsync(client);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var device = new Devices
            {
                Identifier = "string",
                Description = "string",
                Manufacturer = "PredictWeater",
                Url = "string",
                Commands = new List<Command>
                {
                    new Command
                    {
                        Comand = "get_rainfall_intensity",
                        Parameters = new List<CIoTD.Domain.Parameter>
                        {
                            new CIoTD.Domain.Parameter { Name = "string", Description = "string" }
                        }
                    }
                },
                RainFallIntensities = new List<RainFallIntensity>
                {
                    new RainFallIntensity { DateTime = DateTime.UtcNow, Volumetry = 0 }
                }
            };

            var _device = await _fixtureDevice.DeviceService.Create(device);

            if (_device == null)
            {
                Assert.Fail("Falha ao criar o dispositivo.");
            }

            // Act
            var response = await client.GetAsync($"/device/{_device.Identifier}");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetAll_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var token = await AuthenticateAsync(client);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            // Act
            var response = await client.GetAsync("/device");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetById_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var token = await AuthenticateAsync(client);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            // Act
            string id = "88afc829-777e-4331-b961-c5d517acf5dc";
            var response = await client.GetAsync($"/device/{id}");

            // Assert
            response.EnsureSuccessStatusCode();
        }
        
        [Fact]
        public async Task Update_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var token = await AuthenticateAsync(client);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var device = new Devices
            {
                Identifier = "string",
                Description = "string",
                Manufacturer = "PredictWeater",
                Url = "string",
                Commands = new List<Command>
                {
                    new Command
                    {
                        Comand = "get_rainfall_intensity",
                        Parameters = new List<CIoTD.Domain.Parameter>
                        {
                            new CIoTD.Domain.Parameter { Name = "string", Description = "string" }
                        }
                    }
                },
                RainFallIntensities = new List<RainFallIntensity>
                {
                    new RainFallIntensity { DateTime = DateTime.UtcNow, Volumetry = 0 }
                }
            };

            string id = "9c9edeee-3db4-4840-8b28-0318b9c013aa";
            var _device = await _fixtureDevice.DeviceService.Update(id, device);

            // Act
            var response = await client.GetAsync($"/device/{_device.Identifier}");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Delete_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var token = await AuthenticateAsync(client);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            // Act
            string id = "9c9edeee-3db4-4840-8b28-0318b9c013aa";
            var response = await client.DeleteAsync($"/device/{id}");

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}