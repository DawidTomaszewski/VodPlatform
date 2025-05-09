using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using VodPlatform.Core.Domain.Aggregates;
using VodPlatform.Core.Domain.Enums;
using VodPlatform.Core.Domain.Repositories;
using VodPlatform.Core.Domain.ValueObjects;

namespace VodPlatform.Core.Application.Actions.Commands
{
    public record AddMovieGroupCommand(string Title, IEnumerable<Category> categories) : IRequest<int>;
    public record RemoveMovieGroupCommand(int Id) : IRequest<bool>;
    public record AddCategoryToMovieGroupCommand(int Id, Category category) : IRequest<bool>;
    public record RemoveCategoryFromMovieGroupCommand(int Id, Category category) : IRequest<bool>;


    public class AddMovieGroupCommandHandler : IRequestHandler<AddMovieGroupCommand, int>
    {
        private readonly IMovieGroupRepository _repository;
         
        public AddMovieGroupCommandHandler(IMovieGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(AddMovieGroupCommand request, CancellationToken cancellationToken)
        {
            var group = new MovieGroup(new Title(request.Title), request.categories);

            await _repository.AddAsync(group);
            await _repository.SaveChangesAsync();

            return group.Id;
        }
    }

    public class RemoveMovieGroupCommandHandler : IRequestHandler<RemoveMovieGroupCommand, bool>
    {
        private readonly IMovieGroupRepository _repository;

        public RemoveMovieGroupCommandHandler(IMovieGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(RemoveMovieGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _repository.GetByIdAsync(request.Id);

            if (group == null)
            {
                return false;
            }

            await _repository.RemoveAsync(group);
            await _repository.SaveChangesAsync();

            return true;
        }
    }

    public class AddCategoryToMovieGroupCommandHandler : IRequestHandler<AddCategoryToMovieGroupCommand, bool>
    {
        private readonly IMovieGroupRepository _repository;

        public AddCategoryToMovieGroupCommandHandler(IMovieGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(AddCategoryToMovieGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _repository.GetByIdAsync(request.Id);

            if (group == null)
            {
                return false;
            }

            group.AddCategory(request.category);

            await _repository.SaveChangesAsync();

            return true;
        }
    }


    public class RemoveCategoryFromMovieGroupCommandHandler : IRequestHandler<RemoveCategoryFromMovieGroupCommand, bool>
    {
        private readonly IMovieGroupRepository _repository;

        public RemoveCategoryFromMovieGroupCommandHandler(IMovieGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(RemoveCategoryFromMovieGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _repository.GetByIdAsync(request.Id);

            if (group == null)
            {
                return false;
            }

            group.RemoveCategory(request.category);

            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
