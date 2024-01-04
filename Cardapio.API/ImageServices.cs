namespace Cardapio.API
{
    public class ImageServices
    {
        public static IWebHostEnvironment hostEnvironment;
        public ImageServices(IWebHostEnvironment environment)
        {
            hostEnvironment = environment;
        }
        public async Task<string> AddImageAsync(string image)
        {
            if (string.IsNullOrEmpty(image))
            {
                return null;
            }
            byte[] bytes = Convert.FromBase64String(image);
            if (bytes.Length > 1000000)
            {
                return null;
            }
            string directory = $"{hostEnvironment.ContentRootPath}/images";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string file = $"{directory}/{Guid.NewGuid()}.jpg";
            using (FileStream stream = new FileStream(file, FileMode.Create))
            {
                stream.Write(bytes, 0, bytes.Length);
            }
            return await Task.FromResult($"{file}");
        }
    }
}
