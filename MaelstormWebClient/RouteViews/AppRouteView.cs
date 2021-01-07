using MaelstormApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaelstormWebClient.RouteViews
{
    public class AppRouteView:RouteView
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override void Render(RenderTreeBuilder builder)
        {
            ApiClient api = ApiClient.Instance;
            var authorize = Attribute.GetCustomAttribute(RouteData.PageType, typeof(AuthorizeAttribute)) != null;
            if(authorize && !api.Api.IsAuthenticated)
                NavigationManager.NavigateTo("/authentication");
            else
                base.Render(builder);            
        }
    }
}
