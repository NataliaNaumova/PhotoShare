using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Helpers
{
    public static class PhotoViewHelper
    {
        public static MvcHtmlString CreatePhotoPanelBody(this HtmlHelper html, UrlHelper url, PhotoModel photo)
        {
            TagBuilder div = new TagBuilder("div");
            div.AddCssClass("panel-body");

            var imgSrc = url.Action("GetImage", "Photo", new {photoId = photo.Id});
            TagBuilder img = new TagBuilder("img");
            img.MergeAttributes(new Dictionary<string, string>() { { "alt", "Photo" }, { "src", imgSrc }, { "width", "300" }, { "height", "300" } });
            img.AddCssClass("img-responsive");
            div.InnerHtml += img.ToString();

            div.InnerHtml += new TagBuilder("br").ToString(TagRenderMode.SelfClosing);

            var p1 = new TagBuilder("p");
            var strong = new TagBuilder("strong");
            strong.SetInnerText(photo.Description);
            p1.InnerHtml += strong;
            div.InnerHtml += p1;

            var p2 = new TagBuilder("p");
            p2.SetInnerText(photo.CreationTime.ToLocalTime().ToString());
            div.InnerHtml += p2;

            return new MvcHtmlString(div.ToString());
        }
    }
}