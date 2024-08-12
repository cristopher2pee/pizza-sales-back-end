using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PizzaSalesChallenge.Business.DTO.Filter;
using PizzaSalesChallenge.Business.DTO.Mapper;
using PizzaSalesChallenge.Business.DTO.Request;
using PizzaSalesChallenge.Business.Services.Interface;
using PizzaSalesChallenge.Core.Constant;
using PizzaSalesChallenge.Core.Entities;
using PizzaSalesChallenge.Core.Model;
using PizzaSalesChallenge.Infrastructure.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Data;
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

        public async Task<Order?> CreateOrder(OrderRequest order)
        {
            try
            {


                var result = await _unitOfWork._OrderRepository.AddAsync(new Order
                {
                    OrderNo = order.OrderNo,
                    DateTimeOrder = DateTime.Now,

                });

                foreach(var item in order.OrderDetails)
                {
                    await _unitOfWork._OrderDetailsRepository.AddAsync(new OrderDetails
                    {
                        OrderId = result.Id,
                        PizzaId = item.PizzaId,
                        Quantity = item.Quantity
                    });
                }

                return await _unitOfWork.SaveChangeAsync() > 0 ? result : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<bool> isOrderCodeTaken(int code)
        {
            var result = await GetOrderByOrderCode(code);
            return result is null ? false : true;
        }

        public async Task<bool> DeleteOrder(Guid id)
        {
            var toDelete = await GetOrderById(id);

            if (toDelete is null) return false;

            _unitOfWork._OrderRepository.Delete(toDelete);
            return await _unitOfWork.SaveChangeAsync() > 0 ? true : false;
        }

        public async Task<(IEnumerable<Order> data, int totalRow, int TotalRowPage)> GetAll(OrderFilter filter)
        {
            var result = await _unitOfWork._OrderRepository.GetAllAsync(
                filter: filter.OrderNo != 0 ? f => f.OrderNo.Equals(filter.OrderNo) : null,
                orderBy: f => f.OrderBy(f => f.OrderNo),
                pageNumber: filter.pageNumber,
                pageSize: filter.pageSize,
                query => query.Include(f => f.OrderDetails).ThenInclude(f => f.Pizza)
                );


            return (result.Item1, result.TotalCount, result.pageCount);
        }

        public async Task<Order?> GetOrderById(Guid Id)
        {
            var result = await _unitOfWork._OrderRepository.GetAsync(
                filter: f => f.Id.Equals(Id),
                track: false,
                query => query.Include(f => f.OrderDetails)
                    .ThenInclude(f => f.Pizza)
                );

            return result;
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

        public async Task<Order?> UpdateOrder(Order order)
        {
            try
            {
                var toUpdate = await GetOrderById(order.Id);

                if (toUpdate is null) return null;

                toUpdate.OrderNo = order.OrderNo;
                toUpdate.DateTimeOrder = order.DateTimeOrder;

                toUpdate.OrderDetails = order.OrderDetails;

                _unitOfWork._OrderRepository.Update(toUpdate);
                return await _unitOfWork.SaveChangeAsync() > 0 ? toUpdate : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private async Task SaveBatchAsync(List<Order> orders)
        {
            await _unitOfWork._OrderRepository.AddRageAsync(orders.ToArray());
            await _unitOfWork.SaveChangeAsync();
        }


    }
}
