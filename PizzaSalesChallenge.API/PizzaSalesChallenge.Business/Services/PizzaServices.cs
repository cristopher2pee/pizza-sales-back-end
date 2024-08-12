using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PizzaSalesChallenge.Business.DTO.Filter;
using PizzaSalesChallenge.Business.DTO.Mapper;
using PizzaSalesChallenge.Business.Services.Interface;
using PizzaSalesChallenge.Core.Constant;
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

        public async Task ImportCSVFile(IFormFile file)
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

                            if(records.Count == Config.MaximumBatchRecord)
                            {
                                await SaveBatchAsync(records);
                                records.Clear();
                            }
                        }

                        if (records.Count > 0)
                        {
                            await SaveBatchAsync(records);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error in importing csv file.", ex);
            }

        }

        private async Task SaveBatchAsync(List<Pizza> records)
        {
            await _unitOfWork._PizzaRepository.AddRageAsync(records.ToArray());
            await _unitOfWork.SaveChangeAsync();
        }

        private async Task<Pizza?> ProcessPizzaRecord(PizzaCSV csv)
        {
            if (string.IsNullOrEmpty(csv.pizza_id)) return null;

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

        public async Task<Pizza?> UpdatePizza(Pizza param)
        {
            try
            {
                var toUpdate = await GetPizzaById(param.Id);
                if(toUpdate is null) return null;

                toUpdate.PizzaCode = param.PizzaCode;
                toUpdate.PizzaTypeId = param.PizzaTypeId;
                toUpdate.Price = param.Price;
                toUpdate.Size = param.Size;

                _unitOfWork._PizzaRepository.Update(param);
                return await _unitOfWork.SaveChangeAsync() > 0 ? toUpdate : null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<Pizza?> CreatePizza(Pizza param)
        {
            try
            {
                var res = await _unitOfWork._PizzaRepository.AddAsync(param);
                return await _unitOfWork.SaveChangeAsync() > 0 ? res : null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

          
        }

        public async Task<bool> DeletePizza(Guid id)
        {
            var toDelete = await GetPizzaById(id);
            if (toDelete is null) return false;

            _unitOfWork._PizzaRepository.Delete(toDelete);

            return await _unitOfWork.SaveChangeAsync() > 0 ? true : false;
        }

        public async Task<Pizza?> GetPizzaById(Guid id)
        {
            var result = await _unitOfWork._PizzaRepository.GetAsync(
                filter: f => f.Id.Equals(id),
                track: false,
                query => query.Include(f=>f.PizzaType)
                );

            return result;
            
        }

        public async Task<(IEnumerable<Pizza> data, int totalRow, int TotalRowPage)> GetAll(BaseFilter filter)
        {
            var res = await _unitOfWork._PizzaRepository.GetAllAsync(
                filter: filter.search != null ? f => f.PizzaCode.Contains(filter.search) : null,
                orderBy : f=>f.OrderBy(f=>f.PizzaCode),
                pageNumber : filter.pageNumber,
                pageSize : filter.pageSize,
                query => query.Include(f=>f.PizzaType)
                );

            return (res.Item1, res.TotalCount, res.pageCount);
        }
    }
}
