using System;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using System.IO;
using Microsoft.Extensions.Configuration;
using EduZone.QrCode;
using EduZone.QrPdf;
using Microsoft.AspNetCore.Hosting;
namespace EduZone.Emailing
{
    public class EmailSendingJob : AsyncBackgroundJob<EmailSendingArgs>, ITransientDependency
    {
        private readonly IEmailSender _emailSender;
        private readonly Lazy<IDataFilter> _dataFilter;
        private readonly Lazy<IConfiguration> _configuration;
        private readonly IdentityUserManager _userManager;
        private readonly IWebHostEnvironment _Environment;
        private readonly QrCodeService _qrCodeService;
        private readonly QrPdfService _qrPdfService;

        public EmailSendingJob(IEmailSender emailSender, Lazy<IDataFilter> dataFilter, Lazy<IConfiguration> configuration,
            IdentityUserManager userManager,IWebHostEnvironment Environment, QrCodeService qrCodeService, QrPdfService qrPdfService)
        {
            _emailSender = emailSender;
            _dataFilter = dataFilter;
            _configuration = configuration;
            _userManager = userManager;
            _Environment = Environment;
            _qrCodeService = qrCodeService;
            _qrPdfService = qrPdfService;
        }

        public override async Task ExecuteAsync(EmailSendingArgs args)
       {
            var emailBody = _generateEmailBody(args.Template.ToLower(),"Pharmacy User",args);
            if (args.Template == "Verification")
            {
                await _emailSender.SendAsync(args.TargetEmail, "Verification Link", emailBody , true);
            }

            // add pdf of QR Code

            else if (args.Template == "Welcome")
            {
                Volo.Abp.Identity.IdentityUser user;
                using (_dataFilter.Value.Disable<IMultiTenant>())
                {
                    user = await _userManager.FindByEmailAsync(args.TargetEmail);
                }

                if (user is not null)
                {
                    //var qrPath = await _qrCodeService.GenerateQrCode(args.InstructerId);
                    //var qrCodePdfPath = await _qrPdfService.GenerateQrPdf(args.InstructerId);

                    //EmailAttachment emailAttachment = new EmailAttachment();
                    //emailAttachment.File = System.IO.File.ReadAllBytes(qrCodePdfPath);
                    //emailAttachment.Name = "QR_Code.pdf";

                    //AdditionalEmailSendingArgs additionalEmailSendingArgs = new AdditionalEmailSendingArgs();
                    //additionalEmailSendingArgs.Attachments = [];
                    //additionalEmailSendingArgs.Attachments.Add(emailAttachment);
                    await _emailSender.SendAsync(args.TargetEmail, "Welcome To EduZone", emailBody,true);

                    //if (File.Exists(qrCodePdfPath))
                    //{
                    //    File.Delete(qrCodePdfPath);
                    //    if(File.Exists(qrPath)) File.Delete(qrPath);
                    //}
                }
            }
        }

        private string _generateEmailBody(string type , string name , EmailSendingArgs args)
        {
            FileStream fileStream;
            string path, body = "";
            // read logo from assets in angular app
            //string logo = _configuration.Value["App:AngularUrl"] + _configuration.Value["EmailTemplateSetting:Logolink"];
            //string appName = _configuration.Value["EmailTemplateSetting:AppName"];
            path = Path.Combine(_Environment.WebRootPath, EduZoneConsts.EmailingFolderName,$"{type}.html");
            fileStream = new FileStream(path, FileMode.Open);
            using(StreamReader reader = new (fileStream)) 
            { 
                string file = reader.ReadToEnd();
                body = file; // or for add logo string.Format(file,logo) for replace {0} with logo string in url;
                if (type == "verification")
                {
                    body = body.Replace("ConfirmationLink", args.ConfirmationLink);
                }
                else if (type == "welcome")
                {
                    body = body.Replace("EmailPlaceHolder", args.EmailPlaceHolder);
                    body = body.Replace("PharmacyNamePlaceHolder", args.InstructorNamePlaceHolder);
                    body = body.Replace("TenantPlaceHolder", args.TenantPlaceHolder);
                }
            }
            return body;
        }

    }
}
