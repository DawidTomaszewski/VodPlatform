using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodPlatform.Core.Application.DTOs
{
    public class WatchedItemDto
    {
        public int Id { get; set; }
        public int WatchedListId { get; private set; }
        public DateOnly WatchedAt { get; set; }
        public int? MovieId { get; set; }
        public int? EpisodeId { get; private set; }
        public int Duration { get; set; }
    }
}
