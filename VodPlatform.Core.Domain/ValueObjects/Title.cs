using VodPlatform.Core.Domain.Exceptions.ValueObjects;

namespace VodPlatform.Core.Domain.ValueObjects
{
    public class Title
    {
        public string TitleObject { get; private set; }

        public Title() { }

        public Title(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new InvalidTitleException(title);

            TitleObject = title;
        }

        public override string ToString() => TitleObject;

        public static implicit operator string(Title title) => title.TitleObject;
    }
}
