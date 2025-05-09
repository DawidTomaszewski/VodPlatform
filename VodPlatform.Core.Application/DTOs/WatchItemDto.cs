using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodPlatform.Core.Application.DTOs
{
    public class WatchItemDto
    {
        public int Id { get; set; }
        public int WatchlistId { get; set; }
        public int? MovieId { get; set; }
        public int? SeriesGroupId { get; set; }
    }
}
