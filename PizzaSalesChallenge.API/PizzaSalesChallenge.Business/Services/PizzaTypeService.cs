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

namespace PizzaSalesChallenge.Business.Services
{
    public class PizzaTypeService : IPizzaTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PizzaTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ImportCSVFile(IFormFile file)
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

                            if (await GetPizzaTypeByCode(processData.PizzaTypeCode) is null)
                                records.Add(processData);
                        }

                        if (records.Count > 0)
                        {
                            await _unitOfWork._PizzaTypeRepository.AddRageAsync(records.ToArray());
                            return await _unitOfWork.SaveChangeAsync() > 0 ? true : false;

                        }
                    }
                }

                return false;
            }
            catch(Exception ex)
            {
                throw new Exception("Error in importing csv file.", ex);
            }


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


    }
}
