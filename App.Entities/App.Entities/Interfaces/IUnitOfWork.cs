
using App.Entities.Models;

namespace Interfaces
{
    public interface IUnitOfWork: IDisposable
	{
		public IRepository<ApplicationUser> ApplicationUsers { get; }
        public IRepository<Company> Companies { get; }
        public IRepository<Owner> Owners { get; }
        public IRepository<Email> Emails { get;}
        public IRepository<Organization> Organizations { get; }
        public IRepository<DailyTransaction> Transactions { get;}
        public IRepository<TransactionItemCategory> TransactionItemCategories { get;}

        Task<int> SaveAsync();

        void Dispose();

	}
}
