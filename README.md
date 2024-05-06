

# .NET Nedir?
.NET, Microsoft tarafından geliştirilen, farklı türde uygulama oluşturmayı sağlayan bir yazılım geliştirme platformudur.

# .NET Türleri Nelerdir?
.NET Framework
.NET Framework, Windows işletim sisteminde çalışan web siteleri, masaüstü uygulamaları ve daha fazlasını destekleyen .NET uygulamasıdır.

 .NET Core
.NET geliştiricileri için platformlar arası destek sağlamak üzere geliştirilmiş, Windows, Linux ve macOS’ta çalışan ürünler için kullanılan çapraz platformdur.

# .NET SDK
Geliştiricilerin .NET uygulamaları ve kitaplıkları oluşturmak için kullandığı bir kitaplık ve araç kümesidir.

# API (Application Programming Interface) Nedir?
Var olan uygulamamızı dış dünyaya açmamız ve kullanıcıların belirli isteklerle uygulamamızdaki veriler üzerinden çeşitli işlemler yapabilmesine yardımcı olan teknolojidir.

# WebAPI Nedir?
HTTP protokolü üzerinden RESTful web hizmetleri oluşturmak için kullanılan bir mimari yaklaşımdır.

# WebAPI metotları

### GET:
Verileri almak - listelemek için kullanılan istek metodudur.
Browserlar üzerinden çağrılabilir. Parametre olarak Primitive tipler kullanılır (Boolean, Byte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64, IntPtr, UIntPtr, Char, Double, and Single).
İstek gövdesi yoktur ve veri göndermez.
Cevap olarak bir kaynak veya durum kodu dönebilir. 

### POST:
Yeni bir kaynak oluşturmak veya var olan bir kaynağı güncellemek için kullanılır.
İstek gövdesinde veri taşır.
Genellikle form verileri veya JSON gibi veri formatlarıyla kullanılır.
Belirli bir kaynağa veri göndermek için kullanılır.

### PUT:
Belirtilen URI'deki kaynağı oluşturmak veya güncellemek için kullanılır.
İstek gövdesinde taşınan veri, kaynağın tamamını temsil eder.
Belirli bir kaynaktaki verinin tamamının değiştirilmesi için kullanılan metodtur.

### DELETE:
Belirtilen URI'daki kaynağı silmek için kullanılır.
İstek gövdesi olmaz veya boştur.
Belirli bir kaynaktaki verilerin silinmesi için kullanılan metoddur.

# Http Durum Kodları
1xx - Bilgi:
100 Continue: İstemcinin, devam etmesi durumunda bir isteğin tamamlanabileceğini bildiren bir yanıt.

2xx - Başarılı:
200 OK: İstek başarıyla gerçekleştirildi.
201 Created: İstekle yeni bir kaynak oluşturuldu.
204 No Content: İstek başarılı olsa da yanıt içerik taşımaz.

3xx - Yönlendirme:
301 Moved Permanently: Kaynak, kalıcı olarak başka bir URI'a taşındı.
302 Found: Kaynak, geçici olarak başka bir URI'a taşındı.
304 Not Modified: Kaynak, istemcinin önbelleklenmiş bir sürümüne dayalı olarak güncellenmedi.

4xx - İstemci Hatası:
400 Bad Request: İstek yapısal olarak yanlış veya anlaşılamaz.
401 Unauthorized: Kimlik doğrulama gerekiyor veya başarısız oldu.
403 Forbidden: İstemci, kaynağa erişim iznine sahip değil.
404 Not Found: Belirtilen URI'daki kaynak bulunamadı.
405 Method Not Allowed: Belirtilen metot, bu kaynağa uygulanamaz.
429 Too Many Requests: İstemci, belirli bir süre içinde çok fazla istek gönderdi.

5xx - Sunucu Hatası:
500 Internal Server Error: Sunucu genel bir hata ile karşılaştı.
501 Not Implemented: Sunucu, isteği yerine getirmek için gerekli yeteneklere sahip değil.
503 Service Unavailable: Sunucu şu anda hizmet veremiyor. Geçici olarak bakım modunda olabilir veya aşırı yük altında olabilir.
Bu durum kodları, bir HTTP yanıtının genel durumunu belirtir. Ancak, bu kodların her biri daha spesifik durumları ifade edebilir ve belirli durumlar için uygun olan kodlar değişebilir. Bu durum kodlarını doğru bir şekilde anlamak, web uygulamalarının hata ayıklamasını ve hata yönetimini geliştirmek açısından önemlidir.

# Controller | ActionResult
### Controller
.NET Core Web API'da controller, istemciden gelen istekleri alarak işleyen ve uygun yanıtları oluşturan katmandır. 

### ActionResult
Bir Web API eyleminin sonucunu temsil eden bir sınıftır. ActionResult, HTTP yanıtlarını oluşturmak için kullanılır ve çeşitli durumları temsil eder.
Bu sınıf, Web API eylemlerinin dönüş değeri olarak kullanılır ve istemciye gönderilen yanıtın türünü belirtir.
ActionResult sınıfı, genellikle HTTP yanıtlarını belirlemek için kullanılan farklı statü kodlarıyla birlikte kullanılır.
Örneğin, Ok(), NotFound(), BadRequest(), Created(), NoContent(), gibi çeşitli statü kodlarına sahiptir.

# Query Params
.NET Core Web API'da, query parametreleri, HTTP isteğinin URL'sine eklenen ve sunucuya iletilen veri parçalarıdır. Query parametreleri, ? karakterinden sonra gelir.
Genellikle bir istemci tarafından sunucuya veri göndermek veya sunucudan belirli verileri almak için kullanılırlar.
```
https://example.com/api/users?id=123&name=John&age=30 => query params
[HttpGet]
public IActionResult GetUser(int id,string name,int age)
{
    return Ok("API is working...");
}
```

# Route Params
WebAPI'lerde URL'nin bir parçası olarak gönderilen parametrelerdir.
.NET Core Web API'de route parametreleri, HTTP isteklerinde URL'nin parçası olarak gönderilen parametrelerdir. 
```
https://example.com/api/users/{id}
[HttpGet("users/{id}")]
public IActionResult GetUser(int id)
{
    // Id değerini kullanarak belirli bir kullanıcıyı almak için işlemler yapılır.
    return Ok();
}
```


# Body
.NET Core Web API'de "request body" (isteğin gövdesi) ve "response body" (yanıtın gövdesi), HTTP isteklerinin ve yanıtlarının içinde bulunan veri parçalarını ifade eder.

Request Body (İsteğin Gövdesi): İstemci tarafından sunucuya gönderilen verileri içerir. Genellikle HTTP POST, PUT veya PATCH gibi isteklerde kullanılır.
Veri, JSON, XML, form verileri veya başka bir formatta olabilir.

Response Body (Yanıtın Gövdesi): Sunucunun istemciye yanıt olarak gönderdiği verileri içerir. Genellikle HTTP yanıtlarında kullanılır. Yanıtın içeriği, isteğin türüne ve amacına bağlı olarak değişir.
Örneğin, bir GET isteği sonucunda bir liste veri döndüren bir Web API endpointi, bu listeyi JSON formatında response body içinde gönderebilir.

# HttpContext, HttpContextAccessor

### HttpContext
ASP.NET Core uygulamalarında, bir HTTP isteği sırasında o anki isteğe ve yanıta ilişkin bilgileri içeren bir sınıftır.

### HttpContextAccessor
ASP.NET Core uygulamalarında HttpContext nesnesine diğer katmanlardan veya sınıflardan erişilmesini sağlayan yardımcı bir sınıftır. 

# 2.Gün
CORS Policy
Rate Limiting
Asenkron metotlar ve Cancellation Token
Health Check
Personel Kayıt projesi


# Cross-origin resource sharing (CORS)
ASP.NET Core Web API'de CORS politikası, farklı kaynaklar gelen istekleri kabul etmek veya reddetmek için kullanılan bir güvenlik politikasıdır.
Web API sunucusunda hangi kaynakların hangi isteklerden erişilebileceğini belirlemek için kullanılır.
```
builder.Services.AddCors(action =>
{
    action.AddDefaultPolicy(options =>
    {
        options.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});
app.UseCors();
```
```
builder.Services.AddCors(action =>
{
    action.AddPolicy("CustomCorsPolicy",options =>
    {
        options
		.WithOrigins("http://localhost:4500") //  ‘WithOrigins’ metodu sadece belirtilen clientlardan gelen istekleri karşılar.
        .AllowAnyMethod()
        .AllowAnyHeader();
		//.AllowCredentials(); SignalR kullanılıyorsa bu özelliği aktif etmemiz gerekmektedir.
    });
});
app.UseCors("CustomCorsPolicy");
```

# CancellationToken
Bir işlemi başlatan bir kod bloğu, işlem tamamlanmadan önce kullanıcı veya başka bir sistem olayı tarafından iptal edilirse, işlemi sonlandırmak için kullanılır.


# RateLimit
Bir Web API'nin belirli bir zaman aralığında alabileceği istek sayısını veya bir istemcinin belirli bir zaman aralığında gönderebileceği istek sayısını sınırlayan bir mekanizmadır.
Rate Limiting, genellikle API'lerin aşırı kullanımını önlemek, istemcilerin istekleri kötüye kullanmasını engellemek veya hizmetin kötüye kullanımına karşı korumak için kullanılır.
```
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("limiter", configure =>
    {
        configure.PermitLimit = 100;
        configure.Window = TimeSpan.FromSeconds(1); // 1 Saniyede maksimum 100 istek kabul et
        configure.QueueLimit = 100; // 1 saniyede 100 den fazla istek gelirse bunların ilk 100 tanesini kuyruğa ekle
        configure.QueueProcessingOrder = QueueProcessingOrder.OldestFirst; // Kuyruktaki istekleri eskiden yeniye doğru sırala
    });
});
app.MapControllers().RequireRateLimiting("limiter"); // bütün endpointlerde uygulamak için bunu ekliyoruz.
```


# API Health Check
Health Check, uygulamanın çalışma durumunu ve sağlığını izlemek için kullanılan bir mekanizmadır.
Bu mekanizma, uygulamanın canlılığını izlemek, hizmet kalitesini sağlamak ve sistem sorunlarını belirlemek için kullanılır.
```
builder.Services.AddHealthChecks().AddCheck("apiInformation", () => HealthCheckResult.Healthy());
app.UseHealthChecks("/healthcheck", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
```
