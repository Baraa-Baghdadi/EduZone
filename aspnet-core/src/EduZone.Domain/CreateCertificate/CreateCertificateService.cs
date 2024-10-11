using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EduZone.CreateCertificate
{
    public class CreateCertificateService : ITransientDependency
    {
        private readonly IWebHostEnvironment _Environment;
        private readonly IConverter _converter;

        public CreateCertificateService(IWebHostEnvironment Environment, IConverter converter)
        {
            _Environment = Environment;
            _converter = converter;
        }

        public async Task GenerateCertificate(string name,string courseName,DateTime date)
        {
            FileStream fileStream;
            byte[] pdf;
            string path, body = "";
            path = Path.Combine(_Environment.WebRootPath, EduZoneConsts.CertificateFolderName, $"CertificateTemplate.html");
            fileStream = new FileStream(path, FileMode.Open);
            using (StreamReader reader = new(fileStream))
            {
                string file = reader.ReadToEnd();
                body = file;
                body = body.Replace("Name", name);
                body = body.Replace("Course", courseName);
                body = body.Replace("DateIssued", date.ToString("dd/MM/yyyy"));
                var doc = new HtmlToPdfDocument()
                {
                    GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    PaperSize = PaperKind.A4
                    },
                    Objects = {
                    new ObjectSettings()
                        {
                            HtmlContent = body,
                            WebSettings = { DefaultEncoding = "utf-8" }
                        }
                    }
                };
                pdf = _converter.Convert(doc);
            }
            string certificatepath = Path.Combine(_Environment.WebRootPath, EduZoneConsts.CertificateFolderName, $"{courseName +" for "+ name }.pdf");
            // Save the PDF to a file
            File.WriteAllBytes(certificatepath, pdf);
        }
    }
}
