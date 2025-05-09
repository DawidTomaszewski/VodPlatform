using VodPlatform.Core.Domain.Enums;
using VodPlatform.Core.Domain.Exceptions.Aggregates;
using VodPlatform.Core.Domain.ValueObjects;

namespace VodPlatform.Core.Domain.Aggregates
{
    public abstract class ContentGroupBase
    {
        public string TitleObject { get; protected set; }
        public Title Title { get; protected set; }

        public string CategoriesAsString { get; protected set; }

        protected ContentGroupBase() { }

        protected ContentGroupBase(string title, IEnumerable<Category> categories)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new InvalidTitleException();

            TitleObject = title;
            Title = new Title(title);
            CategoriesAsString = string.Join(",", categories.Select(c => c.ToString()));
        }

        public void AddCategory(Category category)
        {
            var currentCategories = CategoriesAsString.Split(',').ToList();
            var categoryStr = category.ToString();

            if (currentCategories.Contains(categoryStr))
                throw new CategoryAlreadyExistsException(categoryStr);

            currentCategories.Add(categoryStr);
            CategoriesAsString = string.Join(",", currentCategories);
        }

        public void RemoveCategory(Category category)
        {
            var currentCategories = CategoriesAsString.Split(',').ToList();
            var categoryStr = category.ToString();

            if (!currentCategories.Contains(categoryStr))
                throw new CategoryNotFoundException(categoryStr);

            currentCategories.Remove(categoryStr);
            CategoriesAsString = string.Join(",", currentCategories);
        }

        public abstract Duration GetTotalDuration();

        public Title GetTitle()
        {
            return new Title(TitleObject);
        }

        public List<Category> GetCategories()
        {
            try
            {
                return CategoriesAsString.Split(',')
                                         .Select(c => Enum.Parse<Category>(c))
                                         .ToList();
            }
            catch (Exception)
            {
                throw new InvalidCategoryException(CategoriesAsString);
            }
        }
    }
}