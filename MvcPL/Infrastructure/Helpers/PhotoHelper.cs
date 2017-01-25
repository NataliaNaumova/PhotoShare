using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Helpers
{
    public static class PhotoViewHelper
    {
        public static MvcHtmlString CreatePhotoPanelBody(this HtmlHelper html, PhotoModel photo)
        {
            TagBuilder div = new TagBuilder("div");
            div.AddCssClass("panel-body");

            var base64 = Convert.ToBase64String(photo.Image);
            var imgSrc = string.Format("data:image/gif;base64,{0}", base64);
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