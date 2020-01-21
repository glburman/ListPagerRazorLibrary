using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace ListPagerRazorLibrary
{
    public static class Extensions
    {
        public static void AddListPagerViews(this IServiceCollection services)
        {
            services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
            {
                options.FileProviders.Add(new EmbeddedFileProvider(typeof(ListPagerRazorLibrary.ViewComponents.ListPager).GetTypeInfo().Assembly));
                options.FileProviders.Add(new EmbeddedFileProvider(typeof(ListPagerRazorLibrary.ViewComponents.ListPagerPostForm).GetTypeInfo().Assembly));
                options.FileProviders.Add(new EmbeddedFileProvider(typeof(ListPagerRazorLibrary.ViewComponents.ListPagerArrows).GetTypeInfo().Assembly));
                options.FileProviders.Add(new EmbeddedFileProvider(typeof(ListPagerRazorLibrary.ViewComponents.ListPagerDropdown).GetTypeInfo().Assembly));
                options.FileProviders.Add(new EmbeddedFileProvider(typeof(ListPagerRazorLibrary.ViewComponents.ListPagerPageOf).GetTypeInfo().Assembly));
                options.FileProviders.Add(new EmbeddedFileProvider(typeof(ListPagerRazorLibrary.ViewComponents.ListPagerLinks).GetTypeInfo().Assembly));
                options.FileProviders.Add(new EmbeddedFileProvider(typeof(ListPagerRazorLibrary.ViewComponents.ListPagerPostForm).GetTypeInfo().Assembly));
                options.FileProviders.Add(new EmbeddedFileProvider(typeof(ListPagerRazorLibrary.ViewComponents.ListPagerRecords).GetTypeInfo().Assembly));
            });
        }

        public static void UseListPagerStatics(this IApplicationBuilder builder)
        {
            var scripts = new EmbeddedFileProvider(typeof(ListPagerRazorLibrary.ViewComponents.ListPager)
                .GetTypeInfo().Assembly, "ListPagerRazorLibrary.js");
            builder.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = scripts,
                RequestPath = new PathString("/js")
            });

            var styles = new EmbeddedFileProvider(typeof(ListPagerRazorLibrary.ViewComponents.ListPager)
                .GetTypeInfo().Assembly, "ListPagerRazorLibrary.css");
            builder.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = styles,
                RequestPath = new PathString("/css")
            });
        }
    }
}
