var builder = WebApplication.CreateBuilder(args);

// 1. Добавляем контроллеры
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// 2. ПРАВИЛЬНАЯ РЕГИСТРАЦИЯ CORS (Должна быть строго в секции builder.Services)
builder.Services.AddCors(options => 
{
    options.AddPolicy("AllowElectron", policy => 
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 3. Собираем приложение без лишних аргументов внутри метода Build()
var app = builder.Build();

// Настройка для режима разработки
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// 4. КОММЕНТИРУЕМ ИЛИ УДАЛЯЕМ HTTPS РЕДИРЕКТ
// Электрон работает локально по http://, редирект на https будет ломать fetch запросы
// app.UseHttpsRedirection();

// 5. ПОДКЛЮЧАЕМ CORS (Строго ДО того, как маппятся контроллеры)
app.UseCors("AllowElectron");

app.UseAuthorization();
app.MapControllers();

// Жестко запускаем C# бэкенд на порту 5000
app.Run("http://localhost:5000");
