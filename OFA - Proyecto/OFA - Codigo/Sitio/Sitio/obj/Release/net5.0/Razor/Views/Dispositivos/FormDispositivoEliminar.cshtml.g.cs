#pragma checksum "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8f90dee4f9df104965853af9857ad490e14c617bdd3408a1f7a87369cf74aee4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Dispositivos_FormDispositivoEliminar), @"mvc.1.0.view", @"/Views/Dispositivos/FormDispositivoEliminar.cshtml")]
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
#line 1 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\_ViewImports.cshtml"
using Sitio;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\_ViewImports.cshtml"
using Sitio.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml"
using Sitio.Utility;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"8f90dee4f9df104965853af9857ad490e14c617bdd3408a1f7a87369cf74aee4", @"/Views/Dispositivos/FormDispositivoEliminar.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"1867eecf72bc49bbadb2a39887fae6872f0611a2f9b918e03aa97fb8f3cdbdb3", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Dispositivos_FormDispositivoEliminar : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Sitio.Models.Dispositivo>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/estandar.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n");
            WriteLiteral(" ");
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 9 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml"
  
    ViewBag.Title = "Eliminar Dispositivo";

    var UsuarioLogueado = Accessor.HttpContext.Session.GetObjectFromJson<Usuario>("UsuarioLog");

    Layout = LayoutForViewHelper.GetLayoutForView(UsuarioLogueado);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<div class=\"row mb-3\">\r\n    <div class=\"col-6\">\r\n        <h2 class=\"font-weight-bold\">Eliminar Dispositivo</h2>\r\n    </div>\r\n\r\n</div>\r\n\r\n");
#nullable restore
#line 25 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml"
 if (!string.IsNullOrEmpty(@ViewBag.Mensaje))
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"alert alert-danger\" role=\"alert\">\r\n        ");
#nullable restore
#line 28 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml"
   Write(ViewBag.Mensaje);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n");
#nullable restore
#line 30 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div>\r\n\r\n    <hr />");
            WriteLiteral("\r\n\r\n    <div class=\"row mt-4\">\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dt class=\"mb-3\">\r\n                    ");
#nullable restore
#line 42 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml"
               Write(Html.DisplayNameFor(model => model.IP));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n                </dt>\r\n\r\n                <dt class=\"mb-3\">\r\n                    ");
#nullable restore
#line 46 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml"
               Write(Html.DisplayNameFor(model => model.Nombre));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n                </dt>\r\n\r\n                <dt class=\"mb-3\">\r\n                    ");
#nullable restore
#line 50 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml"
               Write(Html.DisplayNameFor(model => model.Tipo));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n                </dt>\r\n\r\n                <dt class=\"mb-3\">\r\n                    ");
#nullable restore
#line 54 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml"
               Write(Html.DisplayNameFor(model => model.Conectado));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n                </dt>\r\n\r\n                <dt class=\"mb-3\">\r\n                    ");
#nullable restore
#line 58 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml"
               Write(Html.DisplayNameFor(model => model.Accesible));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n                </dt>\r\n\r\n                <dt class=\"mb-3\">\r\n                    ");
#nullable restore
#line 62 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml"
               Write(Html.DisplayNameFor(model => model.Sector));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n                </dt>\r\n\r\n                <dt class=\"mb-3\">\r\n                    ");
#nullable restore
#line 66 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml"
               Write(Html.DisplayNameFor(model => model.Prioridad));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n                </dt>\r\n\r\n                <dt class=\"mb-3\">\r\n                    ");
#nullable restore
#line 70 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml"
               Write(Html.DisplayNameFor(model => model.Permanencia));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n                </dt>\r\n\r\n                <dt class=\"mb-3\">\r\n");
            WriteLiteral("                    Ultima conexion:\r\n                </dt>\r\n\r\n                <dt class=\"mb-3\">\r\n");
            WriteLiteral("                    Ultima notificacion:\r\n                </dt>\r\n\r\n            </dl>\r\n        </div>\r\n\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 90 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml"
               Write(Html.DisplayFor(model => model.IP));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 94 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml"
               Write(Html.DisplayFor(model => model.Nombre));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 98 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml"
               Write(Html.DisplayFor(model => model.Tipo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 102 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml"
               Write(Html.DisplayFor(model => model.Conectado));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 106 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml"
               Write(Html.DisplayFor(model => model.Accesible));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 110 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml"
               Write(Html.DisplayFor(model => model.Sector));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 114 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml"
               Write(Html.DisplayFor(model => model.Prioridad));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 118 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml"
               Write(Html.DisplayFor(model => model.Permanencia));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 122 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml"
               Write(Html.DisplayFor(model => model.Ultimaconexion));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 126 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml"
               Write(Html.DisplayFor(model => model.UltimaNotificacion));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n            </dl>\r\n        </div>\r\n    </div>\r\n\r\n    <hr />");
            WriteLiteral("\r\n\r\n    <div class=\"alert alert-danger mt-3 mb-3\">\r\n        ¿Esta seguro de que desea eliminar el Dispositivo?\r\n    </div>\r\n\r\n\r\n");
#nullable restore
#line 141 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml"
     using (Html.BeginForm())
    {
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 143 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml"
   Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"form-group mt-2\">\r\n            <div class=\"col-md-offset-2 col-md-10 text-center\">\r\n                <input type=\"submit\" value=\"Eliminar\" class=\"btn btn-danger\" />\r\n            </div>\r\n        </div>\r\n");
#nullable restore
#line 150 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoEliminar.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>\r\n\r\n\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8f90dee4f9df104965853af9857ad490e14c617bdd3408a1f7a87369cf74aee413843", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IHttpContextAccessor Accessor { get; private set; } = default!;
        #nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Sitio.Models.Dispositivo> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
