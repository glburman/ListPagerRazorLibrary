using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace ListPagerExamples.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public IEnumerable<Blogger> FilterLastName(string search)
        {
            Func<Blogger, bool> filter = 
                b =>b.LastName.ToLower(CultureInfo.CurrentCulture)
                .Contains(search.ToLower(CultureInfo.CurrentCulture), StringComparison.CurrentCulture);
            return Set<Blogger>().Where(filter);
        }

        public void LoadData()
        {
            Add(new Blogger() { Id = "1", FirstName = "John", LastName = "Darringer" });
            Add(new Blogger() { Id = "2", FirstName = "Bob", LastName = "Billy" });
            Add(new Blogger() { Id = "3", FirstName = "Sam", LastName = "Hill" });
            Add(new Blogger() { Id = "4", FirstName = "Skip", LastName = "Tracer" });
            Add(new Blogger() { Id = "5", FirstName = "Don", LastName = "Drysdale" });
            Add(new Blogger() { Id = "6", FirstName = "Robbie", LastName = "Alomar" });
            Add(new Blogger() { Id = "7", FirstName = "Rod", LastName = "Fish" });
            Add(new Blogger() { Id = "8", FirstName = "Strong", LastName = "Bow" });
            Add(new Blogger() { Id = "9", FirstName = "Tim", LastName = "Tom" });
            Add(new Blogger() { Id = "10", FirstName = "Lilly", LastName = "Marlaine" });
            Add(new Blogger() { Id = "11", FirstName = "Jane", LastName = "Goodall" });
            Add(new Blogger() { Id = "12", FirstName = "Janet", LastName = "Jackson" });
            Add(new Blogger() { Id = "13", FirstName = "Lucy", LastName = "Arnez" });
            Add(new Blogger() { Id = "14", FirstName = "Jelly", LastName = "Roll" });
            Add(new Blogger() { Id = "15", FirstName = "Lilly", LastName = "Tomlin" });
            Add(new Blogger() { Id = "16", FirstName = "Bo", LastName = "Jangles" });
            Add(new Blogger() { Id = "17", FirstName = "Molly", LastName = "Brown" });
            Add(new Blogger() { Id = "18", FirstName = "Princess", LastName = "Di" });
            Add(new Blogger() { Id = "19", FirstName = "Dolly", LastName = "Parton" });
            Add(new Blogger() { Id = "20", FirstName = "Rank", LastName = "Amateur" });
            Add(new Blogger() { Id = "101", FirstName = "Frank", LastName = "James" });
            Add(new Blogger() { Id = "102", FirstName = "Ace", LastName = "Ventura" });
            Add(new Blogger() { Id = "103", FirstName = "Actual", LastName = "Hill" });
            Add(new Blogger() { Id = "104", FirstName = "Fred", LastName = "Flintstone" });
            Add(new Blogger() { Id = "105", FirstName = "Lolly", LastName = "Pop" });
            Add(new Blogger() { Id = "106", FirstName = "Too", LastName = "Much" });
            Add(new Blogger() { Id = "107", FirstName = "Itsa", LastName = "Laff" });
            Add(new Blogger() { Id = "108", FirstName = "Bubble", LastName = "Gumby" });
            Add(new Blogger() { Id = "109", FirstName = "Ricky", LastName = "Racer" });
            Add(new Blogger() { Id = "1010", FirstName = "Block", LastName = "Hed" });
            Add(new Blogger() { Id = "1011", FirstName = "John", LastName = "John" });
            Add(new Blogger() { Id = "1012", FirstName = "Suzy", LastName = "Queue" });
            Add(new Blogger() { Id = "1013", FirstName = "Lame", LastName = "Duque" });
            Add(new Blogger() { Id = "1014", FirstName = "Strict", LastName = "Mode" });
            Add(new Blogger() { Id = "1015", FirstName = "Depeche", LastName = "Mode" });
            Add(new Blogger() { Id = "1016", FirstName = "Rowen", LastName = "Boat" });
            Add(new Blogger() { Id = "1017", FirstName = "Sin", LastName = "Sitti" });
            Add(new Blogger() { Id = "1018", FirstName = "Data", LastName = "" });
            Add(new Blogger() { Id = "1019", FirstName = "William", LastName = "Shatner" });
            Add(new Blogger() { Id = "1020", FirstName = "Dr.", LastName = "Spock" });
            Add(new Blogger() { Id = "21", FirstName = "John", LastName = "Darringer Jr" });
            Add(new Blogger() { Id = "22", FirstName = "Bob", LastName = "Billy Jr" });
            Add(new Blogger() { Id = "23", FirstName = "Sam", LastName = "Hill Jr" });
            Add(new Blogger() { Id = "24", FirstName = "Skip", LastName = "Tracer Jr" });
            Add(new Blogger() { Id = "25", FirstName = "Don", LastName = "Drysdale Jr" });
            Add(new Blogger() { Id = "26", FirstName = "Robbie", LastName = "Alomar Jr" });
            Add(new Blogger() { Id = "27", FirstName = "Rod", LastName = "Fish Jr" });
            Add(new Blogger() { Id = "28", FirstName = "Strong", LastName = "Bow Jr" });
            Add(new Blogger() { Id = "29", FirstName = "Tim", LastName = "Tom Jr" });
            Add(new Blogger() { Id = "210", FirstName = "Lilly", LastName = "Marlaine Jr" });
            Add(new Blogger() { Id = "211", FirstName = "Jane", LastName = "Goodall Jr" });
            Add(new Blogger() { Id = "212", FirstName = "Janet", LastName = "Jackson Jr" });
            Add(new Blogger() { Id = "213", FirstName = "Lucy", LastName = "Arnez Jr" });
            Add(new Blogger() { Id = "214", FirstName = "Jelly", LastName = "Roll Jr" });
            Add(new Blogger() { Id = "215", FirstName = "Lilly", LastName = "Tomlin Jr" });
            Add(new Blogger() { Id = "216", FirstName = "Bo", LastName = "Jangles Jr" });
            Add(new Blogger() { Id = "217", FirstName = "Molly", LastName = "Brown Jr" });
            Add(new Blogger() { Id = "218", FirstName = "Princess", LastName = "Di Jr" });
            Add(new Blogger() { Id = "219", FirstName = "Dolly", LastName = "Parton Jr" });
            Add(new Blogger() { Id = "220", FirstName = "Rank", LastName = "Amateur Jr" });
            Add(new Blogger() { Id = "2101", FirstName = "Frank", LastName = "James Jr" });
            Add(new Blogger() { Id = "2102", FirstName = "Ace", LastName = "Ventura Jr" });
            Add(new Blogger() { Id = "2103", FirstName = "Actual", LastName = "Hill Jr" });
            Add(new Blogger() { Id = "2104", FirstName = "Fred", LastName = "Flintstone Jr" });
            Add(new Blogger() { Id = "2105", FirstName = "Lolly", LastName = "Pop Jr" });
            Add(new Blogger() { Id = "2106", FirstName = "Too", LastName = "Much Jr" });
            Add(new Blogger() { Id = "2107", FirstName = "Itsa", LastName = "Laff Jr" });
            Add(new Blogger() { Id = "2108", FirstName = "Bubble", LastName = "Gumby Jr" });
            Add(new Blogger() { Id = "2109", FirstName = "Ricky", LastName = "Racer Jr" });
            Add(new Blogger() { Id = "21010", FirstName = "Block", LastName = "Hed Jr" });
            Add(new Blogger() { Id = "21011", FirstName = "John", LastName = "John Jr" });
            Add(new Blogger() { Id = "21012", FirstName = "Suzy", LastName = "Queue Jr" });
            Add(new Blogger() { Id = "21013", FirstName = "Lame", LastName = "Duque Jr" });
            Add(new Blogger() { Id = "21014", FirstName = "Strict", LastName = "Mode Jr" });
            Add(new Blogger() { Id = "21015", FirstName = "Depeche", LastName = "Mode Jr" });
            Add(new Blogger() { Id = "21016", FirstName = "Rowen", LastName = "Boat Jr" });
            Add(new Blogger() { Id = "21017", FirstName = "Sin", LastName = "Sitti Jr" });
            Add(new Blogger() { Id = "21018", FirstName = "Data", LastName = "Jr" });
            Add(new Blogger() { Id = "21019", FirstName = "William", LastName = "Shatner Jr" });
            Add(new Blogger() { Id = "21020", FirstName = "Dr.", LastName = "Spock Jr" });


            SaveChangesAsync();
        }
        public DbSet<Blogger> Bloggers { get; set; }
    }
}
