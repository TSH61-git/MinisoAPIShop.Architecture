using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record OrderCreateDTO
    (
        int UserId,

        decimal OrderSum, 

        List<OrderItemDTO> OrderItems,

        string? Status
    );
    

    public record OrderReadDTO
    
        (int OrderId,

        DateOnly OrderDate,

        decimal? OrderSum,

        int UserId,

        string? Status,

        List<OrderItemDTO> OrderItems);

}
