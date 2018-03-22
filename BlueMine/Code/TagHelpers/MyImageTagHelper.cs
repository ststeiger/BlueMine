
// https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/authoring
// https://www.davepaquette.com/archive/2015/05/06/link-and-script-tag-helpers-in-mvc6.aspx

// https://stackoverflow.com/questions/42039269/create-custom-html-helper-in-asp-net-core
namespace BlueMine.TagHelpers
{

    // Test cases - with and without virt-dir 
    // <img src = "images/Waterfall.png?server=123&img=97#tab=preview" width="200" alt="Waterfall" /><br />
    // <img src = "~/images/Waterfall.png?server=123&img=97#tab=preview" width="300" alt="RootWaterfall" /><br />
    // <img src = "http://localhost:55337/images/Waterfall.png?server=123&img=97&no_cache=1518020623#tab=preview" width="400" alt="Not Waterfall" /><br />
    // <img width = "500" alt="Not Waterfall" /><br />

    
    // [HtmlTargetElement(Attributes = "bold")]
    //[HtmlTargetElement(tag : "image")]
    [Microsoft.AspNetCore.Razor.TagHelpers.HtmlTargetElement(tag: "img", Attributes = "src")]
    public class MyImageTagHelper 
        : Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper
    {

        private Microsoft.AspNetCore.Mvc.TagHelpers.Internal.FileVersionProvider m_fileVersionProvider;
        protected Microsoft.AspNetCore.Hosting.IHostingEnvironment HostingEnvironment { get; }
        protected Microsoft.Extensions.Caching.Memory.IMemoryCache Cache { get; }

        //protected Microsoft.AspNetCore.Http.IHttpContextAccessor HttpContext { get; }
        protected Microsoft.AspNetCore.Http.HttpContext HttpContext { get; }
        /// protected Microsoft.AspNetCore.Mvc.Infrastructure.IActionContextAccessor ActionContext { get; }
        protected Microsoft.AspNetCore.Mvc.ActionContext ActionContext { get; }


        // services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        // services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

        public MyImageTagHelper(
          Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
          Microsoft.Extensions.Caching.Memory.IMemoryCache cache,
          Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor,
          Microsoft.AspNetCore.Mvc.Infrastructure.IActionContextAccessor actionContextAccessor,
          // For base constructor 
          System.Text.Encodings.Web.HtmlEncoder htmlEncoder,
          Microsoft.AspNetCore.Mvc.Routing.IUrlHelperFactory urlHelperFactory
            )
            : base(urlHelperFactory, htmlEncoder)
        {
            this.HostingEnvironment = hostingEnvironment;
            this.Cache = cache;
            this.HttpContext = httpContextAccessor.HttpContext;
            this.ActionContext = actionContextAccessor.ActionContext;
        }


        private void EnsureFileVersionProvider()
        {
            if (this.m_fileVersionProvider == null)
            {
                this.m_fileVersionProvider = new Microsoft.AspNetCore.Mvc.TagHelpers.Internal.FileVersionProvider(
                    HostingEnvironment.WebRootFileProvider,
                    Cache,
                    HttpContext.Request.PathBase);
            }
        }

        public void buildurlsz()
        {
            string imageVirtualPath = System.IO.Path.Combine("imageFolder", "blog.UniqueName", "fileName");
            Microsoft.AspNetCore.Http.HttpRequest request = HttpContext.Request;
            System.UriBuilder uriBuilder = new System.UriBuilder
            {
                Host = request.Host.Host,
                Scheme = request.Scheme,
                Path = imageVirtualPath
            };

            if (request.Host.Port.HasValue)
                uriBuilder.Port = request.Host.Port.Value;

            string url = uriBuilder.ToString();
        }


        public class PathMap
        {
            public string Original;

            public string Absolute;
            public string Canonical;
            public string Physical;

            public string QueryString;
            public string Hash;

            public bool PhysicalApplicable;

            public PathMap()
            {
                this.PhysicalApplicable = true;
            }



            // Will not return physical path, only absolute + Canonical 
            public static PathMap FromUrl(Microsoft.AspNetCore.Mvc.IUrlHelper urlHelper, string baseSrc)
            {
                return FromUrl(null, urlHelper, baseSrc);
            }


            public static PathMap FromUrl(Microsoft.AspNetCore.Hosting.IHostingEnvironment env
                , Microsoft.AspNetCore.Mvc.IUrlHelper urlHelper
                , string baseSrc)
            {
                if (baseSrc.StartsWith("data:"))
                    throw new System.ArgumentException("data: cannot be transformed into URL.");

                PathMap pmr = new PathMap();
                pmr.Original = baseSrc;

                int argsPos = baseSrc.IndexOf("?");
                int hashPos = baseSrc.IndexOf("#");

                pmr.QueryString = "";
                pmr.Hash = "";

                if (hashPos != -1)
                {
                    pmr.Hash = baseSrc.Substring(hashPos);
                    baseSrc = baseSrc.Substring(0, hashPos);
                }

                if (argsPos != -1)
                {
                    pmr.QueryString = baseSrc.Substring(argsPos);
                    baseSrc = baseSrc.Substring(0, argsPos);
                }

                string imageVirtualPath = null;


                if (
                        baseSrc.StartsWith("http:", System.StringComparison.InvariantCultureIgnoreCase)
                    || baseSrc.StartsWith("https:", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    pmr.Canonical = baseSrc;

                    string domain = urlHelper.ActionContext.HttpContext.Request.Scheme + "://"
                        + urlHelper.ActionContext.HttpContext.Request.Host.Value;
                    if (baseSrc.StartsWith(domain, System.StringComparison.InvariantCultureIgnoreCase))
                    {
                        baseSrc = baseSrc.Substring(domain.Length);
                    }
                    else
                    {
                        pmr.PhysicalApplicable = false;

                        System.Uri uri = new System.Uri(baseSrc, System.UriKind.Absolute);
                        pmr.Absolute = uri.AbsolutePath;
                    }

                }

                if (baseSrc.StartsWith("~"))
                {
                    imageVirtualPath = urlHelper.Content(baseSrc); // To Virtual Path
                }
                else if (baseSrc.StartsWith("/"))
                {
                    imageVirtualPath = baseSrc;
                }
                else if (
                       !baseSrc.StartsWith("http:", System.StringComparison.InvariantCultureIgnoreCase)
                    && !baseSrc.StartsWith("https:", System.StringComparison.InvariantCultureIgnoreCase)
                    )
                {
                    imageVirtualPath = urlHelper.ActionContext.HttpContext.Request.PathBase.Value;
                    imageVirtualPath += urlHelper.ActionContext.HttpContext.Request.Path.Value;

                    if (!string.IsNullOrEmpty(imageVirtualPath))
                    {
                        if (!imageVirtualPath.EndsWith("/"))
                        {
                            int li = imageVirtualPath.LastIndexOf('/');
                            if (li != -1)
                                imageVirtualPath = imageVirtualPath.Substring(0, li + 1);
                            System.Console.WriteLine(imageVirtualPath);
                        }

                    //if (!string.IsNullOrEmpty(imageVirtualPath))
                    //        imageVirtualPath += "/";
                    }
                    imageVirtualPath += baseSrc;
                }

                pmr.Absolute = imageVirtualPath;

                if (pmr.Canonical == null)
                {
                    pmr.Canonical = urlHelper.ActionContext.HttpContext.Request.Scheme
                        + "://"
                        + urlHelper.ActionContext.HttpContext.Request.Host.Value;

                    pmr.Canonical += pmr.Absolute;

                    // System.Uri bas = new System.Uri(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host.Value, System.UriKind.Absolute);
                    // System.Uri canonicalUrl = new System.Uri(bas, imagePhysicalPath);
                }

                if (pmr.PhysicalApplicable && env != null)
                {
                    string rootPath = urlHelper.Content("~"); // To Virtual Path

                    if (imageVirtualPath.StartsWith(rootPath, System.StringComparison.InvariantCultureIgnoreCase))
                    {
                        pmr.Physical = imageVirtualPath.Substring(rootPath.Length);
                    }
                    else
                        pmr.Physical = imageVirtualPath;

                    if (pmr.Physical.StartsWith('/'))
                        pmr.Physical = pmr.Physical.Substring(1);

                    // https://stackoverflow.com/questions/40001242/aspnetcore-get-path-to-wwwroot-in-taghelper
                    // this.HostingEnvironment.WebRootPath    //  d.h: /wwwroot
                    // this.HostingEnvironment.ContentRootPath // d.h:  /wwwroot/..
                    pmr.Physical = pmr.Physical.Replace('/', System.IO.Path.DirectorySeparatorChar);
                    pmr.Physical = System.IO.Path.Combine(env.WebRootPath, pmr.Physical);
                    pmr.Physical = System.IO.Path.GetFullPath(pmr.Physical);
                }

                return pmr;
            }

        }



        // https://stackoverflow.com/questions/40001242/aspnetcore-get-path-to-wwwroot-in-taghelper
        public override void Process(Microsoft.AspNetCore.Razor.TagHelpers.TagHelperContext context
            , Microsoft.AspNetCore.Razor.TagHelpers.TagHelperOutput output)
        {
            EnsureFileVersionProvider();
            

            Microsoft.AspNetCore.Mvc.IUrlHelper urlHelper =
                base.UrlHelperFactory.GetUrlHelper(this.ActionContext);

            // string myUrl = urlHelper.Content("~/somefilebelowwwwroot");

            

            // string imgPath = output.Attributes["src"].Value as string;
            // Microsoft.AspNetCore.Html.HtmlString hs = output.Attributes["src"].Value as Microsoft.AspNetCore.Html.HtmlString;
            // string imgPath = hs.Value;

            // Microsoft.AspNetCore.Html.HtmlString hs = context.AllAttributes["src"].Value as Microsoft.AspNetCore.Html.HtmlString;
            // string imgPath = hs.Value;

            // var rcc = new Microsoft.AspNetCore.Mvc.Routing.UrlRouteContext();
            // var rc = new Microsoft.AspNetCore.Routing.RouteContext(this.HttpContext);

            // lol = urlHelper.RouteUrl(rcc);



            string baseSrc = context.AllAttributes["src"].Value.ToString();

            if (baseSrc.StartsWith("data:"))
                return;

            PathMap pm = PathMap.FromUrl(this.HostingEnvironment, urlHelper, baseSrc);
            
            System.IO.FileInfo fi = new System.IO.FileInfo(pm.Physical);

            string unixTicks = fi.Exists ? fi.LastWriteTimeUtc.ToUnixTicksString()
                : System.DateTime.UtcNow.ToUnixTicksString();
            
            if(string.IsNullOrEmpty(pm.QueryString))
                baseSrc = $"{pm.Absolute}?no_cache={unixTicks}{pm.Hash}";
            else 
                baseSrc = $"{pm.Absolute}{pm.QueryString}&no_cache={unixTicks}{pm.Hash}";
            
            // No not have it XML-Attribute-encoded
            //output.Attributes.SetAttribute("src", new Microsoft.AspNetCore.Html.HtmlString(imgPath));
            output.Attributes.SetAttribute("src", baseSrc);

            //output.Attributes.SetAttribute("src", src + "?&v=123");            
            // output.Attributes.SetAttribute(SrcAttributeName, _fileVersionProvider.AddFileVersionToPath(Src));
        }


    }


}