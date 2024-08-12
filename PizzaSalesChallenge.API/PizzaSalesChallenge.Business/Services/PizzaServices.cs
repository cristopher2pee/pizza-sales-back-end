using CsvHelper;
using Microsoft.AspNetCore.Http;
using PizzaSalesChallenge.Business.DTO.Mapper;
using PizzaSalesChallenge.Business.Services.Interface;
using PizzaSalesChallenge.Core.Entities;
using PizzaSalesChallenge.Core.Enum;
using PizzaSalesChallenge.Core.Model;
using PizzaSalesChallenge.Infrastructure.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Business.Services
{
    public class PizzaServices : IPizzaService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PizzaServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Pizza?> GetPizzaByCode(string code)
        {
            var pizza = await _unitOfWork._PizzaRepository.GetAsync(
                 filter: f => f.PizzaCode.Equals(code),
                 track: false
                 );

            return pizza;
        }

        public async Task<bool> ImportCSVFile(IFormFile file)
        {
            try
            {
                var records = new List<Pizza>();
                using (var stream = new StreamReader(file.OpenReadStream()))
                {
                    using (var csv = new CsvReader(stream, CultureInfo.InvariantCulture))
                    {
                        csv.Context.RegisterClassMap<PizzaRecordMap>();

                        while (csv.Read())
                        {
                            var item = csv.GetRecord<PizzaCSV>();
                            var processData = await ProcessPizzaRecord(item);

                            if (processData != null)
                                records.Add(processData);
                        }

                        if (records.Count > 0)
                        {
                            await _unitOfWork._PizzaRepository.AddRageAsync(records.ToArray());
                            return await _unitOfWork.SaveChangeAsync() > 0 ? true : false;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in importing csv file.", ex);
            }

        }

        private async Task<Pizza?> ProcessPizzaRecord(PizzaCSV csv)
        {
            if (string.IsNullOrEmpty(csv.pizza_id)) return null;

            var pizza = await GetPizzaByCode(csv.pizza_id);
            if (pizza is not null) return null;

            var pizzaType = await _unitOfWork._PizzaTypeRepository.GetAsync(
                filter: f => f.PizzaTypeCode.Equals(csv.pizza_type_id),
                track: false
                );

            if (pizzaType is null) return null;

            var sizeMappings = new Dictionary<string, PizzaSize>(StringComparer.OrdinalIgnoreCase)
                {
                    { "S", PizzaSize.Small },
                    { "M", PizzaSize.Medium },
                    { "L", PizzaSize.Large }
                };

            var size = sizeMappings.TryGetValue(csv.size, out var mappedSize) ? mappedSize : PizzaSize.None;

            return new Pizza
            {
                PizzaCode = csv.pizza_id,
                PizzaTypeId = pizzaType.Id,
                Size = size,
                Price = csv.price
            };
        }
    }
}
