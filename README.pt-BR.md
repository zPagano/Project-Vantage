# Project Vantage

[![English](https://img.shields.io/badge/English-EN-blue)](README.md)
[![Português](https://img.shields.io/badge/Português-BR-green)](README.pt-BR.md)
[![Español](https://img.shields.io/badge/Español-ES-yellow)](README.es.md)
[![Français](https://img.shields.io/badge/Français-FR-lightgrey)](README.fr.md)

[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![C# 12](https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=csharp)](https://learn.microsoft.com/dotnet/csharp/)
[![Blazor WASM](https://img.shields.io/badge/Blazor-WASM-512BD4?style=for-the-badge&logo=blazor)](https://learn.microsoft.com/aspnet/core/blazor/)
[![.NET Aspire](https://img.shields.io/badge/.NET-Aspire-5C2D91?style=for-the-badge&logo=dotnet)](https://learn.microsoft.com/dotnet/aspire/)
[![CI Status](https://img.shields.io/github/actions/workflow/status/zPagano/Project-Vantage/ci.yml?branch=main&style=for-the-badge)](https://github.com/zPagano/ProjectVantage/actions)
[![License](https://img.shields.io/badge/License-Proprietary-red?style=for-the-badge)](LICENSE)

---

**Project Vantage** é uma plataforma de inteligência eSports de nível empresarial, independente de jogo. Inicialmente focada no ecossistema de League of Legends, sua arquitetura foi projetada para isolar completamente dados específicos de cada jogo por meio de uma Anti-Corruption Layer (ACL), garantindo expansão futura para outros títulos competitivos.

## 🏗 Arquitetura do Sistema

O Vantage é construído sobre uma arquitetura de microsserviços rigidamente isolada, utilizando **.NET 10**, **Blazor WebAssembly** e **.NET Aspire** para orquestração local e telemetria.

### Princípios Centrais
1. **Multi-Tenancy via RLS:** Todos os dados de tenants são isolados no nível do SQL Server usando Row-Level Security (RLS) e `SESSION_CONTEXT()`.
2. **Zero-Trust AI:** Qualquer inferência de IA ou RAG deve passar por verificações criptográficas de fronteira de tenant antes de acessar o ledger Normalized Player Intelligence (NPI).
3. **Anti-Corruption Layer (ACL):** APIs externas (ex.: Riot Games) nunca ditam os esquemas internos. Todos os dados recebidos são mapeados para DTOs internos padronizados.
4. **RFC 9457 Global Error Handling:** Todas as APIs seguem o padrão Problem Details para APIs HTTP.

## 📂 Estrutura do Repositório

O repositório aplica limites físicos rígidos entre bounded contexts:

* `/src` - Microsserviços, API Gateways (BFF) e clientes UI.
* `/tests` - Espelha `/src` para testes Unitários, de Integração e Arquitetura.
* `/infrastructure` - Manifests de deploy, configurações do Azure Container Apps e pipelines CI/CD.
* `/docs` - Architecture Decision Records (ADRs) e documentação de onboarding.

## 🚀 Stack de Desenvolvimento
* **Backend:** C# 12, .NET 10 Web API  
* **Frontend:** Blazor WebAssembly (WASM)  
* **Banco de Dados:** SQL Server 2025 (Entity Framework Core)  
* **Orquestração:** .NET Aspire  
* **Observabilidade:** OpenTelemetry (OTLP)  

---

## 📬 Contato & Solicitações

Para dúvidas de licenciamento, propostas de colaboração, divulgações de segurança, relatos de bugs, solicitações de funcionalidades ou qualquer contato profissional relacionado ao Project Vantage, consulte o sistema completo de solicitações:

👉 **[Modelos de Solicitação](./docs/inquiry-templates.md)**

Este documento inclui:

- Modelos totalmente localizados (EN, PT‑BR, ES, FR)  
- Conteúdos personalizados para cada tipo de solicitação  
- Um fluxo estruturado para escolher o tipo correto de contato  

Se preferir contato direto, envie um e‑mail para:

📧 **lucaspaganopolisel@gmail.com**

---

## 📞 Contato Adicional

**Website:** https://zpagano.github.io/  
**LinkedIn:** https://www.linkedin.com/in/lucas-pagano-polisel/

---

## 📄 Licença

Este projeto é proprietário. Consulte o arquivo [LICENSE](LICENSE) para mais detalhes.
