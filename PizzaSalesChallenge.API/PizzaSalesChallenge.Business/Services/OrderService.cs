using CsvHelper;
using Microsoft.AspNetCore.Http;
using PizzaSalesChallenge.Business.DTO.Mapper;
using PizzaSalesChallenge.Business.Services.Interface;
using PizzaSalesChallenge.Core.Entities;
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
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Order?> GetOrderByOrderCode(int code)
        {
            var result = await _unitOfWork._OrderRepository.GetAsync(
                filter: f => f.OrderNo.Equals(code),
                track: false
                );

            return result;
        }

        public async Task ImportCSVFile(IFormFile file)
        {
            try
            {
                var batchRecord = 1000;
                var records = new List<Order>();

                using (var stream = new StreamReader(file.OpenReadStream()))
                {
                    using (var csv = new CsvReader(stream, CultureInfo.InvariantCulture))
                    {
                        csv.Context.RegisterClassMap<OrderRecordMap>();

                        while (csv.Read())
                        {
                            var item = csv.GetRecord<OrderCSV>();
                            var processData = item.ConvertRecordToEntity();

                            //  if (await GetOrderByOrderCode(processData.OrderNo) is null)
                            records.Add(processData);

                            if (records.Count == batchRecord)
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

        private async Task SaveBatchAsync(List<Order> orders)
        {
            await _unitOfWork._OrderRepository.AddRageAsync(orders.ToArray());
            await _unitOfWork.SaveChangeAsync();
        }


    }
}
