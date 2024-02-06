using NRIAwards.Common.Entity;
using NRIAwards.Common.Entity.Include;
using NRIAwards.Common.Entity.Order;
using NRIAwards.Common.Entity.Search;

namespace NRIAwards.DAL.Base.Interface.Crud;

public interface ICrudUsersRepository : IBaseRepository<User, int, UsersSearchParams, UsersOrderParams, UsersIncludeParams>
{

}
