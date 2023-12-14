job("Qodana") {
   container("jetbrains/qodana-dotnet:2023.3-eap") {
      env["QODANA_TOKEN"] = Secrets("qodana-token")
      shellScript {
         content = """qodana"""
      }
   }
}