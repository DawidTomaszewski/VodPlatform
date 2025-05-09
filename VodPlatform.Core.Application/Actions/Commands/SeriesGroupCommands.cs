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
    public record AddSeriesGroupCommand(string Title, IEnumerable<Category> categories) : IRequest<int>;
    public record RemoveSeriesGroupCommand(int Id) : IRequest<bool>;
    public record AddCategoryToSeriesGroupCommand(int Id, Category category) : IRequest<bool>;
    public record RemoveCategoryFromSeriesGroupCommand(int Id, Category category) : IRequest<bool>;

    public class AddSeriesGroupCommandHandler : IRequestHandler<AddSeriesGroupCommand, int>
    {
        private readonly ISeriesGroupRepository _repository;

        public AddSeriesGroupCommandHandler(ISeriesGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(AddSeriesGroupCommand request, CancellationToken cancellationToken)
        {
            var group = new SeriesGroup(new Title(request.Title), request.categories);

            await _repository.AddAsync(group);
            await _repository.SaveChangesAsync();

            return group.Id;
        }
    }

    public class RemoveSeriesGroupCommandHandler : IRequestHandler<RemoveSeriesGroupCommand, bool>
    {
        private readonly ISeriesGroupRepository _repository;

        public RemoveSeriesGroupCommandHandler(ISeriesGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(RemoveSeriesGroupCommand request, CancellationToken cancellationToken)
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

    public class AddCategoryToSeriesGroupCommandHandler : IRequestHandler<AddCategoryToSeriesGroupCommand, bool>
    {
        private readonly ISeriesGroupRepository _repository;

        public AddCategoryToSeriesGroupCommandHandler(ISeriesGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(AddCategoryToSeriesGroupCommand request, CancellationToken cancellationToken)
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


    public class RemoveCategoryFromSeriesGroupCommandHandler : IRequestHandler<RemoveCategoryFromSeriesGroupCommand, bool>
    {
        private readonly ISeriesGroupRepository _repository;

        public RemoveCategoryFromSeriesGroupCommandHandler(ISeriesGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(RemoveCategoryFromSeriesGroupCommand request, CancellationToken cancellationToken)
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
