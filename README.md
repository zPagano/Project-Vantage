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

**Project Vantage** is an enterprise-grade, game-agnostic eSports intelligence platform. Initially focused on the League of Legends ecosystem, the architecture is strictly designed to isolate game-specific data through an Anti-Corruption Layer (ACL), ensuring seamless future expansion to other competitive titles.

## 🏗 System Architecture

Vantage is built on a strictly isolated microservices architecture, utilizing **.NET 10**, **Blazor WebAssembly**, and **.NET Aspire** for local orchestration and telemetry.

### Core Principles
1. **Multi-Tenancy via RLS:** All tenant data is strictly isolated at the SQL Server engine level using Row-Level Security (RLS) and `SESSION_CONTEXT()`.
2. **Zero-Trust AI:** Any Retrieval-Augmented Generation (RAG) or AI inference must pass through cryptographic tenant boundary checks before accessing the Normalized Player Intelligence (NPI) ledger.
3. **Anti-Corruption Layer (ACL):** External vendor APIs (e.g., Riot Games) must never dictate internal domain schemas. All ingress data is mapped to standard internal DTOs.
4. **RFC 9457 Global Error Handling:** All APIs conform to the Problem Details standard for HTTP APIs.

## 📂 Repository Structure

The repository enforces strict physical boundaries between bounded contexts:

* `/src` - Contains all microservices, API Gateways (BFF), and UI Clients.
* `/tests` - Mirrors the `/src` directory for Unit, Integration, and Architecture tests.
* `/infrastructure` - Deployment manifests, Azure Container Apps configurations, and CI/CD pipelines.
* `/docs` - Architecture Decision Records (ADRs) and onboarding documentation.

## 🚀 Development Stack
* **Backend:** C# 12, .NET 10 Web API  
* **Frontend:** Blazor WebAssembly (WASM)  
* **Database:** SQL Server 2025 (Entity Framework Core)  
* **Orchestration:** .NET Aspire  
* **Observability:** OpenTelemetry (OTLP)  

---

## 📬 Contact & Inquiries

For licensing questions, collaboration proposals, security disclosures, bug reports, feature requests, or any professional inquiry related to Project Vantage, please refer to the full inquiry system:

👉 **[Inquiry Templates](./docs/inquiry-templates.md)**

This includes:

- Fully localized templates (EN, PT‑BR, ES, FR)  
- Tailored email bodies for each inquiry type  
- A flowchart to help you choose the correct inquiry path  

If you prefer direct contact, you may also email:

📧 **lucaspaganopolisel@gmail.com**

---

## 📞 Additional Contact

**Website:** https://zpagano.github.io/  
**LinkedIn:** https://www.linkedin.com/in/lucas-pagano-polisel/

---

## 📄 License

This project is proprietary. See the [LICENSE](LICENSE) file for details.
