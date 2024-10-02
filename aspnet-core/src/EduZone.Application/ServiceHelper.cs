using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EduZone
{
    public static class ServiceHelper
    {
        public static string GetUsernameBeforeAt(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return string.Empty; // Return empty if email is null or empty
            }

            int atIndex = email.IndexOf('@');
            if (atIndex > 0)
            {
                return email.Substring(0, atIndex); // Extract substring before '@'
            }

            return string.Empty; // Return empty if '@' not found or is at the start
        }

        public static bool IsPasswordComplex(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false; // Password should not be null or empty
            }

            // Check length
            if (password.Length < 8)
            {
                return false;
            }

            // Check for at least one uppercase letter
            if (!password.Any(char.IsUpper))
            {
                return false;
            }

            // Check for at least one lowercase letter
            if (!password.Any(char.IsLower))
            {
                return false;
            }

            // Check for at least one digit
            if (!password.Any(char.IsDigit))
            {
                return false;
            }

            // Check for at least one special character
            var specialChars = new Regex(@"[!@#$%^&*(),.?""{}|<>]");
            if (!specialChars.IsMatch(password))
            {
                return false;
            }

            return true; // All checks passed, password is complex enough
        }

        public static string _generateCustomStudentId()
        {
            const string AllowedChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const int CodeLength = 6;
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] randomByte = new byte[CodeLength];
                rng.GetBytes(randomByte);
                char[] chars = new char[CodeLength];
                for (int i = 0; i < CodeLength; i++)
                {
                    int index = randomByte[i] % AllowedChars.Length;
                    chars[i] = AllowedChars[index];
                }
                return new string(chars);
            }
        }

        public static long? getTimeSpam(DateTime? dt)
        {
            if (dt.HasValue)
            {
                return ((DateTimeOffset)dt).ToUnixTimeSeconds();
            }
            return null;
        }

        public static string GetThumbNail(string iconBase64)
        {
            int maxWidth = 50, maxHeight = 50;
            var icon = Convert.FromBase64String(iconBase64);
            using (MemoryStream ms = new MemoryStream(icon))
            {
                Image image = Image.FromStream(ms);

                // Calculate new distanation for the thumbnail:
                int newWidth, newHeight;
                if (image.Width > image.Height)
                {
                    newWidth = maxHeight;
                    newHeight = (int)((double)image.Height / image.Width * maxWidth);
                }
                else
                {
                    newHeight = maxHeight;
                    newWidth = (int)((double)image.Width / image.Height * maxHeight);
                }

                // Create thumbnail image:
                Image thumnail = new Bitmap(newWidth, newHeight);
                using (Graphics graphic = Graphics.FromImage(thumnail))
                {
                    graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    graphic.DrawImage(image, 0, 0, newWidth, newHeight);
                }

                // Convert thumbnail image to base64:

                using (MemoryStream msThumbnail = new MemoryStream())
                {
                    thumnail.Save(msThumbnail, ImageFormat.Jpeg);
                    byte[] thumbnailByte = msThumbnail.ToArray();
                    return Convert.ToBase64String(thumbnailByte);
                }
            }

        }
    }
}
