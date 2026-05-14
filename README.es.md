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

**Project Vantage** es una plataforma de inteligencia eSports de nivel empresarial, independiente del juego. Inicialmente centrada en el ecosistema de League of Legends, su arquitectura está diseñada para aislar completamente los datos específicos de cada juego mediante una Anti-Corruption Layer (ACL), permitiendo una futura expansión a otros títulos competitivos.

## 🏗 Arquitectura del Sistema

Vantage está construido sobre una arquitectura de microservicios estrictamente aislada, utilizando **.NET 10**, **Blazor WebAssembly** y **.NET Aspire** para orquestación local y telemetría.

### Principios Fundamentales
1. **Multi-Tenancy con RLS:** Todos los datos de tenants están aislados a nivel del motor SQL Server mediante Row-Level Security (RLS) y `SESSION_CONTEXT()`.
2. **Zero-Trust AI:** Cualquier inferencia de IA o RAG debe pasar por verificaciones criptográficas antes de acceder al ledger Normalized Player Intelligence (NPI).
3. **Anti-Corruption Layer (ACL):** Las APIs externas (ej.: Riot Games) nunca dictan los esquemas internos. Todos los datos entrantes se mapean a DTOs internos estandarizados.
4. **RFC 9457 Manejo Global de Errores:** Todas las APIs cumplen con el estándar Problem Details para APIs HTTP.

## 📂 Estructura del Repositorio

El repositorio aplica límites físicos estrictos entre bounded contexts:

* `/src` - Microservicios, API Gateways (BFF) y clientes UI.
* `/tests` - Refleja `/src` para pruebas Unitarias, de Integración y Arquitectura.
* `/infrastructure` - Manifiestos de despliegue, configuraciones de Azure Container Apps y pipelines CI/CD.
* `/docs` - Architecture Decision Records (ADRs) y documentación de onboarding.

## 🚀 Stack de Desarrollo
* **Backend:** C# 12, .NET 10 Web API  
* **Frontend:** Blazor WebAssembly (WASM)  
* **Base de Datos:** SQL Server 2025 (Entity Framework Core)  
* **Orquestación:** .NET Aspire  
* **Observabilidad:** OpenTelemetry (OTLP)  

---

## 📬 Contacto & Solicitudes

Para consultas de licencias, propuestas de colaboración, divulgaciones de seguridad, reportes de errores, solicitudes de funcionalidades o cualquier consulta profesional relacionada con Project Vantage, consulte el sistema completo de solicitudes:

👉 **[Plantillas de Solicitud](./docs/inquiry-templates.md)**

Este documento incluye:

- Plantillas totalmente localizadas (EN, PT‑BR, ES, FR)  
- Contenidos personalizados según el tipo de solicitud  
- Un flujo estructurado para elegir el tipo correcto de contacto  

Si prefiere contacto directo, puede escribir a:

📧 **lucaspaganopolisel@gmail.com**

---

## 📞 Contacto Adicional

**Website:** https://zpagano.github.io/  
**LinkedIn:** https://www.linkedin.com/in/lucas-pagano-polisel/

---

## 📄 Licencia

Este proyecto es propietario. Consulte el archivo [LICENSE](LICENSE) para más detalles.
