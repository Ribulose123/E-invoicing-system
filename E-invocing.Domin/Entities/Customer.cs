using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_invocing.Domin.Entities
{
    public class Customer
    {
        public int Id { get; private set; }
        public string? Name { get; private set; }
        public string? Email { get; private set; }
        public string? Country { get; private set; }

        protected Customer() { }
        public Customer(string? name, string? email, string? country)
        {
            Name = Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email;
            Country = country;
        }
    }
}
