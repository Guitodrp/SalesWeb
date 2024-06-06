using Microsoft.EntityFrameworkCore;
using SalesWeb.Data;
using SalesWeb.Models;
using SalesWeb.Services.Exceptions;

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

        public Seller FindById(int id) => _context.Seller.Include(i => i.Department).FirstOrDefault(f => f.Id == id);

        public void Remove(int id)
        {
            var remove = _context.Seller.Find(id);
            _context.Seller.Remove(remove);
            _context.SaveChanges();
        }
        public void Update(Seller obj)
        {
            if (!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e) //Pegando a excessão padrão e trocando pra excessão criada na apk
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
