using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Multitenant.Application.Common.Interfaces;
using Multitenant.Application.Common.Mappings;
using Multitenant.Domain.Entities;

namespace Multitenant.Application.Branches.Queries
{
    public class GetBranchesQuery : IRequest<List<AllBranchModel>>
    {
    }

    public class AllBranchModel : IMapFrom<Branch>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class GetBranchesQueryHandler : IRequestHandler<GetBranchesQuery, List<AllBranchModel>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetBranchesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<AllBranchModel>> Handle(GetBranchesQuery request, CancellationToken cancellationToken)
        {
            var branches = await _context.Branches.OrderByDescending(a => a.Id)
                                    .ProjectTo<AllBranchModel>(_mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);

            return branches;
        }
    }
}
