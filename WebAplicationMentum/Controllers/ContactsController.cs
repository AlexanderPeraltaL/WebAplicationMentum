using Microsoft.AspNetCore.Mvc;
using WebAplicationMentum.Business;
using WebAplicationMentum.Models;

namespace WebAplicationMentum.Controllers
{
    public class ContactsController : Controller
    {
        [HttpGet]
        [ActionName("GetList")]
        public List<CustomerContactModel> GetList(int id)
        {
            CustomerContactSelectAll customerContactSelectAll = new CustomerContactSelectAll(id);
            return customerContactSelectAll.Process();
        }
        [HttpGet]
        [ActionName("GetbyId")]
        public CustomerContactModel GetbyId(int id)
        {
            CustomerContactSelectById customer = new CustomerContactSelectById(id);
            return customer.Process();
        }
        [HttpPost]
        public int Create(CustomerContactModel customerContactModel)
        {
            CustomerContactInsert customerContactInsert = new CustomerContactInsert(customerContactModel);
            return customerContactInsert.Process();
        }

        [HttpPut]
        public int Update(CustomerContactModel customerContactModel)
        {
            CustomerContactUpdate customerContactUpdate = new CustomerContactUpdate(customerContactModel);
            return customerContactUpdate.Process();
        }

        [HttpDelete]
        public int Delete(int id)
        {
           CustomerContactDelete customerContactDelete  = new CustomerContactDelete(id);    
           return customerContactDelete.Process(); 
        }
    }
}
