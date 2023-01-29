using Microsoft.AspNetCore.Mvc;
using WebAplicationMentum.Business;
using WebAplicationMentum.Models;

namespace WebAplicationMentum.Controllers
{
    public class CustomersController : Controller
    {
        [HttpGet]
        [ActionName("GetList")]
        public List<CustomersModel> GetList()
        {
            CustomerSelectAll customerSelectAll = new CustomerSelectAll();
            return customerSelectAll.Process();
        }

        [HttpGet]
        public ActionResult<CustomersModel> GetById(int id)
        {
            CustomerSelectById customerSelectById = new CustomerSelectById(id);
            var customer = customerSelectById.Process();
            return customer;
        }


        [HttpPost]
        public int Create(CustomersModel customersModel)
        {
            CustomerInsert customerInsert = new CustomerInsert(customersModel);
            return customerInsert.Process();
        }

        [HttpPut]
        public int Update(CustomersModel customersModel)
        {
            CustomerUpdate customerUpdate = new CustomerUpdate(customersModel);
            return customerUpdate.Process();
        }
    }
}
