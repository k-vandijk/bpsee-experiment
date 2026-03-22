using BPSEE.Experiment.Application.RepositoryInterfaces;
using BPSEE.Experiment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BPSEE.Experiment.Infrastructure.Repositories;

public class UserRepository<TContext> : Repository<User, TContext>, IUserRepository
    where TContext : DbContext
{
    public UserRepository(TContext context) : base(context)
    {
    }
}
