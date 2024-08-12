﻿using PizzaSalesChallenge.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Business.DTO.Request
{
    public class PizzaTypeRequest : BaseDto
    {
        public string PizzaTypeCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public PizzaCategoryType Category { get; set; }
        public string Ingredients { get; set; } = string.Empty;
    }
}
