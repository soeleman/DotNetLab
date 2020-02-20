using System;
using System.Threading.Tasks;

using Microsoft.JSInterop;

namespace Soeleman.Rcl
{
    public static class ExampleJsInterop
    {
        public static ValueTask<string> Prompt(
            IJSRuntime jsRuntime, 
            string message)
        {
            // Implemented in exampleJsInterop.js
            return jsRuntime.InvokeAsync<string>(
                "exampleJsFunctions.showPrompt",
                message);
        }

        public static string Status = "Failed";

        [JSInvokable("SayHello")]
        public static void SayHello(){
            Status = "Called";
        }

        [JSInvokable]
        public static Task GetHelloMessage()
        {
            var message = $"Hello from C# @ {DateTime.Now.ToLongTimeString()}";
            return Task.FromResult(message);
        }
    }
}