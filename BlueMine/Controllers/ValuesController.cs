
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using Dapper;


namespace BlueMine.Controllers
{


    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        private readonly BlueMine.Db.BlueMineContext m_BlueMineContext;
        private readonly BlueMine.Data.CRUD m_Repo;
        private readonly BlueMine.Data.Dapper.CRUD m_DapperRepo;


        public ValuesController(BlueMine.Db.BlueMineContext dbContext)
        {
            this.m_BlueMineContext = dbContext;
            this.m_Repo = new BlueMine.Data.CRUD(dbContext);
            this.m_DapperRepo = new BlueMine.Data.Dapper.CRUD();
        }


        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }


        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }


        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


    } // End Class ValuesController : Controller 


} // End Namespace BlueMine.Controllers 
