using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record ChangeOrderStatusDto
(
// מי עושה את הפעולה
            string? Status,      // למנהל בלבד
            bool Received
    );
}
