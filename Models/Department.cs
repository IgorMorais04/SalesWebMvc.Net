using System.Collections;

namespace SalesWebMvc.Models {
    public class Department {
        public int Id { get; set; }
        public string? Name { get; set; } // Torna a propriedade anulável
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department() { }

        public Department(int id, string? name) {
            Id = id;
            Name = name;
        }

        public void Add(Seller seller) {
            Sellers.Add(seller);
        }

        public void Remove(Seller seller) 
            { Sellers.Remove(seller);
        }

        public double TotalSales(DateTime intial, DateTime final) {
            return Sellers.Sum(seller => seller.TotalSales(intial, final));
        }
    }
}
