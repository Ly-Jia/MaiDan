using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using MaiDan.Service.Business;

namespace MaiDan.Infrastructure.ServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            System.ServiceModel.ServiceHost waiterHost = null;

            try
            {
                waiterHost = new System.ServiceModel.ServiceHost(typeof(Waiter));

                ServiceEndpoint productEndpoint = waiterHost.AddServiceEndpoint(typeof(Waiter),
                  new NetTcpBinding(), "net.tcp://localhost:9010/Waiter");

                waiterHost.Faulted += new EventHandler(WaiterHost_Faulted);

                waiterHost.Open();

                Console.WriteLine("The Product service is running and is listening on:");
                Console.WriteLine("{0} ({1})", productEndpoint.Address.ToString(), productEndpoint.Binding.Name);
                Console.WriteLine("\nPress any key to stop the service.");
                Console.ReadKey();
            }

            finally
            {
                if (waiterHost.State == CommunicationState.Faulted)
                {
                    waiterHost.Abort();
                }
                else
                {
                    waiterHost.Close();
                }
            }
        }

        static void WaiterHost_Faulted(object sender, EventArgs e)
        {
            Console.WriteLine("The WaiterService host has faulted.");
        }

    }
}
