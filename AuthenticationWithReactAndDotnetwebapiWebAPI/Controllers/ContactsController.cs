//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using AuthenticationWithReactAndDotnetwebapiWebAPI.Data;
//using AuthenticationWithReactAndDotnetwebapiWebAPI.Models;

//namespace AuthenticationWithReactAndDotnetwebapiWebAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ContactsController : ControllerBase
//    {
//        private readonly ContactsAPIDbContext dbContext;
//        public ContactsController(ContactsAPIDbContext dbContext)
//        {
//            this.dbContext = dbContext;
//        }
//        [HttpGet]
//        public IActionResult GetContacts()
//        {
//            return Ok(dbContext.Contacts.ToList());
//        }
//        [HttpGet("{id:Guid}")]
//        public async Task<IActionResult> GetContact([FromRoute] Guid id)
//        {
//            var contact = await dbContext.Contacts.FindAsync(id);
//            if (contact != null)
//            {
//                return Ok(contact);
//            }
//            return NotFound();
//        }
//        [HttpDelete("{id:Guid}")]
//        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
//        {
//            var contact = await dbContext.Contacts.FindAsync(id);
//            if (contact != null)
//            {
//                dbContext.Contacts.Remove(contact);
//                await dbContext.SaveChangesAsync();
//                return Ok(contact);
//            }
//            return NotFound();
//        }
//        [HttpPost]
//        public async Task<IActionResult> AddContact(AddContactRequest request)
//        {
//            var contact = new Contact()
//            {
//                Id = Guid.NewGuid(),
//                Address = request.Address,
//                Email = request.Email,
//                Phone = request.Phone,
//                FullName = request.FullName,
//            };
//            await dbContext.Contacts.AddAsync(contact);
//            await dbContext.SaveChangesAsync();

//            return Ok(contact);

//        }

//        [HttpPut]
//        [Route("{id:Guid}")]
//        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactReqquest update)
//        {
//            var contact = await dbContext.Contacts.FindAsync(id);
//            if (contact != null)
//            {
//                contact.FullName = update.FullName;
//                contact.Address = update.Address;
//                contact.Email = update.Email;
//                contact.Phone = update.Phone;

//                await dbContext.SaveChangesAsync();
//                return Ok(contact);
//            }
//            return NotFound();

//        }
//    }
//}
