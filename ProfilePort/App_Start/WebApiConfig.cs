using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace ProfilePort
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
               name: "UserApi",
               routeTemplate: "api/v1/Users/{id}",
               defaults: new { controller = "Users" }
           );

            config.Routes.MapHttpRoute(
              name: "ProfileApi",
              routeTemplate: "api/v1/Profiles/{id}",
              defaults: new { controller = "Profile"}
          );

            config.Routes.MapHttpRoute(
              name: "SkillApi",
              routeTemplate: "api/v1/Skill/{id}",
              defaults: new { controller = "Skill", id = RouteParameter.Optional }
          );

            config.Routes.MapHttpRoute(
              name: "ContactInfosApi",
              routeTemplate: "api/v1/ContactInfos/{id}",
              defaults: new { controller = "ContactInfos"}
          );

           config.Routes.MapHttpRoute(
           name: "MessagesApi",
           routeTemplate: "api/v1/Messages/{id}",
           defaults: new { controller = "Messages", id = RouteParameter.Optional }
       );


            config.Routes.MapHttpRoute(
              name: "JobsApi",
              routeTemplate: "api/v1/Jobs/{id}",
              defaults: new { controller = "Jobs" }
          );

            config.Routes.MapHttpRoute(
              name: "NotesApi",
              routeTemplate: "api/v1/Notes/{id}",
              defaults: new { controller = "Notes", id = RouteParameter.Optional }
          );

        }
    }
}
