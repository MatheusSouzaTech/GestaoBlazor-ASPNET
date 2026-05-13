# Script para configurar acesso via celular
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Configuração de Acesso via Celular" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Descobrir o IP local
$ipAddress = (Get-NetIPAddress -AddressFamily IPv4 -InterfaceAlias "Wi-Fi*" | Where-Object { $_.IPAddress -notlike "169.254.*" } | Select-Object -First 1).IPAddress

if (-not $ipAddress) {
    $ipAddress = (Get-NetIPAddress -AddressFamily IPv4 | Where-Object { $_.IPAddress -like "192.168.*" -or $_.IPAddress -like "10.*" } | Select-Object -First 1).IPAddress
}

if ($ipAddress) {
    Write-Host "✅ IP Local detectado: $ipAddress" -ForegroundColor Green
    Write-Host ""
    Write-Host "📋 URLs para acesso:" -ForegroundColor Yellow
    Write-Host ""
    Write-Host "   Computador (localhost):" -ForegroundColor White
    Write-Host "   • Frontend: http://localhost:5200" -ForegroundColor Cyan
    Write-Host "   • API:      https://localhost:7084" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "   Celular (mesma rede WiFi):" -ForegroundColor White
    Write-Host "   • Frontend: http://${ipAddress}:5200" -ForegroundColor Green
    Write-Host "   • API:      http://${ipAddress}:5047" -ForegroundColor Green
    Write-Host "   • Swagger:  http://${ipAddress}:5047/swagger" -ForegroundColor Magenta
    Write-Host ""

    # Perguntar se deseja atualizar appsettings.json
    $update = Read-Host "Deseja atualizar appsettings.json com este IP? (S/N)"

    if ($update -eq "S" -or $update -eq "s") {
        $appsettingsPath = "GestaoMobile\wwwroot\appsettings.json"

        if (Test-Path $appsettingsPath) {
            $config = @{
                ApiBaseUrl = "http://${ipAddress}:5047"
            }

            $config | ConvertTo-Json | Set-Content $appsettingsPath
            Write-Host ""
            Write-Host "✅ Arquivo $appsettingsPath atualizado!" -ForegroundColor Green
        } else {
            Write-Host ""
            Write-Host "❌ Arquivo não encontrado: $appsettingsPath" -ForegroundColor Red
        }
    }

    Write-Host ""
    Write-Host "📱 Passos para conectar o celular:" -ForegroundColor Yellow
    Write-Host "1. Conecte o celular na mesma rede WiFi" -ForegroundColor White
    Write-Host "2. Execute a API (APIGestão)" -ForegroundColor White
    Write-Host "3. Execute o Frontend (GestaoMobile)" -ForegroundColor White
    Write-Host "4. No celular, abra o navegador e acesse:" -ForegroundColor White
    Write-Host "   http://${ipAddress}:5200" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "💡 Para instalar como app:" -ForegroundColor Yellow
    Write-Host "   Android: Menu → 'Instalar aplicativo'" -ForegroundColor White
    Write-Host "   iOS: Compartilhar → 'Adicionar à Tela de Início'" -ForegroundColor White
    Write-Host ""

    # Verificar firewall
    Write-Host "🔥 Verificando regras de firewall..." -ForegroundColor Yellow
    $firewallRule = Get-NetFirewallRule -DisplayName "ASP.NET Dev*" -ErrorAction SilentlyContinue

    if (-not $firewallRule) {
        $addFirewall = Read-Host "Deseja adicionar regra de firewall para as portas 5047 e 7084? (S/N)"

        if ($addFirewall -eq "S" -or $addFirewall -eq "s") {
            try {
                New-NetFirewallRule -DisplayName "ASP.NET Dev - HTTP" -Direction Inbound -Action Allow -Protocol TCP -LocalPort 5047,5200 -ErrorAction Stop
                New-NetFirewallRule -DisplayName "ASP.NET Dev - HTTPS" -Direction Inbound -Action Allow -Protocol TCP -LocalPort 7084,7200 -ErrorAction Stop
                Write-Host "✅ Regras de firewall adicionadas!" -ForegroundColor Green
            } catch {
                Write-Host "❌ Erro ao adicionar regras. Execute como Administrador." -ForegroundColor Red
            }
        }
    } else {
        Write-Host "✅ Regras de firewall já configuradas" -ForegroundColor Green
    }

} else {
    Write-Host "❌ Não foi possível detectar o IP local" -ForegroundColor Red
    Write-Host "Execute manualmente: ipconfig" -ForegroundColor Yellow
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "📚 Documentação completa: MOBILE-SETUP.md" -ForegroundColor White
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
