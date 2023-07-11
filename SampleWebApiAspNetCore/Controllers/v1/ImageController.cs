using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SampleWebApiAspNetCore.Dtos;
using SampleWebApiAspNetCore.Entities;
using SampleWebApiAspNetCore.Helpers;
using SampleWebApiAspNetCore.Services;
using SampleWebApiAspNetCore.Models;
using SampleWebApiAspNetCore.Repositories;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SampleWebApiAspNetCore.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _ImageRepository;
        private readonly IMapper _mapper;
        private readonly ILinkService<ImagesController> _linkService;

        public ImagesController(
            IImageRepository ImageRepository,
            IMapper mapper,
            ILinkService<ImagesController> linkService)
        {
            _ImageRepository = ImageRepository;
            _mapper = mapper;
            _linkService = linkService;
        }

        //reference: https://stackoverflow.com/questions/50223606/aspnetcore-uploading-a-file-through-rest

        // option 1: form data

        [HttpPost]
        [Route("formdata", Name = nameof(PostFormData))]
        public async Task<IActionResult> PostFormData([FromForm(Name = "file")] IFormFile file)
        {
            using (var sr = new StreamReader(file.OpenReadStream()))
            {
                var content = await sr.ReadToEndAsync();
                return Ok(content);
            }
        }

        // option 2: encoded body
        // { "file": "XXXXX" }

        [HttpPost]
        [Route("body", Name = nameof(PostBody))]
        public IActionResult PostBody([FromBody] UploadModel uploadModel)
        {
            var bytes = Convert.FromBase64String(uploadModel.File);
            var decodedString = System.Text.Encoding.UTF8.GetString(bytes);
            return Ok(decodedString);
        }
        public class UploadModel
        {
            public string File { get; set; }
        }


        // option 3 binary
        [HttpPut]
        [Route("binary", Name = nameof(PostBinary))]
        public async Task<IActionResult> PostBinary()
        {

            /*
            var body = "test";
            Request.EnableBuffering();
            using (var reader = new StreamReader(Request.Body, encoding: Encoding.UTF8))
            {
                body = await reader.ReadToEndAsync();
                Request.Body.Position = 0;
            }
            return Ok(body);*/

            /*
            byte[] imgData;
            using (BinaryReader reader = new BinaryReader(Request.Body))
            {
                imgData = reader.ReadBytes(1);
            }
            return Task.FromResult<IActionResult>(Ok(imgData));*/

            /*
            using var buffer = new System.IO.MemoryStream();
            await this.Request.Body.CopyToAsync(buffer, this.Request.HttpContext.RequestAborted);
            var imageBytes = buffer.ToArray();
            return Ok(imageBytes);
            */

            // reader.BaseStream.Seek(0, SeekOrigin.Begin);
            //var body = await reader.ReadToEndAsync();
            /*byte[] data = null;
            Stream input = Request.Body;
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = await input.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                data = ms.ToArray();
            }


            return Ok(data);*/
            char[] buffer = new char[1024];
            Request.EnableBuffering();
            List<IFormFile> files = new List<IFormFile>();
            using (var sr = new StreamReader(Request.Body))
            {
                var body = await sr.ReadAsync(buffer, 0, buffer.Length);
                return Ok(body);
            }
        }

        /*
        [HttpPost("stream")]
        public async Task<IActionResult> ControllerModelStream()
        {
            byte[] buffer = new byte[BUF_SIZE];
            List<IFormFile> files = new List<IFormFile>();

            var model = await this.StreamFiles<StreamModel>(async x => {
                using (var stream = x.OpenReadStream())
                    while (await stream.ReadAsync(buffer, 0, buffer.Length) > 0) ;
                files.Add(x);
            });

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(new
            {
                Model = model,
                Files = files.Select(x => new {
                    x.Name,
                    x.FileName,
                    x.ContentDisposition,
                    x.ContentType,
                    x.Length
                })
            });
        }


        /// <summary>
        /// Processes Multi-part HttpRequest streams via the specified delegate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns>Returns multi-part form fields as the required generic model specified.</returns>
        public static async Task<T> StreamFiles<T>(this ControllerBase controller, Func<IFormFile, Task> func) where T : class, new()
        {
            var form = await controller.Request.StreamFilesModel(func);
            return await UpdateModel<T>(form, controller);
        }

        /// <summary>
        /// Processes Multi-part HttpRequest streams via the specified delegate, no model required for return
        /// </summary>
        /// <param name="func"></param>
        public static async Task StreamFiles(this ControllerBase controller, Func<IFormFile, Task> func)
        {
            await controller.Request.StreamFilesModel(func);
        }

        static async Task<T> UpdateModel<T>(FormValueProvider form, ControllerBase controller) where T : class, new()
        {
            var model = new T();
            await controller.TryUpdateModelAsync<T>(model, prefix: "", valueProvider: form);
            controller.TryValidateModel(model);
            return model;
        }*/
    }
}
