using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;

namespace Laboratorio8.Web.Infrastructure
{
    public static class MvcExtensions
    {
        public static CultureInfo CurrentCulture(this RazorPage page)
        {
            var rqf = page.Context.Features.Get<IRequestCultureFeature>();
            return rqf.RequestCulture.Culture;
        }

        public static bool IsAjaxRequest(this HttpRequest request)
        {
            return request.Headers != null && request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
    }
}
