# Architecture Decision Record: 3D Matrix RBAC

## 1. Executive Summary
Standard Role-Based Access Control (RBAC) relies on 1-dimensional checks (e.g., "Is the user an Admin?"). For Project Vantage, a multi-tenant eSports platform, this is insufficient. A user's authority is dictated not just by their job title, but by the organization they belong to, the specific roster they manage, and the billing tier their organization pays for. 

To solve this, we have implemented a **3-Dimensional Matrix RBAC** system utilizing granular, dynamic permissions embedded directly into the JSON Web Token (JWT).

## 2. The Three Dimensions

### Dimension 1: Tenant Context (The "Where")
Determines the physical data boundaries the user is allowed to query or mutate.
* **Org-Scoped (`OrgId` present, `TeamId` null):** Executive roles (e.g., CEO, General Manager) who have visibility over all teams within the organization.
* **Team-Scoped (`OrgId` present, `TeamId` present):** Tactical roles (e.g., Head Coach, Positional Coach) whose visibility is locked strictly to their assigned roster.
* **Solo-Scoped (Free Agents - `OrgId` null, `TeamId` null):** Single-user tenants who act as their own financial and competitive entity.

### Dimension 2: Dynamic Permissions (The "Who" & "What")
Instead of hardcoding job titles (Roles) into API policies, the Identity Provider maps a user's Role to an array of granular **Permissions**. The API endpoints only check for these specific permissions.
* **Anti-Pattern:** `[Authorize(Policy = "RequireHeadCoach")]`
* **Vantage Pattern:** `[Authorize(Policy = "Permissions.Roster.Manage")]`

*Common Permissions Include:*
* `Roster.TransferInitiate`
* `Roster.TransferApprove`
* `Roster.Manage`
* `Payment.Escrow` (For FA Staff)
* `Analytics.View`

### Dimension 3: Subscription Tier (The "Access Level")
Determines access to premium platform features and enforces systemic limits (e.g., maximum team counts, cross-game support).
* **Tiers:** `Basic`, `Pro`, `Partner`, `Custom`.
* **Enforcement:** Checked via custom ASP.NET Core middleware or UI layout toggles. 
* **Note on State:** Because JWTs are immutable, any billing upgrade processed via payment webhooks will trigger a forced silent token refresh on the Blazor client to immediately reflect the new subscription claim.

## 3. JWT Anatomy Example
When a user authenticates, the BFF gateway receives and stores a JWT containing this 3D state. It looks conceptually like this:

```json
{
  "sub": "usr_98765",
  "name": "Alex Mercer",
  "vantage_role": "General Manager",
  "vantage_org_id": "org_12345",
  "vantage_team_id": null, 
  "vantage_subscription_tier": "Pro",
  "vantage_permissions": [
    "Escrow.Initiate",
    "Escrow.Approve",
    "Roster.Manage",
    "Analytics.View",
    "Billing.Manage"
  ],
  "exp": 1715890000
}
```

## 4. API Defense Strategy

Every microservice in the Vantage ecosystem relies on the shared `Vantage.Presentation.Hosting` library, which provides extension methods to dynamically build ASP.NET Core Authorization Policies based on the `vantage_permissions` claims array.