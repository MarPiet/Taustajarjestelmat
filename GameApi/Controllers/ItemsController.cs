using Microsoft.AspNetCore.Mvc;
using GameApi.models;
using GameApi.processors;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace GameApi.Controllers
{
    [Route("api/players/{playerId}/items")]

    public class ItemsController : Controller
    {
        private ItemsProcessor itemsProcessor;
 

        public ItemsController(ItemsProcessor itemsProcessor)
        {
            this.itemsProcessor = itemsProcessor;
        }
        /* [HttpGet("{id}")]
         public async Task<Item> Get(Guid id)
         {
             return itemsProcessor.GetItem(id);
         }*/

        [HttpGet]
        public async Task<Item[]> GetAll(Guid playerId)
        {
            return itemsProcessor.Get(playerId);
        }


        [HttpPost]
        [LevelFilter]
        public async Task<Item> Create(Guid playerId, NewItem item)
        {
            return itemsProcessor.Create(playerId, item);

        }
        [HttpPut]
        public async Task<Item> Modify(Guid playerId, Guid id, ModifiedItem item)
        {
            return itemsProcessor.Modify(playerId, id, item);
        }



        [HttpDelete]
        public async Task<Item> Delete(Guid playerId, Guid id)
        {
            return itemsProcessor.Delete(playerId, id);
        }



    }
        public class LevelFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is LevelException)
            {
               // context.Result = new NotFoundResult();
                Console.WriteLine(context.Result);
            }
        }
    }

}