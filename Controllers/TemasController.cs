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
    public class TemasController : Controller
    {
        public string uriBase = "http://alitor.somee.com/OrganizEtec/Temas/";

        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                string uriComplementar = "GetAll";
                HttpClient httpClient = new HttpClient();

                HttpResponseMessage response = await httpClient.GetAsync(uriBase + uriComplementar);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<TemaViewModel> listaTemas = await Task.Run(() => 
                        JsonConvert.DeserializeObject<List<TemaViewModel>>(serialized));

                    return View(listaTemas);
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
        public async Task<ActionResult> CreateAsync(TemaViewModel t)
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                var content = new StringContent(JsonConvert.SerializeObject(t));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(uriBase, content);
                string serialized = await response.Content.ReadAsStringAsync();

                if ( response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] = string.Format("Tema {0}, Id {1} salvo comsucesso!!!", t.Nome, serialized);
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
                    TemaViewModel t = await Task.Run(() => 
                        JsonConvert.DeserializeObject<TemaViewModel>(serialized));
                    return View(t);
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
                string token =  HttpContext.Session.GetString("SessionTokenUsuario");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await httpClient.GetAsync(uriBase + id.ToString());

                string serialized = await response.Content.ReadAsStringAsync();

               if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TemaViewModel t = await Task.Run(() => 
                        JsonConvert.DeserializeObject<TemaViewModel>(serialized));
                    return View(t);
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
        public async Task<ActionResult> EditAsync(TemaViewModel a)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token =  HttpContext.Session.GetString("SessionTokenUsuario");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var content = new StringContent(JsonConvert.SerializeObject(a));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await httpClient.PutAsync(uriBase, content);
                string serialized = await response.Content.ReadAsStringAsync();

               if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] = string.Format("Tema {0}, Id {1} salvo com sucesso!!!", a.Nome, a.Id);
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

        [HttpGet]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                
                HttpResponseMessage response = await httpClient.DeleteAsync(uriBase + id.ToString());
                string serialized = await response.Content.ReadAsStringAsync();

               if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] = string.Format("Tema Id {0} removida com sucesso!!!", id);
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