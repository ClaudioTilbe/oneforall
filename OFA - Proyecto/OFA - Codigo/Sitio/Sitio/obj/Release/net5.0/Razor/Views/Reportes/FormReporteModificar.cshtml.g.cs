#pragma checksum "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Reportes\FormReporteModificar.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5b38b9fbb263a5b72b548e6da058a367107e04cb62518ec0f09017766bc25e02"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Reportes_FormReporteModificar), @"mvc.1.0.view", @"/Views/Reportes/FormReporteModificar.cshtml")]
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
#line 4 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Reportes\FormReporteModificar.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Reportes\FormReporteModificar.cshtml"
using Sitio.Utility;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"5b38b9fbb263a5b72b548e6da058a367107e04cb62518ec0f09017766bc25e02", @"/Views/Reportes/FormReporteModificar.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"1867eecf72bc49bbadb2a39887fae6872f0611a2f9b918e03aa97fb8f3cdbdb3", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Reportes_FormReporteModificar : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Sitio.Models.Reporte>
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
            WriteLiteral(" ");
            WriteLiteral("\r\n\r\n\r\n");
#nullable restore
#line 9 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Reportes\FormReporteModificar.cshtml"
  
    ViewBag.Title = "Modificar Reporte";

    var UsuarioLogueado = Accessor.HttpContext.Session.GetObjectFromJson<Usuario>("UsuarioLog");

    Layout = LayoutForViewHelper.GetLayoutForView(UsuarioLogueado);


#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n<div class=\"row mb-3\">\r\n    <div class=\"col-6\">\r\n        <h2 class=\"font-weight-bold\">Modificar Reporte</h2>\r\n    </div>\r\n\r\n</div>\r\n\r\n\r\n\r\n");
#nullable restore
#line 29 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Reportes\FormReporteModificar.cshtml"
 using (Html.BeginForm())
{
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 31 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Reportes\FormReporteModificar.cshtml"
Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"form-horizontal\">\r\n\r\n        <hr />");
            WriteLiteral("\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 38 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Reportes\FormReporteModificar.cshtml"
       Write(Html.LabelFor(model => model.Codigo, htmlAttributes: new { @class = "control-label col-md-2 font-weight-bold" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <div class=\"col-md-10\">\r\n");
            WriteLiteral("                ");
#nullable restore
#line 41 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Reportes\FormReporteModificar.cshtml"
           Write(Html.DisplayFor(model => model.Codigo, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                ");
#nullable restore
#line 42 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Reportes\FormReporteModificar.cshtml"
           Write(Html.HiddenFor(model => model.Codigo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 47 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Reportes\FormReporteModificar.cshtml"
       Write(Html.LabelFor(model => model.MailOrigen.Correo, htmlAttributes: new { @class = "control-label col-md-2 font-weight-bold" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <div class=\"col-md-10\">\r\n                ");
#nullable restore
#line 49 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Reportes\FormReporteModificar.cshtml"
           Write(Html.DisplayFor(model => model.MailOrigen.Correo, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n\r\n\r\n");
            WriteLiteral("        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 56 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Reportes\FormReporteModificar.cshtml"
       Write(Html.Label(null ,"Dispositivo", htmlAttributes: new { @class = "control-label col-md-2 font-weight-bold" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral("\r\n            <div class=\"col-md-10\">\r\n                ");
#nullable restore
#line 60 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Reportes\FormReporteModificar.cshtml"
           Write(Html.DropDownListFor(model => model.DispositivoAsociado.IP, (SelectList)ViewBag.DispositivosDisp));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 66 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Reportes\FormReporteModificar.cshtml"
       Write(Html.LabelFor(model => model.Asunto, htmlAttributes: new { @class = "control-label col-md-2 font-weight-bold" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <div class=\"col-md-10\">\r\n                ");
#nullable restore
#line 68 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Reportes\FormReporteModificar.cshtml"
           Write(Html.EditorFor(model => model.Asunto, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 73 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Reportes\FormReporteModificar.cshtml"
       Write(Html.LabelFor(model => model.Destino, htmlAttributes: new { @class = "control-label col-md-2 font-weight-bold" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <div class=\"col-md-10\">\r\n                ");
#nullable restore
#line 75 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Reportes\FormReporteModificar.cshtml"
           Write(Html.EditorFor(model => model.Destino, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 80 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Reportes\FormReporteModificar.cshtml"
       Write(Html.LabelFor(model => model.Mensaje, htmlAttributes: new { @class = "control-label col-md-2 font-weight-bold" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <div class=\"col-md-10\">\r\n                ");
#nullable restore
#line 82 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Reportes\FormReporteModificar.cshtml"
           Write(Html.EditorFor(model => model.Mensaje, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n\r\n        <hr />");
            WriteLiteral("\r\n");
#nullable restore
#line 88 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Reportes\FormReporteModificar.cshtml"
         if (!string.IsNullOrEmpty(@ViewBag.Mensaje))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"alert alert-danger\" role=\"alert\">\r\n                ");
#nullable restore
#line 91 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Reportes\FormReporteModificar.cshtml"
           Write(ViewBag.Mensaje);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n");
#nullable restore
#line 93 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Reportes\FormReporteModificar.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n        <div class=\"form-group mt-2\">\r\n            <div class=\"col-md-offset-2 col-md-10 text-center\">\r\n                <input type=\"submit\" value=\"Modificar\" class=\"btn btn-success\" />\r\n            </div>\r\n        </div>\r\n\r\n    </div>\r\n");
#nullable restore
#line 103 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\Reportes\FormReporteModificar.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5b38b9fbb263a5b72b548e6da058a367107e04cb62518ec0f09017766bc25e0211765", async() => {
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
            WriteLiteral("\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Sitio.Models.Reporte> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591