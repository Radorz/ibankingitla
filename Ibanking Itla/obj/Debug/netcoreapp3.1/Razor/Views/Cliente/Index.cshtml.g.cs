#pragma checksum "C:\Users\luisi\source\repos\Ibanking Itla\Ibanking Itla\Views\Cliente\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5b9bce39d345226b111811c0fa1c63cb3fdca4ee"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Cliente_Index), @"mvc.1.0.view", @"/Views/Cliente/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\luisi\source\repos\Ibanking Itla\Ibanking Itla\Views\_ViewImports.cshtml"
using Ibanking_Itla;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\luisi\source\repos\Ibanking Itla\Ibanking Itla\Views\_ViewImports.cshtml"
using Ibanking_Itla.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5b9bce39d345226b111811c0fa1c63cb3fdca4ee", @"/Views/Cliente/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9e7de3c420199b5c88724dcb07a7a0eb165498b9", @"/Views/_ViewImports.cshtml")]
    public class Views_Cliente_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\luisi\source\repos\Ibanking Itla\Ibanking Itla\Views\Cliente\Index.cshtml"
  

    ViewData["Title"] = "Home";
    Layout = "~/Views/Shared/_ClienteLayout.cshtml";
    ViewData["Usuario"] = User.Identity.Name.Trim();


#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<h2 class=""text-primary ml-3 mt-4"">Cuentas</h2>
<p class=""text-muted ml-3"">Balance total disponible Cuentas Ahorros y Corrientes</p>
<div class=""ml-lg-5"">
    <strong class=""text-primary h4"">
        <i class=""fa fa-usd"" aria-hidden=""true""></i>
        Disponible
    </strong>
    <p class=""text-muted ml-5"">DOP 20,000</p>

</div>

<div id=""accordion"">
    <div>
        <h5 class=""mb-0 text-right"">
            <button class=""btn btn-link collapsed dropdown-toggle"" data-toggle=""collapse"" data-target=""#collapseTwo"" aria-expanded=""false"" aria-controls=""collapseTwo"">
                Mostrar Cuentas
            </button>
        </h5>
        <div id=""collapseTwo"" class=""collapse"" aria-labelledby=""headingTwo"" data-parent=""#accordion"">
            <div class=""card-body"">
                <table class=""table"">
                    <thead>

                        <tr>
                            <th scope=""col"">Alias</th>
                            <th scope=""col"">Producto</th>
          ");
            WriteLiteral(@"                  <th scope=""col"">Moneda</th>
                            <th scope=""col"">Disponible</th>
                            <th scope=""col""></th>

                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>

<hr />
<div>
    <h2 class=""text-primary ml-3 mt-4"">Tarjetas de Crédito</h2>
    <p class=""text-muted ml-3"">Balance total disponible para consumo</p>
    <div class=""ml-lg-5 float-left"">
        <strong class=""text-primary h4"">
            <i class=""fa fa-usd"" aria-hidden=""true""></i>
            Disponible
        </strong>
        <p class=""text-muted ml-5"">DOP 20,000</p>

    </div>
    <div class=""ml-lg-5 float-left"">
        <strong class=""text-primary h4"">
            <i class=""fa fa-usd"" aria-hidden=""true""></i>
            Balance Actual
        </strong>
        <p class=""text-muted ml-5"">DOP 20,000</p>

    </div>
</div>
<");
            WriteLiteral("br />\r\n<br />\r\n<br />\r\n<br />\r\n<div id=\"accordion\">\r\n    <div");
            BeginWriteAttribute("class", " class=\"", 2261, "\"", 2269, 0);
            EndWriteAttribute();
            WriteLiteral(@">
        <h5 class=""mb-0 text-right"">
            <button class=""btn btn-link collapsed dropdown-toggle"" data-toggle=""collapse"" data-target=""#collapsethree"" aria-expanded=""false"" aria-controls=""collapsethree"">
                Mostrar Tarjetas
            </button>
        </h5>
        <div id=""collapsethree"" class=""collapse"" aria-labelledby=""headingthree"" data-parent=""#accordion"">
            <div class=""card-body"">
                <table class=""table"">
                    <thead>

                        <tr>
                            <th scope=""col"">Numero</th>
                            <th scope=""col"">Disponible</th>
                            <th scope=""col"">Ultima Transaccion</th>
                            <th scope=""col"">Estado</th>

                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>

<hr />
<div>
    <h2 class=""text-primary m");
            WriteLiteral(@"l-3 mt-4"">Prestamos</h2>
    <p class=""text-muted ml-3"">Pago pendientes prestamos</p>
    <div class=""ml-lg-5 float-left"">
        <strong class=""text-primary h4"">
            <i class=""fa fa-usd"" aria-hidden=""true""></i>
            Monto Pendiente
        </strong>
        <p class=""text-muted ml-5"">DOP 20,000</p>

    </div>

</div>
<br />
<br />
<br />
<br />
<div id=""accordion"">
    <div");
            BeginWriteAttribute("class", " class=\"", 3705, "\"", 3713, 0);
            EndWriteAttribute();
            WriteLiteral(@">
        <h5 class=""mb-0 text-right"">
            <button class=""btn btn-link collapsed dropdown-toggle"" data-toggle=""collapse"" data-target=""#collapsethree"" aria-expanded=""false"" aria-controls=""collapsethree"">
                Mostrar Prestamos
            </button>
        </h5>
        <div id=""collapsethree"" class=""collapse"" aria-labelledby=""headingthree"" data-parent=""#accordion"">
            <div class=""card-body"">
                <table class=""table"">
                    <thead>

                        <tr>
                            <th scope=""col"">Referencia</th>
                            <th scope=""col"">Monto</th>
                            <th scope=""col"">Pendiente</th>
                            <th scope=""col"">Estado</th>
                            <th scope=""col""></th>


                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>

");
            WriteLiteral("    </div>\r\n    </div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
