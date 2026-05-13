# 📱 Como Acessar no Celular

## Opção 1: Mesma Rede WiFi (Recomendado para desenvolvimento)

### Passo 1: Descobrir o IP do seu computador
Execute no PowerShell:
```powershell
ipconfig
```
Procure por "Endereço IPv4" (geralmente algo como `192.168.1.X` ou `10.0.0.X`)

### Passo 2: Configurar a API
Edite `GestaoMobile\wwwroot\appsettings.json` e substitua `localhost` pelo IP:
```json
{
  "ApiBaseUrl": "https://192.168.1.X:7084"
}
```
(Substitua `192.168.1.X` pelo seu IP real)

### Passo 3: Executar a aplicação
1. Execute a API (APIGestão)
2. Execute o frontend (GestaoMobile)
3. No celular, conecte-se à mesma rede WiFi
4. Acesse no navegador do celular: `http://192.168.1.X:5200` (substitua pelo seu IP)

### Passo 4: Instalar como App (PWA)
No navegador do celular:
- **Chrome/Edge Android**: Menu → "Instalar aplicativo" ou "Adicionar à tela inicial"
- **Safari iOS**: Compartilhar → "Adicionar à Tela de Início"

---

## Opção 2: Publicar na Nuvem (Produção)

### Azure App Service
1. Publique a API no Azure App Service
2. Publique o Blazor WASM em Azure Static Web Apps ou Blob Storage
3. Configure o `appsettings.json` com a URL da API publicada

### Outras opções
- **IIS (Windows Server)**
- **Docker + Kubernetes**
- **Google Cloud / AWS**

---

## Problemas Comuns

### ❌ Certificado HTTPS inválido
**Solução**: Em desenvolvimento, use HTTP ou configure um certificado confiável

### ❌ Firewall bloqueando
**Solução Windows**: 
```powershell
netsh advfirewall firewall add rule name="ASP.NET Dev" dir=in action=allow protocol=TCP localport=5047,7084
```

### ❌ CORS bloqueando requisições
**Solução**: Já configurado em `Program.cs` para aceitar qualquer origem em desenvolvimento

---

## Testando

### Computador
```
http://localhost:5200
```

### Celular (mesma rede)
```
http://[SEU-IP]:5200
```

### Verificar se a API está acessível
No navegador do celular:
```
http://[SEU-IP]:5047/swagger
```
