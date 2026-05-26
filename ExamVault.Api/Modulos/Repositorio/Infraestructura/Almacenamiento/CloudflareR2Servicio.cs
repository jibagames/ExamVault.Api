using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using ExamVault.Api.Modulos.Repositorio.Aplicacion.Interfaces;

namespace ExamVault.Api.Modulos.Repositorio.Infraestructura.Almacenamiento
{
    public class CloudflareR2Servicio : IServicioAlmacenamiento
    {
        private readonly AmazonS3Client _clienteS3;
        private readonly string _nombreBucket;

        public CloudflareR2Servicio(IConfiguration configuracion)
        {
            var accessKey = configuracion["CloudflareR2:AccessKey"] ?? throw new ArgumentNullException("CloudflareR2:AccessKey no configurado");
            var secretKey = configuracion["CloudflareR2:SecretKey"] ?? throw new ArgumentNullException("CloudflareR2:SecretKey no configurado");
            var accountId = configuracion["CloudflareR2:AccountId"] ?? throw new ArgumentNullException("CloudflareR2:AccountId no configurado");

            _nombreBucket = configuracion["CloudflareR2:BucketName"] ?? throw new ArgumentNullException("CloudflareR2:BucketName no configurado");

            var credenciales = new BasicAWSCredentials(accessKey, secretKey);

            var s3Configuracion = new AmazonS3Config
            {
                ServiceURL = $"https://{accountId}.r2.cloudflarestorage.com",
                AuthenticationRegion = "auto"
            };

            _clienteS3 = new AmazonS3Client(credenciales, s3Configuracion);
        }

        public async Task<string> SubirArchivoAsync(Stream flujoArchivo, string nombreArchivo, string tipoContenido)
        {
            var llaveObjeto = $"{Guid.NewGuid()}-{nombreArchivo.Replace(" ", "_")}";

            var peticion = new PutObjectRequest
            {
                BucketName = _nombreBucket,
                Key = llaveObjeto,
                InputStream = flujoArchivo,
                ContentType = tipoContenido,
                DisablePayloadSigning = true
            };

            var respuesta = await _clienteS3.PutObjectAsync(peticion);

            if (respuesta.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"Fallo al subir el archivo a Cloudflare R2. Estado: {respuesta.HttpStatusCode}");
            }

            return llaveObjeto;
        }

        public async Task<bool> EliminarArchivoAsync(string llaveObjeto)
        {
            var peticion = new DeleteObjectRequest
            {
                BucketName = _nombreBucket,
                Key = llaveObjeto
            };

            var respuesta = await _clienteS3.DeleteObjectAsync(peticion);

            return respuesta.HttpStatusCode == System.Net.HttpStatusCode.NoContent ||
                   respuesta.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }

        public string GenerarUrlPreFirmada(string llaveObjeto, int minutosExpiracion = 15)
        {
            var peticion = new GetPreSignedUrlRequest
            {
                BucketName = _nombreBucket,
                Key = llaveObjeto,
                Expires = DateTime.UtcNow.AddMinutes(minutosExpiracion),
                Verb = HttpVerb.GET,
                Protocol = Protocol.HTTPS
            };

            return _clienteS3.GetPreSignedURL(peticion);
        }
    }
}