#pragma checksum "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\DiagramasRed\FormDiagramaRedBuscar.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "63f9ba81f78914844065fa2ee89d4d076b2cfa14a76642f7e7020fcf387ec41b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_DiagramasRed_FormDiagramaRedBuscar), @"mvc.1.0.view", @"/Views/DiagramasRed/FormDiagramaRedBuscar.cshtml")]
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
#line 4 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\DiagramasRed\FormDiagramaRedBuscar.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\DiagramasRed\FormDiagramaRedBuscar.cshtml"
using Sitio.Utility;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"63f9ba81f78914844065fa2ee89d4d076b2cfa14a76642f7e7020fcf387ec41b", @"/Views/DiagramasRed/FormDiagramaRedBuscar.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"1867eecf72bc49bbadb2a39887fae6872f0611a2f9b918e03aa97fb8f3cdbdb3", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_DiagramasRed_FormDiagramaRedBuscar : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Sitio.Models.DiagramaRed>
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
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 8 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\DiagramasRed\FormDiagramaRedBuscar.cshtml"
  
    ViewBag.Title = "Visualizar Diagrama de Red";

    var UsuarioLogueado = Accessor.HttpContext.Session.GetObjectFromJson<Usuario>("UsuarioLog");

    Layout = LayoutForViewHelper.GetLayoutForView(UsuarioLogueado);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""row mb-3"">
    <div class=""col-6"">
        <h2 class=""font-weight-bold"">Visualizar Diagrama de Red</h2>
    </div>

</div>

<div>
    <hr />

    <div class=""row mt-4"">
        <div class=""col-3"">
            <dl class=""dl-horizontal"">

                <dt class=""mb-3"">
");
            WriteLiteral("                    Numero sucursal:\r\n                </dt>\r\n\r\n            </dl>\r\n        </div>\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 41 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\DiagramasRed\FormDiagramaRedBuscar.cshtml"
               Write(Html.DisplayFor(model => model._Sucursal.NroSucursal));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n            </dl>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row mt-2\">\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n\r\n                <dt class=\"mb-3\">\r\n                    ");
#nullable restore
#line 54 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\DiagramasRed\FormDiagramaRedBuscar.cshtml"
               Write(Html.DisplayNameFor(model => model.Nombre));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n                </dt>\r\n\r\n            </dl>\r\n        </div>\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 63 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\DiagramasRed\FormDiagramaRedBuscar.cshtml"
               Write(Html.DisplayFor(model => model.Nombre));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n            </dl>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row mt-2\">\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dt class=\"mb-3\">\r\n");
            WriteLiteral("                    Fecha de subida:\r\n                </dt>\r\n\r\n            </dl>\r\n        </div>\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 85 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\DiagramasRed\FormDiagramaRedBuscar.cshtml"
               Write(Html.DisplayFor(model => model.FechaSubida));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n            </dl>\r\n        </div>\r\n    </div>\r\n    \r\n\r\n\r\n    <div class=\"mt-2 \">\r\n\r\n        <dl class=\"dl-horizontal\">\r\n\r\n            <dt class=\"mb-2\">\r\n");
            WriteLiteral("                Diagrama de red:\r\n            </dt>\r\n\r\n            <dd class=\"mb-3\">\r\n\r\n");
#nullable restore
#line 105 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\DiagramasRed\FormDiagramaRedBuscar.cshtml"
                 if (!string.IsNullOrEmpty(Model.DiagramaImagenBase64))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <img");
            BeginWriteAttribute("src", " src=\"", 2637, "\"", 2688, 2);
            WriteAttributeValue("", 2643, "data:image;base64,", 2643, 18, true);
#nullable restore
#line 107 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\DiagramasRed\FormDiagramaRedBuscar.cshtml"
WriteAttributeValue("", 2661, Model.DiagramaImagenBase64, 2661, 27, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"border p-3 img-fluid\" alt=\"Imagen\" />\r\n");
#nullable restore
#line 108 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\DiagramasRed\FormDiagramaRedBuscar.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </dd>\r\n        </dl>\r\n\r\n    </div>\r\n\r\n\r\n</div>\r\n\r\n\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "63f9ba81f78914844065fa2ee89d4d076b2cfa14a76642f7e7020fcf387ec41b8854", async() => {
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Sitio.Models.DiagramaRed> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
