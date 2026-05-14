# Project Vantage

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

## 📞 Contact

For inquiries related to licensing, collaboration, security disclosures, or professional communication, please use the contact information below:

**Email:** `your-email@example.com`  
**Website:** `https://your-website-here.com`  
**LinkedIn:** `https://www.linkedin.com/in/your-profile/`  
**Business Address:** `Your Company or Personal Address Here`  

---

*Developed under strict architectural mentorship by the Vantage Execution Engine.*
