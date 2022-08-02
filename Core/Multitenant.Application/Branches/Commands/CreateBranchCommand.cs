using MediatR;
using Multitenant.Application.Common.Interfaces;
using Multitenant.Domain.Entities;

namespace Multitenant.Application.Branches.Commands
{
    public class CreateBranchCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        public CreateBranchCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
        {
            var branch = new Branch(request.Name, request.Description);
            _context.Branches.Add(branch);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
