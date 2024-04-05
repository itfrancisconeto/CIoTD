using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using CIoTD.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CIoTD.Infrastructure
{
    public class DeviceRepository : IRepository<Devices>
    {
        private readonly string _filePath = "devices.json";

        public async Task<List<Devices>> GetAll()
        {
            if (!File.Exists(_filePath))
                return new List<Devices>();

            await using var fileStream = File.OpenRead(_filePath);
            return await JsonSerializer.DeserializeAsync<List<Devices>>(fileStream);
        }

        public async Task<Devices> GetById(string id)
        {
            var devices = await GetAll();
            return devices.Find(device => device.Identifier == id);
        }

        public async Task<Devices> GetByIdCommand(string id, string command)
        {
            RainFallIntensity volumetry = new();
            var devices = await GetAll();
            var index = devices.FindIndex(device => device.Identifier == id);
            var device = devices.Find(device => device.Identifier == id);

            foreach (var _command in device.Commands)
            {
                if (_command.Comand == command)
                {
                    // ToDo: futuramente podee ser incluída a recuperação da volumetria
                    // a partir de um serviço de mensageria preparado para receber dados dos varios
                    // sensores em tópicos utilizando a arquitetura publish / subscriber
                    // Neste exemplo estou simulando o recebimento da volumetria enviada pelo dispositivo
                    // através de uma função que gera números aleatórios
                    volumetry.DateTime = DateTime.Now;
                    volumetry.Volumetry = Math.Round(new Random().Next(0, 1001) + new Random().NextDouble(),2);
                    device.RainFallIntensities.Add(volumetry);
                    break;
                }
            }

            // Persiste os dados da volumetria no armazenamento
            if (index != -1)
            {
                devices[index] = device;
                await SaveData(devices);
            }

            return device;
        }

        public async Task<Devices> Create(Devices entity)
        {
            if (entity.Manufacturer.ToLower() != Constants.AllowedManufacturer.ToLower())
                return null;
            if (entity.Commands[0].Comand.ToLower() != Constants.AllowedCommand.ToLower())
                return null;
            var devices = await GetAll();
            entity.Identifier = Guid.NewGuid().ToString();
            devices.Add(entity);
            await SaveData(devices);
            return entity;
        }

        public async Task<Devices> Update(string id, Devices entity)
        {
            var devices = await GetAll();
            var index = devices.FindIndex(device => device.Identifier == id);
            if (index != -1)
            {
                entity.Identifier = id;
                devices[index] = entity;
                await SaveData(devices);
            }
            return entity;
        }

        public async Task<Devices> Delete(string id)
        {
            var devices = await GetAll();
            var deviceToRemove = devices.Where(device => device.Identifier == id).FirstOrDefault();

            if (deviceToRemove == null)
                return null;

            devices.Remove(deviceToRemove);
            await SaveData(devices);
            return deviceToRemove;
        }

        private async Task SaveData(List<Devices> devices)
        {
            await using var fileStream = File.Create(_filePath);
            await JsonSerializer.SerializeAsync(fileStream, devices);
        }
    }
}
