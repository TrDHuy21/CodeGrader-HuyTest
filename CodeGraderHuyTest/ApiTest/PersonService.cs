 namespace ApiTest
{
    public class PersonService
    {
        public PersonRepo _personRepo;

        public PersonService(PersonRepo personRepo)
        {
            _personRepo = personRepo;
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            var people = await _personRepo.GetAsync();
            return people;
        }

        public async Task Add()
        {
            Person p = new Person()
            {
                Name = "a",
                Age = 18
            };
            await _personRepo.AddAsync(p);
            await _personRepo.SaveChanges();
        }

    }
}
