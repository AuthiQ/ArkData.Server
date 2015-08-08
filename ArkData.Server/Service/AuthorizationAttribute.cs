using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;

namespace ArkData.Server
{
    internal class AuthorizationAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
            if (Program.cfgForm.useAuthentication)
            {
                try { 
                    string token = Convert.ToString(
                      actionContext.Request.Headers.GetValues("Authorization").FirstOrDefault());

                    if (!CheckToken(token.ToLower().Replace("bearer ", "")))
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
                        actionContext.Response.ReasonPhrase = "The specified bearer token seems to be invalid.";
                    }
                } catch(InvalidOperationException IOEx)
                {
                    actionContext.Response =
                  actionContext.Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                    actionContext.Response.ReasonPhrase = "The Authorization header is a required header.";
                }
            }
        }
        private bool CheckToken(string token)
        {
            return new Data.DataContext().XTokens.Any(t => t.Token == token);
        }
    }
}
