using PizzaSalesChallenge.Core.Entities;
using PizzaSalesChallenge.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Business.DTO.Mapper
{
    public static class OrderMapper
    {
        public static Order ConvertRecordToEntity(this OrderCSV record)
        {
            return new Order
            {
                OrderNo = record.order_id,
                DateTimeOrder = Utilities.DateTimeUtilities.ConvertRecordToDateTime(record.date, record.time)
            };
        }
    }
}
