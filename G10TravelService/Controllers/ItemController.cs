using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using G10TravelService.DataObjects;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using G10TravelService.Models;
using System.Web.Http.Controllers;
using System.Threading.Tasks;
using System.Web.Http.OData;
using G10TravelService.Requests;

namespace G10TravelService.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.User)]
    public class ItemController : TableController<Item>
    {
        public IServiceTokenHandler handler { get; set; }
        private G10TravelContext context;
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            context = new G10TravelContext();
            DomainManager = new EntityDomainManager<Item>(context, Request, Services);
        }

        // GET tables/Item
        public IQueryable<Item> GetAllItems()
        {
            return Query();
        }
        // GET tables/Item/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Item> Get(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Item/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Item> PatchItem(string id, Delta<Item> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/Item
        public async Task<IHttpActionResult> PostItem(Item item)
        {
            Item current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Item/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteItem(string id)
        {
            return DeleteAsync(id);
        }
        
        [HttpGet, Route("api/Item/GetItems")]
        public IQueryable<Item> getItemsForList(string listId)
        {
            return GetAllItems().Where(l => l.ListItemId.Contains(listId)).AsQueryable();
        }

        [HttpPost, Route("api/Item/AmountUp")]
        public SingleResult<Item> amountUp(string itemId)
        {
            Item item = Lookup(itemId).Queryable.First();
            item.Amount++;
            return Lookup(itemId);
        }

        [HttpPost, Route("api/Item/AmountDown")]
        public SingleResult<Item> amountDown(string itemId)
        {
            Item item = Lookup(itemId).Queryable.First();
            item.Amount--;
            return Lookup(itemId);
        }
    }
}
