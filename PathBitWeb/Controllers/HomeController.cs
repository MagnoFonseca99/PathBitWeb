using ClientePathBit.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics.Contracts;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Text;

namespace PathBit.API.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Construtor
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private string GetUrlApi { get { return this._configuration.GetValue<string>("Endpoints:API"); } }
        private string GetUrlApiCEP { get { return this._configuration.GetValue<string>("Endpoints:APICEP"); } }
        public HomeController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(new Cliente());
            }
            catch (Exception e)
            {
                TempData["Sucesso"] = null;
                TempData["Erro"] = e.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> CadastroSucesso(string status)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Cliente dados)
        {
            if (ModelState.IsValid)
            {
                if ((dados.Financeiro.Renda + dados.Financeiro.Patrimonio) < 1000)
                {
                    TempData["Erro"] = "Renda + Patrimonio deve ser maior que 1000";
                    return View(dados);
                }
                if (!ValidarEmail(dados.Seguranca.Email))
                {
                    TempData["Erro"] = "Formato de email incorreto";
                    return View(dados);
                }

                dados.Seguranca.Senha = ClientePathBit.Models.Seguranca.CriptografarSHA512(dados.Seguranca.Senha);
                dados.Seguranca.ConfirmacaoSenha = dados.Seguranca.Senha;

                string Json = JsonConvert.SerializeObject(dados);

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync(GetUrlApi, new StringContent(Json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                   
                    TempData["Sucesso"] = "Inclusao Realizada com Sucesso";
                    TempData.Keep("Sucesso");
                    return RedirectToAction("CadastroSucesso", "Home");
                }
                else
                {
                    TempData["Erro"] = "Ocorreu um erro, tente novamente mais tarde";
                    return View(dados);
                }
            }
            return View(dados);
        }

        public static bool ValidarEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return mailAddress.Address == email;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        [HttpGet]
        public async Task<IActionResult> ConsultaCEP(string CEP)
        {
            string endpoint = GetUrlApiCEP;
            if (!string.IsNullOrWhiteSpace(CEP))
            {
                endpoint += $"{CEP}?countryCode=BRA";
            }

            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync(endpoint);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<RetornoCEP> retorno = JsonConvert.DeserializeObject<List<RetornoCEP>>(response.Content.ReadAsStringAsync().Result);

                    return Json(JsonConvert.SerializeObject(retorno[0]));
                }
                else
                {
                    return Json(JsonConvert.SerializeObject("NotFound"));
                }
            }
            catch
            {
                return Json(JsonConvert.SerializeObject("NotFound"));
            }

        }



    }

}