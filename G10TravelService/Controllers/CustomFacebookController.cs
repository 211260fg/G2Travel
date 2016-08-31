using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using G10TravelService.Models;
using G10TravelService.DataObjects;
using G10TravelService.Requests;
using G10TravelService.Providers;
using System.Security.Claims;

namespace G10TravelService.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.Anonymous)]
    public class CustomFacebookController : ApiController
    {
        public ApiServices Services { get; set; }
        public IServiceTokenHandler handler { get; set; }

        // POST api/CustomFacebook
        public HttpResponseMessage Post(LoginFacebookRequest request)
        {
            G10TravelContext context = new G10TravelContext();
            Account account = context.Accounts.Where(a => a.Username == request.name).SingleOrDefault();
            if (account != null)
            {
                ClaimsIdentity claimsIdentity = new ClaimsIdentity();
                claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, account.Username));
                LoginResult loginResult = new CustomLoginProvider(handler)
                        .CreateLoginResult(claimsIdentity, Services.Settings.MasterKey);
                    var customLoginResult = new CustomLoginResult()
                    {
                        UserId = loginResult.User.UserId,
                        MobileServiceAuthenticationToken = loginResult.AuthenticationToken
                    };
                    return this.Request.CreateResponse(HttpStatusCode.OK, customLoginResult);
                
            }
            else
            {
                Account newAccount = new Account
                {
                    Id = request.id,
                    Username = request.name
                };
                context.Accounts.Add(newAccount);
                context.SaveChanges();
                ClaimsIdentity claimsIdentity = new ClaimsIdentity();
                claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, newAccount.Username));
                LoginResult loginResult = new CustomLoginProvider(handler)
                        .CreateLoginResult(claimsIdentity, Services.Settings.MasterKey);
                var customLoginResult = new CustomLoginResult()
                {
                    UserId = loginResult.User.UserId,
                    MobileServiceAuthenticationToken = loginResult.AuthenticationToken
                };
                return this.Request.CreateResponse(HttpStatusCode.OK, customLoginResult);
            }
        }

    }
}
