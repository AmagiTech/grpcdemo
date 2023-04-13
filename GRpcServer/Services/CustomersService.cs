using Grpc.Core;
using GRpcServer;
using System.Collections;

namespace GRpcServer.Services;

public class CustomersService : Customers.CustomersBase
{
    private readonly ILogger<CustomersService> _logger;
    public CustomersService(ILogger<CustomersService> logger)
    {
        _logger = logger;
    }

    public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, Grpc.Core.ServerCallContext context)
    {
        CustomerModel output = new CustomerModel();
        if(request.UserId == 1){
            output.FirstName = "Kimi";
            output.LastName = "Raikkonen";
        }
        else if(request.UserId == 2){
            output.FirstName = "Fernando";
            output.LastName = "Alonso";
        }
        else{
            output.FirstName = "Lewis";
            output.LastName = "Hamilton";
        }

        return Task.FromResult(output);
    }

    public override async Task GetNewCustomers(NewCustomersRequest request, Grpc.Core.IServerStreamWriter<CustomerModel> responseStream, Grpc.Core.ServerCallContext context)
    {
        var customers = new List<CustomerModel>(){
            new CustomerModel(){
                FirstName = "Kimi",
                LastName = "Raikkonen",
            },
            new CustomerModel(){
                FirstName = "Fernando",
                LastName = "Alonso",
            },
            new CustomerModel(){
                FirstName = "Lewis",
                LastName = "Hamilton",
            }
        };

        foreach(var customer in customers){
            await responseStream.WriteAsync(customer);
        } 
    }
}
