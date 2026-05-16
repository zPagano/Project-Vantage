# Architecture Reference: Subscription Tiers & Context Limits

## 1. Free Agent Players
Single-user accounts focusing on individual performance and AI feedback.
* **Basic:** InHouse Queue Access, 10-match AI lookback, Basic NPI metrics.
* **Pro:** Includes Basic features PLUS Full NPI match history, Advanced Visual Analytics, High Token Depth AI feedback, Personal Excel Export (Watermarked).

## 2. Free Agent Staff (Coaches, Analysts, Psychologists)
Solo practitioners acting as B2B service providers.
* **Basic:** Link up to 10 existing platform players (`Tenant.LinkStudent`), access Tactician-level metrics, utilize Platform Escrow for fee payments.
* **Pro:** Includes Basic features PLUS Link up to 30 existing players, Group-NPI comparisons, Level 1 Platform Visibility (Self-Promotion).
* **Partner (B2B2C):** Includes Pro features PLUS Link up to 100 players, Level 2 Platform Visibility, Direct Access to Vantage Support, Provision basic platform access to external students (`Tenant.ProvisionStudent`).

## 3. Small Organizations / Teams
Emerging eSports companies operating on standard limits.
* **Basic:** 1 Lineup (Single Game limit), Scrim Scheduling, Basic Staff Dashboard, Shared NPI.
* **Pro:** Includes Basic features PLUS 3 Lineups (Cross-Game Allowed), AI Scrim Analysis, Priority Ingestion, Org Financial & Payment History.

## 4. Large Organizations
Enterprise entities requiring dedicated infrastructure and massive scale.
* **Basic:** Unlimited Game Contexts, 10 Lineups, Priority AI Inference Queue, Custom Discord Bot Branding.
* **Pro:** Includes Basic features PLUS Unlimited Lineups, White-label Portal, Direct Architectural Support, Direct API Access (M2M), Early-Access Beta Features.
* **Custom:** Isolated AI Nodes, Custom Rate Limits, Dedicated Infrastructure, White-labeling, Full Custom Limits.