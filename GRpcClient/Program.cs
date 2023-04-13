using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using GRpcServer;

var channel = GrpcChannel.ForAddress("http://localhost:5275");
var client = new Greeter.GreeterClient(channel);


var input = new HelloRequest(){
    Name = "Amagi"
};
var reply = await client.SayHelloAsync(input);

Console.WriteLine(reply.Message);


var customersClient = new Customers.CustomersClient(channel);
var customerRequest = new CustomerLookupModel{ UserId = 2 };
var customer = await customersClient.GetCustomerInfoAsync(customerRequest);
Console.WriteLine($"{customer.FirstName} {customer.LastName}");

System.Console.WriteLine();
System.Console.WriteLine("NEW CUSTOMERS");


using(var call = customersClient.GetNewCustomers(new NewCustomersRequest())){
    while(await call.ResponseStream.MoveNext()){
        var currentCustomer = call.ResponseStream.Current;
        Console.WriteLine($"{currentCustomer.FirstName} {currentCustomer.LastName}");
    }
}

var countriesClient = new Countries.CountriesClient(channel);
var countriesRequest = new Empty();
var countriesReply = await countriesClient.GetCountriesAsync(countriesRequest);
foreach (var country in countriesReply.Countries)
{
    Console.WriteLine(country);
}


Console.ReadLine();

