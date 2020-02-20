// This file is to show how a library package may provide JavaScript interop features
// wrapped in a .NET API

window.exampleJsFunctions = {
  showPrompt: function (message) {
    return prompt(message, "...");
  }
};

window.WriteCSharpMessageToConsole = () => {
    DotNet
        .invokeMethodAsync("Soeleman.Rcl", "GetHelloMessage")
        .then(message => {
            console.log(message);
        });
};

// Error since blazor not fully load. see issue #14011
(function () {
	if (window.DotNet) {
		console.log("window.DotNet is set");
		if (window.DotNet.invokeMethodAsync) {
			console.log("window.DotNet.invokeMethodAsync is set");
            DotNet
                .invokeMethodAsync("Soeleman.Rcl", "GetHelloMessage")
                .then(message => {
                    console.log(message);
                });
		}
	}
})();