using PizzaSalesChallenge.Core.Entities;
using PizzaSalesChallenge.Core.Enum;
using PizzaSalesChallenge.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Business.DTO.Mapper
{
    public static class PizzaTypeMapper
    {
        public static PizzaType ConvertCSVRecordToPizzaTypeEntity(this PizzaTypeCSV record)
        {
            var category = PizzaCategoryType.None;

            switch (record.category.ToLower())
            {
                case "chicken":
                    category = PizzaCategoryType.Chicken;
                    break;
                case "classic":
                    category = PizzaCategoryType.Classic;
                    break;

                case "supreme":
                    category = PizzaCategoryType.Suppreme;
                    break;

                case "veggie":
                    category = PizzaCategoryType.Veggie;
                    break;

            }

            return new PizzaType
            {
                Category = category,
                PizzaTypeCode = record.pizza_type_id,
                Name = record.name,
                Ingredients = record.ingredients,
            };
        }
    }
}
