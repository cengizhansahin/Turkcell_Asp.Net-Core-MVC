﻿using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MyAspNetCoreApp.Web.TagHelpers
{
    [HtmlTargetElement("thumbnail")]
    public class ImageThumbnailTagHelper : TagHelper
    {
        public string ImageSrc { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            string fileName = ImageSrc.Split(".")[0];
            string fileExtension = Path.GetExtension(ImageSrc);
            output.Attributes.SetAttribute("src", $"{fileName}-100x100{fileExtension}");
        }
    }
}
