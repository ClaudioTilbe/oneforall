#pragma checksum "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoAgregar.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "e1f0c8130d1f899622dd08daec6b91bd3a5de03b2efdad70c817e1a8d3b26a79"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Dispositivos_FormDispositivoAgregar), @"mvc.1.0.view", @"/Views/Dispositivos/FormDispositivoAgregar.cshtml")]
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
#line 4 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoAgregar.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoAgregar.cshtml"
using Sitio.Utility;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"e1f0c8130d1f899622dd08daec6b91bd3a5de03b2efdad70c817e1a8d3b26a79", @"/Views/Dispositivos/FormDispositivoAgregar.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"1867eecf72bc49bbadb2a39887fae6872f0611a2f9b918e03aa97fb8f3cdbdb3", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Dispositivos_FormDispositivoAgregar : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Sitio.Models.Dispositivo>
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
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
            WriteLiteral(" ");
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 9 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoAgregar.cshtml"
  
    ViewBag.Title = "Agregar Dispositivo";

    var UsuarioLogueado = Accessor.HttpContext.Session.GetObjectFromJson<Usuario>("UsuarioLog");
    var sucursalesAsignadas = Accessor.HttpContext.Session.GetObjectFromJson<List<Sucursal>>("SucursalesDisponibles");

    Layout = LayoutForViewHelper.GetLayoutForView(UsuarioLogueado);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<div class=\"row mb-3\">\r\n    <div class=\"col-6\">\r\n        <h2 class=\"font-weight-bold\">Agregar Dispositivo</h2>\r\n    </div>\r\n\r\n</div>\r\n\r\n\r\n\r\n");
#nullable restore
#line 28 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoAgregar.cshtml"
 using (Html.BeginForm())
{
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 30 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoAgregar.cshtml"
Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"form-horizontal\">\r\n\r\n        <hr />\r\n\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 37 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoAgregar.cshtml"
       Write(Html.LabelFor(model => model.IP, htmlAttributes: new { @class = "control-label col-md-2 font-weight-bold" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <div class=\"col-md-10\">\r\n                ");
#nullable restore
#line 39 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoAgregar.cshtml"
           Write(Html.EditorFor(model => model.IP, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 44 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoAgregar.cshtml"
       Write(Html.LabelFor(model => model.Nombre, htmlAttributes: new { @class = "control-label col-md-2 font-weight-bold" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <div class=\"col-md-10\">\r\n                ");
#nullable restore
#line 46 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoAgregar.cshtml"
           Write(Html.EditorFor(model => model.Nombre, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 51 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoAgregar.cshtml"
       Write(Html.LabelFor(model => model.Tipo, htmlAttributes: new { @class = "control-label col-md-2 font-weight-bold" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <div class=\"col-md-10\">\r\n                ");
#nullable restore
#line 53 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoAgregar.cshtml"
           Write(Html.EditorFor(model => model.Tipo, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n\r\n");
            WriteLiteral("\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 65 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoAgregar.cshtml"
       Write(Html.LabelFor(model => model.Accesible, htmlAttributes: new { @class = "control-label col-md-2 font-weight-bold" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <div class=\"col-md-10\">\r\n                ");
#nullable restore
#line 67 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoAgregar.cshtml"
           Write(Html.EditorFor(model => model.Accesible, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n\r\n\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 73 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoAgregar.cshtml"
       Write(Html.Label(null ,"Sucursales disponibles", htmlAttributes: new { @class = "control-label col-md-2 font-weight-bold" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n            <div class=\"col-md-10\">\r\n\r\n                ");
#nullable restore
#line 77 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoAgregar.cshtml"
           Write(Html.DropDownListFor(model => model.UbSucursal.NroSucursal, (SelectList)ViewBag.SucursalesDisp));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n            </div>\r\n        </div>\r\n\r\n\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 84 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoAgregar.cshtml"
       Write(Html.LabelFor(model => model.Sector, htmlAttributes: new { @class = "control-label col-md-2 font-weight-bold" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <div class=\"col-md-10\">\r\n                ");
#nullable restore
#line 86 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoAgregar.cshtml"
           Write(Html.EditorFor(model => model.Sector, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n\r\n");
            WriteLiteral("\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 98 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoAgregar.cshtml"
       Write(Html.LabelFor(model => model.Prioridad, htmlAttributes: new { @class = "control-label col-md-2 font-weight-bold" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n            <div class=\"col-md-10\">\r\n                ");
#nullable restore
#line 101 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoAgregar.cshtml"
           Write(Html.DropDownListFor(model => model.Prioridad, (SelectList)ViewBag.Prioridad));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 106 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoAgregar.cshtml"
       Write(Html.LabelFor(model => model.Permanencia, htmlAttributes: new { @class = "control-label col-md-2 font-weight-bold" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <div class=\"col-md-10\">\r\n                ");
#nullable restore
#line 108 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoAgregar.cshtml"
           Write(Html.EditorFor(model => model.Permanencia, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n\r\n");
            WriteLiteral("\r\n        <hr class=\"pb-2 pt-2\" />\r\n\r\n");
#nullable restore
#line 128 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoAgregar.cshtml"
         if (!string.IsNullOrEmpty(@ViewBag.Mensaje))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"alert alert-danger\" role=\"alert\">\r\n                ");
#nullable restore
#line 131 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoAgregar.cshtml"
           Write(ViewBag.Mensaje);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n");
#nullable restore
#line 133 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoAgregar.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n        <div class=\"form-group\">\r\n            <div class=\"col-md-offset-2 col-md-10 text-center\">\r\n                <input type=\"submit\" value=\"Agregar\" class=\"btn btn-success\" />\r\n            </div>\r\n        </div>\r\n\r\n    </div>\r\n");
#nullable restore
#line 143 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Dispositivos\FormDispositivoAgregar.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e1f0c8130d1f899622dd08daec6b91bd3a5de03b2efdad70c817e1a8d3b26a7913375", async() => {
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