using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VodPlatform.Core.Domain.Enums;

namespace VodPlatform.Core.Application.DTOs
{
    public class SeriesGroupDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<EpisodeDto> Episodes { get; set; } = new();
        public List<Category> Categories { get; set; }
        public int TotalDuration { get; set; }

    }
}
