#pragma checksum "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\AnalisisRed\FormAnalisisRedGenerar.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "ffea2d675623feb9f3c8c0670271b617634a1668af0c2c9b798b53d504306907"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_AnalisisRed_FormAnalisisRedGenerar), @"mvc.1.0.view", @"/Views/AnalisisRed/FormAnalisisRedGenerar.cshtml")]
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
#nullable restore
#line 4 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\AnalisisRed\FormAnalisisRedGenerar.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\AnalisisRed\FormAnalisisRedGenerar.cshtml"
using Sitio.Utility;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"ffea2d675623feb9f3c8c0670271b617634a1668af0c2c9b798b53d504306907", @"/Views/AnalisisRed/FormAnalisisRedGenerar.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"1867eecf72bc49bbadb2a39887fae6872f0611a2f9b918e03aa97fb8f3cdbdb3", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_AnalisisRed_FormAnalisisRedGenerar : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Sitio.Models.AnalisisRed>
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
#line 9 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\AnalisisRed\FormAnalisisRedGenerar.cshtml"
  
    ViewBag.Title = "Generar Analisis de Red";

    var UsuarioLogueado = Accessor.HttpContext.Session.GetObjectFromJson<Usuario>("UsuarioLog");

    Layout = LayoutForViewHelper.GetLayoutForView(UsuarioLogueado);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<div class=\"row mb-3\">\r\n    <div class=\"col-6\">\r\n        <h2 class=\"font-weight-bold\">Generar Analisis de Red</h2>\r\n    </div>\r\n\r\n</div>\r\n\r\n\r\n\r\n");
#nullable restore
#line 27 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\AnalisisRed\FormAnalisisRedGenerar.cshtml"
 using (Html.BeginForm())
{
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 29 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\AnalisisRed\FormAnalisisRedGenerar.cshtml"
Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"form-horizontal\">\r\n\r\n        <hr />\r\n\r\n        <div class=\"form-group\">\r\n");
            WriteLiteral("            <p class=\"control-label col-md-2 font-weight-bold\">Subred analizada</p>\r\n            \r\n            <div class=\"col-md-10\">\r\n                ");
#nullable restore
#line 40 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\AnalisisRed\FormAnalisisRedGenerar.cshtml"
           Write(Html.EditorFor(model => model.SubredAnalizada, new { htmlAttributes = new { @class = "form-control", type = "text" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                <p class=\"text-muted\">Debe ingresar una Subred que este asignada a una sucursal.</p>\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 46 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\AnalisisRed\FormAnalisisRedGenerar.cshtml"
       Write(Html.LabelFor(model => model.Razon, htmlAttributes: new { @class = "control-label col-md-2 font-weight-bold" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <div class=\"col-md-10\">\r\n                ");
#nullable restore
#line 48 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\AnalisisRed\FormAnalisisRedGenerar.cshtml"
           Write(Html.EditorFor(model => model.Razon, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 53 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\AnalisisRed\FormAnalisisRedGenerar.cshtml"
       Write(Html.LabelFor(model => model.Prioridad, htmlAttributes: new { @class = "control-label col-md-2 font-weight-bold" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n            <div class=\"col-md-10\">\r\n                ");
#nullable restore
#line 56 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\AnalisisRed\FormAnalisisRedGenerar.cshtml"
           Write(Html.DropDownListFor(model => model.Prioridad, (SelectList)ViewBag.Prioridad));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n\r\n        <hr class=\"pb-2 pt-2\" />\r\n\r\n\r\n\r\n");
#nullable restore
#line 64 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\AnalisisRed\FormAnalisisRedGenerar.cshtml"
         if (!string.IsNullOrEmpty(@ViewBag.Mensaje))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"alert alert-danger\" role=\"alert\">\r\n                ");
#nullable restore
#line 67 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\AnalisisRed\FormAnalisisRedGenerar.cshtml"
           Write(ViewBag.Mensaje);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n");
#nullable restore
#line 69 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\AnalisisRed\FormAnalisisRedGenerar.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n        <div class=\"form-group\">\r\n            <div class=\"col-md-offset-2 col-md-10 text-center\">\r\n                <input type=\"submit\" value=\"Generar Analisis de Red\" class=\"btn btn-success\" />\r\n            </div>\r\n        </div>\r\n\r\n    </div>\r\n");
#nullable restore
#line 80 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\AnalisisRed\FormAnalisisRedGenerar.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ffea2d675623feb9f3c8c0670271b617634a1668af0c2c9b798b53d5043069079122", async() => {
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Sitio.Models.AnalisisRed> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
