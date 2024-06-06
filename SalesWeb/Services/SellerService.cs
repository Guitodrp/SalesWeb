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

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }
        public async Task InsertAsync(Seller seller)
        {
            _context.Add(seller);
            await _context.SaveChangesAsync();//Confirmar a adição
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(i => i.Department)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            var remove = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(remove);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e) //Pegando a excessão padrão e trocando pra excessão criada na apk
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
