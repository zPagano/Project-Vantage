# Role-Based Access Control (RBAC) Permissions Matrix
**Status:** Active
**Architecture:** 3D Matrix (Tenant Context x Granular Permissions x Systemic Tier)

## Base Conventions
* **L0 (Default):** Implicit baseline. Requires no permission claim.
* **Parent Permissions:** Macros that the IAM Flattening Engine automatically expands into multiple child permissions.
* **Premium/Injected Permissions:** Capabilities like `Visibility L1/L2/L3`, Advanced Analytics, and White-Labeling are NOT granted by default base roles. They are dynamically injected by the Identity Provider based on Subscription Tiers (which apply at the Organization level) or direct administrative grants.

## 1. Competitive & Roster
| Permission String | Description | Target Personas | Type |
| :--- | :--- | :--- | :--- |
| `Matchmaking.InHouseQueue` | Access internal matchmaking queues. | Players, FA Players | Atomic |
| `Scrim.Schedule` | Book practice matches against other teams. | Org Managers, Head/Assistant Coaches | Atomic |
| `Roster.TransferRequest` | Initiate a transfer request. | FA Players, Org Managers, Coaches | Atomic |
| `Roster.TransferAccept` | Approve an incoming player transfer. | Org Managers, Head Coaches | Atomic |
| `Roster.Manage` | Manage team slots, sub-ins, and lineup settings. | Org Owners, Org Managers, Head Coaches | Atomic |
| `Roster.ManageTransfer` | **[PARENT]** Unlocks Request and Accept transfer powers. | Org Owners, Org Managers, Head Coaches | Parent |

## 2. Analytics & Intelligence
| Permission String | Description | Target Personas | Type |
| :--- | :--- | :--- | :--- |
| `Analytics.ViewBasic` | View standard Anonymized Metrics. | All Users | Atomic |
| `Analytics.ViewAdvanced` | View advanced visual analytics and heatmaps. | *Injected via Tier/Admin Grant* | Atomic |
| `Analytics.ViewCoaching` | Access individual player/student profiles. Data resolution depends on the viewer's Tier. | Org Managers, All Coaches, Analysts, FA Staff | Atomic |
| `Analytics.ViewGroup` | Compare group metrics across a roster. | Org Owners/Managers, Head/Assistant Coaches, Analysts | Atomic |
| `Analytics.ScrimAnalysis` | Execute AI-powered scrimmage analysis. | *Injected via Tier/Admin Grant* | Atomic |
| `Analytics.HighTokenInference` | Execute high token depth AI requests. | *Injected via Tier/Admin Grant* | Atomic |
| `Analytics.ShareInsights` | Share and receive secure snapshots of dashboards internally. | Org Owners/Managers, All Coaches, Analysts, FA Staff | Atomic |
| `Analytics.ExportExcel` | Export watermarked data to Excel (.xlsx) with backend audit logging. | Analysts | Atomic |
| `Analytics.ExportCsv` | Export raw data to CSV with backend audit logging. | Analysts | Atomic |
| `Analytics.ExportPdf` | Export watermarked formatted reports to PDF with backend audit logging. | Analysts | Atomic |
| `Analytics.ExportAll` | **[PARENT]** Unlocks Excel, CSV, and PDF exports. | System Admins, Head Coaches, Analysts | Parent |
| `Analytics.ViewAll` | **[PARENT]** Unlocks Basic, Advanced, Coaching, and Group views. | System Admins | Parent |

## 3. Financial & Organization
| Permission String | Description | Target Personas | Type |
| :--- | :--- | :--- | :--- |
| `Payment.Escrow` | Initiate financial holds for services rendered. | FA Staff | Atomic |
| `Billing.ViewFinancials` | View organizational financial and payment history. | Org Owners | Atomic |

## 4. Tenant & Platform Management
| Permission String | Description | Target Personas | Type |
| :--- | :--- | :--- | :--- |
| `System.DashboardAccess` | Base access to load the Blazor shell application. | All Users | Atomic |
| `System.ApiAccess` | Machine-to-Machine API bypass access. | System Admins | Atomic |
| `System.BetaAccess` | Access to unreleased experimental features. | *Injected via Tier/Admin Grant* | Atomic |
| `Tenant.LinkStudent` | Link an existing user to a staff dashboard. | FA Staff | Atomic |
| `Tenant.ProvisionStudent` | Provision and fund a new account for a student. | *Injected via Tier/Admin Grant* | Atomic |
| `Tenant.WhiteLabel` | Configure white-label portal settings. | *Injected via Tier (Enterprise)* | Atomic |
| `Integration.DiscordBot` | Connect a standard Discord bot. | Org Owners, Org Managers | Atomic |
| `Integration.DiscordBotBranding` | Apply custom branding to the Discord bot. | *Injected via Tier/Admin Grant* | Atomic |

## 5. Ecosystem Visibility Matrices
*Note: L0 is standard visibility.*

| Permission String | Description | Target Personas | Type |
| :--- | :--- | :--- | :--- |
| `Visibility.User.L1/L2/L3` | Prioritizes individual recruitment and content. | *Injected via Tier/Admin Grant* | Atomic |
| `Visibility.Team.L1/L2/L3` | Prioritizes scrim queues and tournament seeding. | *Injected via Tier/Admin Grant* | Atomic |
| `Visibility.Org.L1/L2/L3` | Prioritizes sponsorships and white-glove support. | *Injected via Tier/Admin Grant* | Atomic |