using Laboratorio8.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Laboratorio8.Web.Features.Home
{
    [TypeScriptModule("Home.Server")]
    public class ErrorViewModel
    {
        public string InviaSegnalazioneUrl { get; set; }
        public string RequestId { get; set; }

        public string EmailFrom { get; set; }
        public string[] EmailTo { get; set; }
        public string[] EmailCC { get; set; }
        public string[] EmailCCN { get; set; }
        public string Subject { get; set; }
        public string StandardContent { get; set; }
        public string Content { get; set; }

        public IEnumerable<SelectListItem> EmailOptions { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string ToJson()
        {
            return JsonSerializer.ToJsonCamelCase(this);
        }
    }
}
