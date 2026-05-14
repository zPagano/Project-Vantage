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

**Project Vantage** est une plateforme d’intelligence eSports de niveau entreprise, indépendante du jeu. Initialement centrée sur l’écosystème League of Legends, son architecture est conçue pour isoler complètement les données spécifiques à chaque jeu via une Anti-Corruption Layer (ACL), permettant une expansion future vers d’autres titres compétitifs.

## 🏗 Architecture du Système

Vantage repose sur une architecture de microservices strictement isolée, utilisant **.NET 10**, **Blazor WebAssembly** et **.NET Aspire** pour l’orchestration locale et la télémétrie.

### Principes Fondamentaux
1. **Multi-Tenancy via RLS :** Toutes les données des tenants sont isolées au niveau du moteur SQL Server via Row-Level Security (RLS) et `SESSION_CONTEXT()`.
2. **Zero-Trust AI :** Toute inférence IA ou RAG doit passer par des contrôles cryptographiques avant d’accéder au ledger Normalized Player Intelligence (NPI).
3. **Anti-Corruption Layer (ACL) :** Les APIs externes (ex. Riot Games) ne dictent jamais les schémas internes. Toutes les données entrantes sont mappées vers des DTOs internes standardisés.
4. **RFC 9457 Gestion Globale des Erreurs :** Toutes les APIs respectent le standard Problem Details pour les APIs HTTP.

## 📂 Structure du Dépôt

Le dépôt applique des limites physiques strictes entre les bounded contexts :

* `/src` - Microservices, API Gateways (BFF) et clients UI.
* `/tests` - Reflète `/src` pour les tests Unitaires, d’Intégration et d’Architecture.
* `/infrastructure` - Manifests de déploiement, configurations Azure Container Apps et pipelines CI/CD.
* `/docs` - Architecture Decision Records (ADRs) et documentation d’onboarding.

## 🚀 Stack de Développement
* **Backend :** C# 12, .NET 10 Web API  
* **Frontend :** Blazor WebAssembly (WASM)  
* **Base de Données :** SQL Server 2025 (Entity Framework Core)  
* **Orchestration :** .NET Aspire  
* **Observabilité :** OpenTelemetry (OTLP)  

---

## 📬 Contact & Demandes

Pour toute question de licence, proposition de collaboration, divulgation de sécurité, rapport de bug, demande de fonctionnalité ou toute demande professionnelle liée à Project Vantage, veuillez consulter le système complet de demandes :

👉 **[Modèles de Demande](./docs/inquiry-templates.md)**

Ce document inclut :

- Des modèles entièrement localisés (EN, PT‑BR, ES, FR)  
- Des contenus adaptés à chaque type de demande  
- Un flux structuré pour choisir le bon type de contact  

Si vous préférez un contact direct, vous pouvez écrire à :

📧 **lucaspaganopolisel@gmail.com**

---

## 📞 Contact Additionnel

**Site Web :** https://zpagano.github.io/  
**LinkedIn :** https://www.linkedin.com/in/lucas-pagano-polisel/

---

## 📄 Licence

Ce projet est propriétaire. Voir le fichier [LICENSE](LICENSE) pour plus de détails.
