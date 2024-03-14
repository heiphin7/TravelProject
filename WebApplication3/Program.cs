using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using System;

namespace YourNamespace
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // ƒобавл€ем сервис дл€ обработки запросов и работе с PostgreSQL
            services.AddControllersWithViews();
            services.AddScoped<NpgsqlConnection>(_ => new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=admin;Database=tourismDatabase"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            // ƒобавл€ем обработчики запросов дл€ отправки данных из формы
            app.Map("/submit-form", HandleSubmitForm);
            app.Map("/submit-contact", HandleSubmitContact);
            app.Map("/package-details.html", HandlePackageDetails);
        }

        private static void HandleSubmitForm(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var request = context.Request;

                if (request.Method == "POST")
                {
                    var form = await request.ReadFormAsync();
                    var name = form["name"];
                    var subject = form["subject"];
                    var message = form["message"];

                    if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(message))
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsync("¬се пол€ должны быть заполнены");
                        return;
                    }

                    using (var connection = app.ApplicationServices.GetRequiredService<NpgsqlConnection>())
                    {
                        await connection.OpenAsync();
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = "INSERT INTO tourismTable (name, subject, message) VALUES (@name, @subject, @message)";
                            command.Parameters.AddWithValue("@name", name);
                            command.Parameters.AddWithValue("@subject", subject);
                            command.Parameters.AddWithValue("@message", message);
                            await command.ExecuteNonQueryAsync();
                        }
                    }

                    context.Response.StatusCode = StatusCodes.Status201Created;
                    await context.Response.WriteAsync("ƒанные успешно сохранены в базе данных");
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;
                }
            });
        }

        private static void HandleSubmitContact(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var request = context.Request;

                if (request.Method == "POST")
                {
                    var form = await request.ReadFormAsync();
                    var firstName = form["firstName"];
                    var lastName = form["lastName"];
                    var email = form["email"];
                    var phone = form["phone"];
                    var departureDate = form["departureDate"];
                    var arrivalDate = form["arrivalDate"];
                    var notes = form["notes"];

                    if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(departureDate) || string.IsNullOrEmpty(arrivalDate) || string.IsNullOrEmpty(notes))
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsync("¬се пол€ должны быть заполнены");
                        return;
                    }

                    using (var connection = app.ApplicationServices.GetRequiredService<NpgsqlConnection>())
                    {
                        await connection.OpenAsync();
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = "INSERT INTO contactTable (first_name, last_name, email, phone, departure_date, arrival_date, notes) VALUES (@firstName, @lastName, @email, @phone, @departureDate, @arrivalDate, @notes)";
                            command.Parameters.AddWithValue("@firstName", firstName);
                            command.Parameters.AddWithValue("@lastName", lastName);
                            command.Parameters.AddWithValue("@email", email);
                            command.Parameters.AddWithValue("@phone", phone);
                            command.Parameters.AddWithValue("@departureDate", departureDate);
                            command.Parameters.AddWithValue("@arrivalDate", arrivalDate);
                            command.Parameters.AddWithValue("@notes", notes);
                            await command.ExecuteNonQueryAsync();
                        }
                    }

                    context.Response.StatusCode = StatusCodes.Status201Created;
                    await context.Response.WriteAsync(" онтактные данные успешно сохранены в базе данных");
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;
                }
            });
        }

        private static void HandlePackageDetails(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
            var request = context.Request;

            if (request.Method == "POST")
            {
                var form = await request.ReadFormAsync();
                    // ќбработка данных из формы package-details.html
                    // «десь можно выполнить различные действи€ с полученными данными, например, сохранить их в базе
                    // ¬ данном примере просто отправл€ем ответ об успешном получении данных
                    context.Response.Redirect("/");
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;
                }
            });
        }
    }
