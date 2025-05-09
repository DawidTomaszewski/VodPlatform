using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodPlatform.Core.Domain.Exceptions.Aggregates
{
    public class InvalidTitleException : Exception
    {
        public InvalidTitleException() : base("Title cannot be null or empty.") { }
    }

    public class InvalidCategoryException : Exception
    {
        public InvalidCategoryException(string category)
            : base($"'{category}' is not a valid category.") { }
    }

    public class CategoryAlreadyExistsException : Exception
    {
        public CategoryAlreadyExistsException(string category)
            : base($"Category '{category}' already exists.") { }
    }

    public class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException(string category)
            : base($"Category '{category}' not found.") { }
    }
}
