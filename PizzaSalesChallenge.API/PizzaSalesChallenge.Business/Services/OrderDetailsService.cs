using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PizzaSalesChallenge.Business.DTO.Filter;
using PizzaSalesChallenge.Business.DTO.Mapper;
using PizzaSalesChallenge.Business.Services.Interface;
using PizzaSalesChallenge.Core.Constant;
using PizzaSalesChallenge.Core.Entities;
using PizzaSalesChallenge.Core.Model;
using PizzaSalesChallenge.Infrastructure.DataAccess;
using PizzaSalesChallenge.Infrastructure.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Business.Services
{
    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderService _orderService;
        private readonly IPizzaService _pizzaService;

        public OrderDetailsService(IUnitOfWork unitOfWork,
            IOrderService orderService,
            IPizzaService pizzaService
            )
        {
            _unitOfWork = unitOfWork;
            _orderService = orderService;
            _pizzaService = pizzaService;
        }
        public async Task ImportCSVFile(IFormFile file)
        {
            try
            {
                var records = new List<OrderDetails>();
                using (var stream = new StreamReader(file.OpenReadStream()))
                {
                    using (var csv = new CsvReader(stream, CultureInfo.InvariantCulture))
                    {
                        csv.Context.RegisterClassMap<OrderDetailsRecordMap>();

                        while (csv.Read())
                        {
                            var item = csv.GetRecord<OrderDetailsCSV>();
                            var processData = await ProcessData(item);

                            if (processData is not null)
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

        private async Task SaveBatchAsync(List<OrderDetails> orders)
        {
            await _unitOfWork._OrderDetailsRepository.AddRageAsync(orders.ToArray());
            await _unitOfWork.SaveChangeAsync();
        }

        private async Task<OrderDetails?> ProcessData(OrderDetailsCSV csv)
        {
            var order = await _orderService.GetOrderByOrderCode(csv.order_id);
            var pizza = await _pizzaService.GetPizzaByCode(csv.pizza_id);

            if (order is null || pizza is null) return null;

            return new OrderDetails
            {
                OrderId = order.Id,
                PizzaId = pizza.Id,
                Quantity = csv.quantity
            };
        }

        public async Task<OrderDetails?> CreateOrderDetails(OrderDetails param)
        {
            try
            {
                var res = await _unitOfWork._OrderDetailsRepository.AddAsync(param);
                return await _unitOfWork.SaveChangeAsync() > 0 ? res : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<OrderDetails?> UpdateOrderDetails(OrderDetails param)
        {
            try
            {
                var toUpdate = await GetOrderDetailsById(param.Id);
                if (toUpdate is null) return null;

                toUpdate.PizzaId = param.PizzaId;
                toUpdate.Quantity = param.Quantity;
                toUpdate.OrderId = param.OrderId;

                _unitOfWork._OrderDetailsRepository.Update(toUpdate);
                return await _unitOfWork.SaveChangeAsync() > 0 ? toUpdate : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<bool> DeleteOrderDetails(Guid id)
        {
            var toDelete = await GetOrderDetailsById(id);
            if (toDelete is null) return false;

            _unitOfWork._OrderDetailsRepository.Delete(toDelete);
            return await _unitOfWork.SaveChangeAsync() > 0 ? true : false;
        }

        public async Task<OrderDetails?> GetOrderDetailsById(Guid id)
        {
            var res = await _unitOfWork._OrderDetailsRepository.GetAsync(
                filter : f=>f.Id.Equals(id),
                track : false,
                query => query.Include(f=>f.Order)
                    .Include(f=>f.Pizza)
                );

            return res;
        }

        public async Task<(IEnumerable<OrderDetails> data, int totalRow, int TotalRowPage)> GetAll(BaseFilter filter)
        {
            var list = await _unitOfWork._OrderDetailsRepository.GetAllAsync(
                filter : null,
                orderBy : null,
                pageNumber : filter.pageNumber,
                pageSize : filter.pageSize,
                query=>query.Include(f=>f.Order)
                    .Include(f=>f.Pizza)
                );

            return (list.Item1, list.TotalCount, list.pageCount);
        }

    }
}
