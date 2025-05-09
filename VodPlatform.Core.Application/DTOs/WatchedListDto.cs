using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VodPlatform.Core.Domain.Entities;

namespace VodPlatform.Core.Application.DTOs
{
    public class WatchedListDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public ICollection<WatchedItemDto> Items { get; set; }
    }
}
