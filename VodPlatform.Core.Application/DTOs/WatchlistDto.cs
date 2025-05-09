using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodPlatform.Core.Application.DTOs
{
    public class WatchlistDto
    {
        public int Id { get; private set; }
        public string UserId { get; private set; }
        public ICollection<WatchItemDto> Items { get; set; }
    }
}
