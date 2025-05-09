using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodPlatform.Core.Application.DTOs
{
    public class EpisodeDto
    {
        public int Id { get; set; }
        public int SeriesGroupId { get; set; }
        public int EpisodeNumber { get; private set; }
        public int SeasonNumber { get; private set; }
        public int Duration { get; set; }
    }
}
