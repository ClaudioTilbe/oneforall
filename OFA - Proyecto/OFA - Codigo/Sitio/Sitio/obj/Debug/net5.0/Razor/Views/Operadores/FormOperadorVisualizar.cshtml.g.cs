#pragma checksum "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Operadores\FormOperadorVisualizar.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3274141fdd1fc07e835e166ba084e4f2be112881405c969863f3d1cfa5b17790"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Operadores_FormOperadorVisualizar), @"mvc.1.0.view", @"/Views/Operadores/FormOperadorVisualizar.cshtml")]
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
#line 4 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Operadores\FormOperadorVisualizar.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Operadores\FormOperadorVisualizar.cshtml"
using Sitio.Utility;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"3274141fdd1fc07e835e166ba084e4f2be112881405c969863f3d1cfa5b17790", @"/Views/Operadores/FormOperadorVisualizar.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"1867eecf72bc49bbadb2a39887fae6872f0611a2f9b918e03aa97fb8f3cdbdb3", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Operadores_FormOperadorVisualizar : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Sitio.Models.Operador>
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
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 9 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Operadores\FormOperadorVisualizar.cshtml"
  
    ViewBag.Title = "Visualizar Operador";

    var UsuarioLogueado = Accessor.HttpContext.Session.GetObjectFromJson<Usuario>("UsuarioLog");

    Layout = LayoutForViewHelper.GetLayoutForView(UsuarioLogueado);

    string contraseñaOculta = new string('*', Model.Contraseña.Length);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<div class=""d-flex"">
    <div class=""mr-4"">
        <h2 class=""font-weight-bold"">Visualizar Operador</h2>
    </div>
</div>


<div>

    <hr />

    <div class=""mt-4"">
        <h4 class=""font-weight-bold"">Informacion de Operador</h4>
    </div>

    <div class=""row mt-4"">
        <div class=""col-3"">
            <dl class=""dl-horizontal"">

                <dt class=""mb-3"">
                    ");
#nullable restore
#line 40 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Operadores\FormOperadorVisualizar.cshtml"
               Write(Html.DisplayNameFor(model => model.UsuarioID));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n                </dt>\r\n\r\n            </dl>\r\n        </div>\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 49 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Operadores\FormOperadorVisualizar.cshtml"
               Write(Html.DisplayFor(model => model.UsuarioID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n            </dl>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row mt-2\">\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dt class=\"mb-3\">\r\n                    ");
#nullable restore
#line 61 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Operadores\FormOperadorVisualizar.cshtml"
               Write(Html.DisplayNameFor(model => model.Contraseña));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n                </dt>\r\n\r\n            </dl>\r\n        </div>\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 70 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Operadores\FormOperadorVisualizar.cshtml"
               Write(contraseñaOculta);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral("                </dd>\r\n\r\n            </dl>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row mt-2\">\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dt class=\"mb-3\">\r\n                    ");
#nullable restore
#line 83 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Operadores\FormOperadorVisualizar.cshtml"
               Write(Html.DisplayNameFor(model => model.MailAsignado.Correo));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n                </dt>\r\n\r\n            </dl>\r\n        </div>\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 92 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Operadores\FormOperadorVisualizar.cshtml"
               Write(Html.DisplayFor(model => model.MailAsignado.Correo));

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
                    Numero de funcionario supervisor:
                </dt>

            </dl>
        </div>
        <div class=""col-3"">
            <dl class=""dl-horizontal"">

                <dd class=""mb-3"">
                    ");
#nullable restore
#line 113 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Operadores\FormOperadorVisualizar.cshtml"
               Write(Html.DisplayFor(model => model.NumeroFuncionarioSupervisor));

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
                    Nombre supervisor:
                </dt>

            </dl>
        </div>
        <div class=""col-3"">
            <dl class=""dl-horizontal"">

                <dd class=""mb-3"">
                    ");
#nullable restore
#line 134 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Operadores\FormOperadorVisualizar.cshtml"
               Write(Html.DisplayFor(model => model.NombreSupervisor));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n            </dl>\r\n        </div>\r\n    </div>\r\n\r\n\r\n\r\n\r\n    <div class=\"mt-2\">\r\n        <div>\r\n            <h4 class=\"font-weight-bold\">Sucursales asignadas</h4>\r\n        </div>\r\n\r\n        <div class=\"mt-4\">\r\n");
#nullable restore
#line 150 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Operadores\FormOperadorVisualizar.cshtml"
             if (Model.SucursalesAsignadas.Any())
             {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 152 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Operadores\FormOperadorVisualizar.cshtml"
                 foreach (var sucursal in Model.SucursalesAsignadas)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div>\r\n                        <strong>");
#nullable restore
#line 155 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Operadores\FormOperadorVisualizar.cshtml"
                           Write(Html.DisplayFor(model => sucursal.NroSucursal));

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong> - ");
#nullable restore
#line 155 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Operadores\FormOperadorVisualizar.cshtml"
                                                                                      Write(Html.DisplayFor(model => sucursal.Calle));

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 155 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Operadores\FormOperadorVisualizar.cshtml"
                                                                                                                                Write(Html.DisplayFor(model => sucursal.NumeroLocal));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </div>\r\n");
#nullable restore
#line 157 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Operadores\FormOperadorVisualizar.cshtml"
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 157 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Operadores\FormOperadorVisualizar.cshtml"
                 
             }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div>\r\n                    No hay sucursales asignadas.\r\n                </div>\r\n");
#nullable restore
#line 164 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Operadores\FormOperadorVisualizar.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>\r\n\r\n\r\n\r\n\r\n</div>\r\n\r\n\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3274141fdd1fc07e835e166ba084e4f2be112881405c969863f3d1cfa5b1779012268", async() => {
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Sitio.Models.Operador> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591