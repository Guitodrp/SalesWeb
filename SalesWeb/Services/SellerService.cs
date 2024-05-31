using SalesWeb.Data;
using SalesWeb.Models;

namespace SalesWeb.Services
{
    public class SellerService
    {
        private readonly SalesWebContext _context;

        public SellerService(SalesWebContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }
        public void Insert(Seller seller)
        {
            _context.Add(seller);
            _context.SaveChanges();//Confirmar a adição
        }

        public Seller FindById(int id)
        {
            return _context.Seller.FirstOrDefault(f => f.Id == id);
        }

        public void Remove(int id)
        {
            var remove = _context.Seller.Find(id);
            _context.Seller.Remove(remove);
            _context.SaveChanges();
        }
    }
}
