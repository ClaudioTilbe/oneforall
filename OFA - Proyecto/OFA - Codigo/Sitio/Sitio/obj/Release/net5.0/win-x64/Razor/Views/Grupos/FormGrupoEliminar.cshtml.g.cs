#pragma checksum "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Grupos\FormGrupoEliminar.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "96ab39caa92127cc06fb6e0fb3ff853731cd8bd41a174f5763729ac619012523"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Grupos_FormGrupoEliminar), @"mvc.1.0.view", @"/Views/Grupos/FormGrupoEliminar.cshtml")]
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
#line 5 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Grupos\FormGrupoEliminar.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Grupos\FormGrupoEliminar.cshtml"
using Sitio.Utility;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"96ab39caa92127cc06fb6e0fb3ff853731cd8bd41a174f5763729ac619012523", @"/Views/Grupos/FormGrupoEliminar.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"1867eecf72bc49bbadb2a39887fae6872f0611a2f9b918e03aa97fb8f3cdbdb3", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Grupos_FormGrupoEliminar : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Sitio.Models.Grupo>
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
#line 9 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Grupos\FormGrupoEliminar.cshtml"
  
    ViewBag.Title = "Eliminar Grupo";

    var UsuarioLogueado = Accessor.HttpContext.Session.GetObjectFromJson<Usuario>("UsuarioLog");

    Layout = LayoutForViewHelper.GetLayoutForView(UsuarioLogueado);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<div class=\"row mb-3\">\r\n    <div class=\"col-6\">\r\n        <h2 class=\"font-weight-bold\">Eliminar Grupo</h2>\r\n    </div>\r\n</div>\r\n\r\n<hr />");
            WriteLiteral("\r\n");
#nullable restore
#line 26 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Grupos\FormGrupoEliminar.cshtml"
 if (!string.IsNullOrEmpty(@ViewBag.Mensaje))
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"alert alert-danger\" role=\"alert\">\r\n        ");
#nullable restore
#line 29 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Grupos\FormGrupoEliminar.cshtml"
   Write(ViewBag.Mensaje);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n");
#nullable restore
#line 31 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Grupos\FormGrupoEliminar.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div>\r\n\r\n    <div class=\"row mt-4\">\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dt class=\"mb-3\">\r\n                    ");
#nullable restore
#line 40 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Grupos\FormGrupoEliminar.cshtml"
               Write(Html.DisplayNameFor(model => model.Codigo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dt>\r\n\r\n                <dt class=\"mb-3\">\r\n");
            WriteLiteral("                    Nombre grupo\r\n                </dt>\r\n\r\n                <dt class=\"mb-3\">\r\n                    ");
#nullable restore
#line 49 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Grupos\FormGrupoEliminar.cshtml"
               Write(Html.DisplayNameFor(model => model.Descripcion));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dt>\r\n\r\n            </dl>\r\n        </div>\r\n\r\n        <div class=\"col-3\">\r\n            <dl class=\"dl-horizontal\">\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 59 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Grupos\FormGrupoEliminar.cshtml"
               Write(Html.DisplayFor(model => model.Codigo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 63 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Grupos\FormGrupoEliminar.cshtml"
               Write(Html.DisplayFor(model => model.NombreGrupo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n                <dd class=\"mb-3\">\r\n                    ");
#nullable restore
#line 67 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Grupos\FormGrupoEliminar.cshtml"
               Write(Html.DisplayFor(model => model.Descripcion));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </dd>\r\n\r\n            </dl>\r\n        </div>\r\n    </div>\r\n\r\n\r\n\r\n    <dl class=\"dl-horizontal mt-2\">\r\n\r\n");
            WriteLiteral("        <dt>\r\n            ");
#nullable restore
#line 80 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Grupos\FormGrupoEliminar.cshtml"
       Write(Html.DisplayNameFor(model => model.Dispositivos));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n\r\n        <dd>\r\n            <div class=\"col-md-10 pt-3 pl-0\">\r\n\r\n");
            WriteLiteral("\r\n                <table id=\"example\" class=\"table table-striped table-hover border\" style=\"width:100%\">\r\n\r\n                    <tr>\r\n\r\n                        <th>\r\n                            ");
#nullable restore
#line 93 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Grupos\FormGrupoEliminar.cshtml"
                       Write(Html.DisplayNameFor(model => model.Dispositivos[0].IP));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </th>\r\n                        <th>\r\n                            ");
#nullable restore
#line 96 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Grupos\FormGrupoEliminar.cshtml"
                       Write(Html.DisplayNameFor(model => model.Dispositivos[0].Nombre));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </th>\r\n                        <th>\r\n                            ");
#nullable restore
#line 99 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Grupos\FormGrupoEliminar.cshtml"
                       Write(Html.DisplayNameFor(model => model.Dispositivos[0].Tipo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </th>\r\n                        <th>\r\n                            ");
#nullable restore
#line 102 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Grupos\FormGrupoEliminar.cshtml"
                       Write(Html.DisplayNameFor(model => model.Dispositivos[0].Sector));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </th>\r\n                    </tr>\r\n\r\n\r\n");
#nullable restore
#line 107 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Grupos\FormGrupoEliminar.cshtml"
                     foreach (var item in Model.Dispositivos)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n\r\n                            <td>\r\n                                ");
#nullable restore
#line 112 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Grupos\FormGrupoEliminar.cshtml"
                           Write(Html.DisplayFor(modelItem => item.IP));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                ");
#nullable restore
#line 115 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Grupos\FormGrupoEliminar.cshtml"
                           Write(Html.DisplayFor(modelItem => item.Nombre));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                ");
#nullable restore
#line 118 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Grupos\FormGrupoEliminar.cshtml"
                           Write(Html.DisplayFor(modelItem => item.Tipo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                ");
#nullable restore
#line 121 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Grupos\FormGrupoEliminar.cshtml"
                           Write(Html.DisplayFor(modelItem => item.Sector));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                        </tr>\r\n");
#nullable restore
#line 124 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Grupos\FormGrupoEliminar.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </table>\r\n\r\n            </div>\r\n\r\n        </dd>\r\n\r\n    </dl>\r\n\r\n    <hr />");
            WriteLiteral("\r\n    <div class=\"alert alert-danger mt-3 mb-3\">\r\n        ¿Esta seguro de que desea eliminar este Grupo?\r\n    </div>\r\n\r\n\r\n");
#nullable restore
#line 141 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Grupos\FormGrupoEliminar.cshtml"
     using (Html.BeginForm())
    {
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 143 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Grupos\FormGrupoEliminar.cshtml"
   Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"form-group mt-2\">\r\n            <div class=\"col-md-offset-2 col-md-10 text-center\">\r\n                <input type=\"submit\" value=\"Eliminar Grupo\" class=\"btn btn-danger\" />\r\n            </div>\r\n        </div>\r\n");
#nullable restore
#line 150 "E:\One for all - Final\OFA - Codigo\Sitio\Sitio\Views\Grupos\FormGrupoEliminar.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>\r\n\r\n\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "96ab39caa92127cc06fb6e0fb3ff853731cd8bd41a174f5763729ac61901252313285", async() => {
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Sitio.Models.Grupo> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
