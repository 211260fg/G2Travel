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
    public class ListItemController : TableController<ListItem>
    {
        public IServiceTokenHandler handler { get; set; }
        private G10TravelContext context;
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            context = new G10TravelContext();
            DomainManager = new EntityDomainManager<ListItem>(context, Request, Services);
        }

        // GET tables/ListItem
        public IQueryable<ListItem> GetAllListItems()
        {
            var currentUser = User as ServiceUser;
            return Query().Where(list => list.UserId == currentUser.Id);
        }
        // GET tables/ListItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<ListItem> Get(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/ListItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<ListItem> PatchListItem(string id, Delta<ListItem> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/ListItem
        public async Task<IHttpActionResult> PostListItem(ListItemRequest item)
        {
            var currentUser = User as ServiceUser;
            ListItem newItem = new ListItem();
            newItem.Name = item.name;
            newItem.Location = item.location;
            newItem.startDate = item.startdate;
            newItem.endDate = item.enddate;
            newItem.itemsToBring = item.itemstobring;
            newItem.UserId = currentUser.Id;
            ListItem current = await InsertAsync(newItem);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/ListItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteListItem(string id)
        {
            return DeleteAsync(id);
        }


        /*[HttpDelete, Route("api/ListItem/DeleteListItem")]
        public IQueryable<ListItem> getItemsForList(string listId)
        {
            DeleteTodoItem(listId);
            return null;
        }*/

    }
}
