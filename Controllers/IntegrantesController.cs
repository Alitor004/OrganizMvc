using Microsoft.AspNetCore.Mvc;
using OrganizMvc.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OrganizMvc.Controllers
{
    public class IntegrantesController : Controller
    {
        public string uriBase = "http://alitor.somee.com/OrganizEtec/Integrantes/";
    
        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                string uriComplementar = "GetAll";
                HttpClient httpClient = new HttpClient();

                HttpResponseMessage response = await httpClient.GetAsync(uriBase + uriComplementar);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string serialized = await response.Content.ReadAsStringAsync();
                    List<IntegranteViewModel> listaIntegrantes = await Task.Run(() =>
                        JsonConvert.DeserializeObject<List<IntegranteViewModel>>(serialized));

                    return View(listaIntegrantes);
                }
                else
                {
                    throw new System.Exception();
                }
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(IntegranteViewModel p)
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                var content = new StringContent(JsonConvert.SerializeObject(p));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(uriBase, content);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] = string.Format("Integrante {0}, Id {1} salvo com sucesso!!!", p.Nome, serialized);
                    return RedirectToAction("Index");
                }
                else
                    throw new System.Exception(serialized);

            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Create");
            }
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> DetailsAsync(int? id)
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                HttpResponseMessage response = await httpClient.GetAsync(uriBase + id.ToString());

                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    IntegranteViewModel p = await Task.Run(() =>
                        JsonConvert.DeserializeObject<IntegranteViewModel>(serialized));
                    return View(p);
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<ActionResult> EditAsync(int? id)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(uriBase + id.ToString());

                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    IntegranteViewModel i = await Task.Run(() =>
                        JsonConvert.DeserializeObject<IntegranteViewModel>(serialized));
                    return View(i);
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public async Task<ActionResult> EditAsync(IntegranteViewModel i)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(p));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await httpClient.PutAsync(uriBase, content);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] = string.Format("Integrante {0}, Id {1} salvo comsucesso!!!", i.Nome, serialized);
                    return RedirectToAction("Index");
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }


















    }
}