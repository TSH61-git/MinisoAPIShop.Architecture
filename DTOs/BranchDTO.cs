using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
      public record BranchDTO(int BranchId, string BranchName, string Address, decimal? Latitude, decimal? Longitude);
}
