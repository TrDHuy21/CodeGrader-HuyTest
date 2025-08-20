using Microsoft.EntityFrameworkCore;

namespace ApiTest
{
    public class PersonRepo
    {
        public PersonContext _context { get; set; }

        public PersonRepo(PersonContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Person>> GetAsync()
        {
            var people = await _context.People.ToListAsync();
            return people;
        }
        public async Task AddAsync(Person person)
        {
            await _context.People.AddAsync(person);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
