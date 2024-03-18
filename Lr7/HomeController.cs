using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    public class FileController : Controller
    {
        [HttpGet]
        public IActionResult DownloadFile()
        {
            return View("~/wwwroot/DownloadFile.html");
        }

        [HttpPost]
        public async Task<IActionResult> DownloadFile(string firstName, string lastName, string fileName)
        {
            // Перевірка наявності введених даних
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(fileName))
            {
                ViewBag.Message = "Будь ласка, заповніть всі поля";
                return View();
            }

            // Побудова шляху до файлу
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName + ".txt");

            // Запис інформації у файл
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                await writer.WriteLineAsync($"Ім'я: {firstName}");
                await writer.WriteLineAsync($"Прізвище: {lastName}");
            }

            // Повернення файлу користувачеві
            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, "text/plain", $"{fileName}.txt");
        }
    }
}