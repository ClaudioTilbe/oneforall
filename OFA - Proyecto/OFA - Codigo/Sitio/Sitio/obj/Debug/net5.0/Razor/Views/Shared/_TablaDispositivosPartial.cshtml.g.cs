#pragma checksum "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Shared\_TablaDispositivosPartial.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6b7c09a2f7cfa5216000b488d090ac59b40ac0549440561f63538144e19bb076"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__TablaDispositivosPartial), @"mvc.1.0.view", @"/Views/Shared/_TablaDispositivosPartial.cshtml")]
namespace AspNetCore
{
    #line hidden
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\_ViewImports.cshtml"
using Sitio;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\_ViewImports.cshtml"
using Sitio.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"6b7c09a2f7cfa5216000b488d090ac59b40ac0549440561f63538144e19bb076", @"/Views/Shared/_TablaDispositivosPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"1867eecf72bc49bbadb2a39887fae6872f0611a2f9b918e03aa97fb8f3cdbdb3", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared__TablaDispositivosPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Sitio.Models.Dispositivo>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n<div id=\"tablaDispositivosContainer\">\r\n<table class=\"table\">\r\n    <tr class=\"table-header\">\r\n        <th class=\"text-center\">\r\n            ");
#nullable restore
#line 8 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Shared\_TablaDispositivosPartial.cshtml"
       Write(Html.DisplayNameFor(model => model.IP));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </th>\r\n        <th class=\"text-center\">\r\n            ");
#nullable restore
#line 11 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Shared\_TablaDispositivosPartial.cshtml"
       Write(Html.DisplayNameFor(model => model.Nombre));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </th>\r\n        <th class=\"text-center\">\r\n            ");
#nullable restore
#line 14 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Shared\_TablaDispositivosPartial.cshtml"
       Write(Html.DisplayNameFor(model => model.Tipo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </th>\r\n        <th class=\"text-center\">\r\n            ");
#nullable restore
#line 17 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Shared\_TablaDispositivosPartial.cshtml"
       Write(Html.DisplayNameFor(model => model.Conectado));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </th>\r\n        <th class=\"text-center\">\r\n            ");
#nullable restore
#line 20 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Shared\_TablaDispositivosPartial.cshtml"
       Write(Html.DisplayNameFor(model => model.Accesible));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </th>\r\n        <th class=\"text-center\">\r\n            ");
#nullable restore
#line 23 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Shared\_TablaDispositivosPartial.cshtml"
       Write(Html.DisplayNameFor(model => model.Sector));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </th>\r\n        <th class=\"text-center\">\r\n            ");
#nullable restore
#line 26 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Shared\_TablaDispositivosPartial.cshtml"
       Write(Html.DisplayNameFor(model => model.Prioridad));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </th>\r\n\r\n        <th width=\"160px\" class=\"text-center\">\r\n            Acciones\r\n        </th>\r\n    </tr>\r\n\r\n");
#nullable restore
#line 34 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Shared\_TablaDispositivosPartial.cshtml"
     foreach (var item in Model)
    {


#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td class=\"text-center\">\r\n                ");
#nullable restore
#line 39 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Shared\_TablaDispositivosPartial.cshtml"
           Write(Html.DisplayFor(modelItem => item.IP));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td class=\"text-center\">\r\n                ");
#nullable restore
#line 42 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Shared\_TablaDispositivosPartial.cshtml"
           Write(Html.DisplayFor(modelItem => item.Nombre));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td class=\"text-center\">\r\n                ");
#nullable restore
#line 45 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Shared\_TablaDispositivosPartial.cshtml"
           Write(Html.DisplayFor(modelItem => item.Tipo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n\r\n\r\n            <td class=\"text-center\">\r\n");
#nullable restore
#line 50 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Shared\_TablaDispositivosPartial.cshtml"
                 if (item.Conectado)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <span class=\"text-success\">Online</span>\r\n");
#nullable restore
#line 53 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Shared\_TablaDispositivosPartial.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <span class=\"text-danger\">Offline</span>\r\n");
#nullable restore
#line 57 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Shared\_TablaDispositivosPartial.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </td>\r\n\r\n\r\n            <td class=\"text-center\">\r\n");
#nullable restore
#line 62 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Shared\_TablaDispositivosPartial.cshtml"
                 if (item.Accesible)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <span class=\"text-success\">Si</span>\r\n");
#nullable restore
#line 65 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Shared\_TablaDispositivosPartial.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <span class=\"text-danger\">No</span>\r\n");
#nullable restore
#line 69 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Shared\_TablaDispositivosPartial.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </td>\r\n\r\n\r\n            <td class=\"text-center\">\r\n                ");
#nullable restore
#line 74 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Shared\_TablaDispositivosPartial.cshtml"
           Write(Html.DisplayFor(modelItem => item.Sector));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td class=\"text-center\">\r\n                ");
#nullable restore
#line 77 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Shared\_TablaDispositivosPartial.cshtml"
           Write(Html.DisplayFor(modelItem => item.Prioridad));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n\r\n\r\n            <td class=\"text-center\">\r\n\r\n                <a");
            BeginWriteAttribute("href", " href=\"", 2326, "\"", 2402, 1);
#nullable restore
#line 83 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Shared\_TablaDispositivosPartial.cshtml"
WriteAttributeValue("", 2333, Url.Action("FormDispositivoBuscar", "Dispositivos", new { item.IP} ), 2333, 69, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" title=\"Visualizar\" class=\"text-decoration-none\">\r\n                    <img alt=\"Ver\"");
            BeginWriteAttribute("src", " src=\"", 2488, "\"", 2528, 1);
#nullable restore
#line 84 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Shared\_TablaDispositivosPartial.cshtml"
WriteAttributeValue("", 2494, Url.Content("~/Imagenes/ver.png"), 2494, 34, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" width=\"30\" height=\"30\">\r\n                </a>\r\n\r\n");
            WriteLiteral("\r\n                <a");
            BeginWriteAttribute("href", " href=\"", 2888, "\"", 2967, 1);
#nullable restore
#line 91 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Shared\_TablaDispositivosPartial.cshtml"
WriteAttributeValue("", 2895, Url.Action("FormDispositivoModificar", "Dispositivos", new { item.IP} ), 2895, 72, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" title=\"Modificar\" class=\"text-decoration-none\">\r\n                    <img alt=\"Modificar\"");
            BeginWriteAttribute("src", " src=\"", 3058, "\"", 3104, 1);
#nullable restore
#line 92 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Shared\_TablaDispositivosPartial.cshtml"
WriteAttributeValue("", 3064, Url.Content("~/Imagenes/modificar.png"), 3064, 40, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" width=\"30\" height=\"30\">\r\n                </a>\r\n\r\n\r\n                <a");
            BeginWriteAttribute("href", " href=\"", 3175, "\"", 3253, 1);
#nullable restore
#line 96 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Shared\_TablaDispositivosPartial.cshtml"
WriteAttributeValue("", 3182, Url.Action("FormDispositivoEliminar", "Dispositivos", new { item.IP} ), 3182, 71, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" title=\"Eliminar\" class=\"text-decoration-none\">\r\n                    <img alt=\"Eliminar\"");
            BeginWriteAttribute("src", " src=\"", 3342, "\"", 3387, 1);
#nullable restore
#line 97 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Shared\_TablaDispositivosPartial.cshtml"
WriteAttributeValue("", 3348, Url.Content("~/Imagenes/eliminar.png"), 3348, 39, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" width=\"30\" height=\"30\">\r\n                </a>\r\n\r\n            </td>\r\n        </tr>\r\n");
#nullable restore
#line 102 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Shared\_TablaDispositivosPartial.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</table>\r\n</div>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Sitio.Models.Dispositivo>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
