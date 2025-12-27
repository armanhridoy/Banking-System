using Microsoft.EntityFrameworkCore;
using Smart_Banking_system.ApplicationDB;
using Smart_Banking_system.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));


builder.Services.AddScoped<ICustomerRepository , CustomerRepository>();
builder.Services.AddScoped<IBankAccountRepository , BankaccountRepository>();
builder.Services.AddScoped<ITransactionRepository , TransactionRepository>();
builder.Services.AddScoped<IBranchRepository , BranchRepository>(); 
builder.Services.AddScoped<IEmployeeRepository , EmployeeRepository>();
builder.Services.AddScoped<ILoanRepository , LoanRepository>();
builder.Services.AddScoped<ILoanPaymentRepository , LoanPaymentRepository>();
builder.Services.AddScoped<IAccountTypeRepository , AccountTypeRepository>();
builder.Services.AddScoped<INotificationRepository , NotificationRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
  name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
