using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Business.DTO.Response
{
    public record struct PageResponse(object data, int totalRows, int sizePage);
}
