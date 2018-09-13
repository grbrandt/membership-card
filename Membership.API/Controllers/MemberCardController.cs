using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Membership.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace Membership.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberCardController : Controller
    {
        private readonly MembershipRepository repository;

        public MemberCardController(MembershipRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GenerateQrImage()
        {
            string secret =
                "{\"name\":\"BennyLongbottoms\",\"validity\":\"2018-09-09 20:39:59Z\",\"cardnumber\":6663399,\"club\":\"SuperSecret Villain Club\",\"sign\":\"AEmnuPW30Ga+RKmeoSGcoHQD7/ErDOahDRMdEcI2loqxutsVi8QbxPJpOXsNGn03I4cIhF9UGo82Grp1HnWVbQ==\"}";

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(secret, QRCodeGenerator.ECCLevel.M);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(6, Color.Black, Color.White, 
                icon: (Bitmap)Image.FromFile(@"c:\temp\devilicon.png"), iconSizePercent: 25);

            MemoryStream stream = new MemoryStream();
            qrCodeImage.Save(stream, ImageFormat.Png);
            stream.Position = 0;

            return new FileStreamResult(stream, "image/png");

        }
    }
}