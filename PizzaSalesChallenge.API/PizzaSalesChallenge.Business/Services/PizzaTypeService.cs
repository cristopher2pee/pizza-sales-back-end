using Microsoft.AspNetCore.Http;
using PizzaSalesChallenge.Business.Services.Interface;
using PizzaSalesChallenge.Core.Model;
using PizzaSalesChallenge.Infrastructure.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using PizzaSalesChallenge.Business.DTO.Mapper;
using PizzaSalesChallenge.Core.Entities;
using PizzaSalesChallenge.Core.Constant;
using PizzaSalesChallenge.Business.DTO.Filter;

namespace PizzaSalesChallenge.Business.Services
{
    public class PizzaTypeService : IPizzaTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PizzaTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ImportCSVFile(IFormFile file)
        {
            try
            {
                var records = new List<PizzaType>();
                using (var stream = new StreamReader(file.OpenReadStream()))
                {
                    using (var csv = new CsvReader(stream, CultureInfo.InvariantCulture))
                    {
                        csv.Context.RegisterClassMap<PizzaTypeRecordMap>();

                        while (csv.Read())
                        {
                            var item = csv.GetRecord<PizzaTypeCSV>();

                            var processData = item.ConvertCSVRecordToPizzaTypeEntity();

                            records.Add(processData);

                            if (records.Count == Config.MaximumBatchRecord)
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

        private async Task SaveBatchAsync(List<PizzaType> records)
        {
            await _unitOfWork._PizzaTypeRepository.AddRageAsync(records.ToArray());
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<PizzaType?> GetPizzaTypeByCode(string code)
        {
            var result = await _unitOfWork._PizzaTypeRepository.GetAsync(
                filter: f => f.PizzaTypeCode.Equals(code),
                track: false
                );

            return result;
        }
        public async Task<PizzaType?> UpdatePizzaType(PizzaType pizzaType)
        {
            try
            {
                var toUpdate = await _unitOfWork._PizzaTypeRepository.GetAsync(
                    filter: f => f.Id.Equals(pizzaType.Id),
                    track: false
                    );

                if (toUpdate is null) return null;

                toUpdate.Ingredients = pizzaType.Ingredients;
                toUpdate.Category = pizzaType.Category;
                toUpdate.Name = pizzaType.Name;

                _unitOfWork._PizzaTypeRepository.Update(toUpdate);
                return await _unitOfWork.SaveChangeAsync() > 0 ? toUpdate : null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in updating data", ex);
            }


        }

        public async Task<PizzaType?> CreatePizzaType(PizzaType pizzaType)
        {
            try
            {
                var result = await _unitOfWork._PizzaTypeRepository.AddAsync(pizzaType);
                return await _unitOfWork.SaveChangeAsync() > 0 ? result : null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in creating pizza type.", ex);
            }
        }

        public async Task<bool> DeletePizzaType(Guid id)
        {
            try
            {
                var toDelete = await _unitOfWork._PizzaTypeRepository.GetAsync(
                    filter: f => f.Id.Equals(id),
                    track: false
                    );

                if (toDelete is null) return false;

                _unitOfWork._PizzaTypeRepository.Delete(toDelete);
                return await _unitOfWork.SaveChangeAsync() > 0 ? true : false;
                
            }
            catch (Exception ex)
            {
                throw new Exception("Error in deleting pizza type.", ex);
            }
        }

        public async Task<PizzaType?> GetPizzaTypeById(Guid id)
        {
            var result =  await _unitOfWork._PizzaTypeRepository.GetAsync(
                    filter: f => f.Id.Equals(id),
                    track: false
                    );

            return result;
        }

        public async Task<(IEnumerable<PizzaType> data, int totalRow, int TotalRowPage)> GetAll(BaseFilter filter)
        {
            var list = await _unitOfWork._PizzaTypeRepository.GetAllAsync(
                filter : filter.search != null ? f=>f.PizzaTypeCode.Contains(filter.search) || 
                    f.Name.Contains(filter.search) : null,

                orderBy : f=>f.OrderBy(f=>f.Name),
                pageNumber : filter.pageNumber,
                pageSize : filter.pageSize
                );

            return (list.Item1, list.TotalCount, list.pageCount);
        }
    }
}
