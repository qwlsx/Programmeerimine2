$ErrorActionPreference = "Stop"

dotnet test .\KooliProjekt.Application.UnitTests\KooliProjekt.Application.UnitTests.csproj `
    --collect:"XPlat Code Coverage" `
    --settings .\coverlet.runsettings `
    --results-directory .\TestResults

if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
