using SalesWebMvc.Models;
using SalesWebMvc.Models.Enums;

namespace SalesWebMvc.Data {
    public class SeedingService {
        
        private SalesWebMvcContext _context;

        public SeedingService(SalesWebMvcContext context) { 

            _context = context;
        }

        public void Seed() {

            if (_context.Department.Any() ||
                _context.Seller.Any() ||
                _context.SalesRecords.Any()) {

                return; //Db has been seeded || Banco já foi populado

            }

            Department d1 = new Department(1, "Computers");
            Department d2 = new Department(2, "Eletronics");
            Department d3 = new Department(3, "Fashion");
            Department d4 = new Department(4, "Books");

            Seller s1 = new Seller(1, "Kurt Cobain", "kut@gmail.com", new DateTime(1998, 4, 22), 1000.0, d1);
            Seller s2 = new Seller(2, "Layne", "Lany@gmail.com", new DateTime(1998, 4, 22), 1000.0, d2);
            Seller s3 = new Seller(3, "Chris", "Chris@gmail.com", new DateTime(1998, 4, 22), 1000.0, d3);
            Seller s4 = new Seller(4, "Scott", "Scott@gmail.com", new DateTime(1998, 4, 22), 1000.0, d4);

            SalesRecord r1 = new SalesRecord(1, new DateTime(2025, 09, 25), 11000.0, SaleStatus.Billed, s1);
            SalesRecord r2 = new SalesRecord(2, new DateTime(2025, 09, 25), 12000.0, SaleStatus.Billed, s2);

            _context.Department.AddRange(d1,d2,d3,d4);
            _context.Seller.AddRange(s1,s2,s3,s4);
            _context.SalesRecords.AddRange(r1,r2);

            _context.SaveChanges();
        }
    }
}
