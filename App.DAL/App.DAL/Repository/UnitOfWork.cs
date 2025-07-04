using App.DAL.Data;
using App.Entities.Models;
using DAL;
using Interfaces;

namespace Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;
		public UnitOfWork(ApplicationDbContext _context)
		{
			this._context = _context;
            ApplicationUsers = new Repository<ApplicationUser>(_context);
			Companies = new Repository<Company>(_context);
			Owners = new Repository<Owner>(_context);
			Organizations = new Repository<Organization>(_context);
			Emails = new Repository<Email>(_context);

		}
		public IRepository<ApplicationUser> ApplicationUsers { get; set; }
		public IRepository<Company> Companies { get; set; }
        public IRepository<Owner> Owners { get; set; }
        public IRepository<Email> Emails { get; set; }
        public IRepository<Organization> Organizations{ get; set; }

        public void Dispose()
		{
			_context.Dispose();
		}
		public void ClearChangeTracker()
		{
			_context.ChangeTracker.Clear();
		}
		public int Save()
		{
            return _context.SaveChanges();
        }
	}

}
