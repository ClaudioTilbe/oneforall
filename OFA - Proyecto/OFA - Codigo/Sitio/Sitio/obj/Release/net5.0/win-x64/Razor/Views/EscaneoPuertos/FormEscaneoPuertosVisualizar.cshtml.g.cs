#pragma checksum "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\EscaneoPuertos\FormEscaneoPuertosVisualizar.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6337a4888724d4cf8c17d87a246da1da53d2d65ecdcbf7cb0d606829c72ca9a4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_EscaneoPuertos_FormEscaneoPuertosVisualizar), @"mvc.1.0.view", @"/Views/EscaneoPuertos/FormEscaneoPuertosVisualizar.cshtml")]
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
#line 4 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\EscaneoPuertos\FormEscaneoPuertosVisualizar.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\EscaneoPuertos\FormEscaneoPuertosVisualizar.cshtml"
using Sitio.Utility;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"6337a4888724d4cf8c17d87a246da1da53d2d65ecdcbf7cb0d606829c72ca9a4", @"/Views/EscaneoPuertos/FormEscaneoPuertosVisualizar.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"1867eecf72bc49bbadb2a39887fae6872f0611a2f9b918e03aa97fb8f3cdbdb3", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_EscaneoPuertos_FormEscaneoPuertosVisualizar : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Sitio.Models.EscaneoPuertos>
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
#line 8 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\EscaneoPuertos\FormEscaneoPuertosVisualizar.cshtml"
  
    ViewBag.Title = "Visualizar Escaneo de puertos";

    var UsuarioLogueado = Accessor.HttpContext.Session.GetObjectFromJson<Usuario>("UsuarioLog");

    Layout = LayoutForViewHelper.GetLayoutForView(UsuarioLogueado);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""row mb-3"">
    <div class=""col-6"">
        <h2 class=""font-weight-bold"">Visualizar Escaneo de puertos</h2>
    </div>
</div>

<div>

    <hr />

    <div class=""row mt-4"">
        <div class=""col-3"">
            <dl class=""dl-horizontal"">

                <dt class=""mb-3"">
                    ");
#nullable restore
#line 31 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\EscaneoPuertos\FormEscaneoPuertosVisualizar.cshtml"
               Write(Html.DisplayNameFor(model => model.IDEscaneo));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n                </dt>\r\n\r\n            </dl>\r\n        </div>\r\n        <div class=\"col-6\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 40 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\EscaneoPuertos\FormEscaneoPuertosVisualizar.cshtml"
               Write(Html.DisplayFor(model => model.IDEscaneo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n            </dl>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row mt-2\">\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dt class=\"mb-3\">\r\n                    ");
#nullable restore
#line 52 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\EscaneoPuertos\FormEscaneoPuertosVisualizar.cshtml"
               Write(Html.DisplayNameFor(model => model.DispositivoObjetivo.IP));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n                </dt>\r\n\r\n            </dl>\r\n        </div>\r\n        <div class=\"col-6\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 61 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\EscaneoPuertos\FormEscaneoPuertosVisualizar.cshtml"
               Write(Html.DisplayFor(model => model.DispositivoObjetivo.IP));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n            </dl>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row mt-2\">\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dt class=\"mb-3\">\r\n                    ");
#nullable restore
#line 73 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\EscaneoPuertos\FormEscaneoPuertosVisualizar.cshtml"
               Write(Html.DisplayNameFor(model => model.Razon));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n                </dt>\r\n\r\n            </dl>\r\n        </div>\r\n        <div class=\"col-6\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 82 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\EscaneoPuertos\FormEscaneoPuertosVisualizar.cshtml"
               Write(Html.DisplayFor(model => model.Razon));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n            </dl>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row mt-2\">\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dt class=\"mb-3\">\r\n                    ");
#nullable restore
#line 94 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\EscaneoPuertos\FormEscaneoPuertosVisualizar.cshtml"
               Write(Html.DisplayNameFor(model => model.Estado));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n                </dt>\r\n\r\n            </dl>\r\n        </div>\r\n        <div class=\"col-6\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 103 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\EscaneoPuertos\FormEscaneoPuertosVisualizar.cshtml"
               Write(Html.DisplayFor(model => model.Estado));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n            </dl>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row mt-2\">\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dt class=\"mb-3\">\r\n                    ");
#nullable restore
#line 115 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\EscaneoPuertos\FormEscaneoPuertosVisualizar.cshtml"
               Write(Html.DisplayNameFor(model => model.Prioridad));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n                </dt>\r\n\r\n            </dl>\r\n        </div>\r\n        <div class=\"col-6\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 124 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\EscaneoPuertos\FormEscaneoPuertosVisualizar.cshtml"
               Write(Html.DisplayFor(model => model.Prioridad));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                </dd>

            </dl>
        </div>
    </div>

    <div class=""row mt-2"">
        <div class=""col-3"">
            <dl class=""dl-horizontal"">

                <dt class=""mb-3"">
                    Cadena de salida:
                </dt>

            </dl>
        </div>
        <div class=""col-6"">
            <dl class=""dl-horizontal"">

                <dd class=""mb-3"">
                    <pre>
");
#nullable restore
#line 147 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\EscaneoPuertos\FormEscaneoPuertosVisualizar.cshtml"
                          
                            var cadenaSinEspacios = Model.CadenaSalida.TrimStart();
                            

#line default
#line hidden
#nullable disable
#nullable restore
#line 149 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\EscaneoPuertos\FormEscaneoPuertosVisualizar.cshtml"
                       Write(cadenaSinEspacios);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    </pre>
                </dd> 

            </dl>
        </div>
    </div>

    <div class=""row mt-2"">
        <div class=""col-3"">
            <dl class=""dl-horizontal"">

                <dt class=""mb-3"">
                    Fecha generado:
                </dt>

            </dl>
        </div>
        <div class=""col-6"">
            <dl class=""dl-horizontal"">

                <dd class=""mb-3"">
                    ");
#nullable restore
#line 172 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\EscaneoPuertos\FormEscaneoPuertosVisualizar.cshtml"
               Write(Html.DisplayFor(model => model.FechaGenerado));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                </dd>

            </dl>
        </div>
    </div>

    <div class=""row mt-2"">
        <div class=""col-3"">
            <dl class=""dl-horizontal"">

                <dt class=""mb-3"">
                    Fecha finalizado:
                </dt>

            </dl>
        </div>
        <div class=""col-6"">
            <dl class=""dl-horizontal"">

                <dd class=""mb-3"">
                    ");
#nullable restore
#line 193 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\EscaneoPuertos\FormEscaneoPuertosVisualizar.cshtml"
               Write(Html.DisplayFor(model => model.FechaFinalizado));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                </dd>

            </dl>
        </div>
    </div>

    <div class=""row mt-2"">
        <div class=""col-3"">
            <dl class=""dl-horizontal"">

                <dt class=""mb-3"">
                    Creador:
                </dt>

            </dl>
        </div>
        <div class=""col-6"">
            <dl class=""dl-horizontal"">

                <dd class=""mb-3"">
                    ");
#nullable restore
#line 214 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\EscaneoPuertos\FormEscaneoPuertosVisualizar.cshtml"
               Write(Html.DisplayFor(model => model.AdministradorReg.UsuarioID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n            </dl>\r\n        </div>\r\n    </div>\r\n\r\n\r\n</div>\r\n\r\n\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6337a4888724d4cf8c17d87a246da1da53d2d65ecdcbf7cb0d606829c72ca9a413450", async() => {
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Sitio.Models.EscaneoPuertos> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
