#pragma checksum "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Administradores\FormAdministradorVisualizar.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "a5be820f62f687dc9ad21cad25bf86958b3038b387fd6f82ba7afdb1918ea845"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Administradores_FormAdministradorVisualizar), @"mvc.1.0.view", @"/Views/Administradores/FormAdministradorVisualizar.cshtml")]
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
#line 4 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Administradores\FormAdministradorVisualizar.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Administradores\FormAdministradorVisualizar.cshtml"
using Sitio.Utility;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"a5be820f62f687dc9ad21cad25bf86958b3038b387fd6f82ba7afdb1918ea845", @"/Views/Administradores/FormAdministradorVisualizar.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"1867eecf72bc49bbadb2a39887fae6872f0611a2f9b918e03aa97fb8f3cdbdb3", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Administradores_FormAdministradorVisualizar : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Sitio.Models.Administrador>
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
#line 8 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Administradores\FormAdministradorVisualizar.cshtml"
  
    ViewBag.Title = "Visualizar Administrador";

    var UsuarioLogueado = Accessor.HttpContext.Session.GetObjectFromJson<Usuario>("UsuarioLog");

    Layout = LayoutForViewHelper.GetLayoutForView(UsuarioLogueado);

    string contraseñaOculta = new string('*', Model.Contraseña.Length);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<div class=\"row mb-3\">\r\n    <div class=\"col-6\">\r\n        <h2 class=\"font-weight-bold\">Visualizar Administrador</h2>\r\n    </div>\r\n</div>\r\n\r\n<hr />");
            WriteLiteral(@"
<div class=""mt-4 mb-4"">
    <h4 class=""font-weight-bold"">Informacion de Administrador</h4>
</div>

<div>

    <div class=""row mt-2"">
        <div class=""col-3"">
            <dl class=""dl-horizontal"">

                <dt class=""mb-3"">
                    ");
#nullable restore
#line 38 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Administradores\FormAdministradorVisualizar.cshtml"
               Write(Html.DisplayNameFor(model => model.UsuarioID));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n                </dt>\r\n\r\n            </dl>\r\n        </div>\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 47 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Administradores\FormAdministradorVisualizar.cshtml"
               Write(Html.DisplayFor(model => model.UsuarioID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n            </dl>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row mt-2\">\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dt class=\"mb-3\">\r\n                    ");
#nullable restore
#line 59 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Administradores\FormAdministradorVisualizar.cshtml"
               Write(Html.DisplayNameFor(model => model.Contraseña));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n                </dt>\r\n\r\n            </dl>\r\n        </div>\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 68 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Administradores\FormAdministradorVisualizar.cshtml"
               Write(contraseñaOculta);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral("                </dd>\r\n\r\n            </dl>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row mt-2\">\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dt class=\"mb-3\">\r\n                    ");
#nullable restore
#line 81 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Administradores\FormAdministradorVisualizar.cshtml"
               Write(Html.DisplayNameFor(model => model.MailAsignado.Correo));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n                </dt>\r\n\r\n            </dl>\r\n        </div>\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 90 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Administradores\FormAdministradorVisualizar.cshtml"
               Write(Html.DisplayFor(model => model.MailAsignado.Correo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n            </dl>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row mt-2\">\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dt class=\"mb-3\">\r\n                    ");
#nullable restore
#line 102 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Administradores\FormAdministradorVisualizar.cshtml"
               Write(Html.DisplayNameFor(model => model.Nombre));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n                </dt>\r\n\r\n            </dl>\r\n        </div>\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 111 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Administradores\FormAdministradorVisualizar.cshtml"
               Write(Html.DisplayFor(model => model.Nombre));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n            </dl>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row mt-2\">\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dt class=\"mb-3\">\r\n");
            WriteLiteral("                    Numero funcionario:\r\n                </dt>\r\n\r\n            </dl>\r\n        </div>\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 133 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Administradores\FormAdministradorVisualizar.cshtml"
               Write(Html.DisplayFor(model => model.NumeroFuncionario));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n            </dl>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"row mt-2\">\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dt class=\"mb-3\">\r\n                    ");
#nullable restore
#line 145 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Administradores\FormAdministradorVisualizar.cshtml"
               Write(Html.DisplayNameFor(model => model.Cargo));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n                </dt>\r\n\r\n            </dl>\r\n        </div>\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 154 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Administradores\FormAdministradorVisualizar.cshtml"
               Write(Html.DisplayFor(model => model.Cargo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n            </dl>\r\n        </div>\r\n    </div>\r\n\r\n\r\n</div>\r\n\r\n\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a5be820f62f687dc9ad21cad25bf86958b3038b387fd6f82ba7afdb1918ea84511182", async() => {
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Sitio.Models.Administrador> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
