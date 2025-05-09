using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodPlatform.Core.Domain.Exceptions.Entities
{
    public class WatchItemInvalidTargetException : Exception
    {
        public WatchItemInvalidTargetException()
            : base("A watch item must have either a MovieId or a SeriesGroupId.") { }
    }
}
