#pragma checksum "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\MensajesMotor\FormMensajesMotorListar.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "525f0eeca20b3afa0e87e6bd967804147544defd0cedffe7b97c5adc91a3d59c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_MensajesMotor_FormMensajesMotorListar), @"mvc.1.0.view", @"/Views/MensajesMotor/FormMensajesMotorListar.cshtml")]
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
#line 4 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\MensajesMotor\FormMensajesMotorListar.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\MensajesMotor\FormMensajesMotorListar.cshtml"
using Sitio.Utility;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"525f0eeca20b3afa0e87e6bd967804147544defd0cedffe7b97c5adc91a3d59c", @"/Views/MensajesMotor/FormMensajesMotorListar.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"1867eecf72bc49bbadb2a39887fae6872f0611a2f9b918e03aa97fb8f3cdbdb3", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_MensajesMotor_FormMensajesMotorListar : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Sitio.Models.MensajeMotor>>
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
#line 8 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\MensajesMotor\FormMensajesMotorListar.cshtml"
  
    ViewBag.Title = "Log Motor";

    var UsuarioLogueado = Accessor.HttpContext.Session.GetObjectFromJson<Usuario>("UsuarioLog");

    Layout = LayoutForViewHelper.GetLayoutForView(UsuarioLogueado);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n<div class=\"row mb-3\">\r\n    <div class=\"col-12\">\r\n        <h2 class=\"font-weight-bold\">Log Mensajes de error en Motor</h2>\r\n    </div>\r\n</div>\r\n\r\n\r\n\r\n<hr />");
            WriteLiteral("\r\n\r\n\r\n");
#nullable restore
#line 30 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\MensajesMotor\FormMensajesMotorListar.cshtml"
 if (!string.IsNullOrEmpty(@ViewBag.Mensaje))
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"alert alert-primary\" role=\"alert\">\r\n        ");
#nullable restore
#line 33 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\MensajesMotor\FormMensajesMotorListar.cshtml"
   Write(ViewBag.Mensaje);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n");
#nullable restore
#line 35 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\MensajesMotor\FormMensajesMotorListar.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n");
#nullable restore
#line 39 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\MensajesMotor\FormMensajesMotorListar.cshtml"
 foreach (var item in Model)
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""border border-secondary p-2 mb-3"">

        <table class=""table border-top-0 mb-0"">
            <tbody>

                <tr>
                    <th class=""border-top-0"" scope=""row"">ID Mensaje:</th>
                    <td class=""border-top-0"">");
#nullable restore
#line 48 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\MensajesMotor\FormMensajesMotorListar.cshtml"
                                        Write(Html.DisplayFor(modelItem => item.IDMensaje));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n\r\n                <tr>\r\n                    <th scope=\"row\">Excepcion:</th>\r\n                    <td>");
#nullable restore
#line 53 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\MensajesMotor\FormMensajesMotorListar.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Excepcion));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n\r\n                <tr>\r\n                    <th scope=\"row\">Mensaje:</th>\r\n                    <td>");
#nullable restore
#line 58 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\MensajesMotor\FormMensajesMotorListar.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Mensaje));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n\r\n                <tr>\r\n                    <th scope=\"row\">Metodo de origen:</th>\r\n                    <td>");
#nullable restore
#line 63 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\MensajesMotor\FormMensajesMotorListar.cshtml"
                   Write(Html.DisplayFor(modelItem => item.MetodoOrigen));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n\r\n                <tr>\r\n                    <th scope=\"row\">Estado de variables:</th>\r\n                    <td>");
#nullable restore
#line 68 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\MensajesMotor\FormMensajesMotorListar.cshtml"
                   Write(Html.DisplayFor(modelItem => item.EstadoVariables));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n\r\n                <tr>\r\n                    <th scope=\"row\">Fecha generado:</th>\r\n                    <td>");
#nullable restore
#line 73 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\MensajesMotor\FormMensajesMotorListar.cshtml"
                   Write(Html.DisplayFor(modelItem => item.FechaGenerado));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n\r\n                <tr>\r\n                    <th scope=\"row\">Tipo:</th>\r\n                    <td>");
#nullable restore
#line 78 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\MensajesMotor\FormMensajesMotorListar.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Tipo));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n\r\n            </tbody>\r\n        </table>\r\n\r\n    </div>\r\n");
#nullable restore
#line 85 "E:\OFA - Github\OFA - Codigo\Sitio\Sitio\Views\MensajesMotor\FormMensajesMotorListar.cshtml"

}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "525f0eeca20b3afa0e87e6bd967804147544defd0cedffe7b97c5adc91a3d59c9317", async() => {
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Sitio.Models.MensajeMotor>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
