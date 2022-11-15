using Microsoft.AspNetCore.Mvc;
using WebApplicationApiTest.Models;

namespace WebApplicationApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        [AcceptVerbs("POST"), Route("value")]
        public KeyValueModel Post(KeyValueModel request)
        {
            EmbeddedDb embeddedDb = new EmbeddedDb();
            ConLog.Info("POST received.");
            ConLog.Json(request);
            if (embeddedDb.Get(request.Id) is null)
            {
                if (embeddedDb.Insert(request))
                {
                    ConLog.Success("Successfully entered.");
                    request.Status = "inserted";
                }
                else
                {
                    ConLog.Erro("Database failure.");
                    request.Status = "database failure";
                }
            }
            else
            {
                ConLog.Erro("Already exists.");
                request.Status = "already exists";
            }
            ConLog.Info("Response:");
            ConLog.Json(request);
            return request;
        }

        [AcceptVerbs("GET"), Route("value/{id}")]
        public KeyValueModel Get(int id)
        {
            EmbeddedDb embeddedDb = new EmbeddedDb();
            ConLog.Info("GET received. Id: " + id);
            KeyValueModel response = embeddedDb.Get(id);
            if (response is null)
            {
                response = new KeyValueModel
                {
                    Id = id,
                    Status = "not found"
                };
            }
            else
            {
                response.Status = "found";
            }
            ConLog.Info("Response:");
            ConLog.Json(response);
            return response;
        }


        [AcceptVerbs("PUT"), Route("value")]
        public KeyValueModel Put(KeyValueModel request)
        {
            EmbeddedDb embeddedDb = new EmbeddedDb();
            ConLog.Info("PUT received.");
            ConLog.Json(request);
            if (embeddedDb.Get(request.Id)!=null)
            {
                if (embeddedDb.Update(request))
                {
                    ConLog.Success("Successfully update.");
                    request.Status = "updated";
                }
                else
                {
                    ConLog.Erro("Database failure.");
                    request.Status = "database failure";
                }
            }
            else
            {
                ConLog.Erro("Value not exists.");
                request.Status = "not exists";
            }
            ConLog.Info("Response:");
            ConLog.Json(request);
            return request;
        }


        [AcceptVerbs("DELETE"), Route("value/{Id}")]
        public KeyValueModel Delete(int id)
        {
            EmbeddedDb embeddedDb = new EmbeddedDb();
            ConLog.Info("DELETE received. Id: " + id);
            KeyValueModel response;
            if (embeddedDb.Delete(id))
            {
                response = new KeyValueModel
                {
                    Id = id,
                    Status = "deleted"
                };
            }
            else
            {
                response = new KeyValueModel
                {
                    Id = id,
                    Status = "not found"
                };
            }
            ConLog.Info("Response:");
            ConLog.Json(response);
            return response;
        }
    }
}
